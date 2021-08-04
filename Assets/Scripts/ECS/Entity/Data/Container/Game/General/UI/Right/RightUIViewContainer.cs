using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.System.View.Game.General.UI;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Workers.Game.UI
{
    internal sealed class RightUIViewContainer
    {
        private static SysViewGameGeneralUIManager EGGUIM => Main.Instance.ECSmanager.SysViewGameGeneralUIManager;

        #region Parent

        internal static void SetActiveRightZoneGO(bool isActive) => EGGUIM.RightZoneEnt_ParentCom.ParentGO.SetActive(isActive);

        private static GameObject GetParentZoneGO(UnitUIZoneTypes unitUIZoneType)
        {
            switch (unitUIZoneType)
            {
                case UnitUIZoneTypes.None:
                    throw new Exception();

                case UnitUIZoneTypes.Stats:
                    return EGGUIM.StatsEnt_ParentCom.ParentGO;

                case UnitUIZoneTypes.Condition:
                    return EGGUIM.ConditionZoneEnt_ParentGOCom.ParentGO;

                case UnitUIZoneTypes.Unique:
                    return EGGUIM.UniquePareZoneEnt_ParentCom.ParentGO;

                case UnitUIZoneTypes.Building:
                    return EGGUIM.BuildingAbilitiesZoneEnt_ParentCom.ParentGO;

                default:
                    throw new Exception();
            }
        }
        internal static void SetActiveParentZone(bool isActive, UnitUIZoneTypes unitUIZoneType) => GetParentZoneGO(unitUIZoneType).SetActive(isActive);

        private static TextMeshProUGUI GetTMPZoneGO(UnitUIZoneTypes unitUIZoneType)
        {
            switch (unitUIZoneType)
            {
                case UnitUIZoneTypes.None:
                    throw new Exception();

                case UnitUIZoneTypes.Condition:
                    return EGGUIM.ConditionZoneEnt_TextMeshProUGUICom.TextMeshProUGUI;

                case UnitUIZoneTypes.Unique:
                    throw new Exception();

                case UnitUIZoneTypes.Building:
                    throw new Exception();

                default:
                    throw new Exception();
            }
        }
        internal static void SetTextParentZone(string text, UnitUIZoneTypes unitUIZoneType) => GetTMPZoneGO(unitUIZoneType).text = text;

        #endregion


        #region Stats

        private static TextMeshProUGUI GetStatTMP(StatUITypes statUIType)
        {
            switch (statUIType)
            {
                case StatUITypes.None:
                    throw new Exception();

                case StatUITypes.Health:
                    return EGGUIM.HealthUIEnt_TextMeshProUGUICom.TextMeshProUGUI;

                case StatUITypes.Damage:
                    return EGGUIM.DamageUIEnt_TextMeshProUGUICom.TextMeshProUGUI;

                case StatUITypes.Protiction:
                    return EGGUIM.PowerProtectionUIEnt_TextMeshProUGUICom.TextMeshProUGUI;

                case StatUITypes.Steps:
                    return EGGUIM.AmountStepsUIEnt_TextMeshProUGUICom.TextMeshProUGUI;

                default:
                    throw new Exception();
            }
        }

        internal static void SetStatText(StatUITypes statUIType, string text) => GetStatTMP(statUIType).text = text;

        #endregion


        #region Condition

        private static Button GetConditionButton(ConditionUnitTypes conditionType)
        {
            switch (conditionType)
            {
                case ConditionUnitTypes.None:
                    throw new Exception();

                case ConditionUnitTypes.Protected:
                    return EGGUIM.ProtectConditionEnt_ButtonCom.Button;

                case ConditionUnitTypes.Relaxed:
                    return EGGUIM.RelaxConditionEnt_ButtonCom.Button;

                default:
                    throw new Exception();
            }
        }
        internal static void SetConditionColor(ConditionUnitTypes conditionType, Color color) => GetConditionButton(conditionType).image.color = color;
        internal static void SetActiveConditionButton(bool isActive, ConditionUnitTypes conditionType) => GetConditionButton(conditionType).gameObject.SetActive(isActive);

        #endregion


        #region Unique

        private static Button GetUniqueButton(UniqueAbilitiesTypes uniqueAbilitiesType)
        {
            switch (uniqueAbilitiesType)
            {
                case UniqueAbilitiesTypes.None:
                    throw new Exception();

                case UniqueAbilitiesTypes.First:
                    return EGGUIM.Unique1AbilityEnt_ButtonCom.Button;

                case UniqueAbilitiesTypes.Second:
                    return EGGUIM.Unique2AbilityEnt_ButtonCom.Button;

                case UniqueAbilitiesTypes.Third:
                    return EGGUIM.Unique3AbilityEnt_ButtonCom.Button;

                default:
                    throw new Exception();
            }
        }
        private static TextMeshProUGUI GetUniqueButtonTMP(UniqueAbilitiesTypes uniqueAbilitiesType)
        {
            switch (uniqueAbilitiesType)
            {
                case UniqueAbilitiesTypes.None:
                    throw new Exception();

                case UniqueAbilitiesTypes.First:
                    return EGGUIM.Unique1AbilityEnt_TextMeshProGUICom.TextMeshProUGUI;

                case UniqueAbilitiesTypes.Second:
                    return EGGUIM.Unique2AbilityEnt_TextMeshProGUICom.TextMeshProUGUI;

                case UniqueAbilitiesTypes.Third:
                    return EGGUIM.Unique3AbilityEnt_TextMeshProGUICom.TextMeshProUGUI;

                default:
                    throw new Exception();
            }
        }

        internal static void AddListenerUniqueButton(UnityAction unityAction, UniqueAbilitiesTypes uniqueAbilitiesType) => GetUniqueButton(uniqueAbilitiesType).onClick.AddListener(unityAction);
        internal static void SetActiveUniqueButton(bool isActive, UniqueAbilitiesTypes uniqueAbilitiesType) => GetUniqueButton(uniqueAbilitiesType).gameObject.SetActive(isActive);
        internal static void RemoveAllListenersUniqueButton(UniqueAbilitiesTypes uniqueAbilitiesType) => GetUniqueButton(uniqueAbilitiesType).onClick.RemoveAllListeners();
        internal static void SetUniqueButtonText(UniqueAbilitiesTypes uniqueAbilitiesType, string text) => GetUniqueButtonTMP(uniqueAbilitiesType).text = text;

        #endregion


        #region Building

        private static Button GetBuildingButton(BuildingButtonTypes buildingButtonType)
        {
            switch (buildingButtonType)
            {
                case BuildingButtonTypes.None:
                    throw new Exception();

                case BuildingButtonTypes.First:
                    return EGGUIM.BuildingFirstAbilityEnt_ButtonCom.Button;

                case BuildingButtonTypes.Second:
                    return EGGUIM.BuildingSecondAbilityEnt_ButtonCom.Button;

                case BuildingButtonTypes.Third:
                    return EGGUIM.BuildingThirdAbilityEnt_ButtonCom.Button;

                default:
                    throw new Exception();
            }
        }
        private static TextMeshProUGUI GetTMPBuildButton(BuildingButtonTypes buildingButtonType)
        {
            switch (buildingButtonType)
            {
                case BuildingButtonTypes.None:
                    throw new Exception();

                case BuildingButtonTypes.First:
                    return EGGUIM.BuildingFirstAbilityEnt_TextMeshProGUICom.TextMeshProUGUI;

                case BuildingButtonTypes.Second:
                    return EGGUIM.BuildingSecondAbilityEnt_TextMeshProGUICom.TextMeshProUGUI;

                case BuildingButtonTypes.Third:
                    return EGGUIM.BuildingThirdAbilityEnt_TextMeshProGUICom.TextMeshProUGUI;

                default:
                    throw new Exception();
            }
        }

        internal static void SetActiveBuildingButton(bool isActive, BuildingButtonTypes buildingButtonType)
            => GetBuildingButton(buildingButtonType).gameObject.SetActive(isActive);
        internal static void RemoveAllListenersBuildButton(BuildingButtonTypes buildingButtonType) => GetBuildingButton(buildingButtonType).onClick.RemoveAllListeners();
        internal static void AddListenerBuildButton(UnityAction unityAction, BuildingButtonTypes buildingButtonType) => GetBuildingButton(buildingButtonType).onClick.AddListener(unityAction);
        internal static void SetTextBuildButton(BuildingButtonTypes buildingButtonType, string text) => GetTMPBuildButton(buildingButtonType).text = text;

        #endregion
    }
}
