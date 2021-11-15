using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Chessy.Game
{
    public struct ProtectUIC
    {
        private static Button _button;
        private static Dictionary<UnitTypes, GameObject> _zones;

        public ProtectUIC(Transform condZone)
        {
            _button = condZone.Find("StandartAbilityButton1").GetComponent<Button>();
            _zones = new Dictionary<UnitTypes, GameObject>();

            for (var unit = UnitTypes.First; unit < UnitTypes.End; unit++)
            {
                _zones.Add(unit, _button.transform.Find(unit.ToString()).gameObject);
            }
        }

        public static void SetActiveButton(bool isActive) => _button.gameObject.SetActive(isActive);
        public static void SetZone(UnitTypes unitType)
        {
            foreach (var item in _zones) _zones[item.Key].SetActive(false);

            _zones[unitType].SetActive(true);
        }

        public static void SetColor(Color color) => _button.transform.Find("Image").GetComponent<Image>().color = color;
        public static void AddListener(UnityAction action) => _button.onClick.AddListener(action);
    }
}