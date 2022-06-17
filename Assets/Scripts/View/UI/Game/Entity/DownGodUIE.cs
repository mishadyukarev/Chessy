using Chessy.Common.Component;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game
{
    readonly struct DownGodUIE
    {
        readonly ImageUIC[] _units;

        internal readonly GameObjectVC Parent;
        internal readonly ButtonUIC ButtonC;
        internal readonly TextUIC Cooldown;

        internal ImageUIC Image(in UnitTypes unit) => _units[(byte)unit];


        internal DownGodUIE(in Transform downZoneT)
        {
            var hero = downZoneT.Find("Hero");

            Parent = new GameObjectVC(hero.gameObject);
            ButtonC = new ButtonUIC(hero.Find("Button").GetComponent<Button>());
            Cooldown = new TextUIC(hero.Find("Cooldown").Find("Text (TMP)").GetComponent<TextMeshProUGUI>());



            _units = new ImageUIC[(byte)UnitTypes.End];
            for (var unit = UnitTypes.Elfemale; unit < UnitTypes.Skeleton; unit++)
            {
                _units[(byte)unit] = new ImageUIC(hero.Find(unit.ToString()).GetComponent<Image>());
            }
        }
    }
}