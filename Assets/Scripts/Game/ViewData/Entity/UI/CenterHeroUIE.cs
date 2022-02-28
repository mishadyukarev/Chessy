using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game
{
    public struct CenterHeroUIE
    {
        public GameObjectVC Parent;
        public ButtonUIC ButtonC;

        internal CenterHeroUIE(in Transform heroT, in UnitTypes unit)
        {
            Parent = new GameObjectVC(heroT.gameObject);
            ButtonC = new ButtonUIC(heroT.Find(unit.ToString()).Find("Button").GetComponent<Button>());
        }
    }
}