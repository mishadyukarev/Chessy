using Game.Common;
using static Game.Game.EconomyUpUIE;

namespace Game.Game
{
    sealed class UpEventUIS
    {
        internal UpEventUIS()
        {
            Alpha<ButtonUIC>().AddListener(OpenShop);
        }

        private void OpenShop()
        {
            ShopUIC.EnableZone();
        }
    }
}