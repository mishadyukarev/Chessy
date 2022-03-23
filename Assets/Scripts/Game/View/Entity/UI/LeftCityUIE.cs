using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game
{
    public readonly struct LeftCityUIE
    {
        public readonly Chessy.Common.Component.GameObjectVC Parent;
        public readonly ButtonUIC Button;

        public readonly Chessy.Common.Component.GameObjectVC CostGOC;
        public readonly TextUIC CostTextC;

        internal LeftCityUIE(in Transform buildingZone)
        {
            Parent = new Chessy.Common.Component.GameObjectVC(buildingZone.parent.gameObject);
            Button = new ButtonUIC(buildingZone.Find("Button+").GetComponent<Button>());

            var cost = buildingZone.Find("Cost+");

            CostGOC = new Chessy.Common.Component.GameObjectVC(cost.gameObject);
            CostTextC = new TextUIC(cost.Find("Text_TMP+").GetComponent<TextMeshProUGUI>());
        }
    }
}