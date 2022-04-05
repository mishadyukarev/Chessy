namespace Chessy.Game
{
    public struct RiverTC
    {
        public RiverTypes RiverT { get; internal set; }

        public bool HaveRiverNear => RiverT != RiverTypes.None && RiverT != RiverTypes.End;
    }
}