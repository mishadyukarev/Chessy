using Chessy.Common;
using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class UpEventUIS : IEcsInitSystem
    {
        public void Init()
        {
            AlphaUpUIC.AddList(OpenShop);
        }

        private void OpenShop()
        {
            ShopUIC.EnableZone();
        }
    }
}