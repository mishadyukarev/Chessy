using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Menu
{
    internal sealed class SyncSys : IEcsRunSystem
    {
        private EcsFilter<CenterZoneUICom> _centerUIFilter = default;
        private EcsFilter<ShopZoneUICom> _shopZoneUIFilt = default;


        public void Run()
        {
            ref var centerUICom = ref _centerUIFilter.Get1(0);

            SoundComComp.Volume = centerUICom.MusicVolume;

            _shopZoneUIFilt.Get1(0).SetTextInfo(LanguageComCom.GetText(ComLanguageTypes.InfoBuy));

            HintComCom.EnabledHint = centerUICom.InteractableHint;

        }
    }
}
