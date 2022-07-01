using Chessy.Model;
using Chessy.View.UI.Component;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.View.UI.Entity
{
    public readonly struct RightEffectsUIE
    {
        readonly Dictionary<UnitStatTypes, ImageUIC> _effects_Images;

        public ImageUIC Image(in UnitStatTypes unitStat) => _effects_Images[unitStat];

        public RightEffectsUIE(in Transform rightZone)
        {
            _effects_Images = new Dictionary<UnitStatTypes, ImageUIC>();

            var effects_Trans = rightZone.Find("EffectsZone");

            _effects_Images.Add(UnitStatTypes.Hp, new ImageUIC(effects_Trans.Find("Hp_Image").Find("Arrow_Image").GetComponent<Image>()));
            _effects_Images.Add(UnitStatTypes.Damage, new ImageUIC(effects_Trans.Find("Damage_Image").Find("Arrow_Image").GetComponent<Image>()));
            _effects_Images.Add(UnitStatTypes.Steps, new ImageUIC(effects_Trans.Find("Step_Image").Find("Arrow_Image").GetComponent<Image>()));
        }
    }
}