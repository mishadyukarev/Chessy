using Assets.Scripts.Workers.Cell;

namespace Assets.Scripts.Workers
{
    internal abstract class MainMasterWorker : MainGeneralWorker
    {
        protected static EntitiesGameMasterManager EGMM => Main.Instance.ECSmanager.EntGameMasterManager;
    }
}
