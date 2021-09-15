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
        private EcsFilter<ConnectButtonUIComp, OnlineZoneUIComponent> _connectOnlineButUIFilter = default;
        private EcsFilter<ConnectButtonUIComp, OfflineZoneUIComponent> _connectOfflineButUIFilter = default;
        private EcsFilter<CenterMenuUIComp> _centerUIFilter = default;

        public void Run()
        {
            LanguageComComp.CurLanguageType = _centerUIFilter.Get1(0).LanguageType;
            _connectOnlineButUIFilter.Get1(0).Text = LanguageComComp.GetText(ComLanguageTypes.Online);
            _connectOfflineButUIFilter.Get1(0).Text = LanguageComComp.GetText(ComLanguageTypes.Offline);

            SoundComComp.Volume = _centerUIFilter.Get1(0).MusicVolume;
        }
    }
}
