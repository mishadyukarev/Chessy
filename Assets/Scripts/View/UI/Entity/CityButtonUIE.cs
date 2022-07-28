using Chessy.View.Component;
using Chessy.View.UI.Component;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.View.UI.Entity
{
    public struct CityButtonUIE
    {
        public readonly GameObjectVC ParentGOC;
        public readonly ButtonUIC ButtonC;

        public CityButtonUIE(in Transform down)
        {
            var parent = down.Find("City+");

            ParentGOC = new GameObjectVC(parent.gameObject);
            ButtonC = new ButtonUIC(parent.Find("Button+").GetComponent<Button>());
        }
    }
}