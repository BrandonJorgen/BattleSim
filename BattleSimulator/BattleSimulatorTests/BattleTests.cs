using TransformerBattleSimulator.Server.Interfaces;
using TransformerBattleSimulator.Server.Models;

namespace TransformerBattleSimulatorTests
{
    [TestClass]
    public sealed class BattleTests
    {
        [TestMethod]
        public void Battle1v1Test()
        {
            BattleSimulator BattleSim = new();
            ITransformer dummyBot = new Transformer();
            ITransformer[] LeftTeam = [new Transformer()];
            ITransformer[] RightTeam = [new Transformer()];
            ITransformer[] battle = BattleSim.Battle(0, LeftTeam, RightTeam);

            foreach (ITransformer battler in LeftTeam)  
            {
                if (battler == dummyBot)
                {
                    Assert.Fail();
                }
            }

            foreach (ITransformer battler in RightTeam)
            {
                if (battler == dummyBot)
                {
                    Assert.Fail();
                }
            }
        }

        [TestMethod]
        public void Battle2v2Test()
        {
            BattleSimulator BattleSim = new();
            ITransformer dummyBot = new Transformer();
            ITransformer[] LeftTeam = [new Transformer(), new Transformer()];
            ITransformer[] RightTeam = [new Transformer(), new Transformer()];
            ITransformer[] battle = BattleSim.Battle(1, LeftTeam, RightTeam);

            foreach (ITransformer battler in LeftTeam)
            {
                if (battler == dummyBot)
                {
                    Assert.Fail();
                }
            }

            foreach (ITransformer battler in RightTeam)
            {
                if (battler == dummyBot)
                {
                    Assert.Fail();
                }
            }

        }

        [TestMethod]
        public void Battle3v3Test()
        {
            BattleSimulator BattleSim = new();
            ITransformer dummyBot = new Transformer();
            ITransformer[] LeftTeam = [new Transformer(), new Transformer(), new Transformer(), new Transformer()];
            ITransformer[] RightTeam = [new Transformer(), new Transformer(), new Transformer(), new Transformer()];
            ITransformer[] battle = BattleSim.Battle(2, LeftTeam, RightTeam);

            foreach (ITransformer battler in LeftTeam)
            {
                if (battler == dummyBot)
                {
                    Assert.Fail();
                }
            }

            foreach (ITransformer battler in RightTeam)
            {
                if (battler == dummyBot)
                {
                    Assert.Fail();
                }
            }

        }

        [TestMethod]
        public void Battle4v4Test()
        {
            BattleSimulator BattleSim = new();
            ITransformer dummyBot = new Transformer();
            ITransformer[] LeftTeam = [new Transformer(), new Transformer(), new Transformer(), new Transformer(), new Transformer()];
            ITransformer[] RightTeam = [new Transformer(), new Transformer(), new Transformer(), new Transformer(), new Transformer()];
            ITransformer[] battle = BattleSim.Battle(2, LeftTeam, RightTeam);

            foreach (ITransformer battler in LeftTeam)
            {
                if (battler == dummyBot)
                {
                    Assert.Fail();
                }
            }

            foreach (ITransformer battler in RightTeam)
            {
                if (battler == dummyBot)
                {
                    Assert.Fail();
                }
            }

        }

        [TestMethod]
        public void ChangeWinnerStatsTest()
        {
            BattleSimulator BattleSim = new();
            ITransformer[] battlers = [new Transformer(), new Transformer()];
            ITransformer[] PreStatChange = [new Transformer(), new Transformer()];

            for (int i = 0; i <= battlers.Length - 1; i++)
            {
                PreStatChange[i].Win = battlers[i].Win;
            }
            
            ITransformer[] PostStatChange = [new Transformer(), new Transformer()];
            PostStatChange = BattleSim.ChangeWinnerStats(battlers);

            for (int i = 0; i < battlers.Length - 1; ++i)
            {
                Assert.AreNotEqual(PreStatChange[i].Win, PostStatChange[i].Win);
            }
        }

        [TestMethod]
        public void ChangeLoserStatsTest()
        {
            BattleSimulator BattleSim = new();
            ITransformer[] battlers = [new Transformer(), new Transformer()];
            ITransformer[] PreStatChange = [new Transformer(), new Transformer()];

            for (int i = 0; i <= battlers.Length - 1; i++)
            {
                PreStatChange[i].Loss = battlers[i].Loss;
            }

            ITransformer[] PostStatChange = [new Transformer(), new Transformer()];
            PostStatChange = BattleSim.ChangeLoserStats(battlers);

            for (int i = 0; i < battlers.Length - 1; ++i)
            {
                Assert.AreNotEqual(PreStatChange[i].Loss, PostStatChange[i].Loss);
            }
        }
    }
}
