using Microsoft.AspNetCore.Mvc;
using System;
using TransformerBattleSimulator.Server.Interfaces;
using TransformerBattleSimulator.Server.Models;

namespace TransformerBattleSimulator.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BattleSimulatorController : ControllerBase
    {
        public string result = "test";

        private static readonly Repository repository = new();
        private static readonly BattleSimulator battleSim = new();

        private static readonly Transformer[] Battlers =
        [
            new Transformer{Name = "Megatron", Faction = "Decepticon", Win = 0, Loss = 0, Image = "./Images/Megatron_(Decepticon).jpg", TypeId = 0},
            new Transformer{Name = "Optimus Prime", Faction = "Autobot", Win = 0, Loss = 0, Image = "./Images/Optimus.jpg", TypeId = 0},
            new Transformer{Name = "Bumblebee", Faction = "Autobot", Win = 0, Loss = 0, Image = "./Images/bumblebee.jpg", TypeId = 0},
            new Transformer{Name = "Starscream", Faction = "Decepticon", Win = 0, Loss = 0, Image = "./Images/starscream.jpg", TypeId = 0},
        ];

        private static void InitalizeRepo()
        {
            repository.Battlers = Battlers;
            repository.BattleResults = "";
            repository.SelectedBattlers = [new Transformer(), new Transformer(), new Transformer(), new Transformer()];
        }

        [Route("GetBattlers")]
        [HttpGet]
        public ITransformer[] Get()
        {
            InitalizeRepo();
            return repository.Battlers;
        }

        [Route("UpdateBattlerOne")]
        [HttpPost]
        public IBattler[] UpdateBattlerOne(Transformer battler)
        {
            Console.WriteLine("New battler 1 is: " + battler.Name);
            repository.SelectedBattlers[0] = battler;
            return repository.SelectedBattlers;
        }

        [Route("UpdateBattlerTwo")]
        [HttpPost]
        public IBattler[] UpdateBattlerTwo(Transformer battler)
        {
            repository.SelectedBattlers[1] = battler;
            return repository.SelectedBattlers;
        }

        [Route("UpdateBattlerThree")]
        [HttpPost]
        public IBattler[] UpdateBattlerThree(Transformer battler)
        {
            repository.SelectedBattlers[2] = battler;
            return repository.SelectedBattlers;
        }

        [Route("UpdateBattlerFour")]
        [HttpPost]
        public IBattler[] UpdateBattlerFour(Transformer battler)
        {
            repository.SelectedBattlers[3] = battler;
            return repository.SelectedBattlers;
        }

        [Route("ResetBattlerSlot")]
        [HttpPost]
        public ITransformer[] ResetBattlerSlot([FromBody] int slot)
        {
            repository.ResetSlot(slot);
            return repository.SelectedBattlers;
        }

        [Route("Battle1v1")]
        [HttpGet]
        public string Battle1v1()
        {
            for (int i = 0; i <= 1; i++)
            {
                if (repository.SelectedBattlers[i].Name == null) return "ERROR: Please choose two battlers before starting.";
            }

            if (repository.SelectedBattlers[0].Name == repository.SelectedBattlers[1].Name) return "ERROR: Please choose two DIFFERENT battlers before starting.";

            ITransformer[] lastBattle = battleSim.Battle1v1(repository.SelectedBattlers[0], repository.SelectedBattlers[1]);
            repository.UpdateBattlerList(lastBattle);
            repository.BattleResults = battleSim.LastResult;
            return repository.BattleResults;
        }

        [Route("Battle2v2")]
        [HttpGet]
        public string Battle2v2()
        {
            for (int i = 0; i <= 3; i++)
            {
                if (repository.SelectedBattlers[i].Name == null) return "ERROR: Please choose four battlers before starting.";

                for (int o = 0; o <= 3; o++)
                {
                    if (repository.SelectedBattlers[i].Name == repository.SelectedBattlers[o].Name && i != o) return "ERROR: Please choose four DIFFERENT battlers before starting.";
                }
            }

            ITransformer[] lastBattle = battleSim.Battle2v2(repository.SelectedBattlers[0], repository.SelectedBattlers[1], repository.SelectedBattlers[2], repository.SelectedBattlers[3]);
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
