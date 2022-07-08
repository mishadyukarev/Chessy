namespace Chessy.Model.Component
{
    public struct ShiftingInfoForUnitC
    {
        public byte IdxWhereNeedShiftUnitOnOtherCell { get; internal set; }
        public float DistanceForShiftingOnOtherCell { get; internal set; }
        //public float DelayTimeForShifting { get; internal set; }
        public bool NeedToBackUnitOnHisCell { get; internal set; }

        public bool IsShiftingUnit => IdxWhereNeedShiftUnitOnOtherCell != 0;
    }
}