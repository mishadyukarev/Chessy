﻿using Scripts.Common;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.Game
{
    public struct RightUniqueViewUIC
    {
        private static Dictionary<UniqueButtonTypes, Button> _uniqueAbilit_Buttons;
        private static Dictionary<UniqueButtonTypes, Image> _uniqueAbilit_Images;

        public RightUniqueViewUIC(Transform uniqueZone_Trans)
        {
            _uniqueAbilit_Buttons = new Dictionary<UniqueButtonTypes, Button>();
            _uniqueAbilit_Images = new Dictionary<UniqueButtonTypes, Image>();

            var uniqueAbilityButton1 = uniqueZone_Trans.Find("UniqueAbilityButton1").GetComponent<Button>();
            _uniqueAbilit_Buttons.Add(UniqueButtonTypes.First, uniqueAbilityButton1);
            _uniqueAbilit_Images.Add(UniqueButtonTypes.First, uniqueAbilityButton1.transform.Find("Image").GetComponent<Image>());

            var uniqueAbilityButton2 = uniqueZone_Trans.Find("UniqueAbilityButton2").GetComponent<Button>();
            _uniqueAbilit_Buttons.Add(UniqueButtonTypes.Second, uniqueAbilityButton2);

            var uniqueAbilityButton3 = uniqueZone_Trans.Find("UniqueAbilityButton3").GetComponent<Button>();
            _uniqueAbilit_Buttons.Add(UniqueButtonTypes.Third, uniqueAbilityButton3);
        }

        public static void SetActive_Button(UniqueButtonTypes uniqueButtonType, bool isActive) => _uniqueAbilit_Buttons[uniqueButtonType].gameObject.SetActive(isActive);


        public static void Set_Sprite(UniqueButtonTypes uniqueButtonType, SpriteGameTypes spriteGameType) => _uniqueAbilit_Images[uniqueButtonType].sprite = SpritesResComC.Sprite(spriteGameType);

        public static void AddListener_Button(UniqueButtonTypes uniqueButtonType, UnityAction unityAction) => _uniqueAbilit_Buttons[uniqueButtonType].onClick.AddListener(unityAction);
    }
}