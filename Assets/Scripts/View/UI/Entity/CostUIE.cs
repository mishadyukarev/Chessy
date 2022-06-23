using TMPro;

namespace Chessy.Model.Entity.View.UI.Down
{
    public readonly struct CostUIE
    {
        public readonly TextUIC StepsTextC;
        public readonly TextUIC WoodTextC;
        public readonly TextUIC IronTextC;

        public CostUIE(in TextMeshProUGUI steps, in TextMeshProUGUI wood, in TextMeshProUGUI iron)
        {
            StepsTextC = new TextUIC(steps);
            WoodTextC = new TextUIC(wood);
            IronTextC = new TextUIC(iron);
        }
    }
}