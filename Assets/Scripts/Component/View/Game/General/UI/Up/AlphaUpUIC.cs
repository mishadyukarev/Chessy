//using UnityEngine;
//using UnityEngine.Events;
//using UnityEngine.UI;

//namespace Game.Game
//{
//    public struct AlphaUpUIC
//    {
//        private static Button _button;

//        public AlphaUpUIC(Transform up)
//        {
//            _button = up.Find("Alpha_Button").GetComponent<Button>();
//        }

//        public static void AddList(UnityAction action) => _button.onClick.AddListener(action);
//    }
//}