using Leopotam.Ecs;
using Game.Common;
using System;

namespace Game.Menu
{
    public class LaunchLikeGameAndShopSys : IEcsRunSystem
    {
        public void Run()
        {
            if (!TimeStartGameC.WasLikeGameZone)
            {
                var difTime = DateTime.Now - TimeStartGameC.TimeStartGame;

                if (difTime.Minutes >= 20)
                {
                    LikeGameUICom.SetActiveZone(true);
                    TimeStartGameC.WasLikeGameZone = true;

                    ShopUIC.EnableZone();
                }
            }
        }
    }
}