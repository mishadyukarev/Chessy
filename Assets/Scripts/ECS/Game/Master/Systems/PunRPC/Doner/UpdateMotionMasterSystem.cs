using Assets.Scripts;
using static Assets.Scripts.Main;

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
                    if (_eGM.CellUnitEnt_CellUnitCom(x, y).IsRelaxed)
                    {
                        if(_eGM.CellUnitEnt_CellUnitCom(x,y).AmountHealth == _cellM.CellUnitWorker.MaxAmountHealth(x, y))
                        {
                            if(_eGM.CellUnitEnt_UnitTypeCom(x, y).UnitType == UnitTypes.Pawn
                                || _eGM.CellUnitEnt_UnitTypeCom(x, y).UnitType == UnitTypes.PawnSword)
                            {
                                if (_eGM.CellEnvEnt_CellEnvCom(x, y).HaveFertilizer)
                                {
                                    _eGM.EconomyEnt_EconomyCom.AddAmountResources(ResourceTypes.Food, _eGM.CellUnitEnt_CellOwnerCom(x, y).IsMasterClient, 1);
                                    _eGM.CellEnvEnt_CellEnvCom(x, y).AmountFertilizerResources -= 1;

                                    if (!_eGM.CellEnvEnt_CellEnvCom(x, y).HaveFertilizerResources)
                                    {
                                        _eGM.CellEnvEnt_CellEnvCom(x, y).ResetEnvironment(EnvironmentTypes.Fertilizer);
                                    }
                                }

                                else if (_eGM.CellEnvEnt_CellEnvCom(x, y).HaveAdultTree)
                                {
                                    _eGM.EconomyEnt_EconomyCom.AddAmountResources(ResourceTypes.Wood, _eGM.CellUnitEnt_CellOwnerCom(x, y).IsMasterClient, 1);
                                    _eGM.CellEnvEnt_CellEnvCom(x, y).AmountForestResources -= 1;

                                    if (!_eGM.CellEnvEnt_CellEnvCom(x, y).HaveForestResources)
                                    {
                                        _eGM.CellEnvEnt_CellEnvCom(x, y).ResetEnvironment(EnvironmentTypes.AdultForest);
                                    }
                                }

                                else
                                {
                                    _eGM.CellUnitEnt_CellUnitCom(x, y).IsRelaxed = false;
                                    _eGM.CellUnitEnt_CellUnitCom(x, y).IsProtected = true;
                                }
                            }

                            else
                            {
                                _eGM.CellUnitEnt_CellUnitCom(x, y).IsRelaxed = false;
                                _eGM.CellUnitEnt_CellUnitCom(x, y).IsProtected = true;
                            }
                        }
                        else
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
                                    if (_eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth > _cellM.CellUnitWorker.MaxAmountHealth(x, y))
                                        _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth = _cellM.CellUnitWorker.MaxAmountHealth(x, y);
                                    break;

                                case UnitTypes.PawnSword:
                                    _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth += _startValuesGameConfig.HEALTH_FOR_ADDING_PAWN_SWORD;
                                    if (_eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth > _cellM.CellUnitWorker.MaxAmountHealth(x, y))
                                        _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth = _cellM.CellUnitWorker.MaxAmountHealth(x, y);
                                    break;

                                case UnitTypes.Rook:
                                    _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth += _startValuesGameConfig.HEALTH_FOR_ADDING_ROOK;
                                    if (_eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth > _cellM.CellUnitWorker.MaxAmountHealth(x, y))
                                        _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth = _cellM.CellUnitWorker.MaxAmountHealth(x, y);
                                    break;

                                case UnitTypes.Bishop:
                                    _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth += _startValuesGameConfig.HEALTH_FOR_ADDING_BISHOP;
                                    if (_eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth > _cellM.CellUnitWorker.MaxAmountHealth(x, y))
                                        _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth = _cellM.CellUnitWorker.MaxAmountHealth(x, y);
                                    break;

                                default:
                                    break;
                            }
                        }
                    }

                    else
                    {
                        if (_cellM.CellUnitWorker.HaveMaxSteps(x, y))
                        {
                            _eGM.CellUnitEnt_CellUnitCom(x, y).IsProtected = true;
                        }
                    }
                }

                _cellM.CellUnitWorker.RefreshAmountSteps(x, y);


                if (_eGM.CellBuildEnt_BuilTypeCom(x, y).HaveBuilding)
                {
                    int minus;

                    switch (_eGM.CellBuildEnt_BuilTypeCom(x, y).BuildingType)
                    {
                        case BuildingTypes.None:
                            break;

                        case BuildingTypes.City:
                            break;

                        case BuildingTypes.Farm:
                            minus = Instance.StartValuesGameConfig.BENEFIT_FOOD_FARM * _eGM.BuildingsEnt_UpgradeBuildingsCom.AmountUpgrades(BuildingTypes.Farm, _eGM.CellBuildEnt_OwnerCom(x, y).IsMasterClient);

                            _eGM.CellEnvEnt_CellEnvCom(x, y).AmountFertilizerResources -= minus;
                            _eGM.EconomyEnt_EconomyCom.AddAmountResources(ResourceTypes.Food, _eGM.CellBuildEnt_OwnerCom(x,y).IsMasterClient, minus);

                            if (!_eGM.CellEnvEnt_CellEnvCom(x, y).HaveFertilizerResources)
                            {
                                _eGM.CellEnvEnt_CellEnvCom(x, y).ResetEnvironment(EnvironmentTypes.Fertilizer);
                                _cellM.CellBuildingWorker.ResetBuilding(true, x, y);
                            }
                            break;

                        case BuildingTypes.Woodcutter:
                            minus = Instance.StartValuesGameConfig.BENEFIT_WOOD_WOODCUTTER * _eGM.BuildingsEnt_UpgradeBuildingsCom.AmountUpgrades(BuildingTypes.Woodcutter, _eGM.CellBuildEnt_OwnerCom(x, y).IsMasterClient);

                            _eGM.CellEnvEnt_CellEnvCom(x, y).AmountForestResources -= minus;
                            _eGM.EconomyEnt_EconomyCom.AddAmountResources(ResourceTypes.Wood, _eGM.CellBuildEnt_OwnerCom(x, y).IsMasterClient, minus);

                            if (!_eGM.CellEnvEnt_CellEnvCom(x, y).HaveForestResources)
                            {
                                _eGM.CellEnvEnt_CellEnvCom(x, y).ResetEnvironment(EnvironmentTypes.AdultForest);
                                _cellM.CellBuildingWorker.ResetBuilding(true, x, y);
                            }
                            break;

                        case BuildingTypes.Mine:
                            minus = Instance.StartValuesGameConfig.BENEFIT_ORE_MINE * _eGM.BuildingsEnt_UpgradeBuildingsCom.AmountUpgrades(BuildingTypes.Mine, _eGM.CellBuildEnt_OwnerCom(x, y).IsMasterClient);

                            _eGM.CellEnvEnt_CellEnvCom(x, y).AmountOreResources -= minus;
                            _eGM.EconomyEnt_EconomyCom.AddAmountResources(ResourceTypes.Ore, _eGM.CellBuildEnt_OwnerCom(x, y).IsMasterClient, minus);

                            if (_eGM.CellEnvEnt_CellEnvCom(x, y).MineStep >= 10 || !_eGM.CellEnvEnt_CellEnvCom(x, y).HaveOreResources)
                            {
                                _cellM.CellBuildingWorker.ResetBuilding(true, x, y);
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



        //_eGM.EconomyEnt_EconomyCom.AddAmountResources(ResourceTypes.Food, true, 1);
        //_eGM.EconomyEnt_EconomyCom.AddAmountResources(ResourceTypes.Food, false, 1);

        //_eGM.EconomyEnt_EconomyCom.AddAmountResources(ResourceTypes.Wood, true, 1);
        //_eGM.EconomyEnt_EconomyCom.AddAmountResources(ResourceTypes.Wood, false, 1);


        _eGM.DonerEnt_IsActivatedDictCom.SetIsActivated(true, false);
        _eGM.DonerEnt_IsActivatedDictCom.SetIsActivated(false, false);

        _eGM.MotionEnt_AmountCom.Amount += 1;
    }
}
