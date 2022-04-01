using Chessy.Common;
using Chessy.Common.Entity;

namespace Chessy.Menu
{
    public sealed class SystemsModelMenu : IEcsRunSystem
    {
        //public readonly LaunchLikeGameAndShopS LaunchLikeGameAndShopS;

        //readonly EntitiesViewUICommon _eUICommon;
        readonly EntitiesModelCommon _eMCommon;


        public SystemsModelMenu(/*in EntitiesViewUICommon eUICommon,*/ in EntitiesModelCommon eMCommon)
        {
            //_eUICommon = eUICommon;
            _eMCommon = eMCommon;
        }

        public void Run()
        {
            //SyncS.Run(_eUICommon, _eMCommon);
            //ConnectorMenuS.Run(_eUIMenu);

            //_eMCommon.VolumeMusic = _eUICommon.SettingsE.SliderC.Slider.value;


            //LaunchLikeGameAndShopS.Run(ref _eMCommon.WasLikeGameZone, ref _eMCommon.TimeStartGameC, _eUICommon.ShopE);

        }
    }
}