using ECS;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public readonly struct DownToolWeaponUIEs
    {
        static Dictionary<ToolWeaponTypes, Entity> _ents;
        static Dictionary<string, Entity> _toolWeapon;

        public static ref C Button<C>(in ToolWeaponTypes tw) where C : struct => ref _ents[tw].Get<C>();
        public static ref C Image<C>(in ToolWeaponTypes tw, in LevelTypes level) where C : struct => ref _toolWeapon[tw.ToString() + level].Get<C>();

        public DownToolWeaponUIEs(in EcsWorld gameW, in Transform downZone)
        {
            _ents = new Dictionary<ToolWeaponTypes, Entity>();
            _toolWeapon = new Dictionary<string, Entity>();

            var gTZone = downZone.Find("GiveTake");

            for (var tw = ToolWeaponTypes.Pick; tw < ToolWeaponTypes.End; tw++)
            {
                var zone = gTZone.Find(tw.ToString());

                var button = zone.Find(tw + "_Button").GetComponent<Button>();

                _ents.Add(tw, gameW.NewEntity()
                    .Add(new ButtonUIC(button))
                    .Add(new ImageUIC(button.GetComponent<Image>()))
                    .Add(new TextMPUGUIC(zone.Find("Amount_TextMP").GetComponent<TextMeshProUGUI>())));

                for (var level = LevelTypes.First; level < LevelTypes.End; level++)
                {
                    _toolWeapon.Add(tw.ToString() + level, gameW.NewEntity()
                        .Add(new ImageUIC(zone.Find(tw.ToString() + level + "_Image").GetComponent<Image>())));
                }
            }
        }
    }
}
