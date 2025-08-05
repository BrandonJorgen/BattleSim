using System.Reflection;
using TransformerBattleSimulator.Server.Interfaces;

namespace TransformerBattleSimulator.Server.Models
{
    public class BattleSimulator : IBattleSimulator
    {
        public string Winner { get; set; }

        public string LastResult { get; set; }

        public ITransformer[] Battle(int mode, ITransformer[] leftTeam, ITransformer[] rightTeam)
        {

            DecideWinner();
            
            List<ITransformer> winnerArray;
            List<ITransformer> loserArray;

            //Assign win/lose status
            if (Winner == "left")
            {
                winnerArray = leftTeam.ToList();
                loserArray = rightTeam.ToList();
                LastResult = "Left team is the winner!";
            }
            else
            {
                winnerArray = rightTeam.ToList();
                loserArray = leftTeam.ToList();
                LastResult = "Right team is the winner!";
            }

            ChangeWinnerStats(winnerArray.ToArray());
            ChangeLoserStats(loserArray.ToArray());
            ITransformer[] result = leftTeam.Concat(rightTeam).ToArray();

            return result;
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
