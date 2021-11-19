using Leopotam.Ecs;
using UnityEngine;
using Yodo1.MAS;

namespace Game.Common
{
    public sealed class MyYodo : IEcsInitSystem, IEcsRunSystem
    {
        public void Init()
        {

            //Yodo1AdBuildConfig config = new Yodo1AdBuildConfig().enableUserPrivacyDialog(true);
            //Yodo1U3dMas.SetAdBuildConfig(config);

            //Yodo1AdBuildConfig config = new Yodo1AdBuildConfig().enableUserPrivacyDialog(true).userAgreementUrl("Your user agreement url");
            //Yodo1U3dMas.SetAdBuildConfig(config);


            // COPPA is mandatory with Family SDK
            //Yodo1U3dMas.SetCCPA(true);
            //Yodo1U3dMas.SetCOPPA(true);
            //Yodo1U3dMas.SetGDPR(true);


            SetDelegates();
            Yodo1U3dMas.InitializeSdk();


            //Yodo1U3dMas.

            //ShowRewarded();
        }

        public void Run()
        {
            //if (Input.GetMouseButtonDown(0))
            //{
            //    ShowInterstitial();
            //}
        }

        public void ShowBanner()
        {
            int align = Yodo1U3dBannerAlign.BannerBottom | Yodo1U3dBannerAlign.BannerHorizontalCenter;
            Yodo1U3dMas.ShowBannerAd(align);

        }

        public void HideBanner()
        {
            Yodo1U3dMas.DismissBannerAd();
        }

        public static void ShowInterstitial()
        {
            if (Yodo1U3dMas.IsInterstitialAdLoaded())
            {
                Yodo1U3dMas.ShowInterstitialAd();
            }
            else
            {
                Debug.Log("[Yodo1 Mas] Interstitial ad has not been cached.");
            }
        }

        public void ShowRewarded()
        {
            if (Yodo1U3dMas.IsRewardedAdLoaded())
            {
                Yodo1U3dMas.ShowRewardedAd();
            }
            else
            {
                Debug.Log("[Yodo1 Mas] Reward video ad has not been cached.");
            }
        }

        private void SetDelegates()
        {
            Yodo1U3dMas.SetInitializeDelegate((bool success, Yodo1U3dAdError error) =>
            {
                Debug.Log("[Yodo1 Mas] InitializeDelegate, success:" + success + ", error: \n" + error.ToString());

                if (success)
                {

                }
                else
                {

                }
            });

            Yodo1U3dMas.SetBannerAdDelegate((Yodo1U3dAdEvent adEvent, Yodo1U3dAdError error) =>
            {
                Debug.Log("[Yodo1 Mas] BannerdDelegate:" + adEvent.ToString() + "\n" + error.ToString());
                switch (adEvent)
                {
                    case Yodo1U3dAdEvent.AdClosed:
                        Debug.Log("[Yodo1 Mas] Banner ad has been closed.");
                        break;
                    case Yodo1U3dAdEvent.AdOpened:
                        Debug.Log("[Yodo1 Mas] Banner ad has been shown.");
                        break;
                    case Yodo1U3dAdEvent.AdError:
                        Debug.Log("[Yodo1 Mas] Banner ad error, " + error.ToString());
                        break;
                }
            });

            Yodo1U3dMas.SetInterstitialAdDelegate((Yodo1U3dAdEvent adEvent, Yodo1U3dAdError error) =>
            {
                Debug.Log("[Yodo1 Mas] InterstitialAdDelegate:" + adEvent.ToString() + "\n" + error.ToString());
                switch (adEvent)
                {
                    case Yodo1U3dAdEvent.AdClosed:
                        Debug.Log("[Yodo1 Mas] Interstital ad has been closed.");
                        break;
                    case Yodo1U3dAdEvent.AdOpened:
                        Debug.Log("[Yodo1 Mas] Interstital ad has been shown.");
                        break;
                    case Yodo1U3dAdEvent.AdError:
                        Debug.Log("[Yodo1 Mas] Interstital ad error, " + error.ToString());
                        break;
                }

            });

            Yodo1U3dMas.SetRewardedAdDelegate((Yodo1U3dAdEvent adEvent, Yodo1U3dAdError error) =>
            {
                Debug.Log("[Yodo1 Mas] RewardVideoDelegate:" + adEvent.ToString() + "\n" + error.ToString());
                switch (adEvent)
                {
                    case Yodo1U3dAdEvent.AdClosed:
                        Debug.Log("[Yodo1 Mas] Reward video ad has been closed.");
                        break;
                    case Yodo1U3dAdEvent.AdOpened:
                        Debug.Log("[Yodo1 Mas] Reward video ad has shown successful.");
                        break;
                    case Yodo1U3dAdEvent.AdError:
                        Debug.Log("[Yodo1 Mas] Reward video ad error, " + error);
                        break;
                    case Yodo1U3dAdEvent.AdReward:
                        Debug.Log("[Yodo1 Mas] Reward video ad reward, give rewards to the player.");
                        break;
                }

            });
        }
    }
}
