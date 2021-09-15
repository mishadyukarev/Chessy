using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.ECS.Component.View.UI.Game.General
{
    internal struct BuildLeftZoneViewUICom
    {
        private Button _melt_Button;
        private TextMeshProUGUI _melt_TextMP;

        private Dictionary<BuildingTypes, Button> _upgradeBuild_Buttons;
        private Dictionary<BuildingTypes, TextMeshProUGUI> _upgrBuild_TextMPs;

        internal BuildLeftZoneViewUICom(GameObject leftZone_GO)
        {
            var buildingZone_GO = leftZone_GO.transform.Find("BuildingZone").gameObject;

            _melt_Button = buildingZone_GO.transform.Find("MeltOreButton").GetComponent<Button>();
            _melt_TextMP = _melt_Button.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();

            _upgradeBuild_Buttons = new Dictionary<BuildingTypes, Button>();
            _upgradeBuild_Buttons.Add(BuildingTypes.Farm, buildingZone_GO.transform.Find("UpgradeFarm_Button").GetComponent<Button>());
            _upgradeBuild_Buttons.Add(BuildingTypes.Woodcutter, buildingZone_GO.transform.Find("UpgradeWoodcutter_Button").GetComponent<Button>());
            _upgradeBuild_Buttons.Add(BuildingTypes.Mine, buildingZone_GO.transform.Find("UpgradeMine_Button").GetComponent<Button>());

            _upgrBuild_TextMPs = new Dictionary<BuildingTypes, TextMeshProUGUI>();
            _upgrBuild_TextMPs.Add(BuildingTypes.Farm, _upgradeBuild_Buttons[BuildingTypes.Farm].transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>());
            _upgrBuild_TextMPs.Add(BuildingTypes.Woodcutter, _upgradeBuild_Buttons[BuildingTypes.Woodcutter].transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>());
            _upgrBuild_TextMPs.Add(BuildingTypes.Mine, _upgradeBuild_Buttons[BuildingTypes.Mine].transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>());
        }

        internal void SetActiveZone(bool isActive) => _melt_Button.transform.parent.gameObject.SetActive(isActive);
        internal void AddListenerToMelt(UnityAction unityAction) => _melt_Button.onClick.AddListener(unityAction);
        internal void AddListenerToBuildUpgrade(BuildingTypes buildingType, UnityAction unityAction) => _upgradeBuild_Buttons[buildingType].onClick.AddListener(unityAction);

        internal void SetTextMelt(string text) => _melt_TextMP.text = text;
        internal void SetTextUpgrade(BuildingTypes buildingType, string text) => _upgrBuild_TextMPs[buildingType].text = text;
    }
}
