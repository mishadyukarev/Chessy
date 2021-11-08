using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Chessy.Game
{
    public struct CondUnitUIC
    {
        private static Dictionary<CondUnitTypes, Button> _condition_Buttons;

        public CondUnitUIC(Transform condZone_Transform)
        {
            _condition_Buttons = new Dictionary<CondUnitTypes, Button>();
            _condition_Buttons.Add(CondUnitTypes.Protected, condZone_Transform.Find("StandartAbilityButton1").GetComponent<Button>());
            _condition_Buttons.Add(CondUnitTypes.Relaxed, condZone_Transform.Find("StandartAbilityButton2").GetComponent<Button>());
        }

        public static void SetActive(CondUnitTypes condUnitType, bool isActive) => _condition_Buttons[condUnitType].gameObject.SetActive(isActive);

        public static void SetColor(CondUnitTypes conditionUnitType, Color color) => _condition_Buttons[conditionUnitType].transform.Find("Image").GetComponent<Image>().color = color;
        public static void AddListener(CondUnitTypes conditionUnitType, UnityAction unityAction) => _condition_Buttons[conditionUnitType].onClick.AddListener(unityAction);
    }
}
