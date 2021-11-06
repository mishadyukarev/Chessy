using Leopotam.Ecs;
using System;
using UnityEngine.Advertisements;
using UnityEngine.Purchasing;

namespace Scripts.Common
{
    public class LaunchAdComSys : IEcsInitSystem, IEcsRunSystem
    {

        public void Init()
        {

        }

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