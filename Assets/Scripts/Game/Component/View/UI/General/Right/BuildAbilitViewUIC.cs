using Game.Common;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Game
{
    public struct BuildAbilitViewUIC

    {
        private static Dictionary<BuildButtonTypes, Button> _building_Buttons;
        private static Image _third_Image;

        public BuildAbilitViewUIC(Transform buildZone_Tran)
        {
            _building_Buttons = new Dictionary<BuildButtonTypes, Button>();

            var buildFirstAbil_Buttom = buildZone_Tran.Find("BuildingAbilityButton1").GetComponent<Button>();
            _building_Buttons.Add(BuildButtonTypes.First, buildFirstAbil_Buttom);

            var buildSecondAbil_Buttom = buildZone_Tran.Find("BuildingAbilityButton2").GetComponent<Button>();
            _building_Buttons.Add(BuildButtonTypes.Second, buildSecondAbil_Buttom);

            var buildingThirdAbilityButtom = buildZone_Tran.Find("BuildingAbilityButton3").GetComponent<Button>();
            _building_Buttons.Add(BuildButtonTypes.Third, buildingThirdAbilityButtom);
            _third_Image = buildingThirdAbilityButtom.transform.Find("Image (4)").GetComponent<Image>();
        }
        public static void SetActive_Button(BuildButtonTypes buildingButtonType, bool isActive) => _building_Buttons[buildingButtonType].gameObject.SetActive(isActive);


        public static void SetSpriteThird(SpriteGameTypes spriteGameType) => _third_Image.sprite = SpritesResComC.Sprite(spriteGameType);

        public static void AddListener_Button(BuildButtonTypes buildingButtonType, UnityAction unityAction) => _building_Buttons[buildingButtonType].onClick.AddListener(unityAction);
    }
}
