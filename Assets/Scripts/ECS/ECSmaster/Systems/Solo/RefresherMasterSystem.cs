using Leopotam.Ecs;
using Photon.Pun;
using static MainGame;

internal struct RefresherMasterComponent
{
    private bool _isDone;
    private bool _isRefreshed;

    internal bool IsDone
    {
        get { return _isDone; }
        set { _isDone = value; }
    }
    internal bool IsRefreshed
    {
        get { return _isRefreshed; }
        set { _isRefreshed = value; }
    }
}

public class RefresherMasterSystem : CellReduction, IEcsRunSystem
{
    private EcsComponentRef<RefresherMasterComponent> _refresherMasterComponent = default;
    private EcsComponentRef<DonerMasterComponent> _donerMasterComponentRef = default;
    private EcsComponentRef<FromInfoComponent> _fromPlayerComponentRef = default;

    private EcsComponentRef<EconomyMasterComponent> _economyMasterComponent = default;
    private EcsComponentRef<EconomyMasterComponent.BuildingsMasterComponent> _economyBuildingsMasterComponentRef = default;

    private PhotonMessageInfo _fromInfo => _fromPlayerComponentRef.Unref().FromInfo;
    private bool _isDone => _refresherMasterComponent.Unref().IsDone;
    internal bool _isRefreshed
    {
        get { return _refresherMasterComponent.Unref().IsRefreshed; }
        set { _refresherMasterComponent.Unref().IsRefreshed = value; }
    }

    internal RefresherMasterSystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _refresherMasterComponent = eCSmanager.EntitiesMasterManager.RefresherMasterComponentRef;
        _donerMasterComponentRef = eCSmanager.EntitiesMasterManager.DonerMasterComponentRef;
        _fromPlayerComponentRef = eCSmanager.EntitiesMasterManager.FromInfoComponentRef;

        _economyMasterComponent = eCSmanager.EntitiesMasterManager.EconomyMasterComponentRef;
        _economyBuildingsMasterComponentRef = eCSmanager.EntitiesMasterManager.EconomyBuildingsMasterComponentRef;
    }


    public void Run()
    {
        if (_fromInfo.Sender.IsMasterClient) _donerMasterComponentRef.Unref().IsDoneMaster = _isDone;
        else _donerMasterComponentRef.Unref().IsDoneOther = _isDone;

        if (InstanceGame.IS_TEST || _donerMasterComponentRef.Unref().IsDoneMaster && _donerMasterComponentRef.Unref().IsDoneOther)
        {
            for (int x = 0; x < Xcount; x++)
            {
                for (int y = 0; y < Ycount; y++)
                {
                    switch (CellUnitComponent(x, y).UnitType)
                    {
                        case UnitTypes.King:
                            CellUnitComponent(x, y).AmountSteps = InstanceGame.StartValuesGameConfig.MAX_AMOUNT_STEPS_KING;
                            break;

                        case UnitTypes.Pawn:
                            CellUnitComponent(x, y).AmountSteps = InstanceGame.StartValuesGameConfig.MAX_AMOUNT_STEPS_PAWN;
                            break;

                        default:
                            break;
                    }

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
                _economyMasterComponent.Unref().FoodMaster += InstanceGame.StartValuesGameConfig.BENEFIT_FOOD_CITY;
                _economyMasterComponent.Unref().WoodMaster += InstanceGame.StartValuesGameConfig.BENEFIT_WOOD_CITY;
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


                _economyMasterComponent.Unref().FoodOther += InstanceGame.StartValuesGameConfig.BENEFIT_FOOD_CITY;
                _economyMasterComponent.Unref().WoodOther += InstanceGame.StartValuesGameConfig.BENEFIT_WOOD_CITY;
            }

            _economyMasterComponent.Unref().FoodMaster += _economyBuildingsMasterComponentRef.Unref().AmountFarmMaster * InstanceGame.StartValuesGameConfig.BENEFIT_FOOD_FARM;
            _economyMasterComponent.Unref().FoodOther += _economyBuildingsMasterComponentRef.Unref().AmountFarmOther * InstanceGame.StartValuesGameConfig.BENEFIT_FOOD_FARM;

            _economyMasterComponent.Unref().WoodMaster += _economyBuildingsMasterComponentRef.Unref().AmountWoodcutterMaster * InstanceGame.StartValuesGameConfig.BENEFIT_WOOD_WOODCUTTER;
            _economyMasterComponent.Unref().WoodOther += _economyBuildingsMasterComponentRef.Unref().AmountWoodcutterOther * InstanceGame.StartValuesGameConfig.BENEFIT_WOOD_WOODCUTTER;




            _donerMasterComponentRef.Unref().IsDoneMaster = false;
            _donerMasterComponentRef.Unref().IsDoneOther = false;

            _isRefreshed = true;
        }
        else _isRefreshed = false;
    }
}
