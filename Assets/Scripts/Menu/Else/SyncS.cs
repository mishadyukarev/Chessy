using Chessy.Common;

namespace Chessy.Menu
{
    public struct SyncS
    {
        public void Run()
        {
            SoundC.Volume = CenterZoneUICom.MusicVolume;

            HintC.IsOnHint = CenterZoneUICom.IsOn;
        }
    }
}
