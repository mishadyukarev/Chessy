using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Game
{
    public sealed class SoundSystem : IEcsRunSystem
    {
        public void Run()
        {
            if (SoundEffectC.IsPlaying(SoundEffectTypes.Truce))
            {
                SoundComComp.Volume = 0;
            }
            else
            {
                SoundComComp.Volume = SoundComComp.SavedVolume;
            }
        }
    }
}
