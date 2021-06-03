using static Main;
internal abstract class SystemMasterReduction : SystemGeneralReduction
{
    protected EntitiesMasterManager _eMM;
    protected SystemsMasterManager _sMM;

    public override void Init()
    {
        base.Init();

        _eMM = Instance.ECSmanagerGame.EntitiesMasterManager;
        _sMM = Instance.ECSmanagerGame.SystemsMasterManager;
    }
}
