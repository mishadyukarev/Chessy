using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game
{
    public readonly struct LeftCityUIE
    {
        public readonly GameObjectVC Parent;
        public readonly ButtonUIC Button;
        public readonly GameObjectVC CostGOC;

        internal LeftCityUIE(in Transform buildingZone)
        {
            Parent = new GameObjectVC(buildingZone.parent.gameObject);
            Button = new ButtonUIC(buildingZone.Find("Button+").GetComponent<Button>());
            CostGOC = new GameObjectVC(buildingZone.Find("Cost+").gameObject);
        }
    }
}