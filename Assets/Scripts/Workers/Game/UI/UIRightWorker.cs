using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers.UI;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Workers.Game.UI
{
    internal sealed class UIRightWorker : MainGeneralUIWorker
    {
        private static GameObject GetParentConditionZoneGO(UnitUIZoneTypes unitUIZoneType)
        {
            switch (unitUIZoneType)
            {
                case UnitUIZoneTypes.None:
                    throw new Exception();

                case UnitUIZoneTypes.Condition:
                    return EGGUIM.ConditionZoneEnt_ParentGOCom.ParentGO;

                case UnitUIZoneTypes.Unique:
                    return EGGUIM.UniquePareZoneEnt_ParentCom.ParentGO;

                case UnitUIZoneTypes.Building:
                    throw new Exception();

                default:
                    throw new Exception();
            }
        }
        internal static void SetActiveParentZone(bool isActive, UnitUIZoneTypes unitUIZoneType) => GetParentConditionZoneGO(unitUIZoneType).SetActive(isActive);

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
        internal static void SetActive(bool isActive, ConditionUnitTypes conditionType) => GetConditionButton(conditionType).gameObject.SetActive(isActive);

        internal static Button GetUniqueButton(UniqueAbilitiesTypes uniqueAbilitiesType)
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
        internal static void AddListener(UnityAction unityAction, UniqueAbilitiesTypes uniqueAbilitiesType) => GetUniqueButton(uniqueAbilitiesType).onClick.AddListener(unityAction);
    }
}
