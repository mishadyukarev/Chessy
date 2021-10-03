using Scripts.Common;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Menu
{
    internal struct CenterMenuUICom
    {
        private TextMeshProUGUI _log_TextMP;
        private Slider _musicSlider;
        private TMP_Dropdown _language_Dropdown;
        private Button _discord_Button;

        internal float MusicVolume => _musicSlider.value;


        internal LanguageTypes LanguageType
        {
            get
            {
                if (_language_Dropdown.value == 0) return LanguageTypes.English;
                else if (_language_Dropdown.value == 1) return LanguageTypes.Russian;
                else if (_language_Dropdown.value == 2) return LanguageTypes.Spanish;
                else if (_language_Dropdown.value == 3) return LanguageTypes.Chinese;

                else throw new Exception();
            }
        }

        internal CenterMenuUICom(Transform centerZone_Trans, float value)
        {
            _log_TextMP = centerZone_Trans.Find("Log_TextMP").GetComponent<TextMeshProUGUI>();

            _musicSlider = centerZone_Trans.Find("Slider").GetComponent<Slider>();
            _musicSlider.value = value;

            _language_Dropdown = centerZone_Trans.Find("Language_Dropdown").GetComponent<TMP_Dropdown>();
            if (LanguageComCom.CurLanguageType == LanguageTypes.English) _language_Dropdown.value = 0;
            else if (LanguageComCom.CurLanguageType == LanguageTypes.Russian) _language_Dropdown.value = 1;
            else if (LanguageComCom.CurLanguageType == LanguageTypes.Spanish) _language_Dropdown.value = 2;
            else if (LanguageComCom.CurLanguageType == LanguageTypes.Chinese) _language_Dropdown.value = 3;


            _discord_Button = centerZone_Trans.transform.Find("JoinDiscordButton").GetComponent<Button>();
            _discord_Button.onClick.AddListener(delegate { Application.OpenURL(SpawnInitComSys.IRL_DISCORD); });
        }

        internal void SetLogText(string text) => _log_TextMP.text = text;
    }
}
