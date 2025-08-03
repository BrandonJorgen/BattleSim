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
            if (repository.SelectedBattlers.Length - 1 >= mode)
            {
                for (int i = 0; i <= mode * 2 + 1; i++)
                {
                    if (repository.SelectedBattlers[i] == null || repository.SelectedBattlers[i].Name == " " || repository.SelectedBattlers[i] == new Transformer())
                        return "ERROR: Please fill every battler slot.";

                    for (int o = 0; o <= mode * 2 + 1; o++)
                    {
                        if (repository.SelectedBattlers[o] != null && repository.SelectedBattlers[i].Name == repository.SelectedBattlers[o].Name && i != o)
                            return "ERROR: All battler slots must have a different Transformer!";
                    }
                }
            }

            lastBattle = battleSim.Battle(mode, repository.SelectedBattlers);

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
