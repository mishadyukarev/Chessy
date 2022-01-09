using Game.Common;

namespace Game.Game
{
    sealed class UpEventUIS
    {
        internal UpEventUIS()
        {
            EntityUIPool.AlphaUp<ButtonC>().AddList(OpenShop);
        }

        private void OpenShop()
        {
            ShopUIC.EnableZone();
        }
    }
}