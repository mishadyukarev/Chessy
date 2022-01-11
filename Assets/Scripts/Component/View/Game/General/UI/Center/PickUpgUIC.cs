//using System;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Events;
//using UnityEngine.UI;

//namespace Game.Game
//{
//    public struct PickUpgUIC
//    {
//        private static GameObject _parent_GO;


//        public static bool IsActiveZone => _parent_GO.activeSelf;

//        public PickUpgUIC(Transform center_Trans)
//        {

//        }

//        public static void SetActiveZone(bool isActive) => _parent_GO.SetActive(isActive);

//        public static void SetActive(UnitTypes unit, bool isActive)
//        {
//            _units[unit].gameObject.SetActive(isActive);
//        }
//        public static void SetActive(BuildTypes build, bool isActive)
//        {
//            _builds[build].gameObject.SetActive(isActive);
//        }
//        public static void SetWater(bool isActive)
//        {
//            _upgWater.gameObject.SetActive(isActive);
//        }


//        public static void AddList(UnitTypes unit, UnityAction action)
//        {
//            if (!_units.ContainsKey(unit)) throw new Exception();

//            _units[unit].onClick.AddListener(action);
//        }
//        public static void AddList(BuildTypes build, UnityAction action)
//        {
//            if (!_builds.ContainsKey(build)) throw new Exception();

//            _builds[build].onClick.AddListener(action);
//        }
//        public static void AddListWater(UnityAction action)
//        {
//            _upgWater.onClick.AddListener(action);
//        }
//    }
//}