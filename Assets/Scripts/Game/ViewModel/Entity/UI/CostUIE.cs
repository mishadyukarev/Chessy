using TMPro;
using UnityEngine;

namespace Chessy.Game.Entity.View.UI.Down
{
    public readonly struct CostUIE
    {
        public readonly TextUIC StepsTextC;
        public readonly TextUIC WoodTextC;
        public readonly TextUIC IronTextC;

        public CostUIE(in Transform downZone)
        {
            StepsTextC = new TextUIC(downZone.Find("StepsCost_TMP").GetComponent<TextMeshProUGUI>());
            WoodTextC = new TextUIC(downZone.Find("WoodCost_TMP").GetComponent<TextMeshProUGUI>());
            IronTextC = new TextUIC(downZone.Find("IronCost_TMP").GetComponent<TextMeshProUGUI>());
        }
    }
}