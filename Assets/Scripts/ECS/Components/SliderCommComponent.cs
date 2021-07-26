using UnityEngine.UI;

namespace Assets.Scripts
{
    internal struct SliderCommComponent
    {
        internal Slider Slider { get; private set; }

        internal SliderCommComponent(Slider slider) => Slider = slider; 
    }
}