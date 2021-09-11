using Assets.Scripts.Abstractions.Enums;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.ECS.Component.View.UI.Game.General
{
    internal struct UnitZoneViewUICom
    {
        private GameObject _parentZone_GO;

        private Dictionary<UnitUIZoneTypes, GameObject> _unitZone_GOs;

        private Dictionary<StatTypes, TextMeshProUGUI> _stat_TextMP;

        private TextMeshProUGUI _conditionZone_TextMP;
        private Dictionary<ConditionUnitTypes, Button> _condition_Buttons;
        private Dictionary<ConditionUnitTypes, TextMeshProUGUI> _condition_TextMP;

        private TextMeshProUGUI _uniqueAbilitiesZone_TextMP;
        private Dictionary<UniqueButtonTypes, Button> _uniqueAbilit_Buttons;
        private Dictionary<UniqueButtonTypes, TextMeshProUGUI> _uniqueAbilit_TextMPs;

        private TextMeshProUGUI _buildingZone_TextMP;
        private Dictionary<BuildingButtonTypes, Button> _building_Buttons;
        private Dictionary<BuildingButtonTypes, TextMeshProUGUI> _building_TextMPs;


        internal UnitZoneViewUICom(GameObject rightZone_GO)
        {
            _parentZone_GO = rightZone_GO;


            _unitZone_GOs = new Dictionary<UnitUIZoneTypes, GameObject>();


            _unitZone_GOs.Add(UnitUIZoneTypes.Stats, rightZone_GO.transform.Find("StatsZone").gameObject);

            _stat_TextMP = new Dictionary<StatTypes, TextMeshProUGUI>();


            _stat_TextMP[StatTypes.Health] = _unitZone_GOs[UnitUIZoneTypes.Stats].transform.Find("HpCurrentUnitText").GetComponent<TextMeshProUGUI>();
            _stat_TextMP[StatTypes.Damage] = _unitZone_GOs[UnitUIZoneTypes.Stats].transform.Find("DamageCurrentUnitText").GetComponent<TextMeshProUGUI>();
            _stat_TextMP[StatTypes.Protection] = _unitZone_GOs[UnitUIZoneTypes.Stats].transform.Find("ProtectionCurrentUnitText").GetComponent<TextMeshProUGUI>();
            _stat_TextMP[StatTypes.Steps] = _unitZone_GOs[UnitUIZoneTypes.Stats].transform.Find("StepsCurrentUnitText").GetComponent<TextMeshProUGUI>();




            _unitZone_GOs.Add(UnitUIZoneTypes.Condition, rightZone_GO.transform.Find("ConditionZone").gameObject);

            _conditionZone_TextMP = _unitZone_GOs[UnitUIZoneTypes.Condition].transform.Find("StandartAbilityText").GetComponent<TextMeshProUGUI>();

            _condition_Buttons = new Dictionary<ConditionUnitTypes, Button>();
            _condition_Buttons.Add(ConditionUnitTypes.Protected, _unitZone_GOs[UnitUIZoneTypes.Condition].transform.Find("StandartAbilityButton1").GetComponent<Button>());
            _condition_Buttons.Add(ConditionUnitTypes.Relaxed, _unitZone_GOs[UnitUIZoneTypes.Condition].transform.Find("StandartAbilityButton2").GetComponent<Button>());

            _condition_TextMP = new Dictionary<ConditionUnitTypes, TextMeshProUGUI>();
            _condition_TextMP.Add(ConditionUnitTypes.Protected, _condition_Buttons[ConditionUnitTypes.Protected].transform.Find("Defend_TextMP").GetComponent<TextMeshProUGUI>());
            _condition_TextMP.Add(ConditionUnitTypes.Relaxed, _condition_Buttons[ConditionUnitTypes.Relaxed].transform.Find("Relax_TextMP").GetComponent<TextMeshProUGUI>());



            _unitZone_GOs.Add(UnitUIZoneTypes.Unique, rightZone_GO.transform.Find("UniqueAbilitiesZone").gameObject);

            _uniqueAbilitiesZone_TextMP = _unitZone_GOs[UnitUIZoneTypes.Unique].transform.Find("UniqueAbilitiesText").GetComponent<TextMeshProUGUI>();

            _uniqueAbilit_Buttons = new Dictionary<UniqueButtonTypes, Button>();
            _uniqueAbilit_TextMPs = new Dictionary<UniqueButtonTypes, TextMeshProUGUI>();

            var uniqueAbilityButton1 = _unitZone_GOs[UnitUIZoneTypes.Unique].transform.Find("UniqueAbilityButton1").GetComponent<Button>();
            _uniqueAbilit_Buttons.Add(UniqueButtonTypes.First, uniqueAbilityButton1);
            _uniqueAbilit_TextMPs.Add(UniqueButtonTypes.First, uniqueAbilityButton1.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>());

            var uniqueAbilityButton2 = _unitZone_GOs[UnitUIZoneTypes.Unique].transform.Find("UniqueAbilityButton2").GetComponent<Button>();
            _uniqueAbilit_Buttons.Add(UniqueButtonTypes.Second, uniqueAbilityButton2);
            _uniqueAbilit_TextMPs.Add(UniqueButtonTypes.Second, uniqueAbilityButton2.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>());

            var uniqueAbilityButton3 = _unitZone_GOs[UnitUIZoneTypes.Unique].transform.Find("UniqueAbilityButton3").GetComponent<Button>();
            _uniqueAbilit_Buttons.Add(UniqueButtonTypes.Third, uniqueAbilityButton3);
            _uniqueAbilit_TextMPs.Add(UniqueButtonTypes.Third, uniqueAbilityButton3.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>());



            _unitZone_GOs.Add(UnitUIZoneTypes.Building, rightZone_GO.transform.Find("BuildingZone").gameObject);

            _buildingZone_TextMP = _unitZone_GOs[UnitUIZoneTypes.Building].transform.Find("BuildingAbilitiesText").GetComponent<TextMeshProUGUI>();

            _building_TextMPs = new Dictionary<BuildingButtonTypes, TextMeshProUGUI>();
            _building_Buttons = new Dictionary<BuildingButtonTypes, Button>();

            var buildingFirstAbilityButtom = _unitZone_GOs[UnitUIZoneTypes.Building].transform.Find("BuildingAbilityButton1").GetComponent<Button>();
            _building_Buttons.Add(BuildingButtonTypes.First, buildingFirstAbilityButtom);
            _building_TextMPs.Add(BuildingButtonTypes.First, buildingFirstAbilityButtom.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>());

            _building_Buttons.Add(BuildingButtonTypes.Second, _unitZone_GOs[UnitUIZoneTypes.Building].transform.Find("BuildingAbilityButton2").GetComponent<Button>());

            var buildingThirdAbilityButtom = _unitZone_GOs[UnitUIZoneTypes.Building].transform.Find("BuildingAbilityButton3").GetComponent<Button>();
            _building_Buttons.Add(BuildingButtonTypes.Third, buildingThirdAbilityButtom);
            _building_TextMPs.Add(BuildingButtonTypes.Third, buildingThirdAbilityButtom.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>());
        }

        internal void SetActiveParentZone(bool isActive) => _parentZone_GO.SetActive(isActive);

        internal void SetActiveUnitZone(UnitUIZoneTypes unitUIZoneType, bool isActive) => _unitZone_GOs[unitUIZoneType].SetActive(isActive);

        internal void SetColorToConditionButton(ConditionUnitTypes conditionUnitType, Color color) => _condition_Buttons[conditionUnitType].image.color = color;
        internal void SetColoToUniqueAbilityButton(UniqueButtonTypes uniqueButtonType, Color color) => _uniqueAbilit_Buttons[uniqueButtonType].image.color = color;

        internal void SetTextToStat(StatTypes statType, string text) => _stat_TextMP[statType].text = text;
        internal void SetTextToCondition(ConditionUnitTypes conditionUnitType, string text) => _condition_TextMP[conditionUnitType].text = text;
        internal void SetTextToUnique(UniqueButtonTypes uniqueButtonType, string text) => _uniqueAbilit_TextMPs[uniqueButtonType].text = text;
        internal void SetTextBuildButton(BuildingButtonTypes buildingButtonType, string text) => _building_TextMPs[buildingButtonType].text = text;

        internal void AddListenerToCondtionButton(ConditionUnitTypes conditionUnitType, UnityAction unityAction) => _condition_Buttons[conditionUnitType].onClick.AddListener(unityAction);
        internal void AddListenerToUniqueButton(UniqueButtonTypes uniqueButtonType, UnityAction unityAction) => _uniqueAbilit_Buttons[uniqueButtonType].onClick.AddListener(unityAction);
        internal void AddListenerToBuildButton(BuildingButtonTypes buildingButtonType, UnityAction unityAction) => _building_Buttons[buildingButtonType].onClick.AddListener(unityAction);

        internal void RemoveAllListenersInUniqueButton(UniqueButtonTypes uniqueButtonType) => _uniqueAbilit_Buttons[uniqueButtonType].onClick.RemoveAllListeners();
        internal void RemoveAllListenersInBuildButton(BuildingButtonTypes buildingButtonType) => _building_Buttons[buildingButtonType].onClick.RemoveAllListeners();


        internal void SetActiveUniqeButton(UniqueButtonTypes uniqueButtonType, bool isActive) => _uniqueAbilit_Buttons[uniqueButtonType].gameObject.SetActive(isActive);
        internal void SetActiveBuilButton(BuildingButtonTypes buildingButtonType, bool isActive) => _building_Buttons[buildingButtonType].gameObject.SetActive(isActive);
    }
}
