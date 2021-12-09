using Game.Common;
using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class UpEventUIS : IEcsInitSystem
    {
        public void Init()
        {
            EntityUIPool.AlphaUp<ButtonC>().AddList(OpenShop);
        }

        private void OpenShop()
        {
            ShopUIC.EnableZone();
        }
    }
}