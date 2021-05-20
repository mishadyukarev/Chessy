using Leopotam.Ecs;
using UnityEngine;

internal class SoundSystem : SystemGeneralReduction, IEcsRunSystem
{
    private AudioSource _mistakeAudioSource;
    private AudioSource _attackAudioSource;


    internal SoundSystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _mistakeAudioSource = MainGame.InstanceGame.GameObjectPool.AudioSourceGO.GetComponent<AudioSource>();
        _attackAudioSource = MainGame.InstanceGame.GameObjectPool.AttackAudioSource;

        _eGM.SoundEntSoundCom.MistakeSoundAction = MistakeSound;
        _eGM.SoundEntSoundCom.AttackSoundAction = AttackSound;
    }

    public void Run()
    {

    }

    private void MistakeSound() => _mistakeAudioSource.Play();
    private void AttackSound() => _attackAudioSource.Play();
}
