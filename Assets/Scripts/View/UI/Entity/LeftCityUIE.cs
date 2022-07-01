using Chessy.View.Component;
using Chessy.View.UI.Component;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.View.UI.Entity
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
            ParentGOC = new GameObjectVC(buildingZone.parent.gameObject);
            ZoneGOC = new GameObjectVC(buildingZone.gameObject);
            Button = new ButtonUIC(buildingZone.Find("Button+").GetComponent<Button>());

            var cost = buildingZone.Find("Cost+");

            CostGOC = new GameObjectVC(cost.gameObject);
            CostTextC = new TextUIC(cost.Find("Text_TMP+").GetComponent<TextMeshProUGUI>());
        }
    }
}