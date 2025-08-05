namespace TransformerBattleSimulator.Server.Interfaces
{
    public interface IBattlerTeam
    {
        public int[][] teamIndexes { get; set; }
        public int battleMode { get; set; }
    }
}
