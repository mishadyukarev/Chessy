using Chessy.Common.Entity;
using System;
using Yodo1.MAS;

namespace Chessy.Common
{
    public class TryLaunchAdS : IUpdate
    {
        readonly EntitiesModelCommon _eMC;

        public TryLaunchAdS(in EntitiesModelCommon eMCommon)
        {
            _eMC = eMCommon;

            Yodo1U3dMas.InitializeSdk();
        }

        public void Update()
        {
            var difTime = DateTime.Now - _eMC.AdC.LastTimeAd;

            if (!_eMC.ShopC.StoreController.products.WithID(ShopC.PREMIUM_NAME).hasReceipt)
            {
                if (difTime.Minutes >= AdC.MINUTES_TIME_ADD)
                {
                    if (Yodo1U3dMas.IsInterstitialAdLoaded())
                    {
                        Yodo1U3dMas.ShowInterstitialAd();
                        _eMC.AdC.LastTimeAd = DateTime.Now;

                        _eMC.ShopC.IsOpenedShopZone = true;
                    }
                }
            }
        }
    }
}