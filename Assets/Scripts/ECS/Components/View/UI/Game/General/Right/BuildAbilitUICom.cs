using Assets.Scripts.Abstractions.Enums;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.ECS.Components.View.UI.Game.General
{
    internal struct BuildAbilitUICom
    {
        private TextMeshProUGUI _buildingZone_TextMP;
        private Dictionary<BuildButtonTypes, Button> _building_Buttons;
        private Dictionary<BuildButtonTypes, TextMeshProUGUI> _building_TextMPs;

        internal BuildAbilitUICom(Transform buildZone_Tran)
        {
            _buildingZone_TextMP = buildZone_Tran.Find("BuildingAbilities_TextMP").GetComponent<TextMeshProUGUI>();

            _building_TextMPs = new Dictionary<BuildButtonTypes, TextMeshProUGUI>();
            _building_Buttons = new Dictionary<BuildButtonTypes, Button>();

            var buildFirstAbil_Buttom = buildZone_Tran.Find("BuildingAbilityButton1").GetComponent<Button>();
            _building_Buttons.Add(BuildButtonTypes.First, buildFirstAbil_Buttom);
            _building_TextMPs.Add(BuildButtonTypes.First, buildFirstAbil_Buttom.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>());

            var buildSecondAbil_Buttom = buildZone_Tran.Find("BuildingAbilityButton2").GetComponent<Button>();
            _building_Buttons.Add(BuildButtonTypes.Second, buildSecondAbil_Buttom);
            _building_TextMPs.Add(BuildButtonTypes.Second, buildSecondAbil_Buttom.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>());

            var buildingThirdAbilityButtom = buildZone_Tran.Find("BuildingAbilityButton3").GetComponent<Button>();
            _building_Buttons.Add(BuildButtonTypes.Third, buildingThirdAbilityButtom);
            _building_TextMPs.Add(BuildButtonTypes.Third, buildingThirdAbilityButtom.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>());
        }
        internal void ActiveInfo(bool isActive) => _buildingZone_TextMP.enabled = isActive;
        internal void SetActive_Button(BuildButtonTypes buildingButtonType, bool isActive) => _building_Buttons[buildingButtonType].gameObject.SetActive(isActive);

        internal void SetTextInfo(string text) => _buildingZone_TextMP.text = text;
        internal void SetText_Button(BuildButtonTypes buildButType, string text) => _building_TextMPs[buildButType].text = text;

        internal void AddListener_Button(BuildButtonTypes buildingButtonType, UnityAction unityAction) => _building_Buttons[buildingButtonType].onClick.AddListener(unityAction);
    }
}
