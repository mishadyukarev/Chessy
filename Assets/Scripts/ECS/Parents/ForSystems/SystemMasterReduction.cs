internal abstract class SystemMasterReduction : SystemGeneralReduction
{
    protected EntitiesMasterManager _eMM;
    protected SystemsMasterManager _sMM;

    protected SystemMasterReduction(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _eMM = eCSmanager.EntitiesMasterManager;
        _sMM = eCSmanager.SystemsMasterManager;
    }
}
