using Scripts.Common;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.Game
{
    internal struct BuildAbilitUICom
    {
        private TextMeshProUGUI _buildingZone_TextMP;
        private Dictionary<BuildButtonTypes, Button> _building_Buttons;
        private Image _third_Image;

        internal BuildAbilitUICom(Transform buildZone_Tran)
        {
            _buildingZone_TextMP = buildZone_Tran.Find("BuildingAbilities_TextMP").GetComponent<TextMeshProUGUI>();

            _building_Buttons = new Dictionary<BuildButtonTypes, Button>();

            var buildFirstAbil_Buttom = buildZone_Tran.Find("BuildingAbilityButton1").GetComponent<Button>();
            _building_Buttons.Add(BuildButtonTypes.First, buildFirstAbil_Buttom);

            var buildSecondAbil_Buttom = buildZone_Tran.Find("BuildingAbilityButton2").GetComponent<Button>();
            _building_Buttons.Add(BuildButtonTypes.Second, buildSecondAbil_Buttom);

            var buildingThirdAbilityButtom = buildZone_Tran.Find("BuildingAbilityButton3").GetComponent<Button>();
            _building_Buttons.Add(BuildButtonTypes.Third, buildingThirdAbilityButtom);
            _third_Image = buildingThirdAbilityButtom.transform.Find("Image (4)").GetComponent<Image>();
        }
        internal void ActiveInfo(bool isActive) => _buildingZone_TextMP.enabled = isActive;
        internal void SetActive_Button(BuildButtonTypes buildingButtonType, bool isActive) => _building_Buttons[buildingButtonType].gameObject.SetActive(isActive);

        internal void SetTextInfo(string text) => _buildingZone_TextMP.text = text;

        internal void SetSpriteThird(SpriteGameTypes spriteGameType) => _third_Image.sprite = ResourcesComponent.SpritesConfig.GetSprite(spriteGameType);

        internal void AddListener_Button(BuildButtonTypes buildingButtonType, UnityAction unityAction) => _building_Buttons[buildingButtonType].onClick.AddListener(unityAction);
    }
}
