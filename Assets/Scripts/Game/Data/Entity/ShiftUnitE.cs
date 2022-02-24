namespace Game.Game
{
    public struct ShiftUnitE
    {
        public IdxC FromIdxC;
        public IdxC ToIdxC;

        public void Set(in byte idx_from, in byte idx_to)
        {
            FromIdxC.Idx = idx_from;
            ToIdxC.Idx = idx_to;
        }
    }
}