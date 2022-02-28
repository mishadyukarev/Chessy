using Photon.Pun;
using System;
using Yodo1.MAS;

namespace Chessy.Common
{
    public sealed class AdLaunchS : IEcsRunSystem
    {
        public void Run()
        {
            var difTime = DateTime.Now - AdComC.LastTimeAd;


            if (!ShopComC.HasReceipt(ShopComC.PREMIUM_NAME))
            {
                if (PhotonNetwork.OfflineMode || CurSceneC.Is(SceneTypes.Menu))
                {
                    if (difTime.Minutes >= AdComC.MINUTES_FOR_AD)
                    {
                        if (Yodo1U3dMas.IsInterstitialAdLoaded())
                        {
                            Yodo1U3dMas.ShowInterstitialAd();
                            AdComC.LastTimeAd = DateTime.Now;
                        }
                    }
                }
            }
        }
    }
}