internal class CellWorker
{
    protected EntitiesGeneralManager _eGM;
    internal CellWorker(ECSManager eCSmanager)
    {
        _eGM = eCSmanager.EntitiesGeneralManager;
    }
}
