namespace Assets.Scripts.ECS.Components
{
    internal struct StepModeTypeComponent
    {
        private StepModeTypes _stepModeType;

        internal StepModeTypes StepModeType => _stepModeType;

        internal bool IsByQueueMode => _stepModeType == StepModeTypes.ByQueue;
        internal bool IsTogetherMode => _stepModeType == StepModeTypes.Together;


        internal void StartFill(StepModeTypes stepsModeType = default) => SetStepModeType(stepsModeType);

        internal void SetStepModeType(StepModeTypes stepsModeType) => _stepModeType = stepsModeType;
    }
}
