using Chessy.Common;
using Chessy.Common.Enum;
using Chessy.Model.Enum;
using Photon.Pun;
using UnityEngine;

namespace Chessy.Model
{
    public sealed partial class SystemsModelGameForUI : SystemModel
    {
        internal SystemsModelGameForUI(in SystemsModel sMG, in EntitiesModel eMG) : base(sMG, eMG) { }

        public void TryBuyFromMarketBuilding(in MarketBuyTypes marketBuyT)
        {
            _e.RpcC.Action0(_e.RpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.TryBuyFromMarketBuildingM), marketBuyT });
        }

        public void ClickFriendReady()
        {
            _e.ZoneInfoC.IsActiveFriend = false;
            _e.NeedUpdateView = true;
        }

        public void ClickWindButtonUp()
        {
            if (_e.LessonT == LessonTypes.ClickWindInfo)
            {
                _e.SunSideT = SunSideTypes.Dawn;
                _e.CommonInfoAboutGameC.SetNextLesson();
            }
            else
            {
                if (_e.BookC.IsOpenedBook())
                {
                    _e.OpenedNowPageBookT = PageBookTypes.None;
                    _e.SoundAction(ClipTypes.CloseBook).Invoke();
                }
                else
                {
                    _e.OpenedNowPageBookT = PageBookTypes.Wind;
                    _e.SoundAction(ClipTypes.OpenBook).Invoke();
                }
            }

            _e.NeedUpdateView = true;
        }

        public void ClickSettingUpGame()
        {
            _e.SettingsC.IsOpenedBarWithSettings = !_e.SettingsC.IsOpenedBarWithSettings;
            _e.SoundAction(ClipTypes.Click);
        }

        public void ClickDiscordUp()
        {
            Application.OpenURL(URLC.URL_DISCORD);
        }

        public void ClickSettingsCenterMenu()
        {
            _e.SettingsC.IsOpenedBarWithSettings = !_e.SettingsC.IsOpenedBarWithSettings;
            _e.NeedUpdateView = true;
        }

        public void ClickExitInOpenedSettingZone()
        {
            _e.SettingsC.IsOpenedBarWithSettings = false;
            //eVCommon.Sound(ClipTypes.Click).Play();

            _e.NeedUpdateView = true;
        }


        #region BookZoneButtonsC

        public void ClickNextButtonInBookZone()
        {
            if (_e.OpenedNowPageBookT < PageBookTypes.End - 1)
            {
                _e.OpenedNowPageBookT++;
                _e.SoundAction(ClipTypes.ShiftBookSheet).Invoke();

                _e.NeedUpdateView = true;
            }
        }
        public void ClickBackButtonInBookZone()
        {
            if (_e.OpenedNowPageBookT > 0)
            {
                _e.OpenedNowPageBookT--;
                _e.SoundAction(ClipTypes.ShiftBookSheet).Invoke();

                _e.NeedUpdateView = true;
            }
        }

        #endregion

        public void ConnectOnlineMenu()
        {
            PhotonNetwork.PhotonServerSettings.DevRegion = "ru";
            PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = "ru";
            PhotonNetwork.PhotonServerSettings.AppSettings.BestRegionSummaryFromStorage = "ru";
            PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion = Application.version;

            PhotonNetwork.ConnectUsingSettings();
        }
        public void ConnectOffline()
        {
            if (PhotonNetwork.IsConnected) PhotonNetwork.Disconnect();
            else PhotonNetwork.OfflineMode = true;
        }
    }
}