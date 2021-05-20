using Leopotam.Ecs;
using static MainGame;

internal class UpdateMotionMasterSystem : CellGeneralReduction, IEcsRunSystem
{
    internal UpdateMotionMasterSystem(ECSmanager eCSmanager) : base(eCSmanager) { }

    public void Run()
    {
        for (int x = 0; x < _eGM.Xamount; x++)
        {
            for (int y = 0; y < _eGM.Yamount; y++)
            {
                _eGM.CellUnitEnt_CellUnitCom(x, y).RefreshAmountSteps();

                if (_eGM.CellUnitEnt_CellUnitCom(x, y).IsRelaxed)
                {
                    switch (_eGM.CellUnitEnt_UnitTypeCom(x, y).UnitType)
                    {
                        case UnitTypes.King:
                            _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth += StartValuesGameConfig.HEALTH_FOR_ADDING_KING;
                            if (_eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth > StartValuesGameConfig.AMOUNT_HEALTH_KING)
                                _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth = StartValuesGameConfig.AMOUNT_HEALTH_KING;
                            break;

                        case UnitTypes.Pawn:
                            _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth += StartValuesGameConfig.HEALTH_FOR_ADDING_PAWN;
                            if (_eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth > _eGM.CellUnitEnt_CellUnitCom(x, y).MaxAmountHealth)
                                _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth = _eGM.CellUnitEnt_CellUnitCom(x, y).MaxAmountHealth;
                            break;

                        case UnitTypes.Rook:
                            _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth += StartValuesGameConfig.HEALTH_FOR_ADDING_ROOK;
                            if (_eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth > _eGM.CellUnitEnt_CellUnitCom(x, y).MaxAmountHealth)
                                _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth = _eGM.CellUnitEnt_CellUnitCom(x, y).MaxAmountHealth;
                            break;

                        case UnitTypes.Bishop:
                            _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth += StartValuesGameConfig.HEALTH_FOR_ADDING_BISHOP;
                            if (_eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth > _eGM.CellUnitEnt_CellUnitCom(x, y).MaxAmountHealth)
                                _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth = _eGM.CellUnitEnt_CellUnitCom(x, y).MaxAmountHealth;
                            break;

                        default:
                            break;
                    }
                }

                if (_eGM.CellBuildingEnt_BuildingTypeCom(x, y).BuildingType == BuildingTypes.City)
                {
                    if (_eGM.CellBuildingEnt_OwnerCom(x, y).IsHim(Instance.MasterClient))
                    {
                        _eGM.FoodEAmountDictC.AmountDict[true] += Instance.StartValuesGameConfig.BENEFIT_FOOD_CITY;
                        _eGM.WoodEAmountDictC.AmountDict[true] += Instance.StartValuesGameConfig.BENEFIT_WOOD_CITY;
                    }
                    else
                    {
                        _eGM.FoodEAmountDictC.AmountDict[false] += Instance.StartValuesGameConfig.BENEFIT_FOOD_CITY;
                        _eGM.WoodEAmountDictC.AmountDict[false] += Instance.StartValuesGameConfig.BENEFIT_WOOD_CITY;
                    }
                }
            }
        }



        _eGM.FoodEAmountDictC.AmountDict[true] += _eGM.InfoEnt_BuildingsInfoCom.AmountFarmDict[true] * Instance.StartValuesGameConfig.BENEFIT_FOOD_FARM;
        _eGM.FoodEAmountDictC.AmountDict[false] += _eGM.InfoEnt_BuildingsInfoCom.AmountFarmDict[false] * Instance.StartValuesGameConfig.BENEFIT_FOOD_FARM;

        _eGM.WoodEAmountDictC.AmountDict[true] += _eGM.InfoEnt_BuildingsInfoCom.AmountWoodcutterDict[true] * Instance.StartValuesGameConfig.BENEFIT_WOOD_WOODCUTTER;
        _eGM.WoodEAmountDictC.AmountDict[false] += _eGM.InfoEnt_BuildingsInfoCom.AmountWoodcutterDict[false] * Instance.StartValuesGameConfig.BENEFIT_WOOD_WOODCUTTER;

        _eGM.OreEAmountDictC.AmountDict[true] += _eGM.InfoEnt_BuildingsInfoCom.AmountMineDict[true] * Instance.StartValuesGameConfig.BENEFIT_ORE_MINE;
        _eGM.OreEAmountDictC.AmountDict[false] += _eGM.InfoEnt_BuildingsInfoCom.AmountMineDict[false] * Instance.StartValuesGameConfig.BENEFIT_ORE_MINE;



        _eGM.DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary[true] = false;
        _eGM.DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary[false] = false;


        _eGM.UpdatorEntityAmountComponent.Amount += 1;

    }
}
