using System.Reflection;
using TransformerBattleSimulator.Server.Interfaces;

namespace TransformerBattleSimulator.Server.Models
{
    public class BattleSimulator : IBattleSimulator
    {
        public string Winner { get; set; }

        public string LastResult { get; set; }

        public ITransformer[] Battle(int mode, ITransformer[] battlers)
        {

            DecideWinner();
            
            List<ITransformer> leftArray = new List<ITransformer>();
            List<ITransformer> rightArray = new List<ITransformer>();
            List<ITransformer> winnerArray;
            List<ITransformer> loserArray;

            //Sort lists into teams
            for (int i = 0; i <= mode; i++)
            {
                leftArray.Add(battlers[i]);
                rightArray.Add(battlers[mode + i + 1]);
            }

            //Assign win/lose status
            if (Winner == "left")
            {
                winnerArray = leftArray;
                loserArray = rightArray;
                LastResult = "Left team is the winner!";
            }
            else
            {
                winnerArray = rightArray;
                loserArray = leftArray;
                LastResult = "Right team is the winner!";
            }

            ChangeWinnerStats(winnerArray.ToArray());
            ChangeLoserStats(loserArray.ToArray());

            return battlers;
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

        public ITransformer[] ChangeWinnerStats (ITransformer[] winners)
        {
            foreach (ITransformer winner in winners)
            {
                if (winner == null || winner == new Transformer())
                    return winners;
                    
                winner.Win++;
            }

            return winners;
        }

        public ITransformer[] ChangeLoserStats (ITransformer[] losers)
        {
            foreach (ITransformer loser in losers)
            {
                if (loser == null || loser == new Transformer())
                    return losers;
                    
                loser.Loss++;
            }

            return losers;
        }
    }
}
