using Leopotam.Ecs;
using System;
using UnityEngine;
using Yodo1.MAS;

namespace Game.Common
{
    public class AdLaunchS : IEcsRunSystem
    {
        public void Run()
        {
            var difTime = DateTime.Now - AdComCom.LastTimeAd;


            if (!ShopComC.HasReceipt(ShopComC.PREMIUM_NAME))
            {
                if (difTime.Seconds >= AdComCom.MINUTES_FOR_AD)
                {
                    //if (Advertisement.IsReady())
                    //{
                    //    Advertisement.Show();
                    //    AdComCom.LastTimeAd = DateTime.Now;
                    //}

                    if (Yodo1U3dMas.IsInterstitialAdLoaded())
                    {
                        Yodo1U3dMas.ShowInterstitialAd();
                        AdComCom.LastTimeAd = DateTime.Now;
                    }
                }
            }
        }
    }
}