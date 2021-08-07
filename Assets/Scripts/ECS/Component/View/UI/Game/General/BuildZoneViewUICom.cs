using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.ECS.Component.View.UI.Game.General
{
    internal struct BuildZoneViewUICom
    {
        private Dictionary<UnitTypes, Button> _createUnit_Buttons;
        private Button _melt_Button;
        private Button _upgradeUnits_Button;
        private Dictionary<BuildingTypes, Button> _upgradeBuild_Buttons;

        internal BuildZoneViewUICom(GameObject leftZone_GO)
        {
            var buildingZone_GO = leftZone_GO.transform.Find("BuildingZone").gameObject;

            _createUnit_Buttons = new Dictionary<UnitTypes, Button>();
            _createUnit_Buttons.Add(UnitTypes.Pawn, buildingZone_GO.transform.Find("BuyPawnButton").GetComponent<Button>());
            _createUnit_Buttons.Add(UnitTypes.Rook, buildingZone_GO.transform.Find("BuyRookButton").GetComponent<Button>());
            _createUnit_Buttons.Add(UnitTypes.Bishop, buildingZone_GO.transform.Find("BuyBishopButton").GetComponent<Button>());

            _melt_Button = buildingZone_GO.transform.Find("MeltOreButton").GetComponent<Button>();
            _upgradeUnits_Button = buildingZone_GO.transform.Find("UpgradeUnitButton").GetComponent<Button>();

            _upgradeBuild_Buttons = new Dictionary<BuildingTypes, Button>();
            _upgradeBuild_Buttons.Add(BuildingTypes.Farm, buildingZone_GO.transform.Find("UpgradeFarmButton").GetComponent<Button>());
            _upgradeBuild_Buttons.Add(BuildingTypes.Woodcutter, buildingZone_GO.transform.Find("UpgradeWoodcutterButton").GetComponent<Button>());
            _upgradeBuild_Buttons.Add(BuildingTypes.Mine, buildingZone_GO.transform.Find("UpgradeMineButton").GetComponent<Button>());
        }


        internal void SetActiveZone(bool isActive) => _melt_Button.transform.parent.gameObject.SetActive(isActive);
        internal void AddListenerToCreateUnit(UnitTypes unitType, UnityAction unityAction) => _createUnit_Buttons[unitType].onClick.AddListener(unityAction);
        internal void AddListenerToMelt(UnityAction unityAction) => _melt_Button.onClick.AddListener(unityAction);
        internal void AddListenerToUpgradeUnits(UnityAction unityAction) => _upgradeUnits_Button.onClick.AddListener(unityAction);
        internal void AddListenerToBuildUpgrade(BuildingTypes buildingType, UnityAction unityAction) => _upgradeBuild_Buttons[buildingType].onClick.AddListener(unityAction);
    }
}
