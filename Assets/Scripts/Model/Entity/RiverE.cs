using Chessy.Model.Component;
namespace Chessy.Model
{
    public struct RiverE
    {
        public RiverC RiverC;
        public HaveRiverAroundCellC HaveRiverC;

        internal RiverE(in bool[] haveRive)
        {
            RiverC = default;
            HaveRiverC = new HaveRiverAroundCellC(haveRive);
        }
    }
}