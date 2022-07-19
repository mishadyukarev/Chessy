namespace Chessy.Model.Component
{
    public struct ShiftingObjectC
    {
        public bool NeedReturnBack { get; internal set; }
        internal byte WhereNeedShiftIdxCell;
        public float Distance { get; internal set; }

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
    }
}