namespace Chessy.Model
{
    public sealed class RiverC
    {
        public RiverTypes RiverT { get; internal set; }

        public bool HaveRiverNear => RiverT != RiverTypes.None && RiverT != RiverTypes.End;
    }
}