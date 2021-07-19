﻿using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.Static;
using Photon.Pun;
using System.Collections.Generic;
using static Assets.Scripts.Main;

internal sealed class UpdateMotionMasterSystem : RPCMasterSystemReduction
{
    private Dictionary<bool, int> _amountMotionsWithoutFood = new Dictionary<bool, int>();
    private int _countForResetUnitMaster = 2;
    private int _countForResetUnitOther = 2;
    private Dictionary<bool, int> _amountMotionsWithoutFoodForTruce = new Dictionary<bool, int>();

    public override void Init()
    {
        base.Init();

        _amountMotionsWithoutFood.Add(true, 0);
        _amountMotionsWithoutFood.Add(false, 0);

        _amountMotionsWithoutFoodForTruce.Add(true, 0);
        _amountMotionsWithoutFoodForTruce.Add(false, 0);
    }


    public override void Run()
    {
        base.Run();


        _sMM.TryInvokeRunSystem(nameof(FireUpdatorMasterSystem), _sMM.RpcSystems);
        //_sMM.TryInvokeRunSystem(nameof(EconomyUpdatorMasterSystem), _sMM.RPCSystems);
        //_sMM.TryInvokeRunSystem(nameof(FertilizeUpdatorMasterSystem), _sMM.RPCSystems);



        for (int x = 0; x < _eGM.Xamount; x++)
        {
            for (int y = 0; y < _eGM.Yamount; y++)
            {
                if (_eGM.CellUnitEnt_UnitTypeCom(x, y).HaveAnyUnit)
                {
                    var unitType = _eGM.CellUnitEnt_UnitTypeCom(x, y).UnitType;

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

                    if (_eGM.CellUnitEnt_ProtectRelaxCom(x, y).IsType(ProtectRelaxTypes.Relaxed))
                    {
                        if (_eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth == CellUnitWorker.MaxAmountHealth(unitType))
                        {
                            if (_eGM.CellUnitEnt_CellOwnerCom(x, y).HaveOwner)
                            {
                                if (unitType == UnitTypes.Pawn || unitType == UnitTypes.PawnSword)
                                {
                                    if (_eGM.CellEnvEnt_CellEnvCom(x, y).HaveEnvironment(EnvironmentTypes.AdultForest))
                                    {
                                        _eGM.EconomyEnt_EconomyCom.AddAmountResources(ResourceTypes.Wood, _eGM.CellUnitEnt_CellOwnerCom(x, y).IsMasterClient, 1);
                                        _eGM.CellEnvEnt_CellEnvCom(x, y).TakeAmountResources(ResourceTypes.Wood);

                                        if (!_eGM.CellEnvEnt_CellEnvCom(x, y).HaveResources(ResourceTypes.Wood))
                                        {
                                            _eGM.CellEnvEnt_CellEnvCom(x, y).ResetEnvironment(EnvironmentTypes.AdultForest);
                                        }

                                        if (_eGM.CellBuildEnt_BuilTypeCom(x, y).HaveBuilding)
                                        {
                                            _eGM.CellUnitEnt_CellUnitCom(x, y).ResetAmountStepsInProtectRelax(ProtectRelaxTypes.Relaxed);
                                        }
                                        else
                                        {
                                            _eGM.CellUnitEnt_CellUnitCom(x, y).AddAmountStepsInProtectRelax(ProtectRelaxTypes.Relaxed);

                                            if (_eGM.CellEnvEnt_CellEnvCom(x, y).HaveResources(ResourceTypes.Wood))
                                            {
                                                if (_eGM.CellUnitEnt_CellUnitCom(x, y).AmountStepsInProtectRelax(ProtectRelaxTypes.Relaxed) >= 1)
                                                {
                                                    CellBuildingWorker.SetPlayerBuilding(true, BuildingTypes.Woodcutter, _eGM.CellUnitEnt_CellOwnerCom(x, y).Owner, x, y);
                                                }
                                            }
                                            else
                                            {
                                                _eGM.CellEnvEnt_CellEnvCom(x, y).ResetEnvironment(EnvironmentTypes.AdultForest);
                                            }
                                        }
                                    }

                                    else
                                    {
                                        _eGM.CellUnitEnt_ProtectRelaxCom(x, y).ProtectRelaxType = ProtectRelaxTypes.Protected;
                                    }
                                }

                                else
                                {
                                    _eGM.CellUnitEnt_ProtectRelaxCom(x, y).ProtectRelaxType = ProtectRelaxTypes.Protected;
                                }
                            }

                        }
                        else
                        {
                            switch (unitType)
                            {
                                case UnitTypes.King:
                                    _eGM.CellUnitEnt_CellUnitCom(x, y).AddAmountHealth(UnitValues.HEALTH_FOR_ADDING_KING);
                                    if (_eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth > UnitValues.STANDART_AMOUNT_HEALTH_KING)
                                        _eGM.CellUnitEnt_CellUnitCom(x, y).SetAmountHealth(UnitValues.STANDART_AMOUNT_HEALTH_KING);
                                    break;

                                case UnitTypes.Pawn:
                                    _eGM.CellUnitEnt_CellUnitCom(x, y).AddAmountHealth(UnitValues.HEALTH_FOR_ADDING_PAWN);
                                    if (_eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth > CellUnitWorker.MaxAmountHealth(unitType))
                                        _eGM.CellUnitEnt_CellUnitCom(x, y).SetAmountHealth(CellUnitWorker.MaxAmountHealth(unitType));
                                    break;

                                case UnitTypes.PawnSword:
                                    _eGM.CellUnitEnt_CellUnitCom(x, y).AddAmountHealth(UnitValues.HEALTH_FOR_ADDING_PAWN_SWORD);
                                    if (_eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth > CellUnitWorker.MaxAmountHealth(unitType))
                                        _eGM.CellUnitEnt_CellUnitCom(x, y).SetAmountHealth(CellUnitWorker.MaxAmountHealth(unitType));
                                    break;

                                case UnitTypes.Rook:
                                    _eGM.CellUnitEnt_CellUnitCom(x, y).AddAmountHealth(UnitValues.HEALTH_FOR_ADDING_ROOK);
                                    if (_eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth > CellUnitWorker.MaxAmountHealth(unitType))
                                        _eGM.CellUnitEnt_CellUnitCom(x, y).SetAmountHealth(CellUnitWorker.MaxAmountHealth(unitType));
                                    break;

                                case UnitTypes.RookCrossbow:
                                    _eGM.CellUnitEnt_CellUnitCom(x, y).AddAmountHealth(UnitValues.HEALTH_FOR_ADDING_ROOK_CROSSBOW);
                                    if (_eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth > CellUnitWorker.MaxAmountHealth(unitType))
                                        _eGM.CellUnitEnt_CellUnitCom(x, y).SetAmountHealth(CellUnitWorker.MaxAmountHealth(unitType));
                                    break;

                                case UnitTypes.Bishop:
                                    _eGM.CellUnitEnt_CellUnitCom(x, y).AddAmountHealth(UnitValues.HEALTH_FOR_ADDING_BISHOP);
                                    if (_eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth > CellUnitWorker.MaxAmountHealth(unitType))
                                        _eGM.CellUnitEnt_CellUnitCom(x, y).SetAmountHealth(CellUnitWorker.MaxAmountHealth(unitType));
                                    break;

                                case UnitTypes.BishopCrossbow:
                                    _eGM.CellUnitEnt_CellUnitCom(x, y).AddAmountHealth(UnitValues.HEALTH_FOR_ADDING_BISHOP_CROSSBOW);
                                    if (_eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth > CellUnitWorker.MaxAmountHealth(unitType))
                                        _eGM.CellUnitEnt_CellUnitCom(x, y).SetAmountHealth(CellUnitWorker.MaxAmountHealth(unitType));
                                    break;

                                default:
                                    break;
                            }
                        }
                    }

                    else
                    {
                        _eGM.CellUnitEnt_CellUnitCom(x, y).ResetAmountStepsInProtectRelax(ProtectRelaxTypes.Relaxed);

                        if (_eGM.CellUnitEnt_CellUnitCom(x, y).HaveMaxSteps(unitType))
                        {
                            _eGM.CellUnitEnt_ProtectRelaxCom(x, y).ProtectRelaxType = ProtectRelaxTypes.Protected;
                        }
                    }

                    _eGM.CellUnitEnt_CellUnitCom(x, y).RefreshAmountSteps(unitType);
                }


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
                            minus = EconomyValues.BENEFIT_FOOD_FARM * _eGM.BuildingsEnt_UpgradeBuildingsCom.AmountUpgrades(BuildingTypes.Farm, _eGM.CellBuildEnt_OwnerCom(x, y).IsMasterClient);

                            _eGM.CellEnvEnt_CellEnvCom(x, y).TakeAmountResources(ResourceTypes.Food, minus);
                            _eGM.EconomyEnt_EconomyCom.AddAmountResources(ResourceTypes.Food, _eGM.CellBuildEnt_OwnerCom(x, y).IsMasterClient, minus);

                            if (!_eGM.CellEnvEnt_CellEnvCom(x, y).HaveResources(ResourceTypes.Food))
                            {
                                _eGM.CellEnvEnt_CellEnvCom(x, y).ResetEnvironment(EnvironmentTypes.Fertilizer);
                                CellBuildingWorker.ResetBuilding(true, x, y);
                            }
                            break;

                        case BuildingTypes.Woodcutter:
                            minus = EconomyValues.BENEFIT_WOOD_WOODCUTTER * _eGM.BuildingsEnt_UpgradeBuildingsCom.AmountUpgrades(BuildingTypes.Woodcutter, _eGM.CellBuildEnt_OwnerCom(x, y).IsMasterClient);

                            _eGM.CellEnvEnt_CellEnvCom(x, y).TakeAmountResources(ResourceTypes.Wood, minus);
                            _eGM.EconomyEnt_EconomyCom.AddAmountResources(ResourceTypes.Wood, _eGM.CellBuildEnt_OwnerCom(x, y).IsMasterClient, minus);

                            if (!_eGM.CellEnvEnt_CellEnvCom(x, y).HaveResources(ResourceTypes.Wood))
                            {
                                _eGM.CellEnvEnt_CellEnvCom(x, y).ResetEnvironment(EnvironmentTypes.AdultForest);
                                CellBuildingWorker.ResetBuilding(true, x, y);

                                if (_eGM.CellEffectEnt_CellEffectCom(x, y).HaveEffect(EffectTypes.Fire))
                                    _eGM.CellEffectEnt_CellEffectCom(x, y).ResetEffect(EffectTypes.Fire);

                            }
                            break;

                        case BuildingTypes.Mine:
                            minus = EconomyValues.BENEFIT_ORE_MINE * _eGM.BuildingsEnt_UpgradeBuildingsCom.AmountUpgrades(BuildingTypes.Mine, _eGM.CellBuildEnt_OwnerCom(x, y).IsMasterClient);

                            _eGM.CellEnvEnt_CellEnvCom(x, y).TakeAmountResources(ResourceTypes.Ore, minus);
                            _eGM.EconomyEnt_EconomyCom.AddAmountResources(ResourceTypes.Ore, _eGM.CellBuildEnt_OwnerCom(x, y).IsMasterClient, minus);

                            if (_eGM.CellBuildEnt_CellBuilCom(x, y).TimeStepsBuilding(BuildingTypes.Mine) >= 10 || !_eGM.CellEnvEnt_CellEnvCom(x, y).HaveResources(ResourceTypes.Ore))
                            {
                                CellBuildingWorker.ResetBuilding(true, x, y);
                                _eGM.CellBuildEnt_CellBuilCom(x, y).SetTimeStepsBuilding(BuildingTypes.Mine, 0);
                            }

                            _eGM.CellBuildEnt_CellBuilCom(x, y).AddTimeStepsBuilding(BuildingTypes.Mine, minus);
                            break;

                        default:
                            break;
                    }
                }

            }
        }

        _eGM.DonerUIEnt_IsActivatedDictCom.SetActivated(true, false);
        _eGM.DonerUIEnt_IsActivatedDictCom.SetActivated(false, false);

        _eGM.MotionEnt_AmountCom.AddAmount();





        if (0 > _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, true))
        {
            ++_amountMotionsWithoutFoodForTruce[true];

            _eGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Food, true, 0);
        }
        else
        {
            _amountMotionsWithoutFoodForTruce[true] = 0;
            _amountMotionsWithoutFood[true] = 0;
            _countForResetUnitMaster = 2;
        }



        if (0 > _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, false))
        {
            ++_amountMotionsWithoutFoodForTruce[false];

            _eGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Food, false, 0);


        }
        else
        {
            _amountMotionsWithoutFoodForTruce[false] = 0;
            _amountMotionsWithoutFood[false] = 0;
            _countForResetUnitOther = 2;
        }

        int amountAdultForest = 0;

        for (int x = 0; x < _eGM.Xamount; x++)
            for (int y = 0; y < _eGM.Yamount; y++)
            {
                if (_eGM.CellEnvEnt_CellEnvCom(x, y).HaveEnvironment(EnvironmentTypes.AdultForest)
                    && _eGM.CellEnt_CellBaseCom(x, y).IsActiveSelfGO)
                {
                    ++amountAdultForest;
                }
            }

        if (amountAdultForest <= 3)
        {
            _photonPunRPC.SoundToGeneral(RpcTarget.All, SoundEffectTypes.Truce);
            _sMM.TryInvokeRunSystem(nameof(TruceMasterSystem), _sMM.RpcSystems);
        }



        if (_amountMotionsWithoutFoodForTruce[true] >= 2 && _amountMotionsWithoutFoodForTruce[false] >= 2)
        {
            _photonPunRPC.SoundToGeneral(RpcTarget.All, SoundEffectTypes.Truce);

            _sMM.TryInvokeRunSystem(nameof(TruceMasterSystem), _sMM.RpcSystems);

            _amountMotionsWithoutFoodForTruce[true] = 0;
            _amountMotionsWithoutFoodForTruce[false] = 0;
        }
        else
        {
            if (++_amountMotionsWithoutFood[true] >= _countForResetUnitMaster)
            {
                var isResetedUnit = false;
                for (int x = 0; x < _eGM.Xamount; x++)
                {
                    for (int y = 0; y < _eGM.Yamount; y++)
                    {
                        if (_eGM.CellUnitEnt_UnitTypeCom(x, y).HaveAnyUnit)
                        {
                            if (_eGM.CellUnitEnt_CellOwnerCom(x, y).HaveOwner)
                            {
                                if (_eGM.CellUnitEnt_CellOwnerCom(x, y).IsMasterClient)
                                {
                                    if (_eGM.CellUnitEnt_UnitTypeCom(x, y).UnitType != UnitTypes.King)
                                    {
                                        CellUnitWorker.ResetPlayerUnit(x, y);
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

            if (++_amountMotionsWithoutFood[false] >= _countForResetUnitOther)
            {
                var isResetedUnit = false;
                for (int x = 0; x < _eGM.Xamount; x++)
                {
                    for (int y = 0; y < _eGM.Yamount; y++)
                    {
                        if (_eGM.CellUnitEnt_UnitTypeCom(x, y).HaveAnyUnit)
                        {
                            if (_eGM.CellUnitEnt_CellOwnerCom(x, y).HaveOwner)
                            {
                                if (!_eGM.CellUnitEnt_CellOwnerCom(x, y).IsMasterClient)
                                {
                                    if (_eGM.CellUnitEnt_UnitTypeCom(x, y).UnitType != UnitTypes.King)
                                    {
                                        CellUnitWorker.ResetPlayerUnit(x, y);

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
}
