using ECS;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public struct UIEntRightStats
    {
        static readonly Dictionary<UnitStatTypes, Entity> _stats;

        public static ref C Stat<C>(in UnitStatTypes stat) where C : struct => ref _stats[stat].Get<C>();

        static UIEntRightStats()
        {
            _stats = new Dictionary<UnitStatTypes, Entity>();
            for (var stat = UnitStatTypes.Start; stat < UnitStatTypes.End; stat++) _stats.Add(stat, default);
        }
        public UIEntRightStats(in EcsWorld gameW, in GameObject rightZone)
        {
            var statZone = rightZone.transform.Find("StatsZone").gameObject;


            var zone = statZone.transform.Find("HpZone");
            _stats[UnitStatTypes.Hp] = gameW.NewEntity()
                .Add(new ImageUIC(zone.Find("Bar_Image").GetComponent<Image>()))
                .Add(new TextUIC(zone.Find("HpCurrentUnit_TextMP").GetComponent<TextMeshProUGUI>()));


            zone = statZone.transform.Find("Damage");
            _stats[UnitStatTypes.Damage] = gameW.NewEntity()
                .Add(new ImageUIC(zone.transform.Find("PowerDamage_Image").GetComponent<Image>()))
                .Add(new TextUIC(zone.transform.Find("DamageCurrentUnit_TextMP").GetComponent<TextMeshProUGUI>()));


            zone = statZone.transform.Find("Steps");
            _stats[UnitStatTypes.Steps] = gameW.NewEntity()
                .Add(new ImageUIC(zone.transform.Find("Steps_Image").GetComponent<Image>()))
                .Add(new TextUIC(zone.transform.Find("StepsCurrentUnit_TextMP").GetComponent<TextMeshProUGUI>()));


            zone = statZone.transform.Find("Water");
            _stats[UnitStatTypes.Water] = gameW.NewEntity()
                .Add(new ImageUIC(zone.transform.Find("Water_Image").GetComponent<Image>()))
                .Add(new TextUIC(zone.transform.Find("Water_TMP").GetComponent<TextMeshProUGUI>()));
        }
    }
}
