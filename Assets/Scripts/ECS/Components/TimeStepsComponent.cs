namespace Assets.Scripts.ECS.Components
{
    internal struct TimeStepsComponent
    {
        internal int TimeSteps { get; set; }

        internal void StartFill(int timeSteps = default) => TimeSteps = timeSteps;
    }
}
