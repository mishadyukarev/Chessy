namespace Assets.Scripts.Workers.Common
{
    internal sealed class SaverComWorker
    {
        private static EntCommonManager ECM => Main.Instance.ECSmanager.EntCommonManager;

        internal static float SliderVolume => ECM.SaverEnt_SaverCommCom.SliderVolume;
        internal static StepModeTypes StepModeType
        {
            get => ECM.SaverEnt_StepModeTypeCom.StepModeType;
            set => ECM.SaverEnt_StepModeTypeCom.StepModeType = value;
        }
    }
}
