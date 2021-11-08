using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Chessy.Game
{
    public struct CutyLeftZoneViewUIC
    {
        private static Button _melt_Button;

        private static Dictionary<ResTypes, Button> _buy_Buts;

        public CutyLeftZoneViewUIC(GameObject leftZone_GO)
        {
            var buildingZone_GO = leftZone_GO.transform.Find("BuildingZone").gameObject;

            _melt_Button = buildingZone_GO.transform.Find("MeltOreButton").GetComponent<Button>();

            _buy_Buts = new Dictionary<ResTypes, Button>();
            _buy_Buts.Add(ResTypes.Food, buildingZone_GO.transform.Find("UpgradeFarm_Button").GetComponent<Button>());
            _buy_Buts.Add(ResTypes.Wood, buildingZone_GO.transform.Find("UpgradeWoodcutter_Button").GetComponent<Button>());
        }

        public static void SetActiveZone(bool isActive) => _melt_Button.transform.parent.gameObject.SetActive(isActive);
        public static void AddListenerToMelt(UnityAction unityAction) => _melt_Button.onClick.AddListener(unityAction);
        public static void AddListToBuyRes(ResTypes res, UnityAction unityAction)
        {
            if (!_buy_Buts.ContainsKey(res)) throw new Exception();

            _buy_Buts[res].onClick.AddListener(unityAction);
        }
    }
}
