using UnityEngine.UI;

namespace Chessy.Game
{
    public struct LeftCityBuildUIE
    {
        public ButtonUIC ButtonC;

        internal LeftCityBuildUIE(in Button button)
        {
            ButtonC = new ButtonUIC(button);
        }
    }
}