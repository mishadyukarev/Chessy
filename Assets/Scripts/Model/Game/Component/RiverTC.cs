namespace Chessy.Game
{
    public struct RiverTC
    {
        public RiverTypes River { get; internal set; }

        public bool HaveRiverNear => River != RiverTypes.None && River != RiverTypes.End;
    }
}