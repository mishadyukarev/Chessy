using Chessy.Common.Component;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game
{
    public readonly struct CenterHeroUIE
    {
        public readonly GameObjectVC Parent;
        public readonly ButtonUIC ButtonC;

        internal CenterHeroUIE(in Transform heroT, in UnitTypes unit)
        {
            Parent = new GameObjectVC(heroT.gameObject);
            ButtonC = new ButtonUIC(heroT.Find(unit.ToString()).Find("Button").GetComponent<Button>());
        }
    }
}