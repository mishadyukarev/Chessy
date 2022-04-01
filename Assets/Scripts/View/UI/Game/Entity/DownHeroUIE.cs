using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game
{
    public sealed class DownHeroUIE
    {
        readonly Dictionary<UnitTypes, ImageUIC> _units;

        public readonly Chessy.Common.Component.GameObjectVC Parent;
        public readonly ButtonUIC ButtonC;
        public readonly TextUIC Cooldown;

        public ImageUIC Image(in UnitTypes unit) => _units[unit];


        public DownHeroUIE(in Transform down)
        {
            var hero = down.Find("Hero");

            Parent = new Chessy.Common.Component.GameObjectVC(hero.gameObject);
            ButtonC = new ButtonUIC(hero.Find("Button").GetComponent<Button>());
            Cooldown = new TextUIC(hero.Find("Cooldown").Find("Text (TMP)").GetComponent<TextMeshProUGUI>());



            _units = new Dictionary<UnitTypes, ImageUIC>();
            for (var unit = UnitTypes.Elfemale; unit < UnitTypes.Skeleton; unit++)
            {
                _units.Add(unit, new ImageUIC(hero.Find(unit.ToString()).GetComponent<Image>()));
            }
        }
    }
}