using ECS;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public readonly struct UIEntDownToolWeapon
    {
        static readonly Dictionary<TWTypes, Entity> _ents;
        static readonly Dictionary<string, Entity> _toolWeapon;

        public static ref C Button<C>(in TWTypes tw) where C : struct => ref _ents[tw].Get<C>();
        public static ref C Image<C>(in TWTypes tw, in LevelTypes level) where C : struct => ref _toolWeapon[tw.ToString() + level].Get<C>();

        static UIEntDownToolWeapon()
        {
            _ents = new Dictionary<TWTypes, Entity>();
            _toolWeapon = new Dictionary<string, Entity>();
            for (var tw = TWTypes.Start; tw <= TWTypes.End; tw++)
            {
                _ents.Add(tw, default);
                for (var level = LevelTypes.Start; level <= LevelTypes.End; level++)
                    _toolWeapon.Add(tw.ToString() + level, default);
            }
        }
        public UIEntDownToolWeapon(in EcsWorld gameW, in Transform downZone)
        {
            var gTZone = downZone.Find("GiveTakeZone");


            for (var tw = TWTypes.Pick; tw < TWTypes.End; tw++)
            {
                var button = gTZone.Find(tw + "_Button").GetComponent<Button>();
                _ents[tw] = gameW.NewEntity()
                    .Add(new ButtonUIC(button))
                    .Add(new TextMPUGUIC(button.transform.Find("Amount_TextMP").GetComponent<TextMeshProUGUI>()));

                for (var level = LevelTypes.First; level < LevelTypes.End; level++)
                {
                    _toolWeapon[tw.ToString() + level] = gameW.NewEntity()
                        .Add(new ImageUIC(Button<ButtonUIC>(tw).Find(tw.ToString() + level + "_Image")
                        .GetComponent<Image>()));
                }
            }
        }
    }
}
