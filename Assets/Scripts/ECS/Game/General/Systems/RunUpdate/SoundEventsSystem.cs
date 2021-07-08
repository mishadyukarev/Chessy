using Assets.Scripts;

internal sealed class SoundEventsSystem : SystemGeneralReduction
{
    private int[] XySelectedCell => _eGM.SelectorEnt_SelectorCom.XySelectedCell;

    public override void Init()
    {
        base.Init();

        _eGM.SoundEnt_SoundCom.MistakeSoundAction = MistakeSound;
        _eGM.SoundEnt_SoundCom.AttackSoundAction = AttackSound;
    }

    public override void Run()
    {
        base.Run();
    }

    private void MistakeSound() => _eGM.MistakeAudioSource.Play();
    private void AttackSound() => _eGM.AttackAudioSource.Play();
}
