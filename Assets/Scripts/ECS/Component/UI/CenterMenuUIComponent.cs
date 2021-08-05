using Assets.Scripts.Abstractions.Enums;
using System.Collections.Generic;
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

        internal CenterMenuUIComponent(CanvasComponent canvasComponent, SaverComponent saverComponent)
        {
            _musicSlider = canvasComponent.FindUnderParent<Slider>("Slider");
            _musicSlider.value = saverComponent.SliderVolume;
        }
    }
}
