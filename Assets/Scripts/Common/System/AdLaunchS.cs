using Leopotam.Ecs;
using System;
using UnityEngine.Advertisements;

namespace Chessy.Common
{
    public class AdLaunchS : IEcsRunSystem
    {
        public void Run()
        {
            var difTime = DateTime.Now - AdComCom.LastTimeAd;


            if (!ShopComC.HasReceipt(ShopComC.PREMIUM_NAME))
            {
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
}