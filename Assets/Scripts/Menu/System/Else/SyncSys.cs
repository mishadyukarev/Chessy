using Leopotam.Ecs;
using Game.Common;

namespace Game.Menu
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
