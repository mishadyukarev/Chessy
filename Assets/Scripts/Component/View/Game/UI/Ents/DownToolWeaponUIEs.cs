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
        public static ref ImageUIC Image(in ToolWeaponTypes tw, in LevelTypes level) => ref _toolWeapon[tw.ToString() + level].Get<ImageUIC>();

        public DownToolWeaponUIEs(in EcsWorld gameW, in Transform downZone)
        {
            _ents = new Dictionary<ToolWeaponTypes, Entity>();
            _toolWeapon = new Dictionary<string, Entity>();

            var gTZone = downZone.Find("GiveTake");

            for (var tw = ToolWeaponTypes.None + 1; tw < ToolWeaponTypes.End; tw++)
            {
                var zone = gTZone.Find(tw.ToString());

                _ents.Add(tw, gameW.NewEntity()
                    .Add(new ButtonUIC(zone.Find("Button").GetComponent<Button>()))
                    .Add(new ImageUIC(zone.Find("Back_Image").GetComponent<Image>()))
                    .Add(new TextUIC(zone.Find("Amount_TextMP").GetComponent<TextMeshProUGUI>())));

                for (var level = LevelTypes.None + 1; level < LevelTypes.End; level++)
                {
                    _toolWeapon.Add(tw.ToString() + level, gameW.NewEntity()
                        .Add(new ImageUIC(zone.Find(tw.ToString()).Find(level + "_Image").GetComponent<Image>())));
                }
            }
        }
    }
}
