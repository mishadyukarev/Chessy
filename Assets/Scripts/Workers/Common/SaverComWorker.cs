namespace Assets.Scripts.Workers.Common
{
    internal sealed class SaverComWorker
    {
        private static EntitiesCommonManager ECM => Main.Instance.ECSmanager.EntitiesCommonManager;

        internal static float SliderVolume => ECM.SaverEnt_SaverCommCom.SliderVolume;
    }
}
