using Assets.Scripts.Abstractions.Enums;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.ECS.Component.View.UI.Game.General
{
    internal struct StatZoneViewUICom
    {
        private GameObject _parentZone_GO;

        private GameObject _statZone_GO;

        private Dictionary<StatTypes, TextMeshProUGUI> _stat_TextMP;

        internal StatZoneViewUICom(GameObject rightZone_GO)
        {
            _parentZone_GO = rightZone_GO;

            _statZone_GO = rightZone_GO.transform.Find("StatsZone").gameObject;

            _stat_TextMP = new Dictionary<StatTypes, TextMeshProUGUI>();
            _stat_TextMP[StatTypes.Health] = _statZone_GO.transform.Find("HpCurrentUnit_TextMP").GetComponent<TextMeshProUGUI>();
            _stat_TextMP[StatTypes.Damage] = _statZone_GO.transform.Find("DamageCurrentUnit_TextMP").GetComponent<TextMeshProUGUI>();
            _stat_TextMP[StatTypes.Protection] = _statZone_GO.transform.Find("ProtectionCurrentUnit_TextMP").GetComponent<TextMeshProUGUI>();
            _stat_TextMP[StatTypes.Steps] = _statZone_GO.transform.Find("StepsCurrentUnit_TextMP").GetComponent<TextMeshProUGUI>();
        }

        internal void SetActiveParentZone(bool isActive) => _parentZone_GO.SetActive(isActive);
        internal void SetActiveStatZone(bool isActive) => _statZone_GO.SetActive(isActive);

        internal void SetTextToStat(StatTypes statType, string text) => _stat_TextMP[statType].text = text;
    }
}
