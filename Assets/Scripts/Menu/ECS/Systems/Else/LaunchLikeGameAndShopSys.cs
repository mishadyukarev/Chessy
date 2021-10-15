using Leopotam.Ecs;
using Scripts.Common;
using System;

namespace Scripts.Menu
{
    public class LaunchLikeGameAndShopSys : IEcsRunSystem
    {
        private EcsFilter<LikeGameUICom> _timeStartGameFilt = default;


        private EcsFilter<ShopZoneUICom> _shopZoneUIFilt = default;


        public void Run()
        {
            if (!TimeStartGameComCom.WasLikeGameZone)
            {
                ref var timeStartGameCom = ref _timeStartGameFilt.Get1(0);

                var difTime = DateTime.Now - TimeStartGameComCom.TimeStartGame;

                if (difTime.Minutes >= 20)
                {
                    timeStartGameCom.SetActiveZone(true);
                    TimeStartGameComCom.WasLikeGameZone = true;

                    _shopZoneUIFilt.Get1(0).EnableZone();
                }
            }
        }
    }
}