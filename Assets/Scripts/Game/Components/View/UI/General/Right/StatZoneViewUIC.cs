using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Scripts.Game
{
    internal struct StatZoneViewUIC
    {
        private static GameObject _parentZone_GO;
        private static GameObject _statZone_GO;
        private static Dictionary<StatTypes, TextMeshProUGUI> _stat_TextMP;

        internal StatZoneViewUIC(GameObject rightZone_GO)
        {
            _parentZone_GO = rightZone_GO;

            _statZone_GO = rightZone_GO.transform.Find("StatsZone").gameObject;

            _stat_TextMP = new Dictionary<StatTypes, TextMeshProUGUI>();
            _stat_TextMP[StatTypes.Health] = _statZone_GO.transform.Find("HpZone").Find("HpCurrentUnit_TextMP").GetComponent<TextMeshProUGUI>();
            _stat_TextMP[StatTypes.Damage] = _statZone_GO.transform.Find("DamageCurrentUnit_TextMP").GetComponent<TextMeshProUGUI>();
            _stat_TextMP[StatTypes.Steps] = _statZone_GO.transform.Find("StepsCurrentUnit_TextMP").GetComponent<TextMeshProUGUI>();
        }

        internal static void SetActiveParentZone(bool isActive) => _parentZone_GO.SetActive(isActive);
        internal static void SetActiveStatZone(bool isActive) => _statZone_GO.SetActive(isActive);

        internal static void SetTextToStat(StatTypes statType, string text) => _stat_TextMP[statType].text = text;
    }
}
