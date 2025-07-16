using TransformerBattleSimulator.Server.Interfaces;

namespace TransformerBattleSimulator.Server.Models
{
    public class Transformer : Battler, ITransformer
    {
        public string Faction { get; set; }
    }
}
