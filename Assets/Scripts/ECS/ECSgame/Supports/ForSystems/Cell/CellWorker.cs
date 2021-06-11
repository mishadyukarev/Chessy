internal class CellWorker
{
    protected EntitiesGeneralManager _eGM;
    internal CellWorker(ECSmanager eCSmanager)
    {
        _eGM = eCSmanager.EntitiesGeneralManager;
    }
}
