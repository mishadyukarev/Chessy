//using Assets.Scripts.Abstractions.Enums;
//using Assets.Scripts.ECS.Game.General.Systems.StartFill;
//using System;
//using TMPro;
//using UnityEngine;
//using UnityEngine.Events;
//using UnityEngine.UI;

//namespace Assets.Scripts.Workers.Game.UI
//{
//    internal sealed class RightUIViewContainer
//    {

//        #region Parent

//        internal static void SetActiveRightZoneGO(bool isActive) => MainGameSystem.RightZoneEnt_ParentCom.ParentGO.SetActive(isActive);

//        private static GameObject GetParentZoneGO(UnitUIZoneTypes unitUIZoneType)
//        {
//            switch (unitUIZoneType)
//            {
//                case UnitUIZoneTypes.None:
//                    throw new Exception();

//                case UnitUIZoneTypes.Stats:
//                    return MainGameSystem.StatsEnt_ParentCom.ParentGO;

//                case UnitUIZoneTypes.Condition:
//                    return MainGameSystem.ConditionZoneEnt_ParentGOCom.ParentGO;

//                case UnitUIZoneTypes.Unique:
//                    return MainGameSystem.UniquePareZoneEnt_ParentCom.ParentGO;

//                case UnitUIZoneTypes.Building:
//                    return MainGameSystem.BuildingAbilitiesZoneEnt_ParentCom.ParentGO;

//                default:
//                    throw new Exception();
//            }
//        }
//        internal static void SetActiveParentZone(bool isActive, UnitUIZoneTypes unitUIZoneType) => GetParentZoneGO(unitUIZoneType).SetActive(isActive);

//        private static TextMeshProUGUI GetTMPZoneGO(UnitUIZoneTypes unitUIZoneType)
//        {
//            switch (unitUIZoneType)
//            {
//                case UnitUIZoneTypes.None:
//                    throw new Exception();

//                case UnitUIZoneTypes.Condition:
//                    return MainGameSystem.ConditionZoneEnt_TextMeshProUGUICom.TextMeshProUGUI;

//                case UnitUIZoneTypes.Unique:
//                    throw new Exception();

//                case UnitUIZoneTypes.Building:
//                    throw new Exception();

//                default:
//                    throw new Exception();
//            }
//        }
//        internal static void SetTextParentZone(string text, UnitUIZoneTypes unitUIZoneType) => GetTMPZoneGO(unitUIZoneType).text = text;

//        #endregion


//        #region Stats

//        private static TextMeshProUGUI GetStatTMP(StatUITypes statUIType)
//        {
//            switch (statUIType)
//            {
//                case StatUITypes.None:
//                    throw new Exception();

//                case StatUITypes.Health:
//                    return MainGameSystem.HealthUIEnt_TextMeshProUGUICom.TextMeshProUGUI;

//                case StatUITypes.Damage:
//                    return MainGameSystem.DamageUIEnt_TextMeshProUGUICom.TextMeshProUGUI;

//                case StatUITypes.Protiction:
//                    return MainGameSystem.PowerProtectionUIEnt_TextMeshProUGUICom.TextMeshProUGUI;

//                case StatUITypes.Steps:
//                    return MainGameSystem.AmountStepsUIEnt_TextMeshProUGUICom.TextMeshProUGUI;

//                default:
//                    throw new Exception();
//            }
//        }

//        internal static void SetStatText(StatUITypes statUIType, string text) => GetStatTMP(statUIType).text = text;

//        #endregion


//        #region Condition

//        private static Button GetConditionButton(ConditionUnitTypes conditionType)
//        {
//            switch (conditionType)
//            {
//                case ConditionUnitTypes.None:
//                    throw new Exception();

//                case ConditionUnitTypes.Protected:
//                    return MainGameSystem.ProtectConditionEnt_ButtonCom.Button;

//                case ConditionUnitTypes.Relaxed:
//                    return MainGameSystem.RelaxConditionEnt_ButtonCom.Button;

//                default:
//                    throw new Exception();
//            }
//        }
//        internal static void SetConditionColor(ConditionUnitTypes conditionType, Color color) => GetConditionButton(conditionType).image.color = color;
//        internal static void SetActiveConditionButton(bool isActive, ConditionUnitTypes conditionType) => GetConditionButton(conditionType).gameObject.SetActive(isActive);

//        #endregion


//        #region Unique

//        private static Button GetUniqueButton(UniqueButtonTypes uniqueAbilitiesType)
//        {
//            switch (uniqueAbilitiesType)
//            {
//                case UniqueButtonTypes.None:
//                    throw new Exception();

//                case UniqueButtonTypes.First:
//                    return MainGameSystem.Unique1AbilityEnt_ButtonCom.Button;

//                case UniqueButtonTypes.Second:
//                    return MainGameSystem.Unique2AbilityEnt_ButtonCom.Button;

//                case UniqueButtonTypes.Third:
//                    return MainGameSystem.Unique3AbilityEnt_ButtonCom.Button;

//                default:
//                    throw new Exception();
//            }
//        }
//        private static TextMeshProUGUI GetUniqueButtonTMP(UniqueButtonTypes uniqueAbilitiesType)
//        {
//            switch (uniqueAbilitiesType)
//            {
//                case UniqueButtonTypes.None:
//                    throw new Exception();

//                case UniqueButtonTypes.First:
//                    return MainGameSystem.Unique1AbilityEnt_TextMeshProGUICom.TextMeshProUGUI;

//                case UniqueButtonTypes.Second:
//                    return MainGameSystem.Unique2AbilityEnt_TextMeshProGUICom.TextMeshProUGUI;

//                case UniqueButtonTypes.Third:
//                    return MainGameSystem.Unique3AbilityEnt_TextMeshProGUICom.TextMeshProUGUI;

//                default:
//                    throw new Exception();
//            }
//        }

//        internal static void AddListenerUniqueButton(UnityAction unityAction, UniqueButtonTypes uniqueAbilitiesType) => GetUniqueButton(uniqueAbilitiesType).onClick.AddListener(unityAction);
//        internal static void SetActiveUniqueButton(bool isActive, UniqueButtonTypes uniqueAbilitiesType) => GetUniqueButton(uniqueAbilitiesType).gameObject.SetActive(isActive);
//        internal static void RemoveAllListenersUniqueButton(UniqueButtonTypes uniqueAbilitiesType) => GetUniqueButton(uniqueAbilitiesType).onClick.RemoveAllListeners();
//        internal static void SetUniqueButtonText(UniqueButtonTypes uniqueAbilitiesType, string text) => GetUniqueButtonTMP(uniqueAbilitiesType).text = text;

//        #endregion


//        #region Building

//        private static Button GetBuildingButton(BuildingButtonTypes buildingButtonType)
//        {
//            switch (buildingButtonType)
//            {
//                case BuildingButtonTypes.None:
//                    throw new Exception();

//                case BuildingButtonTypes.First:
//                    return MainGameSystem.BuildingFirstAbilityEnt_ButtonCom.Button;

//                case BuildingButtonTypes.Second:
//                    return MainGameSystem.BuildingSecondAbilityEnt_ButtonCom.Button;

//                case BuildingButtonTypes.Third:
//                    return MainGameSystem.BuildingThirdAbilityEnt_ButtonCom.Button;

//                default:
//                    throw new Exception();
//            }
//        }
//        private static TextMeshProUGUI GetTMPBuildButton(BuildingButtonTypes buildingButtonType)
//        {
//            switch (buildingButtonType)
//            {
//                case BuildingButtonTypes.None:
//                    throw new Exception();

//                case BuildingButtonTypes.First:
//                    return MainGameSystem.BuildingFirstAbilityEnt_TextMeshProGUICom.TextMeshProUGUI;

//                case BuildingButtonTypes.Second:
//                    return MainGameSystem.BuildingSecondAbilityEnt_TextMeshProGUICom.TextMeshProUGUI;

//                case BuildingButtonTypes.Third:
//                    return MainGameSystem.BuildingThirdAbilityEnt_TextMeshProGUICom.TextMeshProUGUI;

//                default:
//                    throw new Exception();
//            }
//        }

//        internal static void SetActiveBuildingButton(bool isActive, BuildingButtonTypes buildingButtonType)
//            => GetBuildingButton(buildingButtonType).gameObject.SetActive(isActive);
//        internal static void RemoveAllListenersBuildButton(BuildingButtonTypes buildingButtonType) => GetBuildingButton(buildingButtonType).onClick.RemoveAllListeners();
//        internal static void AddListenerBuildButton(UnityAction unityAction, BuildingButtonTypes buildingButtonType) => GetBuildingButton(buildingButtonType).onClick.AddListener(unityAction);
//        internal static void SetTextBuildButton(BuildingButtonTypes buildingButtonType, string text) => GetTMPBuildButton(buildingButtonType).text = text;

//        #endregion
//    }
//}
