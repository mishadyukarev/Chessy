using Leopotam.Ecs;
using Photon.Realtime;

internal struct RefresherMasterComponent
{
    private bool _isDoneIN;
    private Player _playerIN;

    private bool _isRefreshedOUT;

    private SystemsMasterManager _systemsMasterManager;

    public RefresherMasterComponent(ECSmanager eCSmanager)
    {
        _isDoneIN = default;
        _playerIN = default;

        _isRefreshedOUT = default;

        _systemsMasterManager = eCSmanager.SystemsMasterManager;
    }


    public bool TryRefresh(bool isDoneIN, Player playerIN)
    {
        _isDoneIN = isDoneIN;
        _playerIN = playerIN;

        _systemsMasterManager.InvokeRunSystem(SystemMasterTypes.Else, nameof(RefresherMasterSystem));

        return _isRefreshedOUT;
    }

    internal void Unpack(out bool isDoneIN, out Player playerIN)
    {
        isDoneIN = _isDoneIN;
        playerIN = _playerIN;
    }

    internal void Pack(bool isRefreshedOUT)
    {
        _isRefreshedOUT = isRefreshedOUT;
    }
}

public class RefresherMasterSystem : CellReduction, IEcsRunSystem
{
    private EcsComponentRef<RefresherMasterComponent> _refresherMasterComponent = default;
    private EcsComponentRef<DonerMasterComponent> _donerMasterComponentRef = default;
    private EcsComponentRef<EconomyMasterComponent> _economyMasterComponent = default;

    internal RefresherMasterSystem(ECSmanager eCSmanager, SupportGameManager supportManager) : base(eCSmanager, supportManager)
    {
        _refresherMasterComponent = eCSmanager.EntitiesMasterManager.RefresherMasterComponentRef;
        _donerMasterComponentRef = eCSmanager.EntitiesMasterManager.DonerMasterComponentRef;
        _economyMasterComponent = eCSmanager.EntitiesMasterManager.EconomyMasterComponentRef;
    }


    public void Run()
    {
        _refresherMasterComponent.Unref().Unpack(out bool isDoneIN, out Player playerIN);

        if (playerIN.IsMasterClient) _donerMasterComponentRef.Unref().IsDoneMaster = isDoneIN;
        else _donerMasterComponentRef.Unref().IsDoneOther = isDoneIN;

        if (_donerMasterComponentRef.Unref().IsDoneMaster && _donerMasterComponentRef.Unref().IsDoneOther)
        {
            for (int x = 0; x < Xcount; x++)
            {
                for (int y = 0; y < Ycount; y++)
                {
                    CellUnitComponent(x, y).RefreshAmountSteps();

                    if (CellUnitComponent(x, y).IsRelaxed)
                    {
                        switch (CellUnitComponent(x, y).UnitType)
                        {
                            case UnitTypes.King:
                                CellUnitComponent(x, y).AmountHealth += _startValuesGameConfig.HEALTH_FOR_ADDING_KING;
                                if (CellUnitComponent(x, y).AmountHealth > _startValuesGameConfig.AMOUNT_HEALTH_KING)
                                    CellUnitComponent(x, y).AmountHealth = _startValuesGameConfig.AMOUNT_HEALTH_KING;
                                break;

                            case UnitTypes.Pawn:
                                CellUnitComponent(x, y).AmountHealth += _startValuesGameConfig.HEALTH_FOR_ADDING_PAWN;
                                if (CellUnitComponent(x, y).AmountHealth > _startValuesGameConfig.AMOUNT_HEALTH_PAWN)
                                    CellUnitComponent(x, y).AmountHealth = _startValuesGameConfig.AMOUNT_HEALTH_PAWN;
                                break;

                            default:
                                break;
                        }
                    }

                }
            }
            _economyMasterComponent.Unref().GoldMaster += 20;
            _economyMasterComponent.Unref().GoldOther += 20;

            _donerMasterComponentRef.Unref().IsDoneMaster = false;
            _donerMasterComponentRef.Unref().IsDoneOther = false;

            _refresherMasterComponent.Unref().Pack(true);
        }
        else _refresherMasterComponent.Unref().Pack(false);
    }
}
