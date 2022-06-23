using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Model
{
    public struct RightStatsUIEs
    {
        readonly Dictionary<UnitStatTypes, RightUnitStatUIE> _stats;


        public readonly EnergyUIE EnergyE;
        internal readonly DamageUIE DamageE;
        internal readonly DamageUIE WaterE;

        internal RightUnitStatUIE Stat(in UnitStatTypes stat) => _stats[stat];

        public RightStatsUIEs(in GameObject rightZone)
        {
            _stats = new Dictionary<UnitStatTypes, RightUnitStatUIE>();

            var statZone = rightZone.transform.Find("StatsZone").gameObject;






            var zone = statZone.transform.Find("HpZone");
            _stats.Add(UnitStatTypes.Hp, new RightUnitStatUIE(zone.Find("Bar_Image").GetComponent<Image>(),
                zone.Find("HpCurrentUnit_TextMP").GetComponent<TextMeshProUGUI>()));



            zone = statZone.transform.Find("Damage+");
            DamageE = new DamageUIE(new ImageUIC(zone.Find("PowerDamage_Image+").GetComponent<Image>()),
                new TextUIC(zone.Find("DamageCurrentUnit_TextMP+").GetComponent<TextMeshProUGUI>()),
                new ButtonUIC(zone.Find("Button+").GetComponent<Button>()));


            zone = statZone.transform.Find("Steps+");
            EnergyE = new EnergyUIE(new Common.AnimationVC(zone.GetComponent<Animation>()),
                new ImageUIC(zone.Find("Steps_Image+").GetComponent<Image>()),
                new TextUIC(zone.Find("StepsCurrentUnit_TextMP+").GetComponent<TextMeshProUGUI>()),
                new ButtonUIC(zone.Find("Button+").GetComponent<Button>()));


            zone = statZone.transform.Find("Water+");
            WaterE = new DamageUIE(new ImageUIC(zone.Find("Water_Image+").GetComponent<Image>()),
                new TextUIC(zone.Find("Water_TMP+").GetComponent<TextMeshProUGUI>()),
                new ButtonUIC(zone.Find("Button+").GetComponent<Button>()));
        }
    }
}
