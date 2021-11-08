using Leopotam.Ecs;
using Chessy.Common;

namespace Chessy.Menu
{
    public sealed class SyncSys : IEcsRunSystem
    {
        public void Run()
        {
            SoundComC.Volume = CenterZoneUICom.MusicVolume;

            HintComC.IsOnHint = CenterZoneUICom.IsOn;
        }
    }
}
