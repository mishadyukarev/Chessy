namespace Assets.Scripts
{
    public sealed class CellManager
    {
        private CellBaseOperations _cellBaseOperations;
        private CellUnitWorker _cellUnitWorker;
        private CellBuildingWorker _cellBuildingWorker;
        private CellEnvironmentWorker _cellEnvironmentWorker;

        public CellBaseOperations CellBaseOperations => _cellBaseOperations;
        public CellUnitWorker CellUnitWorker => _cellUnitWorker;
        public CellBuildingWorker CellBuildingWorker => _cellBuildingWorker;
        public CellEnvironmentWorker CellEnvironmentWorker => _cellEnvironmentWorker;

        internal CellManager(ECSManager eCSmanager)
        {
            _cellBaseOperations = new CellBaseOperations();
            _cellUnitWorker = new CellUnitWorker(eCSmanager, _cellBaseOperations);
            _cellBuildingWorker = new CellBuildingWorker(eCSmanager);
            _cellEnvironmentWorker = new CellEnvironmentWorker(eCSmanager);
        }
    }
}