namespace Chessy.Model.Component
{
    public struct ShiftingObjectC
    {
        public bool NeedReturnBack { get; internal set; }
        public byte WhereIdxCell { get; internal set; }
        public float Distance { get; internal set; }

        public bool IsShiftingUnit => WhereIdxCell != 0;
        public bool IsIdle => !IsShiftingUnit;

        internal void Dispose()
        {
            NeedReturnBack = default;
            WhereIdxCell = default;
            Distance = default;
        }
    }
}