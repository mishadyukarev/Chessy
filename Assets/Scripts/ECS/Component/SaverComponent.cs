namespace Assets.Scripts
{
    internal struct SaverComponent
    {
        internal static float SliderVolume { get; set; }
        internal static StepModeTypes StepModeType { get; set; }

        internal SaverComponent(float sliderVolume)
        {
            SliderVolume = sliderVolume;
            StepModeType = default;
        }
    }
}