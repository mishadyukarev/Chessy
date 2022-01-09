using Game.Common;

namespace Game.Game
{
    public sealed class SoundS : IEcsRunSystem
    {
        public void Run()
        {
            if (SoundEffectVC.IsPlaying(new[] { ClipTypes.Truce, ClipTypes.AfterBuildTown, ClipTypes.PickUpgrade }))
            {
                SoundC.Volume = 0;
            }
            else
            {
                SoundC.Volume = SoundC.SavedVolume;
            }
        }
    }
}
