namespace TransformerBattleSimulator.Server.Interfaces
{
    public interface IRepository
    {
        public ITransformer[] Battlers { get; set; }
        public List<ITransformer> SelectedBattlers { get; set; }
        public List<ITransformer> LeftTeam { get; set; }
        public List<ITransformer> RightTeam { get; set; }
        public string BattleResults { get; set; }
    }
}
