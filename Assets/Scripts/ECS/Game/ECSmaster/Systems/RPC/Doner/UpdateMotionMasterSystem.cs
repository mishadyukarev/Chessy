using static Main;

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


                if (_eGM.CellBuildEnt_BuilTypeCom(x, y).HaveBuilding)
                {
                    switch (_eGM.CellBuildEnt_BuilTypeCom(x, y).BuildingType)
                    {
                        case BuildingTypes.None:
                            break;

                        case BuildingTypes.City:
                            break;

                        case BuildingTypes.Farm:
                            _eGM.CellEnvEnt_CellEnvCom(x, y).AmountFertilizerResources -= (int)(Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.StartValuesGameConfig.BENEFIT_FOOD_FARM + (0.25f * _eGM.BuildingsEnt_UpgradeBuildingsCom.AmountUpgrades(BuildingTypes.Farm, _eGM.CellBuildEnt_OwnerCom(x, y).IsMasterClient)));
                            if (!_eGM.CellEnvEnt_CellEnvCom(x, y).HaveFertilizerResources)
                            {
                                _eGM.CellEnvEnt_CellEnvCom(x, y).ResetEnvironment(EnvironmentTypes.Fertilizer);
                                _cM.CellBuildingWorker.ResetBuilding(true,x, y);
                            }
                            break;

                        case BuildingTypes.Woodcutter:
                            _eGM.CellEnvEnt_CellEnvCom(x, y).AmountForestResources -= (int)(Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.StartValuesGameConfig.BENEFIT_WOOD_WOODCUTTER + (0.25f * _eGM.BuildingsEnt_UpgradeBuildingsCom.AmountUpgrades(BuildingTypes.Woodcutter, _eGM.CellBuildEnt_OwnerCom(x, y).IsMasterClient)));
                            if (!_eGM.CellEnvEnt_CellEnvCom(x, y).HaveForestResources)
                            {
                                _eGM.CellEnvEnt_CellEnvCom(x, y).ResetEnvironment(EnvironmentTypes.AdultForest);
                                _cM.CellBuildingWorker.ResetBuilding(true,x, y);
                            }
                            break;

                        case BuildingTypes.Mine:
                            _eGM.CellEnvEnt_CellEnvCom(x, y).AmountOreResources -= (int)(Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.StartValuesGameConfig.BENEFIT_ORE_MINE + (0.25f * _eGM.BuildingsEnt_UpgradeBuildingsCom.AmountUpgrades(BuildingTypes.Mine, _eGM.CellBuildEnt_OwnerCom(x, y).IsMasterClient)));
                            if (_eGM.CellEnvEnt_CellEnvCom(x, y).MineStep >= 10 || !_eGM.CellEnvEnt_CellEnvCom(x, y).HaveOreResources)
                            {
                                _cM.CellBuildingWorker.ResetBuilding(true,x, y);
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



        _eGM.EconomyEnt_EconomyCom.AddFood(true, (int)(_eGM.BuildingsEnt_BuildingsCom.AmountBuildings(BuildingTypes.Farm, true) * (Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.StartValuesGameConfig.BENEFIT_FOOD_FARM + (0.25f * _eGM.BuildingsEnt_UpgradeBuildingsCom.AmountUpgrades(BuildingTypes.Farm, true)))));
        _eGM.EconomyEnt_EconomyCom.AddFood(false, (int)(_eGM.BuildingsEnt_BuildingsCom.AmountBuildings(BuildingTypes.Farm, false) * (Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.StartValuesGameConfig.BENEFIT_FOOD_FARM + (0.25f * _eGM.BuildingsEnt_UpgradeBuildingsCom.AmountUpgrades(BuildingTypes.Farm, false)))));

        _eGM.EconomyEnt_EconomyCom.AddWood(true, (int)(_eGM.BuildingsEnt_BuildingsCom.AmountBuildings(BuildingTypes.Woodcutter, true) * (Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.StartValuesGameConfig.BENEFIT_WOOD_WOODCUTTER + (0.25f * _eGM.BuildingsEnt_UpgradeBuildingsCom.AmountUpgrades(BuildingTypes.Woodcutter, true)))));
        _eGM.EconomyEnt_EconomyCom.AddWood(false, (int)(_eGM.BuildingsEnt_BuildingsCom.AmountBuildings(BuildingTypes.Woodcutter, false) * (Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.StartValuesGameConfig.BENEFIT_WOOD_WOODCUTTER + (0.25f * _eGM.BuildingsEnt_UpgradeBuildingsCom.AmountUpgrades(BuildingTypes.Woodcutter, false)))));

        _eGM.EconomyEnt_EconomyCom.AddOre(true, (int)(_eGM.BuildingsEnt_BuildingsCom.AmountBuildings(BuildingTypes.Mine, true) * (Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.StartValuesGameConfig.BENEFIT_ORE_MINE + (0.25f * _eGM.BuildingsEnt_UpgradeBuildingsCom.AmountUpgrades(BuildingTypes.Mine, true)))));
        _eGM.EconomyEnt_EconomyCom.AddOre(false, (int)(_eGM.BuildingsEnt_BuildingsCom.AmountBuildings(BuildingTypes.Mine, false) * (Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.StartValuesGameConfig.BENEFIT_ORE_MINE + (0.25f * _eGM.BuildingsEnt_UpgradeBuildingsCom.AmountUpgrades(BuildingTypes.Mine, false)))));


        _eGM.DonerEnt_IsActivatedDictCom.SetIsActivated(true, false);
        _eGM.DonerEnt_IsActivatedDictCom.SetIsActivated(false, false);

        _eGM.MotionEnt_AmountCom.Amount += 1;
    }
}
