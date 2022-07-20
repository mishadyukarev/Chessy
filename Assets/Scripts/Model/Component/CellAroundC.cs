using Chessy.Model.Enum;

namespace Chessy.Model.Component
{
    public sealed class CellAroundC
    {
        public DirectTypes DirectT { get; internal set; }
        public DistanceFromCellTypes LevelFromCellT { get; internal set; }
    }
}