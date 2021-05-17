using Leopotam.Ecs;
using Photon.Pun;
using static MainGame;

internal class RefreshMasterSystem : CellReduction, IEcsRunSystem
{
    private EcsComponentRef<MotionComponent> _refresherMasterComponent = default;
    private EcsComponentRef<DonerComponent> _donerComponentRef = default;


    internal ref EconomyComponent EconomyComponent => ref _entitiesGeneralManager.EconomyEntity.Get<EconomyComponent>();
    internal ref BuildingsInfoComponent BuildingsInfoComponent => ref _entitiesGeneralManager.EconomyEntity.Get<BuildingsInfoComponent>();


    internal RefreshMasterSystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _refresherMasterComponent = eCSmanager.EntitiesMasterManager.RefresherMasterComponentRef;
        _donerComponentRef = eCSmanager.EntitiesGeneralManager.DonerComponentRef;
    }


    public void Run()
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
                            if (CellUnitComponent(x, y).AmountHealth > CellUnitComponent(x, y).MaxAmountHealth)
                                CellUnitComponent(x, y).AmountHealth = CellUnitComponent(x, y).MaxAmountHealth;
                            break;

                        case UnitTypes.Rook:
                            CellUnitComponent(x, y).AmountHealth += _startValuesGameConfig.HEALTH_FOR_ADDING_ROOK;
                            if (CellUnitComponent(x, y).AmountHealth > CellUnitComponent(x, y).MaxAmountHealth)
                                CellUnitComponent(x, y).AmountHealth = CellUnitComponent(x, y).MaxAmountHealth;
                            break;

                        case UnitTypes.Bishop:
                            CellUnitComponent(x, y).AmountHealth += _startValuesGameConfig.HEALTH_FOR_ADDING_BISHOP;
                            if (CellUnitComponent(x, y).AmountHealth > CellUnitComponent(x, y).MaxAmountHealth)
                                CellUnitComponent(x, y).AmountHealth = CellUnitComponent(x, y).MaxAmountHealth;
                            break;

                        default:
                            break;
                    }
                }

            }
        }

        if (BuildingsInfoComponent.IsBuildedCityMaster)
        {
            EconomyComponent.FoodMaster += InstanceGame.StartValuesGameConfig.BENEFIT_FOOD_CITY;
            EconomyComponent.WoodMaster += InstanceGame.StartValuesGameConfig.BENEFIT_WOOD_CITY;
        }

        if (BuildingsInfoComponent.IsBuildedCityOther)
        {
            EconomyComponent.FoodOther += InstanceGame.StartValuesGameConfig.BENEFIT_FOOD_CITY;
            EconomyComponent.WoodOther += InstanceGame.StartValuesGameConfig.BENEFIT_WOOD_CITY;
        }



        EconomyComponent.FoodMaster += BuildingsInfoComponent.AmountFarmMaster * InstanceGame.StartValuesGameConfig.BENEFIT_FOOD_FARM;
        EconomyComponent.FoodOther += BuildingsInfoComponent.AmountFarmOther * InstanceGame.StartValuesGameConfig.BENEFIT_FOOD_FARM;

        EconomyComponent.WoodMaster += BuildingsInfoComponent.AmountWoodcutterMaster * InstanceGame.StartValuesGameConfig.BENEFIT_WOOD_WOODCUTTER;
        EconomyComponent.WoodOther += BuildingsInfoComponent.AmountWoodcutterOther * InstanceGame.StartValuesGameConfig.BENEFIT_WOOD_WOODCUTTER;

        EconomyComponent.OreMaster += BuildingsInfoComponent.AmountMineMaster * InstanceGame.StartValuesGameConfig.BENEFIT_ORE_MINE;
        EconomyComponent.OreOther += BuildingsInfoComponent.AmountMineOther * InstanceGame.StartValuesGameConfig.BENEFIT_ORE_MINE;



        _donerComponentRef.Unref().IsDoneMaster = false;
        _donerComponentRef.Unref().IsDoneOther = false;


        _refresherMasterComponent.Unref().NumberMotion += 1;

    }
}
