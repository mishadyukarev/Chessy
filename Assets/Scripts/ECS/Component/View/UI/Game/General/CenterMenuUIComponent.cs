using UnityEngine.UI;

namespace Assets.Scripts.ECS.Component.UI
{
    internal struct CenterMenuUIComponent
    {
        private Slider _musicSlider;

        internal float MusicVolume
        {
            get => _musicSlider.value;
        }

        internal CenterMenuUIComponent(Slider slider, float sliderVolume)
        {
            _musicSlider = slider;
            _musicSlider.value = sliderVolume;
        }
    }
}
