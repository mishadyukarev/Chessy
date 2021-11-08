using Leopotam.Ecs;

namespace Chessy.Common
{
    public sealed class EventSys : IEcsInitSystem
    {
        public void Init()
        {
            ShopUIC.AddListExit_Button(ExitShop);
        }

        private void ExitShop()
        {
            ShopUIC.DisableZone();
        }
    }
}