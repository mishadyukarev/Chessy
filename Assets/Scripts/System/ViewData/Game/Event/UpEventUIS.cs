using Game.Common;
using static Game.Game.EntityUpUIPool;

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