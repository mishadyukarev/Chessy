using Chessy.Model.Enum;

namespace Chessy.Model.Component
{
    public struct CellAroundC
    {
        public DirectTypes DirectT { get; internal set; }
        public DistanceFromCellTypes LevelFromCellT { get; internal set; }
    }
}