using Assets.Scripts.Abstractions.ValuesConsts;
using System;
using TMPro;
using UnityEngine.UI;

namespace Assets.Scripts.ECS.Component.UI
{
    internal struct CenterMenuUIComp
    {
        private Slider _musicSlider;
        private TMP_Dropdown _language_Dropdown;

        internal float MusicVolume
        {
            get => _musicSlider.value;
        }
        internal LanguageTypes LanguageType
        {
            get
            {
                if(_language_Dropdown.value == 0)
                {
                    return LanguageTypes.English;
                }
                else if(_language_Dropdown.value == 1)
                {
                    return LanguageTypes.Russian;
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        internal CenterMenuUIComp(Slider slider, float value)
        {
            _musicSlider = slider;
            _musicSlider.value = value;

            _language_Dropdown = CanvasComponent.FindUnderParent<TMP_Dropdown>("Language_Dropdown");
        }
    }
}
