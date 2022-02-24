namespace Game.Game
{
    public struct SetUnitME
    {
        public IdxC IdxC;
        public UnitTC UnitTC;

        public void Set(in byte idx, in UnitTypes unitT)
        {
            IdxC.Idx = idx;
            UnitTC.Unit = unitT;
        }
    }
}