using Assets.Scripts.Abstractions.Enums;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.ECS.Components.View.UI.Game.General.Right
{
    internal struct CondUnitUICom
    {
        private TextMeshProUGUI _conditionZone_TextMP;
        private Dictionary<CondUnitTypes, Button> _condition_Buttons;
        private Dictionary<CondUnitTypes, TextMeshProUGUI> _condition_TextMP;

        internal CondUnitUICom(Transform condZone_Transform)
        {
            _conditionZone_TextMP = condZone_Transform.Find("StandartAbility_TextMP").GetComponent<TextMeshProUGUI>();

            _condition_Buttons = new Dictionary<CondUnitTypes, Button>();
            _condition_Buttons.Add(CondUnitTypes.Protected, condZone_Transform.Find("StandartAbilityButton1").GetComponent<Button>());
            _condition_Buttons.Add(CondUnitTypes.Relaxed, condZone_Transform.Find("StandartAbilityButton2").GetComponent<Button>());

            _condition_TextMP = new Dictionary<CondUnitTypes, TextMeshProUGUI>();
            _condition_TextMP.Add(CondUnitTypes.Protected, _condition_Buttons[CondUnitTypes.Protected].transform.Find("Defend_TextMP").GetComponent<TextMeshProUGUI>());
            _condition_TextMP.Add(CondUnitTypes.Relaxed, _condition_Buttons[CondUnitTypes.Relaxed].transform.Find("Relax_TextMP").GetComponent<TextMeshProUGUI>());
        }

        internal void SetActiveInfo(bool isActive) => _conditionZone_TextMP.gameObject.SetActive(isActive);
        internal void SetActive(CondUnitTypes condUnitType, bool isActive) => _condition_Buttons[condUnitType].gameObject.SetActive(isActive);

        internal void SetText_Info(string text) => _conditionZone_TextMP.text = text;
        internal void SetText_Button(CondUnitTypes conditionUnitType, string text) => _condition_TextMP[conditionUnitType].text = text;

        internal void SetColor(CondUnitTypes conditionUnitType, Color color) => _condition_Buttons[conditionUnitType].image.color = color;
        internal void AddListener(CondUnitTypes conditionUnitType, UnityAction unityAction) => _condition_Buttons[conditionUnitType].onClick.AddListener(unityAction);
    }
}
