using Leopotam.Ecs;
using Game.Common;

namespace Game.Menu
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
