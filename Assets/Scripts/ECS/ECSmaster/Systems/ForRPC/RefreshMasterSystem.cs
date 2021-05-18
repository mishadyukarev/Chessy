using Leopotam.Ecs;
using static MainGame;

internal class RefreshMasterSystem : CellGeneralReduction, IEcsRunSystem
{
    internal RefreshMasterSystem(ECSmanager eCSmanager) : base(eCSmanager) { }

    public void Run()
    {
        for (int x = 0; x < _eGM.Xcount; x++)
        {
            for (int y = 0; y < _eGM.Ycount; y++)
            {
                _eGM.CellUnitComponent(x, y).RefreshAmountSteps();

                if (_eGM.CellUnitComponent(x, y).IsRelaxed)
                {
                    switch (_eGM.CellUnitComponent(x, y).UnitType)
                    {
                        case UnitTypes.King:
                            _eGM.CellUnitComponent(x, y).AmountHealth += StartValuesGameConfig.HEALTH_FOR_ADDING_KING;
                            if (_eGM.CellUnitComponent(x, y).AmountHealth > StartValuesGameConfig.AMOUNT_HEALTH_KING)
                                _eGM.CellUnitComponent(x, y).AmountHealth = StartValuesGameConfig.AMOUNT_HEALTH_KING;
                            break;

                        case UnitTypes.Pawn:
                            _eGM.CellUnitComponent(x, y).AmountHealth += StartValuesGameConfig.HEALTH_FOR_ADDING_PAWN;
                            if (_eGM.CellUnitComponent(x, y).AmountHealth > _eGM.CellUnitComponent(x, y).MaxAmountHealth)
                                _eGM.CellUnitComponent(x, y).AmountHealth = _eGM.CellUnitComponent(x, y).MaxAmountHealth;
                            break;

                        case UnitTypes.Rook:
                            _eGM.CellUnitComponent(x, y).AmountHealth += StartValuesGameConfig.HEALTH_FOR_ADDING_ROOK;
                            if (_eGM.CellUnitComponent(x, y).AmountHealth > _eGM.CellUnitComponent(x, y).MaxAmountHealth)
                                _eGM.CellUnitComponent(x, y).AmountHealth = _eGM.CellUnitComponent(x, y).MaxAmountHealth;
                            break;

                        case UnitTypes.Bishop:
                            _eGM.CellUnitComponent(x, y).AmountHealth += StartValuesGameConfig.HEALTH_FOR_ADDING_BISHOP;
                            if (_eGM.CellUnitComponent(x, y).AmountHealth > _eGM.CellUnitComponent(x, y).MaxAmountHealth)
                                _eGM.CellUnitComponent(x, y).AmountHealth = _eGM.CellUnitComponent(x, y).MaxAmountHealth;
                            break;

                        default:
                            break;
                    }
                }

            }
        }

        if (_eGM.InfoEntityBuildingsInfoComponent.IsBuildedCityDictionary[true])
        {
            _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[true] += InstanceGame.StartValuesGameConfig.BENEFIT_FOOD_CITY;
            _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[true] += InstanceGame.StartValuesGameConfig.BENEFIT_WOOD_CITY;
        }

        if (_eGM.InfoEntityBuildingsInfoComponent.IsBuildedCityDictionary[true])
        {
            _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[false] += InstanceGame.StartValuesGameConfig.BENEFIT_FOOD_CITY;
            _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[false] += InstanceGame.StartValuesGameConfig.BENEFIT_WOOD_CITY;
        }



        _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[true] += _eGM.InfoEntityBuildingsInfoComponent.AmountFarmDictionary[true] * InstanceGame.StartValuesGameConfig.BENEFIT_FOOD_FARM;
        _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[false] += _eGM.InfoEntityBuildingsInfoComponent.AmountFarmDictionary[false] * InstanceGame.StartValuesGameConfig.BENEFIT_FOOD_FARM;

        _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[true] += _eGM.InfoEntityBuildingsInfoComponent.AmountWoodcutterDictionary[true] * InstanceGame.StartValuesGameConfig.BENEFIT_WOOD_WOODCUTTER;
        _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[false] += _eGM.InfoEntityBuildingsInfoComponent.AmountWoodcutterDictionary[false] * InstanceGame.StartValuesGameConfig.BENEFIT_WOOD_WOODCUTTER;

        _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[true] += _eGM.InfoEntityBuildingsInfoComponent.AmountMineDictionary[true] * InstanceGame.StartValuesGameConfig.BENEFIT_ORE_MINE;
        _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[false] += _eGM.InfoEntityBuildingsInfoComponent.AmountMineDictionary[false] * InstanceGame.StartValuesGameConfig.BENEFIT_ORE_MINE;



        _eGM.DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary[true] = false;
        _eGM.DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary[false] = false;


        _eGM.RefreshComponent.NumberMotion += 1;

    }
}
