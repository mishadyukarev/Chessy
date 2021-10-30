using System;

namespace Scripts.Game
{
    public struct StepComponent
    {
        public int AmountSteps { get; set; }

        public bool HaveMinSteps => AmountSteps > 0;
        public bool IsMinusSteps => AmountSteps < 0;

        public void AddSteps(int adding = 1)
        {
            if (adding < 0) throw new Exception("Need a positive number");
            else if (adding == 0) throw new Exception("You're adding zero");
            AmountSteps += adding;
        }
        public void TakeSteps(int taking = 1)
        {
            if (taking < 0) throw new Exception("Need a positive number");
            else if (taking == 0) throw new Exception("You're taking zero");
            AmountSteps -= taking;
            if (IsMinusSteps) ZeroSteps();
        }

        public int MaxSteps(UnitEffectsC unitEffectsC, UnitTypes unitType) => UnitValues.StandartAmountSteps(unitEffectsC.Have(StatTypes.Steps), unitType);
        public bool HaveMaxSteps(UnitEffectsC unitEffectsC, UnitTypes unitType) => AmountSteps >= MaxSteps(unitEffectsC, unitType);
        public void ZeroSteps() => AmountSteps = 0;
        public void SetMaxSteps(UnitEffectsC unitEffectsC, UnitTypes unitType) => AmountSteps = MaxSteps(unitEffectsC, unitType);

        public void AddBonus() => AmountSteps += 1;
    }
}