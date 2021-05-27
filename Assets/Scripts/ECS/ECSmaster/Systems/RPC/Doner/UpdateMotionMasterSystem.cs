using Leopotam.Ecs;
using UnityEngine;
using static MainGame;

internal sealed class UpdateMotionMasterSystem : SystemMasterReduction
{
    internal UpdateMotionMasterSystem(ECSmanager eCSmanager) : base(eCSmanager) { }

    public override void Run()
    {
        base.Run();


        _sMM.TryInvokeRunSystem(nameof(FireUpdatorMasterSystem), _sMM.RPCSystems);
        //_sMM.TryInvokeRunSystem(nameof(EconomyUpdatorMasterSystem), _sMM.RPCSystems);
        _sMM.TryInvokeRunSystem(nameof(FertilizeUpdatorMasterSystem), _sMM.RPCSystems);

        for (int x = 0; x < _eGM.Xamount; x++)
        {
            for (int y = 0; y < _eGM.Yamount; y++)
            {
                if (_eGM.CellEnt_CellUnitCom(x, y).HaveUnit)
                {
                    _eGM.CellEnt_CellUnitCom(x, y).RefreshAmountSteps();

                    if (_eGM.CellEnt_CellUnitCom(x, y).IsRelaxed)
                    {
                        switch (_eGM.CellEnt_CellUnitCom(x, y).UnitType)
                        {
                            case UnitTypes.King:
                                _eGM.CellEnt_CellUnitCom(x, y).AmountHealth += StartValuesGameConfig.HEALTH_FOR_ADDING_KING;
                                if (_eGM.CellEnt_CellUnitCom(x, y).AmountHealth > StartValuesGameConfig.AMOUNT_HEALTH_KING)
                                    _eGM.CellEnt_CellUnitCom(x, y).AmountHealth = StartValuesGameConfig.AMOUNT_HEALTH_KING;
                                break;

                            case UnitTypes.Pawn:
                                _eGM.CellEnt_CellUnitCom(x, y).AmountHealth += StartValuesGameConfig.HEALTH_FOR_ADDING_PAWN;
                                if (_eGM.CellEnt_CellUnitCom(x, y).AmountHealth > _eGM.CellEnt_CellUnitCom(x, y).MaxAmountHealth)
                                    _eGM.CellEnt_CellUnitCom(x, y).AmountHealth = _eGM.CellEnt_CellUnitCom(x, y).MaxAmountHealth;
                                break;

                            case UnitTypes.Rook:
                                _eGM.CellEnt_CellUnitCom(x, y).AmountHealth += StartValuesGameConfig.HEALTH_FOR_ADDING_ROOK;
                                if (_eGM.CellEnt_CellUnitCom(x, y).AmountHealth > _eGM.CellEnt_CellUnitCom(x, y).MaxAmountHealth)
                                    _eGM.CellEnt_CellUnitCom(x, y).AmountHealth = _eGM.CellEnt_CellUnitCom(x, y).MaxAmountHealth;
                                break;

                            case UnitTypes.Bishop:
                                _eGM.CellEnt_CellUnitCom(x, y).AmountHealth += StartValuesGameConfig.HEALTH_FOR_ADDING_BISHOP;
                                if (_eGM.CellEnt_CellUnitCom(x, y).AmountHealth > _eGM.CellEnt_CellUnitCom(x, y).MaxAmountHealth)
                                    _eGM.CellEnt_CellUnitCom(x, y).AmountHealth = _eGM.CellEnt_CellUnitCom(x, y).MaxAmountHealth;
                                break;

                            default:
                                break;
                        }
                    }
                }

                


                if (_eGM.CellEnt_CellBuildingCom(x, y).BuildingType == BuildingTypes.City)
                {
                    if (_eGM.CellEnt_CellBuildingCom(x, y).IsHim(Instance.MasterClient))
                    {
                        _eGM.FoodEnt_AmountDictCom.AmountDict[true] += Instance.StartValuesGameConfig.BENEFIT_FOOD_CITY;
                        _eGM.WoodEAmountDictC.AmountDict[true] += Instance.StartValuesGameConfig.BENEFIT_WOOD_CITY;
                    }
                    else
                    {
                        _eGM.FoodEnt_AmountDictCom.AmountDict[false] += Instance.StartValuesGameConfig.BENEFIT_FOOD_CITY;
                        _eGM.WoodEAmountDictC.AmountDict[false] += Instance.StartValuesGameConfig.BENEFIT_WOOD_CITY;
                    }
                }

                if (_eGM.CellEnt_CellEnvCom(x, y).HaveAdultTree)
                {
                    if (_eGM.CellEnt_CellBuildingCom(x, y).BuildingType == BuildingTypes.Woodcutter)
                    {
                        _eGM.CellEnt_CellEnvCom(x, y).AmountResourcesForest -= (int)(Instance.StartValuesGameConfig.BENEFIT_WOOD_WOODCUTTER + (0.25f * _eGM.InfoEnt_UpgradeCom.AmountUpgradeWoodcutterDict[_eGM.CellEnt_CellBuildingCom(x, y).IsMasterClient]));
                        if (_eGM.CellEnt_CellEnvCom(x, y).AmountResourcesForest <= 0)
                        {
                            _eGM.FoodEnt_AmountDictCom.AmountDict[_eGM.CellEnt_CellBuildingCom(x, y).Owner.IsMasterClient] += StartValuesGameConfig.FOOD_FOR_BUILDING_WOODCUTTER;
                            _eGM.WoodEAmountDictC.AmountDict[_eGM.CellEnt_CellBuildingCom(x, y).Owner.IsMasterClient] += StartValuesGameConfig.WOOD_FOR_BUILDING_WOODCUTTER;
                            _eGM.OreEAmountDictC.AmountDict[_eGM.CellEnt_CellBuildingCom(x, y).Owner.IsMasterClient] += StartValuesGameConfig.ORE_FOR_BUILDING_WOODCUTTER;
                            _eGM.IronEAmountDictC.AmountDict[_eGM.CellEnt_CellBuildingCom(x, y).Owner.IsMasterClient] += StartValuesGameConfig.IRON_FOR_BUILDING_WOODCUTTER;
                            _eGM.GoldEAmountDictC.AmountDict[_eGM.CellEnt_CellBuildingCom(x, y).Owner.IsMasterClient] += StartValuesGameConfig.GOLD_FOR_BUILDING_WOODCUTTER;

                            _eGM.CellEnt_CellEnvCom(x, y).SetResetEnvironment(false, EnvironmentTypes.AdultForest);
                            _eGM.InfoEnt_BuildingsInfoCom.AmountWoodcutterDict[_eGM.CellEnt_CellBuildingCom(x, y).IsMasterClient] -= 1;
                            _eGM.CellEnt_CellBuildingCom(x, y).ResetBuilding();
                        }
                    }
                }
            }
        }

        


        _eGM.FoodEnt_AmountDictCom.AmountDict[true] += (int)(_eGM.InfoEnt_BuildingsInfoCom.AmountFarmDict[true] * (Instance.StartValuesGameConfig.BENEFIT_FOOD_FARM + (0.25f * _eGM.InfoEnt_UpgradeCom.AmountUpgradeFarmDict[true])));
        _eGM.FoodEnt_AmountDictCom.AmountDict[false] += (int)(_eGM.InfoEnt_BuildingsInfoCom.AmountFarmDict[false] * (Instance.StartValuesGameConfig.BENEFIT_FOOD_FARM + (0.25f * _eGM.InfoEnt_UpgradeCom.AmountUpgradeFarmDict[false])));

        _eGM.WoodEAmountDictC.AmountDict[true] += (int)(_eGM.InfoEnt_BuildingsInfoCom.AmountWoodcutterDict[true] * (Instance.StartValuesGameConfig.BENEFIT_WOOD_WOODCUTTER + (0.25f * _eGM.InfoEnt_UpgradeCom.AmountUpgradeWoodcutterDict[true])));
        _eGM.WoodEAmountDictC.AmountDict[false] += (int)(_eGM.InfoEnt_BuildingsInfoCom.AmountWoodcutterDict[false] * (Instance.StartValuesGameConfig.BENEFIT_WOOD_WOODCUTTER + (0.25f * _eGM.InfoEnt_UpgradeCom.AmountUpgradeWoodcutterDict[false])));

        _eGM.OreEAmountDictC.AmountDict[true] += (int)(_eGM.InfoEnt_BuildingsInfoCom.AmountMineDict[true] * (Instance.StartValuesGameConfig.BENEFIT_ORE_MINE + (0.25f * _eGM.InfoEnt_UpgradeCom.AmountUpgradeMineDict[true])));
        _eGM.OreEAmountDictC.AmountDict[false] += (int)(_eGM.InfoEnt_BuildingsInfoCom.AmountMineDict[false] * (Instance.StartValuesGameConfig.BENEFIT_ORE_MINE + (0.25f * _eGM.InfoEnt_UpgradeCom.AmountUpgradeMineDict[false])));


        _eGM.DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary[true] = false;
        _eGM.DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary[false] = false;

        _eGM.UpdatorEntityAmountComponent.Amount += 1;
    }
}
