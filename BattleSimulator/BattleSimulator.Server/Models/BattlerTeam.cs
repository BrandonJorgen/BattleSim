using TransformerBattleSimulator.Server.Interfaces;

namespace TransformerBattleSimulator.Server.Models
{
    public class BattlerTeam : IBattlerTeam
    {
        public int[][] teamIndexes { get; set; }
        public int battleMode { get; set; }
    }
}
