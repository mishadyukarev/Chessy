using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Components.Data.Else.Common;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ECS.Component.UI
{
    internal struct CenterMenuUIComp
    {
        private Slider _musicSlider;
        private TMP_Dropdown _language_Dropdown;
        private TextMeshProUGUI _info_TextMP;
        private TextMeshProUGUI _exit_TextMP;

        internal float MusicVolume => _musicSlider.value;


        internal LanguageTypes LanguageType
        {
            get
            {
                if(_language_Dropdown.value == 0) return LanguageTypes.English;
                else if(_language_Dropdown.value == 1) return LanguageTypes.Russian;
                else if (_language_Dropdown.value == 2) return LanguageTypes.Spanish;
                else if(_language_Dropdown.value == 3) return LanguageTypes.Chinese;

                else throw new Exception();
            }
        }

        internal CenterMenuUIComp(Slider slider, float value)
        {
            _musicSlider = slider;
            _musicSlider.value = value;

            _language_Dropdown = CanvasComp.FindUnderParent<TMP_Dropdown>("Language_Dropdown");
            if(LanguageComComp.CurLanguageType == LanguageTypes.English) _language_Dropdown.value = 0;
            else if(LanguageComComp.CurLanguageType == LanguageTypes.Russian) _language_Dropdown.value = 1;
            else if (LanguageComComp.CurLanguageType == LanguageTypes.Spanish) _language_Dropdown.value = 2;
            else if (LanguageComComp.CurLanguageType == LanguageTypes.Chinese) _language_Dropdown.value = 3;

            _info_TextMP = CanvasComp.FindUnderParent<TextMeshProUGUI>("Info_TextMP");
            _exit_TextMP = CanvasComp.FindUnderParent<Transform>("QuitButton").Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
        }

        internal void SetTextInfo(string text) => _info_TextMP.text = text;
        internal void SetTextExit(string text) => _exit_TextMP.text = text;
    }
}
