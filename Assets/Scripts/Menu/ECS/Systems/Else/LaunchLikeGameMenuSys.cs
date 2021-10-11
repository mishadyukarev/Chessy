using Leopotam.Ecs;
using Scripts.Common;
using System;

namespace Scripts.Menu
{
    public class LaunchLikeGameMenuSys : IEcsRunSystem
    {
        private EcsFilter<LikeGameZoneCom> _timeStartGameFilt = default;

        public void Run()
        {
            if (!TimeStartGameComCom.WasLikeGameZone)
            {
                ref var timeStartGameCom = ref _timeStartGameFilt.Get1(0);

                var difTime = DateTime.Now - TimeStartGameComCom.TimeStartGame;

                if (difTime.Seconds >= 5)
                {
                    timeStartGameCom.SetActiveZone(true);
                    TimeStartGameComCom.WasLikeGameZone = true;
                }
            }
        }
    }
}