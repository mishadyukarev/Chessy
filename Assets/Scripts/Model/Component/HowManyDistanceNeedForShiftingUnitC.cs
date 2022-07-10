namespace Chessy.Model
{
    public readonly struct HowManyDistanceNeedForShiftingUnitC
    {
        readonly float[] _howMany;

        public float HowMany(in byte cellIdx) => _howMany[cellIdx];
        public bool[] HowManyClone => (bool[])_howMany.Clone();

        internal HowManyDistanceNeedForShiftingUnitC(in float[] howManyEnergyNeedForShiftingUnit)
        {
            _howMany = howManyEnergyNeedForShiftingUnit;
        }

        internal void Set(in byte cellIdx, in float distance) => _howMany[cellIdx] = distance;
    }
}