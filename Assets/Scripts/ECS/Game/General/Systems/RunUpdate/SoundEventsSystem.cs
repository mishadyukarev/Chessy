using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;

internal sealed class SoundEventsSystem : SystemGeneralReduction
{
    private int[] XySelectedCell => _eGM.SelectorEnt_SelectorCom.GetXy(SelectorCellTypes.Selected);

    public override void Init()
    {
        base.Init();


    }

    public override void Run()
    {
        base.Run();


        if (_eGM.TruceSoundEnt_AudioSourceCom.IsPlaying)
        {
            Main.Instance.ECSmanager.EntitiesCommonManager.SoundEnt_AudioSourceCommCom.Volume = 0;
        }
        else
        {
            Main.Instance.ECSmanager.EntitiesCommonManager.SoundEnt_AudioSourceCommCom.Volume
                = Main.Instance.ECSmanager.EntitiesCommonManager.SaverEnt_SaverCommCom.SliderVolume;
        }
    }
}
