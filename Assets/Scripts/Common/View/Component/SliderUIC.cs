using UnityEngine.UI;

namespace Chessy.Common.Component
{
    public struct SliderUIC
    {
        public Slider Slider;

        public SliderUIC(in Slider slider) => Slider = slider;
    }
}