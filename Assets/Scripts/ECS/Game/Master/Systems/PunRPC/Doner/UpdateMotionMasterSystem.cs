﻿using Assets.Scripts;
using Assets.Scripts.Static;
using System.Collections.Generic;
using static Assets.Scripts.Main;

internal sealed class UpdateMotionMasterSystem : SystemMasterReduction
{
    private Dictionary<bool, int> _amountMotionsWithoutFood = new Dictionary<bool, int>();
    private int _countForResetUnitMaster = 2;
    private int _countForResetUnitOther = 2;

    public override void Init()
    {
        base.Init();

        _amountMotionsWithoutFood.Add(true, 0);
        _amountMotionsWithoutFood.Add(false, 0);
    }


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
                    if (_eGM.CellUnitEnt_CellOwnerCom(x, y).HaveOwner)
                    {
                        switch (_eGM.CellUnitEnt_UnitTypeCom(x, y).UnitType)
                        {
                            case UnitTypes.None:
                                break;

                            case UnitTypes.King:
                                break;

                            case UnitTypes.Pawn:
                                _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, _eGM.CellUnitEnt_CellOwnerCom(x, y).IsMasterClient);
                                break;

                            case UnitTypes.PawnSword:
                                _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, _eGM.CellUnitEnt_CellOwnerCom(x, y).IsMasterClient);
                                break;

                            case UnitTypes.Rook:
                                _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, _eGM.CellUnitEnt_CellOwnerCom(x, y).IsMasterClient);
                                break;

                            case UnitTypes.RookCrossbow:
                                _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, _eGM.CellUnitEnt_CellOwnerCom(x, y).IsMasterClient);
                                break;

                            case UnitTypes.Bishop:
                                _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, _eGM.CellUnitEnt_CellOwnerCom(x, y).IsMasterClient);
                                break;

                            case UnitTypes.BishopCrossbow:
                                _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, _eGM.CellUnitEnt_CellOwnerCom(x, y).IsMasterClient);
                                break;

                            default:
                                break;
                        }
                    }

                    if (_eGM.CellUnitEnt_CellUnitCom(x, y).IsRelaxed)
                    {
                        if (_eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth == CellUnitWorker.MaxAmountHealth(x, y))
                        {
                            if (_eGM.CellUnitEnt_CellOwnerCom(x, y).HaveOwner)
                            {

                                if (_eGM.CellUnitEnt_UnitTypeCom(x, y).UnitType == UnitTypes.Pawn
                                    || _eGM.CellUnitEnt_UnitTypeCom(x, y).UnitType == UnitTypes.PawnSword)
                                {
                                    if (_eGM.CellEnvEnt_CellEnvCom(x, y).HaveAdultTree)
                                    {
                                        _eGM.EconomyEnt_EconomyCom.AddAmountResources(ResourceTypes.Wood, _eGM.CellUnitEnt_CellOwnerCom(x, y).IsMasterClient, 1);
                                        _eGM.CellEnvEnt_CellEnvCom(x, y).AmountForestResources -= 1;
                                        _eGM.CellEnvEnt_CellEnvCom(x, y).AmountStepsExtractForest += 1;

                                        if (_eGM.CellEnvEnt_CellEnvCom(x, y).HaveForestResources)
                                        {
                                            if(_eGM.CellEnvEnt_CellEnvCom(x, y).AmountStepsExtractForest >= 3)
                                            {
                                                CellBuildingWorker.SetPlayerBuilding(true, BuildingTypes.Woodcutter, _eGM.CellUnitEnt_CellOwnerCom(x, y).Owner, x, y);
                                            }
                                        }
                                        else
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
                                    if (_eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth > CellUnitWorker.MaxAmountHealth(x, y))
                                        _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth = CellUnitWorker.MaxAmountHealth(x, y);
                                    break;

                                case UnitTypes.PawnSword:
                                    _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth += _startValuesGameConfig.HEALTH_FOR_ADDING_PAWN_SWORD;
                                    if (_eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth > CellUnitWorker.MaxAmountHealth(x, y))
                                        _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth = CellUnitWorker.MaxAmountHealth(x, y);
                                    break;

                                case UnitTypes.Rook:
                                    _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth += _startValuesGameConfig.HEALTH_FOR_ADDING_ROOK;
                                    if (_eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth > CellUnitWorker.MaxAmountHealth(x, y))
                                        _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth = CellUnitWorker.MaxAmountHealth(x, y);
                                    break;

                                case UnitTypes.RookCrossbow:
                                    _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth += _startValuesGameConfig.HEALTH_FOR_ADDING_ROOK_CROSSBOW;
                                    if (_eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth > CellUnitWorker.MaxAmountHealth(x, y))
                                        _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth = CellUnitWorker.MaxAmountHealth(x, y);
                                    break;

                                case UnitTypes.Bishop:
                                    _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth += _startValuesGameConfig.HEALTH_FOR_ADDING_BISHOP;
                                    if (_eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth > CellUnitWorker.MaxAmountHealth(x, y))
                                        _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth = CellUnitWorker.MaxAmountHealth(x, y);
                                    break;

                                case UnitTypes.BishopCrossbow:
                                    _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth += _startValuesGameConfig.HEALTH_FOR_ADDING_BISHOP_CROSSBOW;
                                    if (_eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth > CellUnitWorker.MaxAmountHealth(x, y))
                                        _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth = CellUnitWorker.MaxAmountHealth(x, y);
                                    break;

                                default:
                                    break;
                            }
                        }
                    }

                    else
                    {
                        _eGM.CellEnvEnt_CellEnvCom(x, y).AmountStepsExtractForest = 0;

                        if (CellUnitWorker.HaveMaxSteps(x, y))
                        {
                            _eGM.CellUnitEnt_CellUnitCom(x, y).IsProtected = true;
                        }
                    }
                }

                CellUnitWorker.RefreshAmountSteps(x, y);


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
                            _eGM.EconomyEnt_EconomyCom.AddAmountResources(ResourceTypes.Food, _eGM.CellBuildEnt_OwnerCom(x, y).IsMasterClient, minus);

                            if (!_eGM.CellEnvEnt_CellEnvCom(x, y).HaveFertilizerResources)
                            {
                                _eGM.CellEnvEnt_CellEnvCom(x, y).ResetEnvironment(EnvironmentTypes.Fertilizer);
                                CellBuildingWorker.ResetBuilding(true, x, y);
                            }
                            break;

                        case BuildingTypes.Woodcutter:
                            minus = Instance.StartValuesGameConfig.BENEFIT_WOOD_WOODCUTTER * _eGM.BuildingsEnt_UpgradeBuildingsCom.AmountUpgrades(BuildingTypes.Woodcutter, _eGM.CellBuildEnt_OwnerCom(x, y).IsMasterClient);

                            _eGM.CellEnvEnt_CellEnvCom(x, y).AmountForestResources -= minus;
                            _eGM.EconomyEnt_EconomyCom.AddAmountResources(ResourceTypes.Wood, _eGM.CellBuildEnt_OwnerCom(x, y).IsMasterClient, minus);

                            if (!_eGM.CellEnvEnt_CellEnvCom(x, y).HaveForestResources)
                            {
                                _eGM.CellEnvEnt_CellEnvCom(x, y).ResetEnvironment(EnvironmentTypes.AdultForest);
                                CellBuildingWorker.ResetBuilding(true, x, y);
                            }
                            break;

                        case BuildingTypes.Mine:
                            minus = Instance.StartValuesGameConfig.BENEFIT_ORE_MINE * _eGM.BuildingsEnt_UpgradeBuildingsCom.AmountUpgrades(BuildingTypes.Mine, _eGM.CellBuildEnt_OwnerCom(x, y).IsMasterClient);

                            _eGM.CellEnvEnt_CellEnvCom(x, y).AmountOreResources -= minus;
                            _eGM.EconomyEnt_EconomyCom.AddAmountResources(ResourceTypes.Ore, _eGM.CellBuildEnt_OwnerCom(x, y).IsMasterClient, minus);

                            if (_eGM.CellEnvEnt_CellEnvCom(x, y).MineStep >= 10 || !_eGM.CellEnvEnt_CellEnvCom(x, y).HaveOreResources)
                            {
                                CellBuildingWorker.ResetBuilding(true, x, y);
                                _eGM.CellEnvEnt_CellEnvCom(x, y).MineStep = 0;
                            }
                            _eGM.CellEnvEnt_CellEnvCom(x, y).MineStep += minus;
                            break;

                        default:
                            break;
                    }
                }
            }
        }

        _eGM.DonerEnt_IsActivatedDictCom.SetIsActivated(true, false);
        _eGM.DonerEnt_IsActivatedDictCom.SetIsActivated(false, false);

        _eGM.MotionEnt_AmountCom.Amount += 1;





        if (0 > _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, true))
        {
            _amountMotionsWithoutFood[true] += 1;
        }
        else
        {
            _countForResetUnitMaster = 2;
        }

        if (_amountMotionsWithoutFood[true] >= _countForResetUnitMaster)
        {
            var isResetedUnit = false;
            for (int x = 0; x < _eGM.Xamount; x++)
            {
                for (int y = 0; y < _eGM.Yamount; y++)
                {
                    if (_eGM.CellUnitEnt_UnitTypeCom(x, y).HaveUnit)
                    {
                        if (_eGM.CellUnitEnt_CellOwnerCom(x, y).HaveOwner)
                        {
                            if (_eGM.CellUnitEnt_CellOwnerCom(x, y).IsMasterClient)
                            {
                                if (_eGM.CellUnitEnt_UnitTypeCom(x, y).UnitType != UnitTypes.King)
                                {
                                    CellUnitWorker.ResetUnit(x, y);
                                    isResetedUnit = true;
                                    _amountMotionsWithoutFood[true] = 0;
                                    _countForResetUnitMaster = 1;
                                    break;
                                }
                            }
                        }
                    }

                }
                if (isResetedUnit) break;
            }
        }



        if (0 > _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, false))
        {
            _amountMotionsWithoutFood[false] += 1;
        }
        else
        {
            _countForResetUnitOther = 2;
        }

        if (_amountMotionsWithoutFood[false] >= _countForResetUnitOther)
        {
            var isResetedUnit = false;
            for (int x = 0; x < _eGM.Xamount; x++)
            {
                for (int y = 0; y < _eGM.Yamount; y++)
                {
                    if (_eGM.CellUnitEnt_UnitTypeCom(x, y).HaveUnit)
                    {
                        if (_eGM.CellUnitEnt_CellOwnerCom(x, y).HaveOwner)
                        {
                            if (!_eGM.CellUnitEnt_CellOwnerCom(x, y).IsMasterClient)
                            {
                                if (_eGM.CellUnitEnt_UnitTypeCom(x, y).UnitType != UnitTypes.King)
                                {
                                    CellUnitWorker.ResetUnit(x, y);
                                    isResetedUnit = true;
                                    _amountMotionsWithoutFood[false] = 0;
                                    _countForResetUnitOther = 1;
                                    break;
                                }
                            }
                        }
                    }

                }
                if (isResetedUnit) break;
            }
        }
    }
}
