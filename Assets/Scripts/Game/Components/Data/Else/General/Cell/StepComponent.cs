using System;

namespace Scripts.Game
{
    public struct StepComponent
    {
        public int AmountSteps { get; set; }

        public bool HaveMinSteps => AmountSteps > 0;

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
        }

        public int MaxSteps(UnitTypes unitType) => UnitValues.StandartAmountSteps(unitType);
        public bool HaveMaxSteps(UnitTypes unitType) => AmountSteps >= MaxSteps(unitType);
        public void DefSteps() => AmountSteps = default;
        public void SetMaxSteps(UnitTypes unitType) => AmountSteps = MaxSteps(unitType);

        public void AddBonus() => AmountSteps += 1;
    }
}