namespace Assets.Scripts.Workers.Cell
{
    public abstract class MainGeneralWorker
    {
        protected static EntitiesGameGeneralManager EGGM => Main.Instance.EntGGM;

        internal static int Xamount => EGGM.Xamount;
        internal static int Yamount => EGGM.Yamount;
    }
}
