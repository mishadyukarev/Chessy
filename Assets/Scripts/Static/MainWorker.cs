namespace Assets.Scripts.Static.Cell
{
    public abstract class MainWorker
    {
        internal static EntitiesGameGeneralManager EGGM => Main.Instance.EntGGM;

        internal static int Xamount => EGGM.Xamount;
        internal static int Yamount => EGGM.Yamount;
    }
}
