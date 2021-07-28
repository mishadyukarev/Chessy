namespace Assets.Scripts.Workers.Common
{
    internal sealed class SaverComWorker
    {
        private static EntitiesCommonManager ECM => Main.Instance.ECSmanager.EntitiesCommonManager;

        internal static float SliderVolume => ECM.SaverEnt_SaverCommCom.SliderVolume;
        internal static StepModeTypes StepModeType
        {
            get => ECM.SaverEnt_StepModeTypeCom.StepModeType;
            set => ECM.SaverEnt_StepModeTypeCom.StepModeType = value;
        }
    }
}
