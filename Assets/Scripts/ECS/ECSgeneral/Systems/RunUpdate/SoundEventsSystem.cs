using static Main;

internal sealed class SoundEventsSystem : SystemGeneralReduction
{
    internal ObjectPoolGame ObjectPoolGame => Instance.ObjectPoolGame;

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

    private void MistakeSound() => ObjectPoolGame.MistakeAudioSource.Play();
    private void AttackSound() => ObjectPoolGame.AttackAudioSource.Play();
}
