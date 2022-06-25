using Chessy.Model;
using System;

namespace Chessy.Common
{
    public class TryLaunchAdS : IUpdate
    {
        readonly EntitiesModel _e;

        public TryLaunchAdS(in EntitiesModel eM)
        {
            _e = eM;

            //Yodo1U3dMas.InitializeSdk();
        }

        public void Update()
        {
            var difTime = DateTime.Now - _e.AdC.LastTimeAd;

            if (!_e.ShopC.StoreController.products.WithID(ShopC.PREMIUM_NAME).hasReceipt)
            {
                if (difTime.Minutes >= AdC.MINUTES_TIME_ADD)
                {
                    //if (Yodo1U3dMas.IsInterstitialAdLoaded())
                    //{
                    //    Yodo1U3dMas.ShowInterstitialAd();
                    //    _eMC.AdC.LastTimeAd = DateTime.Now;

                    //    _eMC.ShopC.IsOpenedShopZone = true;
                    //}
                }
            }
        }
    }
}