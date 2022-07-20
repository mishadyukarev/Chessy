namespace Chessy.Model.Component
{
    public sealed class ShiftingObjectC
    {
        internal bool NeedReturnBack;
        internal byte WhereNeedShiftIdxCell;
        internal float Distance;

        public byte WhereNeedShiftIdxCellP => WhereNeedShiftIdxCell;
        public float DistanceP => Distance;
        public bool IsShifting => WhereNeedShiftIdxCell != 0;
        public bool IsIdle => !IsShifting;


        internal void Dispose()
        {
            NeedReturnBack = default;
            WhereNeedShiftIdxCell = default;
            Distance = default;
        }

        internal void Set(in bool needReturnBack, in byte whereIdxCell, in float distance)
        {
            NeedReturnBack = needReturnBack;
            WhereNeedShiftIdxCell = whereIdxCell;
            Distance = distance;
        }

        internal void Clone(in ShiftingObjectC newShiftingObjectC)
        {
            Set(newShiftingObjectC.NeedReturnBack, newShiftingObjectC.WhereNeedShiftIdxCell, newShiftingObjectC.Distance);
        }
    }
}