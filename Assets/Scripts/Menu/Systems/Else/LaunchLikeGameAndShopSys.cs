using Leopotam.Ecs;
using Scripts.Common;
using System;

namespace Scripts.Menu
{
    public class LaunchLikeGameAndShopSys : IEcsRunSystem
    {
        public void Run()
        {
            if (!TimeStartGameComCom.WasLikeGameZone)
            {
                var difTime = DateTime.Now - TimeStartGameComCom.TimeStartGame;

                if (difTime.Minutes >= 20)
                {
                    LikeGameUICom.SetActiveZone(true);
                    TimeStartGameComCom.WasLikeGameZone = true;

                    ShopZoneUICom.EnableZone();
                }
            }
        }
    }
}