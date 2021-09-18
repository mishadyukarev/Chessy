using Assets.Scripts.Abstractions.Enums;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.ECS.Components.View.UI.Game.General.Right
{
    internal struct UniqueAbiltUICom
    {
        private TextMeshProUGUI _uniqueAbilitiesZone_TextMP;
        private Dictionary<UniqueButtonTypes, Button> _uniqueAbilit_Buttons;
        private Dictionary<UniqueButtonTypes, TextMeshProUGUI> _uniqueAbilit_TextMPs;

        internal UniqueAbiltUICom(Transform uniqueZone_Trans)
        {
            _uniqueAbilitiesZone_TextMP = uniqueZone_Trans.Find("UniqueAbilities_TextMP").GetComponent<TextMeshProUGUI>();

            _uniqueAbilit_Buttons = new Dictionary<UniqueButtonTypes, Button>();
            _uniqueAbilit_TextMPs = new Dictionary<UniqueButtonTypes, TextMeshProUGUI>();

            var uniqueAbilityButton1 = uniqueZone_Trans.Find("UniqueAbilityButton1").GetComponent<Button>();
            _uniqueAbilit_Buttons.Add(UniqueButtonTypes.First, uniqueAbilityButton1);
            _uniqueAbilit_TextMPs.Add(UniqueButtonTypes.First, uniqueAbilityButton1.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>());

            var uniqueAbilityButton2 = uniqueZone_Trans.Find("UniqueAbilityButton2").GetComponent<Button>();
            _uniqueAbilit_Buttons.Add(UniqueButtonTypes.Second, uniqueAbilityButton2);
            _uniqueAbilit_TextMPs.Add(UniqueButtonTypes.Second, uniqueAbilityButton2.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>());

            var uniqueAbilityButton3 = uniqueZone_Trans.Find("UniqueAbilityButton3").GetComponent<Button>();
            _uniqueAbilit_Buttons.Add(UniqueButtonTypes.Third, uniqueAbilityButton3);
            _uniqueAbilit_TextMPs.Add(UniqueButtonTypes.Third, uniqueAbilityButton3.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>());
        }

        internal void SetActiveInfo(bool isActive) => _uniqueAbilitiesZone_TextMP.gameObject.SetActive(isActive);
        internal void SetActive_Button(UniqueButtonTypes uniqueButtonType, bool isActive) => _uniqueAbilit_Buttons[uniqueButtonType].gameObject.SetActive(isActive);


        internal void SetColor_Button(UniqueButtonTypes uniqueButtonType, Color color) => _uniqueAbilit_Buttons[uniqueButtonType].image.color = color;

        internal void SetTextInfo(string text) => _uniqueAbilitiesZone_TextMP.text = text;
        internal void SetText_Button(UniqueButtonTypes uniqueButtonType, string text) => _uniqueAbilit_TextMPs[uniqueButtonType].text = text;

        internal void AddListener_Button(UniqueButtonTypes uniqueButtonType, UnityAction unityAction) => _uniqueAbilit_Buttons[uniqueButtonType].onClick.AddListener(unityAction);

        internal void RemoveAllList(UniqueButtonTypes uniqueButtonType) => _uniqueAbilit_Buttons[uniqueButtonType].onClick.RemoveAllListeners();
    }
}
