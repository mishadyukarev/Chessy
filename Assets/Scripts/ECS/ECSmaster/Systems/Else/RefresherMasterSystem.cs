using Leopotam.Ecs;
using Photon.Realtime;

public struct RefresherMasterComponent
{
    private bool _isDoneMasterIN;
    private bool _isDoneOtherIN;
    private bool _isRefreshedOUT;

    private SystemsMasterManager _systemsMasterManager;


    public bool IsDoneMaster
    {
        get { return _isDoneMasterIN; }
        set { _isDoneMasterIN = value; }
    }
    public bool IsDoneOther
    {
        get { return _isDoneOtherIN; }
        set { _isDoneOtherIN = value; }
    }
    public bool IsRefreshed
    {
        get { return _isRefreshedOUT; }
        set { _isRefreshedOUT = value; }
    }


    public RefresherMasterComponent(SystemsMasterManager systemsMasterManager)
    {
        _isDoneMasterIN = default;
        _isDoneOtherIN = default;
        _isRefreshedOUT = default;

        _systemsMasterManager = systemsMasterManager;
    }


    public bool TryRefresh(Player player, bool isDone)
    {
        if (player.IsMasterClient) _isDoneMasterIN = isDone;
        else { _isDoneOtherIN = isDone; }

        _systemsMasterManager.InvokeRunSystem(SystemMasterTypes.Else, nameof(RefresherMasterSystem));

        return _isRefreshedOUT;
    }

    public void Unpack(out bool isDoneMaster, out bool isDoneOther)
    {
        isDoneMaster = _isDoneMasterIN;
        isDoneOther = _isDoneOtherIN;
    }

    internal void Pack(bool isRefreshed) =>_isRefreshedOUT = isRefreshed;

    internal void ResetValues()
    {
        _isDoneMasterIN = default;
        _isDoneOtherIN = default;
        _isRefreshedOUT = default;
    }
}



public class RefresherMasterSystem : CellReductionSystem, IEcsRunSystem
{
    private EcsComponentRef<RefresherMasterComponent> _refresherMasterComponent = default;
    private EcsComponentRef<EconomyMasterComponent> _economyMasterComponent = default;


    internal RefresherMasterSystem(ECSmanager eCSmanager, SupportManager supportManager) : base(eCSmanager, supportManager)
    {
        _refresherMasterComponent = eCSmanager.EntitiesMasterManager.RefresherMasterComponentRef;
        _economyMasterComponent = eCSmanager.EntitiesMasterManager.EconomyMasterComponentRef;
        _startValues = supportManager.StartValuesConfig;
    }


    public void Run()
    {
        //_refresherMasterComponent.Unref().Unpack(out bool isDoneMaster, out bool isDoneOther);

        //if (isDoneMaster && isDoneOther)
        //{
        for (int x = 0; x < _startValues.CellCountX; x++)
        {
            for (int y = 0; y < _startValues.CellCountY; y++)
            {
                CellUnitComponent(x, y).RefreshAmountSteps();
            }
        }

        _refresherMasterComponent.Unref().Pack(true);

        _economyMasterComponent.Unref().GoldMaster += 20;
        _economyMasterComponent.Unref().GoldOther += 20;
        //}
    }
}
