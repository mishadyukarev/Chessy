using Leopotam.Ecs;
using UnityEngine;

internal class SoundSystem : IEcsRunSystem
{
    private AudioSource _mistakeAudioSource;
    private AudioSource _attackAudioSource;

    private EcsComponentRef<SoundComponent> _soundComponentRef = default;


    internal SoundSystem(ECSmanager eCSmanager)
    {
        _soundComponentRef = eCSmanager.EntitiesGeneralManager.SoundComponentRef;

        _mistakeAudioSource = MainGame.InstanceGame.GameObjectPool.AudioSourceGO.GetComponent<AudioSource>();
        _attackAudioSource = MainGame.InstanceGame.GameObjectPool.AttackAudioSource;

        _soundComponentRef.Unref().MistakeSoundAction = MistakeSound;
        _soundComponentRef.Unref().AttackSoundAction = AttackSound;
    }

    public void Run()
    {

    }

    private void MistakeSound() => _mistakeAudioSource.Play();
    private void AttackSound() => _attackAudioSource.Play();
}
