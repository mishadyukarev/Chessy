namespace Assets.Scripts.ECS.Components
{
    internal struct TimeStepsComponent
    {
        private int _timeSteps;

        internal int TimeSteps => _timeSteps;

        internal void StartFill(int timeSteps = default) => _timeSteps = timeSteps;

        internal void ResetTimeSteps() => _timeSteps = default;
        internal void SetTimeSteps(int value) => _timeSteps = value;
        internal void AddTimeSteps(int adding = 1) => _timeSteps += adding;
        internal void TakeTimeSteps(int taking = 1) => _timeSteps -= taking;
    }
}
