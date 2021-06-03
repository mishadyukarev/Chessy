﻿using static Main;

internal sealed class UpdateMotionMasterSystem : SystemMasterReduction
{

    public override void Run()
    {
        base.Run();


        _sMM.TryInvokeRunSystem(nameof(FireUpdatorMasterSystem), _sMM.RPCSystems);
        //_sMM.TryInvokeRunSystem(nameof(EconomyUpdatorMasterSystem), _sMM.RPCSystems);
        //_sMM.TryInvokeRunSystem(nameof(FertilizeUpdatorMasterSystem), _sMM.RPCSystems);

        for (int x = 0; x < _eGM.Xamount; x++)
        {
            for (int y = 0; y < _eGM.Yamount; y++)
            {
                if (_eGM.CellUnitEnt_UnitTypeCom(x, y).HaveUnit)
                {
                    _cM.CellUnitWorker.RefreshAmountSteps(x, y);

                    if (_eGM.CellUnitEnt_CellUnitCom(x, y).IsRelaxed)
                    {
                        switch (_eGM.CellUnitEnt_UnitTypeCom(x, y).UnitType)
                        {
                            case UnitTypes.King:
                                _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth += _startValuesGameConfig.HEALTH_FOR_ADDING_KING;
                                if (_eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth > _startValuesGameConfig.AMOUNT_HEALTH_KING)
                                    _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth = _startValuesGameConfig.AMOUNT_HEALTH_KING;
                                break;

                            case UnitTypes.Pawn:
                                _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth += _startValuesGameConfig.HEALTH_FOR_ADDING_PAWN;
                                if (_eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth > _cM.CellUnitWorker.MaxAmountHealth(x, y))
                                    _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth = _cM.CellUnitWorker.MaxAmountHealth(x, y);
                                break;

                            case UnitTypes.Rook:
                                _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth += _startValuesGameConfig.HEALTH_FOR_ADDING_ROOK;
                                if (_eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth > _cM.CellUnitWorker.MaxAmountHealth(x, y))
                                    _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth = _cM.CellUnitWorker.MaxAmountHealth(x, y);
                                break;

                            case UnitTypes.Bishop:
                                _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth += _startValuesGameConfig.HEALTH_FOR_ADDING_BISHOP;
                                if (_eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth > _cM.CellUnitWorker.MaxAmountHealth(x, y))
                                    _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth = _cM.CellUnitWorker.MaxAmountHealth(x, y);
                                break;

                            default:
                                break;
                        }
                    }
                }


                if (_eGM.CellBuildingEnt_BuildingTypeCom(x, y).HaveBuilding)
                {
                    switch (_eGM.CellBuildingEnt_BuildingTypeCom(x, y).BuildingType)
                    {
                        case BuildingTypes.None:
                            break;

                        case BuildingTypes.City:
                            break;

                        case BuildingTypes.Farm:
                            _eGM.CellEnvEnt_CellEnvCom(x, y).AmountFertilizerResources -= (int)(Instance.StartValuesGameConfig.BENEFIT_FOOD_FARM + (0.25f * _eGM.InfoEnt_UpgradeInfoCom.AmountUpgradeFarmDict[_eGM.CellBuildingEnt_OwnerCom(x, y).IsMasterClient]));
                            if (!_eGM.CellEnvEnt_CellEnvCom(x, y).HaveFertilizerResources)
                            {
                                _eGM.CellEnvEnt_CellEnvCom(x, y).ResetEnvironment(EnvironmentTypes.Fertilizer);
                                _cM.CellBuildingWorker.ResetBuilding(x, y);
                            }
                            break;

                        case BuildingTypes.Woodcutter:
                            _eGM.CellEnvEnt_CellEnvCom(x, y).AmountForestResources -= (int)(Instance.StartValuesGameConfig.BENEFIT_WOOD_WOODCUTTER + (0.25f * _eGM.InfoEnt_UpgradeInfoCom.AmountUpgradeWoodcutterDict[_eGM.CellBuildingEnt_OwnerCom(x, y).IsMasterClient]));
                            if (!_eGM.CellEnvEnt_CellEnvCom(x, y).HaveForestResources)
                            {
                                _eGM.CellEnvEnt_CellEnvCom(x, y).ResetEnvironment(EnvironmentTypes.AdultForest);
                                _cM.CellBuildingWorker.ResetBuilding(x, y);
                            }
                            break;

                        case BuildingTypes.Mine:
                            _eGM.CellEnvEnt_CellEnvCom(x, y).AmountOreResources -= (int)(Instance.StartValuesGameConfig.BENEFIT_ORE_MINE + (0.25f * _eGM.InfoEnt_UpgradeInfoCom.AmountUpgradeMineDict[_eGM.CellBuildingEnt_OwnerCom(x, y).IsMasterClient]));
                            if (_eGM.CellEnvEnt_CellEnvCom(x, y).MineStep >= 10 || !_eGM.CellEnvEnt_CellEnvCom(x, y).HaveOreResources)
                            {
                                _cM.CellBuildingWorker.ResetBuilding(x, y);
                                _eGM.CellEnvEnt_CellEnvCom(x, y).MineStep = 0;
                            }
                            _eGM.CellEnvEnt_CellEnvCom(x, y).MineStep += 1;
                            break;

                        default:
                            break;
                    }
                }
            }
        }




        _eGM.FoodEnt_AmountDictCom.AmountDict[true] += (int)(_eGM.InfoEnt_BuildingsInfoCom.AmountFarmDict[true] * (Instance.StartValuesGameConfig.BENEFIT_FOOD_FARM + (0.25f * _eGM.InfoEnt_UpgradeInfoCom.AmountUpgradeFarmDict[true])));
        _eGM.FoodEnt_AmountDictCom.AmountDict[false] += (int)(_eGM.InfoEnt_BuildingsInfoCom.AmountFarmDict[false] * (Instance.StartValuesGameConfig.BENEFIT_FOOD_FARM + (0.25f * _eGM.InfoEnt_UpgradeInfoCom.AmountUpgradeFarmDict[false])));

        _eGM.WoodEAmountDictC.AmountDict[true] += (int)(_eGM.InfoEnt_BuildingsInfoCom.AmountWoodcutterDict[true] * (Instance.StartValuesGameConfig.BENEFIT_WOOD_WOODCUTTER + (0.25f * _eGM.InfoEnt_UpgradeInfoCom.AmountUpgradeWoodcutterDict[true])));
        _eGM.WoodEAmountDictC.AmountDict[false] += (int)(_eGM.InfoEnt_BuildingsInfoCom.AmountWoodcutterDict[false] * (Instance.StartValuesGameConfig.BENEFIT_WOOD_WOODCUTTER + (0.25f * _eGM.InfoEnt_UpgradeInfoCom.AmountUpgradeWoodcutterDict[false])));

        _eGM.OreEAmountDictC.AmountDict[true] += (int)(_eGM.InfoEnt_BuildingsInfoCom.AmountMineDict[true] * (Instance.StartValuesGameConfig.BENEFIT_ORE_MINE + (0.25f * _eGM.InfoEnt_UpgradeInfoCom.AmountUpgradeMineDict[true])));
        _eGM.OreEAmountDictC.AmountDict[false] += (int)(_eGM.InfoEnt_BuildingsInfoCom.AmountMineDict[false] * (Instance.StartValuesGameConfig.BENEFIT_ORE_MINE + (0.25f * _eGM.InfoEnt_UpgradeInfoCom.AmountUpgradeMineDict[false])));


        _eGM.DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary[true] = false;
        _eGM.DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary[false] = false;

        _eGM.UpdatorEntityAmountComponent.AmountMotions += 1;
    }
}
