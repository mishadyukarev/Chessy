using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Common;
using Assets.Scripts.ECS.Component.View.Else.Game.General;
using Leopotam.Ecs;

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
