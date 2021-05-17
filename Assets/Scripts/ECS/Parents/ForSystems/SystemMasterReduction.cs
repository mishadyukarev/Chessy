internal class SystemMasterReduction : SystemGeneralReduction
{
    protected EntitiesMasterManager _eMM;

    internal SystemMasterReduction(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _eMM = eCSmanager.EntitiesMasterManager;
    }
}
