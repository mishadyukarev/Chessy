using Chessy.Common;

namespace Chessy.Menu
{
    public sealed class SyncSys : IEcsRunSystem
    {
        public void Run()
        {
            SoundC.Volume = CenterZoneUICom.MusicVolume;

            HintC.IsOnHint = CenterZoneUICom.IsOn;
        }
    }
}
