using Leopotam.Ecs;
using Photon.Pun;
using System;
using UnityEngine;
using Yodo1.MAS;

namespace Game.Common
{
    public sealed class AdLaunchS : IEcsRunSystem
    {
        public void Run()
        {
            var difTime = DateTime.Now - AdComCom.LastTimeAd;


            if (!ShopComC.HasReceipt(ShopComC.PREMIUM_NAME))
            {
                if (PhotonNetwork.OfflineMode || CurSceneC.Is(SceneTypes.Menu))
                {
                    if (difTime.Minutes >= AdComCom.MINUTES_FOR_AD)
                    {
                        if (Yodo1U3dMas.IsInterstitialAdLoaded())
                        {
                            Yodo1U3dMas.ShowInterstitialAd();
                            AdComCom.LastTimeAd = DateTime.Now;
                        }
                        else
                        {
                            Debug.Log("my");
                        }
                    }
                }
            }
        }
    }
}