namespace Chessy.Model
{
    public struct HowManyEnergyNeedForShiftingUnitC
    {
        readonly float[] _howManyEnergyNeedForShiftingUnit;

        public float HowManyEnergyNeedForShiftingToHere(in byte cellIdx) => _howManyEnergyNeedForShiftingUnit[cellIdx];
        public bool[] HowManyEnergyNeedForShiftingUnit => (bool[])_howManyEnergyNeedForShiftingUnit.Clone();

        internal HowManyEnergyNeedForShiftingUnitC(in float[] howManyEnergyNeedForShiftingUnit)
        {
            _howManyEnergyNeedForShiftingUnit = howManyEnergyNeedForShiftingUnit;
        }

        internal void SetHowManyEnergyNeedForShiftingToHere(in byte idxCell, in float energy) => _howManyEnergyNeedForShiftingUnit[idxCell] = energy;
    }
}