using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Common.Entity.View;
using Chessy.Common.View.UI;
using Chessy.Menu.View.UI;

namespace Chessy.Menu
{
    public struct SyncSliderS
    {
        public void Run(in EntitiesViewUICommon eUIC, in EntitiesModelCommon eC)
        {
            eC.VolumeMusic = eUIC.SettingsE.SliderC.Slider.value;
            //SoundC.Volume = CenterZoneUICom.MusicVolume;

            //eC.IsOnHint = CenterZoneUICom.IsOn;
        }
    }
}
