using System;

namespace Game.Game
{
    public struct StepC : IUnitStatCell
    {
        public int Steps { get; internal set; }
        public bool HaveMin => Steps > 0;
        public bool IsMinusSteps => Steps < 0;
        public bool IsNull => Steps == 0;




        internal void Set(in StepC stepC) => Steps = stepC.Steps;
        internal void Set(in int steps) => Steps = steps;

        internal void AddSteps(in int adding = 1)
        {
            if (adding < 0) throw new Exception("Need a positive number");
            else if (adding == 0) throw new Exception("You're adding zero");
            Steps += adding;
        }
        public void Take(in int taking = 1)
        {
            if (taking < 0) throw new Exception("Need a positive number");
            else if (taking == 0) throw new Exception("You're taking zero");
            Steps -= taking;
            if (IsMinusSteps) Reset();
        }
        public void Reset() => Steps = 0;
    }
}