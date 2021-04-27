using Leopotam.Ecs;
using Photon.Realtime;
using static MainGame;

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
    private EcsComponentRef<EconomyMasterComponent.BuildingsMasterComponent> _economyBuildingsMasterComponentRef = default;

    internal RefresherMasterSystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _refresherMasterComponent = eCSmanager.EntitiesMasterManager.RefresherMasterComponentRef;
        _donerMasterComponentRef = eCSmanager.EntitiesMasterManager.DonerMasterComponentRef;

        _economyMasterComponent = eCSmanager.EntitiesMasterManager.EconomyMasterComponentRef;
        _economyBuildingsMasterComponentRef = eCSmanager.EntitiesMasterManager.EconomyBuildingsMasterComponentRef;
    }


    public void Run()
    {
        _refresherMasterComponent.Unref().Unpack(out bool isDoneIN, out Player playerIN);

        if (playerIN.IsMasterClient) _donerMasterComponentRef.Unref().IsDoneMaster = isDoneIN;
        else _donerMasterComponentRef.Unref().IsDoneOther = isDoneIN;

        if (InstanceGame.IS_TEST || _donerMasterComponentRef.Unref().IsDoneMaster && _donerMasterComponentRef.Unref().IsDoneOther)
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
            if (_economyBuildingsMasterComponentRef.Unref().IsBuildedCityMaster)
            {
                if (CellEnvironmentComponent(_economyBuildingsMasterComponentRef.Unref().XYsettedCityMaster).HaveFood)
                {
                    _economyMasterComponent.Unref().FoodMaster += InstanceGame.StartValuesGameConfig.BENEFIT_FOOD_FARM;
                }
                if (CellEnvironmentComponent(_economyBuildingsMasterComponentRef.Unref().XYsettedCityMaster).HaveTree)
                {
                    _economyMasterComponent.Unref().WoodMaster += InstanceGame.StartValuesGameConfig.BENEFIT_WOOD_WOODCUTTER;
                }
                _economyMasterComponent.Unref().FoodMaster += InstanceGame.StartValuesGameConfig.ADDING_FOOD;
                _economyMasterComponent.Unref().WoodMaster += InstanceGame.StartValuesGameConfig.ADDING_WOOD;
            }
            if (_economyBuildingsMasterComponentRef.Unref().IsBuildedCityOther)
            {
                if (CellEnvironmentComponent(_economyBuildingsMasterComponentRef.Unref().XYsettedCityOther).HaveFood)
                {
                    _economyMasterComponent.Unref().FoodOther += InstanceGame.StartValuesGameConfig.BENEFIT_FOOD_FARM;
                }
                if (CellEnvironmentComponent(_economyBuildingsMasterComponentRef.Unref().XYsettedCityOther).HaveTree)
                {
                    _economyMasterComponent.Unref().WoodOther += InstanceGame.StartValuesGameConfig.BENEFIT_WOOD_WOODCUTTER;
                }


                _economyMasterComponent.Unref().FoodOther += InstanceGame.StartValuesGameConfig.ADDING_FOOD;
                _economyMasterComponent.Unref().WoodOther += InstanceGame.StartValuesGameConfig.ADDING_WOOD;
            }

            _economyMasterComponent.Unref().FoodMaster += _economyBuildingsMasterComponentRef.Unref().AmountFarmMaster * InstanceGame.StartValuesGameConfig.BENEFIT_FOOD_FARM;
            _economyMasterComponent.Unref().FoodOther += _economyBuildingsMasterComponentRef.Unref().AmountFarmOther * InstanceGame.StartValuesGameConfig.BENEFIT_FOOD_FARM;

            _economyMasterComponent.Unref().WoodMaster += _economyBuildingsMasterComponentRef.Unref().AmountWoodcutterMaster * InstanceGame.StartValuesGameConfig.BENEFIT_WOOD_WOODCUTTER;
            _economyMasterComponent.Unref().WoodOther += _economyBuildingsMasterComponentRef.Unref().AmountWoodcutterOther * InstanceGame.StartValuesGameConfig.BENEFIT_WOOD_WOODCUTTER;




            _donerMasterComponentRef.Unref().IsDoneMaster = false;
            _donerMasterComponentRef.Unref().IsDoneOther = false;

            _refresherMasterComponent.Unref().Pack(true);
        }
        else _refresherMasterComponent.Unref().Pack(false);
    }
}
