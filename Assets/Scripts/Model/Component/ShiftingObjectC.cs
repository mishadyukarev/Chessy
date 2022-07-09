namespace Chessy.Model.Component
{
    public struct ShiftingObjectC
    {
        public bool NeedReturnBack { get; internal set; }
        public byte WhereNeedShiftIdxCell { get; internal set; }
        public float Distance { get; internal set; }

        public bool IsShiftingUnit => WhereNeedShiftIdxCell != 0;
        public bool IsIdle => !IsShiftingUnit;

        internal void Dispose()
        {
            NeedReturnBack = default;
            WhereNeedShiftIdxCell = default;
            Distance = default;
        }
    }
}