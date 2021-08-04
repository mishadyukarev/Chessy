namespace Assets.Scripts
{
    internal struct SaverComponent
    {
        internal float SliderVolume { get; set; }
        internal StepModeTypes StepModeType { get; set; }

        internal SaverComponent(float sliderVolume)
        {
            SliderVolume = sliderVolume;
            StepModeType = default;
        }
    }
}