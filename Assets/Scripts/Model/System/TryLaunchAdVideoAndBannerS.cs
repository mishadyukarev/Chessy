using Chessy.Model.Entity;

namespace Chessy.Model.System
{
    public class TryLaunchAdVideoAndBannerS : IUpdate
    {
        //readonly CommonInfoAboutGameC _infoAboutGameC;
        //readonly ShopC _shopC;
        //readonly AdC _adC;

        public TryLaunchAdVideoAndBannerS(in EntitiesModel eM)
        {
            //_infoAboutGameC = eM.CommonGameE.CommonInfoAboutGameC;
            //_shopC = eM.CommonGameE.ShopC;
            //_adC = eM.CommonGameE.AdC;

            //Advertisement.Initialize(AdValues.ANDROID_GAME_ID, false);

            //if (!_shopC.HasReceipt(ShopValues.PREMIUM_NAME))
            //{
            //    Advertisement.Banner.SetPosition(AdValues.BANNER_POSSITION);
            //    Advertisement.Banner.Show(AdValues.NAME_BANNER);
            //}
        }

        public void Update()
        {
            //var difTime = DateTime.Now - _adC.LastTimeAd;

            //if (!_shopC.HasReceipt(ShopValues.PREMIUM_NAME))
            //{
            //if(_infoAboutGameC.SceneT == SceneTypes.Menu)
            //{
            //    if (difTime.Seconds >=  10/*AdValues.MINUTES_FOR_TURN_ON_TIME_ADD*/)
            //    {
            //        if (Advertisement.IsReady(AdValues.NAME_VIDEO))
            //        {
            //            Advertisement.Show(AdValues.NAME_VIDEO);

            //            _adC.LastTimeAd = DateTime.Now;
            //            _shopC.IsOpenedShopZone = true;
            //        }
            //        else
            //        {
            //            Advertisement.Load(AdValues.NAME_VIDEO);
            //        }
            //    }
            //}
            //}
        }
    }
}