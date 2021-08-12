﻿using Assets.Scripts.Abstractions.Enums.Cell;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.ECS.Component.View.UI.Game.General
{
    internal struct BuildZoneViewUICom
    {
        private Dictionary<SecondToolTypes, Button> _giveTool_Buttons;

        private Button _melt_Button;
        private Button _upgradeUnits_Button;

        private Dictionary<BuildingTypes, Button> _upgradeBuild_Buttons;

        internal BuildZoneViewUICom(GameObject leftZone_GO)
        {
            var buildingZone_GO = leftZone_GO.transform.Find("BuildingZone").gameObject;

            _giveTool_Buttons = new Dictionary<SecondToolTypes, Button>();

            _melt_Button = buildingZone_GO.transform.Find("MeltOreButton").GetComponent<Button>();
            _upgradeUnits_Button = buildingZone_GO.transform.Find("UpgradeUnit_Button").GetComponent<Button>();

            _upgradeBuild_Buttons = new Dictionary<BuildingTypes, Button>();
            _upgradeBuild_Buttons.Add(BuildingTypes.Farm, buildingZone_GO.transform.Find("UpgradeFarm_Button").GetComponent<Button>());
            _upgradeBuild_Buttons.Add(BuildingTypes.Woodcutter, buildingZone_GO.transform.Find("UpgradeWoodcutter_Button").GetComponent<Button>());
            _upgradeBuild_Buttons.Add(BuildingTypes.Mine, buildingZone_GO.transform.Find("UpgradeMine_Button").GetComponent<Button>());
        }


        internal void SetActiveZone(bool isActive) => _melt_Button.transform.parent.gameObject.SetActive(isActive);
        internal void AddListenerToMelt(UnityAction unityAction) => _melt_Button.onClick.AddListener(unityAction);
        internal void AddListenerToUpgradeUnits(UnityAction unityAction) => _upgradeUnits_Button.onClick.AddListener(unityAction);
        internal void AddListenerToBuildUpgrade(BuildingTypes buildingType, UnityAction unityAction) => _upgradeBuild_Buttons[buildingType].onClick.AddListener(unityAction);
    }
}