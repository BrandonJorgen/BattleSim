namespace TransformerBattleSimulator.Server.Interfaces
{
    public interface IBattler
    {
        public string Name { get; set; }
        public int Win { get; set; }
        public int Loss { get; set; }
        public string Image { get; set; }
        public int TypeId { get; set; }
    }
}
