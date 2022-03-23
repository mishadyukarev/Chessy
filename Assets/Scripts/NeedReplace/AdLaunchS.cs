using Photon.Pun;
using System;
using Yodo1.MAS;

namespace Chessy.Common
{
    public struct AdLaunchS
    {
        public void Run(ref AdC adC, in SceneC sceneC)
        {
            var difTime = DateTime.Now - adC.LastTimeAd;


            if (!ShopC.HasReceipt(ShopC.PREMIUM_NAME))
            {
                if (PhotonNetwork.OfflineMode || sceneC.Is(SceneTypes.Menu))
                {
                    if (difTime.Minutes >= AdC.MINUTES_FOR_AD)
                    {
                        if (Yodo1U3dMas.IsInterstitialAdLoaded())
                        {
                            Yodo1U3dMas.ShowInterstitialAd();
                            adC.LastTimeAd = DateTime.Now;
                        }
                    }
                }
            }
        }
    }
}