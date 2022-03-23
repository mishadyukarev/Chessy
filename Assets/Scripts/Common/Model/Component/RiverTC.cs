namespace Chessy.Game
{
    public struct RiverTC
    {
        public RiverTypes River;

        public bool HaveRiverNear => River != RiverTypes.None && River != RiverTypes.End;

        public RiverTC(in RiverTypes river) => River = river;
    }
}