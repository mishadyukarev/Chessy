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

        public int MaxSteps(UnitEffectsC effC, UnitTypes unit, float upgPerc) => UnitValues.StandartAmountSteps(effC.Have(UnitStatTypes.Steps), unit, upgPerc);
        public bool HaveMaxSteps(UnitEffectsC effC, UnitTypes unit, float upgPerc) => StepsAmount >= MaxSteps(effC, unit, upgPerc);
        public void ZeroSteps() => StepsAmount = 0;
        public void SetMaxSteps(UnitEffectsC effC, UnitTypes unit, float upgPerc) => StepsAmount = MaxSteps(effC, unit, upgPerc);
        public int StepsForDoing(CellEnvDataC cellEnvC, DirectTypes dir_cur, CellTrailDataC trailC)
        {
            var needSteps = 1;

            if (cellEnvC.Have(EnvTypes.AdultForest))
            {
                needSteps += UnitValues.NeedAmountSteps(EnvTypes.AdultForest);
                if (trailC.Have(dir_cur.Invert())) needSteps -= 1;
            }

            if (cellEnvC.Have(EnvTypes.Hill))
                needSteps += UnitValues.NeedAmountSteps(EnvTypes.Hill);

            return needSteps;
        }
        public  bool HaveStepsForDoing(CellEnvDataC cellEnvC, DirectTypes dir_cur, CellTrailDataC trailC) => StepsAmount >= StepsForDoing(cellEnvC, dir_cur, trailC);
        public void TakeStepsForDoing(CellEnvDataC cellEnvC, DirectTypes dir_cur, CellTrailDataC trailC) => StepsAmount -= StepsForDoing(cellEnvC, dir_cur, trailC);

        public void AddBonus() => StepsAmount += 1;
    }
}