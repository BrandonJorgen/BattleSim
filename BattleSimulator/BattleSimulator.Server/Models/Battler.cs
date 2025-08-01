using TransformerBattleSimulator.Server.Interfaces;

namespace TransformerBattleSimulator.Server.Models
{
    public class Battler : IBattler
    {
        public string Name { get; set; }
        public int Win { get; set; }
        public int Loss { get; set; }
        public string Image { get; set; }
    }
}
