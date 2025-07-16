using TransformerBattleSimulator.Server.Interfaces;

namespace TransformerBattleSimulator.Server.Models
{
    public class BattleSimulator : IBattleSimulator
    {
        public string Winner { get; set; }

        public string LastResult { get; set; }

        public ITransformer[] Battle1v1(ITransformer battlerOne, ITransformer battlerTwo)
        {

            DecideWinner();

            if (Winner == "left")
            {
                ChangeWinnerStats(battlerOne);
                ChangeLoserStats(battlerTwo);
                LastResult = battlerOne.Name + " is the winner in the fight between " + battlerOne.Name + " and " + battlerTwo.Name;
            }
            else
            {
                ChangeWinnerStats(battlerTwo);
                ChangeLoserStats(battlerOne);
                LastResult = battlerTwo.Name + " is the winner in the fight between " + battlerOne.Name + " and " + battlerTwo.Name;
            }

            return [battlerOne, battlerTwo];
        }

        public ITransformer[] Battle2v2(ITransformer battlerOne, ITransformer battlerTwo, ITransformer battlerThree, ITransformer battlerFour)
        {

            DecideWinner();

            if (Winner == "left")
            {
                ChangeWinnerStats(battlerOne);
                ChangeWinnerStats(battlerTwo);
                ChangeLoserStats(battlerThree);
                ChangeLoserStats(battlerFour);
                LastResult = battlerOne.Name + " and " + battlerTwo.Name + " are the winners in the fight between " + battlerOne.Name + " and " + battlerTwo.Name + " vs " + battlerThree.Name + " and " + battlerFour.Name;
            }
            else
            {
                ChangeWinnerStats(battlerThree);
                ChangeWinnerStats(battlerFour);
                ChangeLoserStats(battlerOne);
                ChangeLoserStats(battlerTwo);
                LastResult = battlerThree.Name + " and " + battlerFour.Name + " are the winners in the fight between " + battlerOne.Name + " and " + battlerTwo.Name + " vs " + battlerThree.Name + " and " + battlerFour.Name;
            }

            return [battlerOne, battlerTwo, battlerThree, battlerFour];
        }

        private void DecideWinner()
        {
            int randNum = new Random().Next(0, 2);

            if (randNum == 0)
            {
                Winner = "left";
            }
            else
            {
                Winner = "right";
            }
        }

        public ITransformer ChangeWinnerStats (ITransformer winner)
        {
            winner.Win++;

            return winner;
        }

        public ITransformer ChangeLoserStats (ITransformer loser)
        {
            loser.Loss++;

            return loser;
        }
    }
}
