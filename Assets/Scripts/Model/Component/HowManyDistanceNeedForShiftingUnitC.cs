namespace Chessy.Model
{
    public sealed class HowManyDistanceNeedForShiftingUnitC
    {
        internal readonly float[] HowManyArray;

        public float HowMany(in byte cellIdx) => HowManyArray[cellIdx];

        internal HowManyDistanceNeedForShiftingUnitC(in float[] howManyEnergyNeedForShiftingUnit)
        {
            HowManyArray = howManyEnergyNeedForShiftingUnit;
        }

        internal void Set(in byte cellIdx, in float distance) => HowManyArray[cellIdx] = distance;
    }
}