using System.Text.Json;
using TransformerBattleSimulator.Server.Interfaces;

namespace TransformerBattleSimulator.Server.Models
{
    public class Repository : IRepository
    {
        public ITransformer[] Battlers { get; set; }
        public List<ITransformer> SelectedBattlers { get; set; }
        public string BattleResults { get; set; }
        public List<ITransformer> LeftTeam { get; set; }
        public List<ITransformer> RightTeam { get; set; }

        public Transformer[] ReadTransformerFile(string file)
        {
            string json = File.ReadAllText(file);

            Transformer[]? transformers = JsonSerializer.Deserialize<Transformer[]>(json);

            return transformers;
        }

        public void UpdateBattlerList(ITransformer[] battlers)
        {
            for (int i = 0; i <= battlers.Length - 1; i++)
            {
                if (battlers[i] != null)
                    for (int o = 0; o <= Battlers.Length - 1; o++)
                    {
                        if (Battlers[o] != null)
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
