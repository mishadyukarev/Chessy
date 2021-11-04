using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Game
{
    public sealed class SoundSystem : IEcsRunSystem
    {
        public void Run()
        {
            if (SoundEffectC.IsPlaying(new[] { ClipGameTypes.Truce, ClipGameTypes.AfterBuildTown, ClipGameTypes.PickUpgrade }))
            {
                SoundComC.Volume = 0;
            }
            else
            {
                SoundComC.Volume = SoundComC.SavedVolume;
            }
        }
    }
}
