using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Menu
{
    internal sealed class SyncMenuSys : IEcsRunSystem
    {
        private EcsFilter<ConnectButtonUICom, OnlineZoneUICom> _olineButUIFilter = default;
        private EcsFilter<ConnectButtonUICom, OfflineZoneUICom> _offlineButUIFilter = default;
        private EcsFilter<CenterMenuUICom> _centerUIFilter = default;
        private EcsFilter<DownZoneUIMenuCom> _downZoneFilt = default;
        private EcsFilter<ShopZoneUIMenuCom> _shopZoneUIFilt = default;

        public void Run()
        {
            ref var centerMenuUICom = ref _centerUIFilter.Get1(0);

            LanguageComCom.CurLanguageType = centerMenuUICom.LanguageType;
            SoundComComp.Volume = centerMenuUICom.MusicVolume;


            _downZoneFilt.Get1(0).SetTextExit(LanguageComCom.GetText(ComLanguageTypes.Exit));
            _downZoneFilt.Get1(0).SetTextHelp(LanguageComCom.GetText(ComLanguageTypes.HelpProject));


            _shopZoneUIFilt.Get1(0).SetTextInfo(LanguageComCom.GetText(ComLanguageTypes.InfoBuy));


            ref var onlineZoneUICom = ref _olineButUIFilter.Get2(0);

            onlineZoneUICom.SetTextCreatePGR(LanguageComCom.GetText(ComLanguageTypes.CreatePGR));
            onlineZoneUICom.SetTextJoinPGR(LanguageComCom.GetText(ComLanguageTypes.JoinPGR));
            onlineZoneUICom.SetTextPublicG(LanguageComCom.GetText(ComLanguageTypes.PublicGame));

            onlineZoneUICom.SetTextFriendG(LanguageComCom.GetText(ComLanguageTypes.FriendGame));
            onlineZoneUICom.SetTextCreateFGR(LanguageComCom.GetText(ComLanguageTypes.CreateFGR));
            onlineZoneUICom.SetTextJoinFGR(LanguageComCom.GetText(ComLanguageTypes.JoinFGR));



            _olineButUIFilter.Get1(0).Text = LanguageComCom.GetText(ComLanguageTypes.Online);
            _offlineButUIFilter.Get1(0).Text = LanguageComCom.GetText(ComLanguageTypes.Offline);

            _offlineButUIFilter.Get2(0).SetTextTraining(LanguageComCom.GetText(ComLanguageTypes.Training));
        }
    }
}
