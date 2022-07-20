namespace Chessy.Model
{
    public sealed class PlayerInfoC
    {
        internal float WoodForBuyHouse;
        internal int AmountBuiltHouses;
        internal bool IsReadyForStartOnlineGame;
        internal bool HaveKingInInventor;

        public bool IsReadyForStartOnlineGameP => IsReadyForStartOnlineGame;
        public float WoodForBuyHouseP => WoodForBuyHouse;
        public bool HaveKingInInventorP => HaveKingInInventor;
        public int AmountFarmsInGame { get; internal set; }
        public int AmountBuiltHousesP => AmountBuiltHouses;

        internal void Dispose()
        {
            WoodForBuyHouse = default;
            AmountBuiltHouses = default;
            IsReadyForStartOnlineGame = default;
            HaveKingInInventor = default;
        }
    }
}