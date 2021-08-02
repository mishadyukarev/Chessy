using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Common;
using Leopotam.Ecs;

internal sealed class SoundEventsSystem : IEcsRunSystem
{
    public void Run()
    {
        if (SoundGameGeneralViewWorker.IsPlaying(SoundEffectTypes.Truce))
        {
            SoundComWorker.ResetVolume(SoundComTypes.StandartMusic);
        }
        else
        {
            SoundComWorker.SetVolume(SoundComTypes.StandartMusic, SaverComWorker.SliderVolume);
        }
    }
}
