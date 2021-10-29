namespace Scripts.Game
{
    public struct StepComponent
    {
        public int AmountSteps;
        public void AddAmountSteps(int adding = 1) => AmountSteps += adding;
        public void TakeAmountSteps(int taking = 1) => AmountSteps -= taking;
        public int MaxAmountSteps(UnitTypes unitType) => UnitValues.StandartAmountSteps(unitType);
        public bool HaveMaxAmountSteps(UnitTypes unitType) => AmountSteps == MaxAmountSteps(unitType);
        public bool HaveMinAmountSteps => AmountSteps >= 1;
        public void DefAmountSteps() => AmountSteps = default;
        public void SetMaxAmountSteps(UnitTypes unitType) => AmountSteps = MaxAmountSteps(unitType);
    }
}