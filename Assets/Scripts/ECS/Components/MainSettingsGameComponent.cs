namespace Assets.Scripts.ECS.Components
{
    internal struct MainSettingsGameComponent
    {
        private StepModeTypes _stepModeType;

        internal StepModeTypes StepModeType => _stepModeType;


        internal void StartFill(StepModeTypes stepsModeType = default) => SetStepModeType(stepsModeType);

        internal void SetStepModeType(StepModeTypes stepsModeType) => _stepModeType = stepsModeType;
    }
}
