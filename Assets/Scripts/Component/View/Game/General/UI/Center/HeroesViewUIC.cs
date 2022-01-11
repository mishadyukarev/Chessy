//using UnityEngine;
//using UnityEngine.Events;
//using UnityEngine.UI;

//namespace Game.Game
//{
//    public struct HeroesViewUIC
//    {
//        private static GameObject _parent;
//        private static Button _elffemale;
//        private static Button _premium;

//        public HeroesViewUIC(Transform centerZone)
//        {

//        }

//        public static void AddListElf(UnityAction action) => _elffemale.onClick.AddListener(action);
//        public static void AddListPremium(UnityAction action) => _premium.onClick.AddListener(action);
//        public static void SetActiveZone(bool isActive) => _parent.SetActive(isActive);
//    }
//}