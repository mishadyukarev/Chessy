using UnityEngine;
using static Main;

internal sealed class SoundSystem : SystemGeneralReduction
{
    private AudioSource _mistakeAudioSource;
    private AudioSource _attackAudioSource;


    internal SoundSystem()
    {
        _mistakeAudioSource = Instance.ObjectPoolGame.AudioSource;
        _attackAudioSource = Instance.ObjectPoolGame.AttackAudioSource;
    }
    public override void Init()
    {
        base.Init();

        _eGM.SoundEntSoundCom.MistakeSoundAction = MistakeSound;
        _eGM.SoundEntSoundCom.AttackSoundAction = AttackSound;
    }

    public override void Run()
    {
        base.Run();
    }

    private void MistakeSound() => _mistakeAudioSource.Play();
    private void AttackSound() => _attackAudioSource.Play();
}
