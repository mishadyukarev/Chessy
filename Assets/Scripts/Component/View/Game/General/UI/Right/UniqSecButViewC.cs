//using UnityEngine;
//using UnityEngine.Events;
//using UnityEngine.UI;

//namespace Chessy.Game
//{
//    public struct UniqSecButViewC
//    {
//        private static Button _button;

//        public UniqSecButViewC(Transform parent)
//        {
//            _button = parent.Find("Second").GetComponent<Button>();
//        }

//        public static void AddListener(UnityAction action) => _button.onClick.AddListener(action);
//        public static void SetActive(UniqSecAbilTypes ability)
//        {
//            if (ability == default) _button.gameObject.SetActive(false);
//            else _button.gameObject.SetActive(true);
//        }
//    }
//}