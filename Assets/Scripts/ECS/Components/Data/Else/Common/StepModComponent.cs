namespace Assets.Scripts
{
    internal struct StepModComponent
    {
        internal static StepModeTypes StepModeType { get; set; }

        internal StepModComponent(StepModeTypes stepModeType)
        {
            StepModeType = stepModeType;
        }
    }
}