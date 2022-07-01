namespace Chessy.Model
{
    public struct PlayerInfoC
    {
        public bool HaveKingInInventor { get; internal set; }
        public bool IsReadyForStartOnlineGame { get; internal set; }
        public float WoodForBuyHouse { get; internal set; }
        public int AmountFarmsInGame { get; internal set; }
        public int AmountBuiltHouses { get; internal set; }
    }
}