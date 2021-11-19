using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public struct StatUIC
    {
        private static GameObject _parentZone_GO;

        private static GameObject _statZone_GO;
        private static Dictionary<UnitStatTypes, Image> _stats_Images;
        private static Dictionary<UnitStatTypes, TextMeshProUGUI> _stat_TextMP;

        public StatUIC(GameObject rightZone_GO)
        {
            _parentZone_GO = rightZone_GO;

            _statZone_GO = rightZone_GO.transform.Find("StatsZone").gameObject;


            _stats_Images = new Dictionary<UnitStatTypes, Image>();
            var hpZone_Trans = _statZone_GO.transform.Find("HpZone");
            _stats_Images.Add(UnitStatTypes.Hp, hpZone_Trans.Find("Bar_Image").GetComponent<Image>());
            _stats_Images.Add(UnitStatTypes.Damage, _statZone_GO.transform.Find("PowerDamage_Image").GetComponent<Image>());
            _stats_Images.Add(UnitStatTypes.Steps, _statZone_GO.transform.Find("Steps_Image").GetComponent<Image>());
            _stats_Images.Add(UnitStatTypes.Water, _statZone_GO.transform.Find("Water_Image").GetComponent<Image>());

            _stat_TextMP = new Dictionary<UnitStatTypes, TextMeshProUGUI>();
            _stat_TextMP[UnitStatTypes.Hp] = hpZone_Trans.Find("HpCurrentUnit_TextMP").GetComponent<TextMeshProUGUI>();
            _stat_TextMP[UnitStatTypes.Damage] = _statZone_GO.transform.Find("DamageCurrentUnit_TextMP").GetComponent<TextMeshProUGUI>();
            _stat_TextMP[UnitStatTypes.Steps] = _statZone_GO.transform.Find("StepsCurrentUnit_TextMP").GetComponent<TextMeshProUGUI>();
            _stat_TextMP[UnitStatTypes.Water] = _statZone_GO.transform.Find("Water_TMP").GetComponent<TextMeshProUGUI>();
        }

        public static void SetActiveParentZone(bool isActive) => _parentZone_GO.SetActive(isActive);
        public static void SetActiveStatZone(bool isActive) => _statZone_GO.SetActive(isActive);

        public static void SetTextToStat(UnitStatTypes statType, string text) => _stat_TextMP[statType].text = text;

        public static void FillAmount(UnitStatTypes statType, int amount, int maxAmount)
        {
            _stats_Images[statType].fillAmount = (float)(amount * 100 / maxAmount / 100f);
        }
    }
}
