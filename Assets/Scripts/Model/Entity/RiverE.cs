using Chessy.Model.Model.Component;

namespace Chessy.Model
{
    public sealed class RiverE
    {
        public RiverTypes RiverT { get; internal set; }
        public HaveRiverAroundCellC HaveRiverC;

        internal RiverE(in bool[] haveRive)
        {
            HaveRiverC = new HaveRiverAroundCellC(haveRive);
        }
    }
}