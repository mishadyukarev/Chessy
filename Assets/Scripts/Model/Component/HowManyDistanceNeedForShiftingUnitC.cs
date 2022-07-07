namespace Chessy.Model
{
    public struct HowManyDistanceNeedForShiftingUnitC
    {
        readonly float[] _howManyEnergyNeedForShiftingUnit;

        public float HowMany(in byte cellIdx) => _howManyEnergyNeedForShiftingUnit[cellIdx];
        public bool[] HowManyClone => (bool[])_howManyEnergyNeedForShiftingUnit.Clone();

        internal HowManyDistanceNeedForShiftingUnitC(in float[] howManyEnergyNeedForShiftingUnit)
        {
            _howManyEnergyNeedForShiftingUnit = howManyEnergyNeedForShiftingUnit;
        }

        internal void Set(in byte idxCell, in float distance) => _howManyEnergyNeedForShiftingUnit[idxCell] = distance;
    }
}