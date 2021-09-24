using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Common;
using Assets.Scripts.ECS.Component.UI;
using Assets.Scripts.ECS.Components.Data.Else.Common;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.Else.Common
{
    internal sealed class SyncMenuSys : IEcsRunSystem
    {
        private EcsFilter<ConnectButtonUICom, OnlineZoneUICom> _olineButUIFilter = default;
        private EcsFilter<ConnectButtonUICom, OfflineZoneUICom> _offlineButUIFilter = default;
        private EcsFilter<CenterMenuUICom> _centerUIFilter = default;

        public void Run()
        {
            ref var centerMenuUICom = ref _centerUIFilter.Get1(0);

            LanguageComCom.CurLanguageType = centerMenuUICom.LanguageType;
            SoundComComp.Volume = centerMenuUICom.MusicVolume;
            centerMenuUICom.SetTextExit(LanguageComCom.GetText(ComLanguageTypes.Exit));
            centerMenuUICom.SetTextInfo(LanguageComCom.GetText(ComLanguageTypes.Info));


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
