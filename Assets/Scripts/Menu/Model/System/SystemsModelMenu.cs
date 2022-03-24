using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Common.View.UI;
using Chessy.Menu.View.UI;

namespace Chessy.Menu
{
    public sealed class SystemsModelMenu : IEcsRunSystem
    {
        public readonly LaunchLikeGameAndShopS LaunchLikeGameAndShopS;

        public readonly SyncSliderS SyncS;
        public readonly ConnectorMenuS ConnectorMenuS;


        readonly EntitiesViewUIMenu _eUIMenu;
        readonly EntitiesViewUICommon _eUICommon;
        readonly EntitiesModelCommon _eMCommon;


        public SystemsModelMenu(in EntitiesViewUIMenu eUIMenu, in EntitiesViewUICommon eUICommon, in EntitiesModelCommon eMCommon)
        {
            _eUIMenu = eUIMenu;
            _eUICommon = eUICommon;
            _eMCommon = eMCommon;
        }

        public void Run()
        {
            if(_eMCommon.SceneTC.Scene == SceneTypes.Menu)
            {
                SyncS.Run(_eUICommon, _eMCommon);
                ConnectorMenuS.Run(_eUIMenu);

                _eMCommon.VolumeMusic = _eUICommon.SettingsE.SliderC.Slider.value;


                LaunchLikeGameAndShopS.Run(ref _eMCommon.WasLikeGameZone, ref _eMCommon.TimeStartGameC, _eUICommon.ShopE);
            }
        }
    }
}