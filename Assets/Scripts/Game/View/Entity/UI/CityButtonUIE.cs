using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game.Entity.View.UI.Down
{
    public struct CityButtonUIE
    {
        public readonly ButtonUIC ButtonC;

        public CityButtonUIE(in Transform down)
        {
            ButtonC = new ButtonUIC(down.Find("City+").Find("Button+").GetComponent<Button>());
        }
    }
}