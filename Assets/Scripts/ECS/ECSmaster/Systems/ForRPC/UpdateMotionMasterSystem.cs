using Leopotam.Ecs;
using static MainGame;

internal class UpdateMotionMasterSystem : CellGeneralReduction, IEcsRunSystem
{
    internal UpdateMotionMasterSystem(ECSmanager eCSmanager) : base(eCSmanager) { }

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

        if (_eGM.InfoEntBuildingsInfoCom.IsBuildedCityDictionary[true])
        {
            _eGM.FoodEAmountDictC.AmountDict[true] += InstanceGame.StartValuesGameConfig.BENEFIT_FOOD_CITY;
            _eGM.WoodEAmountDictC.AmountDict[true] += InstanceGame.StartValuesGameConfig.BENEFIT_WOOD_CITY;
        }

        if (_eGM.InfoEntBuildingsInfoCom.IsBuildedCityDictionary[true])
        {
            _eGM.FoodEAmountDictC.AmountDict[false] += InstanceGame.StartValuesGameConfig.BENEFIT_FOOD_CITY;
            _eGM.WoodEAmountDictC.AmountDict[false] += InstanceGame.StartValuesGameConfig.BENEFIT_WOOD_CITY;
        }



        _eGM.FoodEAmountDictC.AmountDict[true] += _eGM.InfoEntBuildingsInfoCom.AmountFarmDict[true] * InstanceGame.StartValuesGameConfig.BENEFIT_FOOD_FARM;
        _eGM.FoodEAmountDictC.AmountDict[false] += _eGM.InfoEntBuildingsInfoCom.AmountFarmDict[false] * InstanceGame.StartValuesGameConfig.BENEFIT_FOOD_FARM;

        _eGM.WoodEAmountDictC.AmountDict[true] += _eGM.InfoEntBuildingsInfoCom.AmountWoodcutterDict[true] * InstanceGame.StartValuesGameConfig.BENEFIT_WOOD_WOODCUTTER;
        _eGM.WoodEAmountDictC.AmountDict[false] += _eGM.InfoEntBuildingsInfoCom.AmountWoodcutterDict[false] * InstanceGame.StartValuesGameConfig.BENEFIT_WOOD_WOODCUTTER;

        _eGM.OreEAmountDictC.AmountDict[true] += _eGM.InfoEntBuildingsInfoCom.AmountMineDict[true] * InstanceGame.StartValuesGameConfig.BENEFIT_ORE_MINE;
        _eGM.OreEAmountDictC.AmountDict[false] += _eGM.InfoEntBuildingsInfoCom.AmountMineDict[false] * InstanceGame.StartValuesGameConfig.BENEFIT_ORE_MINE;



        _eGM.DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary[true] = false;
        _eGM.DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary[false] = false;


        _eGM.UpdatorEntityAmountComponent.Amount += 1;

    }
}
