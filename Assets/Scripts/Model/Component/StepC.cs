namespace Chessy.Game
{
    public struct StepsC
    {
        public double Steps { get; internal set; }

        public bool HaveAnySteps => Steps > 0;
    }
}