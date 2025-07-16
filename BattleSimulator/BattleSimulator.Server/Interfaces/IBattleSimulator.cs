namespace TransformerBattleSimulator.Server.Interfaces
{
    public interface IBattleSimulator
    {
        public string Winner { get; set; }

        public string LastResult { get; set; }
    }
}
