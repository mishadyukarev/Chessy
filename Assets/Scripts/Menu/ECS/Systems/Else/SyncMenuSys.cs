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
        private EcsFilter<ShopZoneUICom> _shopZoneUIFilt = default;

        public void Run()
        {
            ref var centerMenuUICom = ref _centerUIFilter.Get1(0);

            LanguageComCom.CurLanguageType = centerMenuUICom.LanguageType;
            SoundComComp.Volume = centerMenuUICom.MusicVolume;


            _shopZoneUIFilt.Get1(0).SetTextInfo(LanguageComCom.GetText(ComLanguageTypes.InfoBuy));


            ref var onlineZoneUICom = ref _olineButUIFilter.Get2(0);
        }
    }
}
