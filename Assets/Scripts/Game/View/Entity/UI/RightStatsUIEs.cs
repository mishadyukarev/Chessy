using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game
{
    public struct RightStatsUIEs
    {
        readonly Dictionary<UnitStatTypes, RightUnitStatUIE> _stats;

        public RightUnitStatUIE Stat(in UnitStatTypes stat) => _stats[stat];

        public RightStatsUIEs(in GameObject rightZone)
        {
            _stats = new Dictionary<UnitStatTypes, RightUnitStatUIE>();

            var statZone = rightZone.transform.Find("StatsZone").gameObject;


            var zone = statZone.transform.Find("HpZone");
            _stats.Add(UnitStatTypes.Hp, new RightUnitStatUIE(zone.Find("Bar_Image").GetComponent<Image>(),
                zone.Find("HpCurrentUnit_TextMP").GetComponent<TextMeshProUGUI>()));


            zone = statZone.transform.Find("Damage");
            _stats.Add(UnitStatTypes.Damage, new RightUnitStatUIE(zone.transform.Find("PowerDamage_Image").GetComponent<Image>(),
                zone.transform.Find("DamageCurrentUnit_TextMP").GetComponent<TextMeshProUGUI>()));


            zone = statZone.transform.Find("Steps");
            _stats.Add(UnitStatTypes.Steps, new RightUnitStatUIE(zone.transform.Find("Steps_Image").GetComponent<Image>(),
                zone.transform.Find("StepsCurrentUnit_TextMP").GetComponent<TextMeshProUGUI>()));


            zone = statZone.transform.Find("Water");
            _stats.Add(UnitStatTypes.Water, new RightUnitStatUIE(zone.transform.Find("Water_Image").GetComponent<Image>(),
                zone.transform.Find("Water_TMP").GetComponent<TextMeshProUGUI>()));
        }
    }
}
