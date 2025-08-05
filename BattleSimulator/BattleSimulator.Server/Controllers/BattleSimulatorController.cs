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
        private static readonly Repository repository = new();
        private static readonly BattleSimulator battleSim = new();
        private static readonly Transformer newTransformer = new();

        private static void InitalizeRepo()
        {
            repository.Battlers = repository.ReadTransformerFile("./Transformers/Transformers.json");
            repository.BattleResults = "";

            repository.SelectedBattlers = new List<ITransformer>();
            repository.LeftTeam = new List<ITransformer>();
            repository.RightTeam = new List<ITransformer>();
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
        public IBattler[] UpdateBattlers(BattlerTeam battlerTeam)
        {
            if (battlerTeam.teamIndexes.Length != 0)
            {
                for (int o = 0; o <=  battlerTeam.teamIndexes.Length - 1; o++)
                {
                    List<ITransformer> tempBattlerList = new List<ITransformer>();

                    for (int i = 0; i <= battlerTeam.battleMode; i++)
                    {
                        if (battlerTeam.teamIndexes[o][i] == -1)
                            tempBattlerList.Add(new Transformer());
                        else
                            tempBattlerList.Add(repository.Battlers[battlerTeam.teamIndexes[o][i]]);
                    }

                    if (o == 0)
                        repository.LeftTeam = tempBattlerList;
                    else
                        repository.RightTeam = tempBattlerList;
                }
                
                repository.SelectedBattlers = repository.LeftTeam.Concat(repository.RightTeam).ToList();
            }

            return repository.SelectedBattlers.ToArray();
        }

        [Route("ResetBattlerSlot")]
        [HttpPost]
        public ITransformer[] ResetBattlerSlot([FromBody] int slot)
        {
            repository.ResetSlot(slot);
            return repository.SelectedBattlers.ToArray();
        }

        [Route("Battle")]
        [HttpPost]
        public string Battle([FromBody] int mode)
        {
            ITransformer[] lastBattle;

            if (repository.SelectedBattlers.Count <= mode * 2 + 1)
                return "ERROR: Please fill every battler slot.";

            for (int i = 0; i <= mode * 2 + 1; i++)
            {
                if (repository.SelectedBattlers[i] == null || repository.SelectedBattlers[i].Name == newTransformer.Name || repository.SelectedBattlers.Count == 0)
                    return "ERROR: Please fill every battler slot.";

                for (int o = 0; o <= mode * 2 + 1; o++)
                {
                    if (repository.SelectedBattlers[i].Name == repository.SelectedBattlers[o].Name && i != o)
                        return "ERROR: All battler slots must have a different Transformer!";
                }
            }

            lastBattle = battleSim.Battle(mode, repository.LeftTeam.ToArray(), repository.RightTeam.ToArray());

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
