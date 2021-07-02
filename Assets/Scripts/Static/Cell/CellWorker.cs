namespace Assets.Scripts
{
    public class CellWorker
    {
        protected EntitiesGameGeneralManager _eGM;
        public CellWorker(ECSManager eCSmanager)
        {
            _eGM = eCSmanager.EntitiesGameGeneralManager;
        }
    }
}