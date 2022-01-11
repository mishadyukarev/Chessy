using Game.Common;
using static Game.Game.EntityUpUIPool;

namespace Game.Game
{
    sealed class UpEventUIS
    {
        internal UpEventUIS()
        {
            Alpha<ButtonVC>().AddList(OpenShop);
        }

        private void OpenShop()
        {
            ShopUIC.EnableZone();
        }
    }
}