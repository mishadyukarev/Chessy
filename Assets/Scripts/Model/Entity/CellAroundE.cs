using Chessy.Model.Component;
using Chessy.Model.Enum;

namespace Chessy.Model
{
    public struct CellAroundE
    {
        public readonly IdxCellC IdxC;
        public readonly XyCellC XyC;
        public CellAroundC CellAroundC;

        internal CellAroundE(in byte cellIdx, in byte[] xy, in DirectTypes dirT, in DistanceFromCellTypes levelFromCellT)
        {
            IdxC = new IdxCellC(cellIdx);
            XyC = new XyCellC(xy);
            CellAroundC = new CellAroundC() { DirectT = dirT, LevelFromCellT = levelFromCellT };
        }
    }
}