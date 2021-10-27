using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.Game
{
    internal struct CondUnitUIC
    {
        private static Dictionary<CondUnitTypes, Button> _condition_Buttons;

        internal CondUnitUIC(Transform condZone_Transform)
        {
            _condition_Buttons = new Dictionary<CondUnitTypes, Button>();
            _condition_Buttons.Add(CondUnitTypes.Protected, condZone_Transform.Find("StandartAbilityButton1").GetComponent<Button>());
            _condition_Buttons.Add(CondUnitTypes.Relaxed, condZone_Transform.Find("StandartAbilityButton2").GetComponent<Button>());
        }

        internal static void SetActive(CondUnitTypes condUnitType, bool isActive) => _condition_Buttons[condUnitType].gameObject.SetActive(isActive);

        internal static void SetColor(CondUnitTypes conditionUnitType, Color color) => _condition_Buttons[conditionUnitType].transform.Find("Image").GetComponent<Image>().color = color;
        internal static void AddListener(CondUnitTypes conditionUnitType, UnityAction unityAction) => _condition_Buttons[conditionUnitType].onClick.AddListener(unityAction);
    }
}
