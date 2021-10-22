using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Game
{
    internal sealed class SoundSystem : IEcsRunSystem
    {
        private EcsFilter<SoundEffectsComp> _soundEffecFilter = default;

        public void Run()
        {
            ref var soundEffectCom = ref _soundEffecFilter.Get1(0);

            if (soundEffectCom.IsPlaying(SoundEffectTypes.Truce))
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
