namespace TransformerBattleSimulator.Server.Interfaces
{
    public interface IRepository
    {
        public ITransformer[] Battlers { get; set; }

        public ITransformer[] SelectedBattlers { get; set; }

        public string BattleResults { get; set; }
    }
}
