using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game
{
    public struct CenterHeroUIE
    {
        public Chessy.Common.Component.GameObjectVC Parent;
        public ButtonUIC ButtonC;

        internal CenterHeroUIE(in Transform heroT, in UnitTypes unit)
        {
            Parent = new Chessy.Common.Component.GameObjectVC(heroT.gameObject);
            ButtonC = new ButtonUIC(heroT.Find(unit.ToString()).Find("Button").GetComponent<Button>());
        }
    }
}