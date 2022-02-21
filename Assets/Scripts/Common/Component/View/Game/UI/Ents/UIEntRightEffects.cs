using ECS;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public readonly struct UIEntRightEffects
    {
        static readonly Dictionary<UnitStatTypes, Entity> _effects_Images;

        public static ref C Image<C>(in UnitStatTypes unitStat) where C : struct => ref _effects_Images[unitStat].Get<C>();

        static UIEntRightEffects()
        {
            _effects_Images = new Dictionary<UnitStatTypes, Entity>();
            for (var unitStat = UnitStatTypes.Start; unitStat <= UnitStatTypes.End; unitStat++)
                _effects_Images.Add(unitStat, default);
        }
        public UIEntRightEffects(in EcsWorld gameW, in Transform rightZone)
        {
            var effects_Trans = rightZone.Find("EffectsZone");

            _effects_Images[UnitStatTypes.Hp] = gameW.NewEntity()
                .Add(new ImageUIC(effects_Trans.Find("Hp_Image").Find("Arrow_Image").GetComponent<Image>()));

            _effects_Images[UnitStatTypes.Damage] = gameW.NewEntity()
                .Add(new ImageUIC(effects_Trans.Find("Damage_Image").Find("Arrow_Image").GetComponent<Image>()));

            _effects_Images[UnitStatTypes.Steps] = gameW.NewEntity()
                .Add(new ImageUIC(effects_Trans.Find("Step_Image").Find("Arrow_Image").GetComponent<Image>()));
        }
    }
}