namespace Chessy.Model
{
    public struct PlayerInfoC
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
    }
}