namespace Assets.Scripts
{
    internal class CellWorker
    {
        protected EntitiesGameGeneralManager _eGM;
        internal CellWorker(ECSManager eCSmanager)
        {
            _eGM = eCSmanager.EntitiesGameGeneralManager;
        }
    }
}