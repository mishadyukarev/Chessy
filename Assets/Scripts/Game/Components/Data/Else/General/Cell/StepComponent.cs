using System;

namespace Scripts.Game
{
    public struct StepComponent
    {
        public int StepsAmount { get; set; }

        public bool HaveMinSteps => StepsAmount > 0;
        public bool IsMinusSteps => StepsAmount < 0;

        public void AddSteps(int adding = 1)
        {
            if (adding < 0) throw new Exception("Need a positive number");
            else if (adding == 0) throw new Exception("You're adding zero");
            StepsAmount += adding;
        }
        public void TakeSteps(int taking = 1)
        {
            if (taking < 0) throw new Exception("Need a positive number");
            else if (taking == 0) throw new Exception("You're taking zero");
            StepsAmount -= taking;
            if (IsMinusSteps) ZeroSteps();
        }

        public int MaxSteps(UnitEffectsC unitEffectsC, UnitTypes unitType) => UnitValues.StandartAmountSteps(unitEffectsC.Have(StatTypes.Steps), unitType);
        public bool HaveMaxSteps(UnitEffectsC unitEffectsC, UnitTypes unitType) => StepsAmount >= MaxSteps(unitEffectsC, unitType);
        public void ZeroSteps() => StepsAmount = 0;
        public void SetMaxSteps(UnitEffectsC unitEffectsC, UnitTypes unitType) => StepsAmount = MaxSteps(unitEffectsC, unitType);
        public int StepsForDoing(CellEnvDataC cellEnvC)
        {
            var needSteps = 1;

            if (cellEnvC.Have(EnvirTypes.AdultForest))
                needSteps += UnitValues.NeedAmountSteps(EnvirTypes.AdultForest);

            if (cellEnvC.Have(EnvirTypes.Hill))
                needSteps += UnitValues.NeedAmountSteps(EnvirTypes.Hill);

            return needSteps;
        }
        public  bool HaveStepsForDoing(CellEnvDataC cellEnvC) => StepsAmount >= StepsForDoing(cellEnvC);
        public void TakeStepsForDoing(CellEnvDataC cellEnvC) => StepsAmount -= StepsForDoing(cellEnvC);

        public void AddBonus() => StepsAmount += 1;
    }
}