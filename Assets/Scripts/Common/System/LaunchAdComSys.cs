using Leopotam.Ecs;
using System;
using UnityEngine.Advertisements;

namespace Scripts.Common
{
    public class LaunchAdComSys : IEcsRunSystem
    {
        public void Run()
        {
            var difTime = DateTime.Now - AdComCom.LastTimeAd;

            if (difTime.Minutes >= AdComCom.MINUTES_FOR_AD)
            {
                if (Advertisement.IsReady())
                {
                    Advertisement.Show();
                    AdComCom.LastTimeAd = DateTime.Now;
                }
            }
        }
    }
}