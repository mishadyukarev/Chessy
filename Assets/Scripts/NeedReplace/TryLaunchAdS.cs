using Chessy.Common.Entity;
using System;
using Yodo1.MAS;

namespace Chessy.Common
{
    public class TryLaunchAdS : IUpdate
    {
        const int MINUTES_TIME_ADD = 5;
        DateTime _lastTimeAd;


        readonly EntitiesModelCommon _eMC;

        public TryLaunchAdS(in EntitiesModelCommon eMCommon)
        {
            _eMC = eMCommon;

            Yodo1U3dMas.InitializeSdk();

            _lastTimeAd = DateTime.Now;
        }

        public void Update()
        {
            var difTime = DateTime.Now - _lastTimeAd;

            if (!_eMC.ShopC.StoreController.products.WithID(ShopC.PREMIUM_NAME).hasReceipt)
            {
                if (difTime.Minutes >= MINUTES_TIME_ADD)
                {
                    if (Yodo1U3dMas.IsInterstitialAdLoaded())
                    {
                        Yodo1U3dMas.ShowInterstitialAd();
                        _lastTimeAd = DateTime.Now;

                        _eMC.ShopC.IsOpenedShopZone = true;
                    }
                }
            }
        }
    }
}