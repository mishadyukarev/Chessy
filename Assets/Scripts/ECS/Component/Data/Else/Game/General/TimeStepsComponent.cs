namespace Assets.Scripts.ECS.Components
{
    internal struct TimeStepsComponent
    {
        internal int TimeSteps { get; set; }

        internal void Add(int add = 1) => TimeSteps += add;
        internal void Take(int take = 1) => TimeSteps -= take;
        internal void Reset() => TimeSteps = default;
    }
}
