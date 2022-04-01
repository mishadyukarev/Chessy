using Chessy.Common.Entity;
using Photon.Pun;
using System;
//using Yodo1.MAS;

namespace Chessy.Common
{
    public struct AdLaunchS : IEcsRunSystem
    {
        readonly EntitiesModelCommon _eMCommon;

        public AdLaunchS(in EntitiesModelCommon eMCommon)
        {
            _eMCommon = eMCommon;
        }

        public void Run()
        {
            //var difTime = DateTime.Now - _eMCommon.AdC.LastTimeAd;


            //if (!_eMCommon.ShopC.StoreController.products.WithID(ShopC.PREMIUM_NAME).hasReceipt)
            //{
            //    if (PhotonNetwork.OfflineMode || _eMCommon.SceneTC.Is(SceneTypes.Menu))
            //    {
            //        if (difTime.Minutes >= AdC.MINUTES_FOR_AD)
            //        {
            //            //if (Yodo1U3dMas.IsInterstitialAdLoaded())
            //            //{
            //            //    Yodo1U3dMas.ShowInterstitialAd();
            //            //    adC.LastTimeAd = DateTime.Now;
            //            //}
            //        }
            //    }
            //}
        }
    }
}