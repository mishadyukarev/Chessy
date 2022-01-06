//using Scripts.Common;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Events;
//using UnityEngine.UI;

//namespace Scripts.Game
//{
//    public struct RightUniqueViewUIC
//    {
//        private static Dictionary<UniqueButtonTypes, Button> _uniqueAbilit_Buttons;
//        private static Dictionary<UniqueButtonTypes, Image> _uniqueAbilit_Images;

//        public RightUniqueViewUIC(Transform uniqueZone_Trans)
//        {
//            _uniqueAbilit_Buttons = new Dictionary<UniqueButtonTypes, Button>();
//            _uniqueAbilit_Images = new Dictionary<UniqueButtonTypes, Image>();





//        }

//        public static void SetActive_Button(UniqueButtonTypes uniqueButtonType, bool isActive) => _uniqueAbilit_Buttons[uniqueButtonType].gameObject.SetActive(isActive);


//        public static void Set_Sprite(UniqueButtonTypes uniqueButtonType, SpriteGameTypes spriteGameType) => _uniqueAbilit_Images[uniqueButtonType].sprite = SpritesResComC.Sprite(spriteGameType);

//        public static void AddListener_Button(UniqueButtonTypes uniqueButtonType, UnityAction unityAction) => _uniqueAbilit_Buttons[uniqueButtonType].onClick.AddListener(unityAction);
//    }
//}
