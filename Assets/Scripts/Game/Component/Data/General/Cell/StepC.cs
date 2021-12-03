using System;

namespace Game.Game
{
    public struct StepC : IUnitStatCell
    {
        int _steps;

        public int Steps => _steps;
        public bool HaveMin => Steps > 0;
        public bool IsMinusSteps => Steps < 0;
        public bool IsNull => Steps == 0;




        internal void Set(in StepC stepC) => _steps = stepC._steps;
        internal void Set(in int steps) => _steps = steps;

        public void AddSteps(in int adding = 1)
        {
            if (adding < 0) throw new Exception("Need a positive number");
            else if (adding == 0) throw new Exception("You're adding zero");
            _steps += adding;
        }
        public void Take(in int taking = 1)
        {
            if (taking < 0) throw new Exception("Need a positive number");
            else if (taking == 0) throw new Exception("You're taking zero");
            _steps -= taking;
            if (IsMinusSteps) Reset();
        }
        public void Reset() => _steps = 0;
    }
}