using Leopotam.Ecs;
using UnityEngine;

internal class SoundSystem : IEcsRunSystem
{
    private AudioSource _audioSource;

    private EcsComponentRef<SoundComponent> _soundComponentRef = default;


    internal SoundSystem(ECSmanager eCSmanager, StartSpawnManager startSpawnManager)
    {
        _soundComponentRef = eCSmanager.EntitiesGeneralManager.SoundComponentRef;
        _audioSource = startSpawnManager.AudioSource;

        _soundComponentRef.Unref().MistakeSoundDelegate = MistakeSound;
    }

    public void Run()
    {

    }

    private void MistakeSound() => _audioSource.Play();
}
