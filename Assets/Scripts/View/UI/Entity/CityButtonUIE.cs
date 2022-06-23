using Chessy.Common.Component;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Model.Entity.View.UI.Down
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