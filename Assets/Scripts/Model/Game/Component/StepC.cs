namespace Chessy.Game
{
    public struct StepsC
    {
        public float Steps { get; internal set; }

        public bool HaveAnySteps => Steps > 0;
    }
}