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
            ITransformer battlerOne = new Transformer();
            ITransformer battlerTwo = new Transformer();
            ITransformer[] battlers = BattleSim.Battle1v1(battlerOne, battlerTwo);

            if (battlerOne == dummyBot || battlerTwo == dummyBot)  
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Battle2v2Test()
        {
            BattleSimulator BattleSim = new();
            ITransformer dummyBot = new Transformer();
            ITransformer battlerOne = new Transformer();
            ITransformer battlerTwo = new Transformer();
            ITransformer battlerThree = new Transformer();
            ITransformer battlerFour = new Transformer();
            ITransformer[] battlers = BattleSim.Battle2v2(battlerOne, battlerTwo, battlerThree, battlerFour);

            if (battlerOne == dummyBot || battlerTwo == dummyBot || battlerThree == dummyBot || battlerFour == dummyBot)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void ChangeWinnerStatsTest()
        {
            BattleSimulator BattleSim = new();
            ITransformer Battler = new Transformer();
            ITransformer PreStatChange = new Transformer();
            PreStatChange.Win = Battler.Win;
            ITransformer PostStatChange = new Transformer();
            PostStatChange = BattleSim.ChangeWinnerStats(Battler);
            
            Assert.AreNotEqual(PreStatChange.Win, PostStatChange.Win);
        }

        [TestMethod]
        public void ChangeLoserStatsTest()
        {
            BattleSimulator BattleSim = new();
            ITransformer Battler = new Transformer();
            ITransformer PreStatChange = new Transformer();
            PreStatChange.Loss = Battler.Loss;
            ITransformer PostStatChange = new Transformer();
            PostStatChange = BattleSim.ChangeLoserStats(Battler);

            Assert.AreNotEqual(PreStatChange.Loss, PostStatChange.Loss);
        }
    }
}
