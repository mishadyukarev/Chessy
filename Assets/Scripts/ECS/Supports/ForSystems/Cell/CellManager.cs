namespace Assets.Scripts
{
    public sealed class CellManager
    {
        private CellBaseOperations _cellBaseOperations;
        private CellUnitWorker _cellUnitWorker;
        private CellBuildingWorker _cellBuildingWorker;
        private CellEnvironmentWorker _cellEnvironmentWorker;

        internal CellBaseOperations CellBaseOperations => _cellBaseOperations;
        internal CellUnitWorker CellUnitWorker => _cellUnitWorker;
        internal CellBuildingWorker CellBuildingWorker => _cellBuildingWorker;
        internal CellEnvironmentWorker CellEnvironmentWorker => _cellEnvironmentWorker;

        internal CellManager(ECSManager eCSmanager)
        {
            _cellBaseOperations = new CellBaseOperations();
            _cellUnitWorker = new CellUnitWorker(eCSmanager, _cellBaseOperations);
            _cellBuildingWorker = new CellBuildingWorker(eCSmanager);
            _cellEnvironmentWorker = new CellEnvironmentWorker(eCSmanager);
        }
    }
}