using Leopotam.Ecs;
using Game.Common;

namespace Game.Game
{
    public sealed class SoundS : IEcsRunSystem
    {
        public void Run()
        {
            if (SoundEffectVC.IsPlaying(new[] { ClipTypes.Truce, ClipTypes.AfterBuildTown, ClipTypes.PickUpgrade }))
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
