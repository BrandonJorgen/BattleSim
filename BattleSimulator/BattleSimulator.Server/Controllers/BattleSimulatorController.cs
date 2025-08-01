using Microsoft.AspNetCore.Mvc;
using System;
using TransformerBattleSimulator.Server.Interfaces;
using TransformerBattleSimulator.Server.Models;
using System.Text.Json;

namespace TransformerBattleSimulator.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BattleSimulatorController : ControllerBase
    {
        public string result = "test";

        private static readonly Repository repository = new();
        private static readonly BattleSimulator battleSim = new();

        public ITransformer[] lastBattle;

        private static void InitalizeRepo()
        {
            repository.Battlers = repository.ReadTransformerFile("./Transformers/Transformers.json");
            repository.BattleResults = "";
            repository.SelectedBattlers = [new Transformer(), new Transformer(), new Transformer(), new Transformer(), new Transformer(), new Transformer(), new Transformer(), new Transformer()];
        }

        [Route("GetBattlers")]
        [HttpGet]
        public ITransformer[] Get()
        {
            InitalizeRepo();
            return repository.Battlers;
        }

        [Route("UpdateBattlers")]
        [HttpPost]
        public IBattler[] UpdateBattlers(Transformer[] battler)
        {
            for (int i = 0; i <= battler.Length - 1; i++)
            {
                repository.SelectedBattlers[i] = battler[i];
            }
            
            return repository.SelectedBattlers;
        }

        [Route("ResetBattlerSlot")]
        [HttpPost]
        public ITransformer[] ResetBattlerSlot([FromBody] int slot)
        {
            repository.ResetSlot(slot);
            return repository.SelectedBattlers;
        }

        [Route("Battle")]
        [HttpPost]
        public string Battle([FromBody] int mode)
        {
            switch (mode)
            {
                //1v1
                case 0:
                    for (int i = 0; i <= 1; i++)
                    {
                        if (repository.SelectedBattlers[i].Name == null) return "ERROR: Please choose two battlers before starting.";
                    }

                    if (repository.SelectedBattlers[0].Name == repository.SelectedBattlers[1].Name) return "ERROR: Please choose two DIFFERENT battlers before starting.";

                    lastBattle = battleSim.Battle(0, repository.SelectedBattlers);
                    break;

                //2v2
                case 1:
                    for (int i = 0; i <= 3; i++)
                    {
                        if (repository.SelectedBattlers[i].Name == null) return "ERROR: Please choose four battlers before starting.";

                        for (int o = 0; o <= 3; o++)
                        {
                            if (repository.SelectedBattlers[i].Name == repository.SelectedBattlers[o].Name && i != o) return "ERROR: Please choose four DIFFERENT battlers before starting.";
                        }
                    }

                    lastBattle = battleSim.Battle(1, repository.SelectedBattlers);
                    break;

                //3v3
                case 2:
                    for (int i = 0; i <= 5; i++)
                    {
                        if (repository.SelectedBattlers[i].Name == null) return "ERROR: Please choose six battlers before starting.";

                        for (int o = 0; o <= 5; o++)
                        {
                            if (repository.SelectedBattlers[i].Name == repository.SelectedBattlers[o].Name && i != o) return "ERROR: Please choose six DIFFERENT battlers before starting.";
                        }
                    }

                    lastBattle = battleSim.Battle(2, repository.SelectedBattlers);
                    break;

                //4v4
                case 3:
                    for (int i = 0; i <= 7; i++)
                    {
                        if (repository.SelectedBattlers[i].Name == null) return "ERROR: Please choose eight battlers before starting.";

                        for (int o = 0; o <= 7; o++)
                        {
                            if (repository.SelectedBattlers[i].Name == repository.SelectedBattlers[o].Name && i != o) return "ERROR: Please choose eight DIFFERENT battlers before starting.";
                        }
                    }

                    lastBattle = battleSim.Battle(3, repository.SelectedBattlers);
                    break;

                default:
                    Console.WriteLine("ERROR: NO CORRECT MODE SELECTED");
                    repository.BattleResults = "ERROR: NO CORRECT MODE SELECTED";
                    return repository.BattleResults;
            }

            repository.UpdateBattlerList(lastBattle);
            repository.BattleResults = battleSim.LastResult;
            return repository.BattleResults;
        }

        [Route("UpdateStats")]
        [HttpGet]
        public ITransformer[] UpdatedStats()
        {
            return repository.Battlers;
        }
    }
}
