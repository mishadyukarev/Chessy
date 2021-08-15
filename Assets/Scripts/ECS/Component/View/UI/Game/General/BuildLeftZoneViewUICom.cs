using Assets.Scripts.Abstractions.Enums.Cell;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.ECS.Component.View.UI.Game.General
{
    internal struct BuildLeftZoneViewUICom
    {
        private Button _melt_Button;
        private Button _takePawnExtraTool_Button;
        private Dictionary<PawnExtraToolTypes, Button> _giveTool_Buttons;
        private Button _giveSwordWeapon_Button;
        private Dictionary<BuildingTypes, Button> _upgradeBuild_Buttons;

        internal BuildLeftZoneViewUICom(GameObject leftZone_GO)
        {
            var buildingZone_GO = leftZone_GO.transform.Find("BuildingZone").gameObject;

            _melt_Button = buildingZone_GO.transform.Find("MeltOreButton").GetComponent<Button>();

            _takePawnExtraTool_Button = buildingZone_GO.transform.Find("TakeTool_Button").GetComponent<Button>();

            _giveTool_Buttons = new Dictionary<PawnExtraToolTypes, Button>();
            //_giveTool_Buttons.Add(PawnToolTypes.Hoe, buildingZone_GO.transform.Find("GiveHoe_Button").GetComponent<Button>());
            _giveTool_Buttons.Add(PawnExtraToolTypes.Pick, buildingZone_GO.transform.Find("GivePick_Button").GetComponent<Button>());
            _giveSwordWeapon_Button = buildingZone_GO.transform.Find("GiveSword_Button").GetComponent<Button>();

            _upgradeBuild_Buttons = new Dictionary<BuildingTypes, Button>();
            _upgradeBuild_Buttons.Add(BuildingTypes.Farm, buildingZone_GO.transform.Find("UpgradeFarm_Button").GetComponent<Button>());
            _upgradeBuild_Buttons.Add(BuildingTypes.Woodcutter, buildingZone_GO.transform.Find("UpgradeWoodcutter_Button").GetComponent<Button>());
            _upgradeBuild_Buttons.Add(BuildingTypes.Mine, buildingZone_GO.transform.Find("UpgradeMine_Button").GetComponent<Button>());
        }


        internal void SetActiveZone(bool isActive) => _melt_Button.transform.parent.gameObject.SetActive(isActive);
        internal void AddListenerToMelt(UnityAction unityAction) => _melt_Button.onClick.AddListener(unityAction);
        internal void AddListenerToTakePawnTool(UnityAction unityAction) => _takePawnExtraTool_Button.onClick.AddListener(unityAction);
        internal void AddListenerToBuildUpgrade(BuildingTypes buildingType, UnityAction unityAction) => _upgradeBuild_Buttons[buildingType].onClick.AddListener(unityAction);

        internal void AddListenerToGiveTool(PawnExtraToolTypes secondToolType, UnityAction unityAction)
        {
            if (secondToolType == PawnExtraToolTypes.Hoe) throw new Exception();

            _giveTool_Buttons[secondToolType].onClick.AddListener(unityAction);
        }
        internal void AddListenerToGiveWeapon(UnityAction unityAction) => _giveSwordWeapon_Button.onClick.AddListener(unityAction);
    }
}
