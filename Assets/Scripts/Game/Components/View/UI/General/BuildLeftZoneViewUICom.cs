﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.Game
{
    public struct BuildLeftZoneViewUICom
    {
        private static Button _melt_Button;

        private static Dictionary<BuildTypes, Button> _upgradeBuild_Buttons;

        public BuildLeftZoneViewUICom(GameObject leftZone_GO)
        {
            var buildingZone_GO = leftZone_GO.transform.Find("BuildingZone").gameObject;

            _melt_Button = buildingZone_GO.transform.Find("MeltOreButton").GetComponent<Button>();

            _upgradeBuild_Buttons = new Dictionary<BuildTypes, Button>();
            _upgradeBuild_Buttons.Add(BuildTypes.Farm, buildingZone_GO.transform.Find("UpgradeFarm_Button").GetComponent<Button>());
            _upgradeBuild_Buttons.Add(BuildTypes.Woodcutter, buildingZone_GO.transform.Find("UpgradeWoodcutter_Button").GetComponent<Button>());
            _upgradeBuild_Buttons.Add(BuildTypes.Mine, buildingZone_GO.transform.Find("UpgradeMine_Button").GetComponent<Button>());
        }

        public static void SetActiveZone(bool isActive) => _melt_Button.transform.parent.gameObject.SetActive(isActive);
        public static void AddListenerToMelt(UnityAction unityAction) => _melt_Button.onClick.AddListener(unityAction);
        public static void AddListBuildUpgrade(BuildTypes buildingType, UnityAction unityAction) => _upgradeBuild_Buttons[buildingType].onClick.AddListener(unityAction);
    }
}
