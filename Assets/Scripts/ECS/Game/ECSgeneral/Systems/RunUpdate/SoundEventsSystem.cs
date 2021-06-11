internal sealed class SoundEventsSystem : SystemGeneralReduction
{
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

    private void MistakeSound() => _eGM.MistakeAudioSource.Play();
    private void AttackSound() => _eGM.AttackAudioSource.Play();
}
