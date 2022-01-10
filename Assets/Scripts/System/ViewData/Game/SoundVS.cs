using Game.Common;
using static Game.Game.EntityVPool;

namespace Game.Game
{
    public struct SoundVS : IEcsRunSystem
    {
        public void Run()
        {
            if (SoundV<AudioSourceVC>(ClipTypes.Truce).IsPlaying
                || SoundV<AudioSourceVC>(ClipTypes.AfterBuildTown).IsPlaying
                || SoundV<AudioSourceVC>(ClipTypes.PickUpgrade).IsPlaying)
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
