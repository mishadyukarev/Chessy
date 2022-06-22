using Chessy.Game.Model.Component;

namespace Chessy.Game
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