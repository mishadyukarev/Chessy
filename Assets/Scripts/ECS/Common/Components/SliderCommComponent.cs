using UnityEngine.UI;

internal struct SliderCommComponent
{
    private Slider _slider;

    internal float Value
    {
        get => _slider.value;
        set => _slider.value = value;
    }

    internal void SetSlider(Slider slider) => _slider = slider;
}
