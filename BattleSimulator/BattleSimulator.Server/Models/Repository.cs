using TransformerBattleSimulator.Server.Interfaces;

namespace TransformerBattleSimulator.Server.Models
{
    public class Repository : IRepository
    {
        public ITransformer[] Battlers { get; set; }
        public ITransformer[] SelectedBattlers { get; set; }
        public string BattleResults { get; set; }

        public void UpdateBattlerList(ITransformer[] battlers)
        {
            for (int i = 0; i <= battlers.Length - 1; i++)
            {
                for (int o = 0; i <= Battlers.Length - 1; o++)
                {
                    if (battlers[i].Name == Battlers[o].Name)
                    {
                        Battlers[o] = battlers[i];
                        break;
                    }
                }
            }
        }

        public void UpdateBattleResults(string results)
        {
            BattleResults = results;
        }

        public void ResetSlot(int slot)
        {
            SelectedBattlers[slot] = new Transformer();
        }
    }
}
