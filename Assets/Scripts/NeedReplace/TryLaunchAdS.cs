using Chessy.Common.Entity;
using Chessy.Model.Model.Entity;
using System;

namespace Chessy.Common
{
    public class TryLaunchAdS : IUpdate
    {
        readonly EntitiesModel _eM;

        public TryLaunchAdS(in EntitiesModel eM)
        {
            _eM = eM;

            //Yodo1U3dMas.InitializeSdk();
        }

        public void Update()
        {
            var difTime = DateTime.Now - _eM.AdC.LastTimeAd;

            if (!_eM.ShopC.StoreController.products.WithID(ShopC.PREMIUM_NAME).hasReceipt)
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