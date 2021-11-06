﻿using System;

namespace Scripts.Game
{
    public struct StepComponent
    {
        private int _steps;

        public int Steps => _steps;
        public bool HaveMinSteps => Steps > 0;
        public bool IsMinusSteps => Steps < 0;
        public bool IsNull => Steps == 0;


        public void AddSteps(int adding = 1)
        {
            if (adding < 0) throw new Exception("Need a positive number");
            else if (adding == 0) throw new Exception("You're adding zero");
            _steps += adding;
        }
        public void TakeSteps(int taking = 1)
        {
            if (taking < 0) throw new Exception("Need a positive number");
            else if (taking == 0) throw new Exception("You're taking zero");
            _steps -= taking;
            if (IsMinusSteps) DefSteps();
        }

        public int MaxSteps(UnitTypes unit, bool haveEff, float upgPerc) => UnitValues.StandartAmountSteps(unit, haveEff, upgPerc);
        public bool HaveMaxSteps(UnitTypes unit, bool haveEff, float upgPerc) => Steps >= MaxSteps(unit, haveEff, upgPerc);
        public void DefSteps() => _steps = 0;
        public void SetMaxSteps(UnitTypes unit, bool haveEff, float upgPerc) => _steps = MaxSteps(unit, haveEff, upgPerc);
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
        public bool HaveStepsForDoing(CellEnvDataC cellEnvC, DirectTypes dir_cur, CellTrailDataC trailC) => Steps >= StepsForDoing(cellEnvC, dir_cur, trailC);
        public void TakeStepsForDoing(CellEnvDataC cellEnvC, DirectTypes dir_cur, CellTrailDataC trailC) => _steps -= StepsForDoing(cellEnvC, dir_cur, trailC);

        public void Sync(int steps)
        {
            _steps = steps;
        }
    }
}