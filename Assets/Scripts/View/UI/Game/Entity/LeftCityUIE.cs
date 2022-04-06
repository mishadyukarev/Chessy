using Chessy.Common.Component;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game
{
    public readonly struct LeftCityUIE
    {
        public readonly GameObjectVC ParentGOC;
        public readonly GameObjectVC ZoneGOC;
        public readonly ButtonUIC Button;

        public readonly GameObjectVC CostGOC;
        public readonly TextUIC CostTextC;

        internal LeftCityUIE(in Transform buildingZone)
        {
            ParentGOC = new Chessy.Common.Component.GameObjectVC(buildingZone.parent.gameObject);
            ZoneGOC = new GameObjectVC(buildingZone.gameObject);
            Button = new ButtonUIC(buildingZone.Find("Button+").GetComponent<Button>());

            var cost = buildingZone.Find("Cost+");

            CostGOC = new Chessy.Common.Component.GameObjectVC(cost.gameObject);
            CostTextC = new TextUIC(cost.Find("Text_TMP+").GetComponent<TextMeshProUGUI>());
        }
    }
}