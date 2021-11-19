using Leopotam.Ecs;

namespace Game.Common
{
    public sealed class EventSys : IEcsInitSystem
    {
        public void Init()
        {
            ShopUIC.AddListExit(ExitShop);
        }

        private void ExitShop()
        {
            ShopUIC.DisableZone();
        }
    }
}