namespace Assets.Scripts
{
    internal struct SaverComponent
    {
        internal static float SliderVolume { get; set; }
        internal static StepModeTypes StepModeType { get; set; }

        internal SaverComponent(StepModeTypes stepModeType, float sliderVolume)
        {
            SliderVolume = sliderVolume;
            StepModeType = stepModeType;
        }
    }
}