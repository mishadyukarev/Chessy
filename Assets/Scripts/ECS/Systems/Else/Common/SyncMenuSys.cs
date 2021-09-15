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
        private EcsFilter<ConnectButtonUIComp, OnlineZoneUIComponent> _olineButUIFilter = default;
        private EcsFilter<ConnectButtonUIComp, OfflineZoneUIComponent> _offlineButUIFilter = default;
        private EcsFilter<CenterMenuUIComp> _centerUIFilter = default;

        public void Run()
        {
            ref var centerMenuUICom = ref _centerUIFilter.Get1(0);

            LanguageComComp.CurLanguageType = centerMenuUICom.LanguageType;
            SoundComComp.Volume = centerMenuUICom.MusicVolume;
            centerMenuUICom.SetTextExit(LanguageComComp.GetText(ComLanguageTypes.Exit));
            centerMenuUICom.SetTextInfo(LanguageComComp.GetText(ComLanguageTypes.Info));


            ref var onlineZoneUICom = ref _olineButUIFilter.Get2(0);

            onlineZoneUICom.SetTextCreatePGR(LanguageComComp.GetText(ComLanguageTypes.CreatePGR));
            onlineZoneUICom.SetTextJoinPGR(LanguageComComp.GetText(ComLanguageTypes.JoinPGR));
            onlineZoneUICom.SetTextPublicG(LanguageComComp.GetText(ComLanguageTypes.PublicGame));

            onlineZoneUICom.SetTextFriendG(LanguageComComp.GetText(ComLanguageTypes.FriendGame));
            onlineZoneUICom.SetTextCreateFGR(LanguageComComp.GetText(ComLanguageTypes.CreateFGR));
            onlineZoneUICom.SetTextJoinFGR(LanguageComComp.GetText(ComLanguageTypes.JoinFGR));



            _olineButUIFilter.Get1(0).Text = LanguageComComp.GetText(ComLanguageTypes.Online);
            _offlineButUIFilter.Get1(0).Text = LanguageComComp.GetText(ComLanguageTypes.Offline);

            _offlineButUIFilter.Get2(0).SetTextTraining(LanguageComComp.GetText(ComLanguageTypes.Training));


        }
    }
}
