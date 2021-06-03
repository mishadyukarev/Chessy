internal class CellWorker
{
    protected EntitiesGeneralManager _eGM;
    internal CellWorker(ECSmanagerGame eCSmanager)
    {
        _eGM = eCSmanager.EntitiesGeneralManager;
    }
}
