namespace Assets.Scripts
{
    internal struct SaverComponent
    {
        internal float SliderVolume { get; set; }

        internal SaverComponent(float sliderVolume) => SliderVolume = sliderVolume;
    }
}