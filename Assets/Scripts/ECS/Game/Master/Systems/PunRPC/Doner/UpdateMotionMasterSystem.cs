using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.Static;
using Assets.Scripts.Static.Cell;
using Photon.Pun;
using System.Collections.Generic;
using static Assets.Scripts.Main;
using static Assets.Scripts.CellEnvironmentWorker;
using static Assets.Scripts.CellUnitWorker;

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
                var xy = new int[] { x, y };

                if (HaveAnyUnit(xy))
                {
                    var unitType = UnitType(xy);

                    if (HaveOwner(xy))
                    {
                        switch (UnitType(xy))
                        {
                            case UnitTypes.None:
                                break;

                            case UnitTypes.King:
                                break;

                            case UnitTypes.Pawn:
                                _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, IsMasterClient(xy));
                                break;

                            case UnitTypes.PawnSword:
                                _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, IsMasterClient(xy));
                                break;

                            case UnitTypes.Rook:
                                _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, IsMasterClient(xy));
                                break;

                            case UnitTypes.RookCrossbow:
                                _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, IsMasterClient(xy));
                                break;

                            case UnitTypes.Bishop:
                                _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, IsMasterClient(xy));
                                break;

                            case UnitTypes.BishopCrossbow:
                                _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, IsMasterClient(xy));
                                break;

                            default:
                                break;
                        }
                    }

                    if (IsTypeProtectRelax(ProtectRelaxTypes.Relaxed, xy))
                    {
                        if (AmountHealth(xy) == CellUnitWorker.MaxAmountHealth(unitType))
                        {
                            if (HaveOwner(xy))
                            {
                                if (unitType == UnitTypes.Pawn || unitType == UnitTypes.PawnSword)
                                {
                                    if (HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                                    {
                                        _eGM.EconomyEnt_EconomyCom.AddAmountResources(ResourceTypes.Wood, IsMasterClient(xy), 1);
                                        TakeAmountResources(ResourceTypes.Wood, xy);

                                        if (!HaveResources(ResourceTypes.Wood, xy))
                                        {
                                            ResetEnvironment(EnvironmentTypes.AdultForest, xy);
                                        }

                                        if (_eGM.CellBuildEnt_BuilTypeCom(x, y).HaveBuilding)
                                        {
                                            ResetAmountStepsInProtectRelax(ProtectRelaxTypes.Relaxed, xy);
                                        }
                                        else
                                        {
                                            AddAmountStepsInProtectRelax(ProtectRelaxTypes.Relaxed, xy);

                                            if (HaveResources(ResourceTypes.Wood, xy))
                                            {
                                                if (AmountStepsInProtectRelax(ProtectRelaxTypes.Relaxed, xy) >= 1)
                                                {
                                                    CellBuildingWorker.SetPlayerBuilding(true, BuildingTypes.Woodcutter, Owner(xy), x, y);
                                                }
                                            }
                                            else
                                            {
                                                ResetEnvironment(EnvironmentTypes.AdultForest, xy);
                                            }
                                        }
                                    }

                                    else
                                    {
                                        SetProtectRelaxType(ProtectRelaxTypes.Protected, xy);
                                    }
                                }

                                else
                                {
                                    SetProtectRelaxType(ProtectRelaxTypes.Protected, xy);
                                }
                            }

                        }
                        else
                        {
                            switch (unitType)
                            {
                                case UnitTypes.King:
                                    AddAmountHealth(xy, UnitValues.HEALTH_FOR_ADDING_KING);
                                    if (AmountHealth(xy) > UnitValues.STANDART_AMOUNT_HEALTH_KING)
                                        SetAmountHealth(UnitValues.STANDART_AMOUNT_HEALTH_KING, xy);
                                    break;

                                case UnitTypes.Pawn:
                                    AddAmountHealth(xy, UnitValues.HEALTH_FOR_ADDING_PAWN);
                                    if (AmountHealth(xy) > MaxAmountHealth(unitType))
                                        SetAmountHealth(MaxAmountHealth(unitType), xy);
                                    break;

                                case UnitTypes.PawnSword:
                                    AddAmountHealth(xy, UnitValues.HEALTH_FOR_ADDING_PAWN_SWORD);
                                    if (AmountHealth(xy) > MaxAmountHealth(unitType))
                                        SetAmountHealth(MaxAmountHealth(unitType), xy);
                                    break;

                                case UnitTypes.Rook:
                                    AddAmountHealth(xy, UnitValues.HEALTH_FOR_ADDING_ROOK);
                                    if (AmountHealth(xy) > MaxAmountHealth(unitType))
                                        SetAmountHealth(MaxAmountHealth(unitType), xy);
                                    break;

                                case UnitTypes.RookCrossbow:
                                    AddAmountHealth(xy, UnitValues.HEALTH_FOR_ADDING_ROOK_CROSSBOW);
                                    if (AmountHealth(xy) > MaxAmountHealth(unitType))
                                        SetAmountHealth(MaxAmountHealth(unitType), xy);
                                    break;

                                case UnitTypes.Bishop:
                                    AddAmountHealth(xy, UnitValues.HEALTH_FOR_ADDING_BISHOP);
                                    if (AmountHealth(xy) > MaxAmountHealth(unitType))
                                        SetAmountHealth(MaxAmountHealth(unitType), xy);
                                    break;

                                case UnitTypes.BishopCrossbow:
                                    AddAmountHealth(xy, UnitValues.HEALTH_FOR_ADDING_BISHOP_CROSSBOW);
                                    if (AmountHealth(xy) > MaxAmountHealth(unitType))
                                        SetAmountHealth(MaxAmountHealth(unitType), xy);
                                    break;

                                default:
                                    break;
                            }
                        }
                    }

                    else
                    {
                        ResetAmountStepsInProtectRelax(ProtectRelaxTypes.Relaxed, xy);

                        if (HaveMaxAmountSteps(unitType, xy))
                        {
                            SetProtectRelaxType(ProtectRelaxTypes.Protected, xy);
                        }
                    }

                    RefreshAmountSteps(unitType, xy);
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

                            TakeAmountResources(ResourceTypes.Food, xy, minus);
                            _eGM.EconomyEnt_EconomyCom.AddAmountResources(ResourceTypes.Food, _eGM.CellBuildEnt_OwnerCom(x, y).IsMasterClient, minus);

                            if (!HaveResources(ResourceTypes.Food, xy))
                            {
                                ResetEnvironment(EnvironmentTypes.Fertilizer, xy);
                                CellBuildingWorker.ResetBuilding(true, x, y);
                            }
                            break;

                        case BuildingTypes.Woodcutter:
                            minus = EconomyValues.BENEFIT_WOOD_WOODCUTTER * _eGM.BuildingsEnt_UpgradeBuildingsCom.AmountUpgrades(BuildingTypes.Woodcutter, _eGM.CellBuildEnt_OwnerCom(x, y).IsMasterClient);

                            TakeAmountResources(ResourceTypes.Wood, xy, minus);
                            _eGM.EconomyEnt_EconomyCom.AddAmountResources(ResourceTypes.Wood, _eGM.CellBuildEnt_OwnerCom(x, y).IsMasterClient, minus);

                            if (!HaveResources(ResourceTypes.Wood, xy))
                            {
                                ResetEnvironment(EnvironmentTypes.AdultForest, xy);
                                CellBuildingWorker.ResetBuilding(true, x, y);

                                if (CellEffectsWorker.HaveEffect(EffectTypes.Fire, xy))
                                    CellEffectsWorker.ResetEffect(EffectTypes.Fire, xy);

                            }
                            break;

                        case BuildingTypes.Mine:
                            minus = EconomyValues.BENEFIT_ORE_MINE * _eGM.BuildingsEnt_UpgradeBuildingsCom.AmountUpgrades(BuildingTypes.Mine, _eGM.CellBuildEnt_OwnerCom(x, y).IsMasterClient);

                            TakeAmountResources(ResourceTypes.Ore, xy, minus);
                            _eGM.EconomyEnt_EconomyCom.AddAmountResources(ResourceTypes.Ore, _eGM.CellBuildEnt_OwnerCom(x, y).IsMasterClient, minus);

                            if (_eGM.CellBuildEnt_CellBuilCom(x, y).TimeStepsBuilding(BuildingTypes.Mine) >= 10 || !HaveResources(ResourceTypes.Ore, xy))
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
                var xy = new int[] { x, y };

                if (HaveEnvironment(EnvironmentTypes.AdultForest, xy)
                    && CellWorker.IsActiveSelfGO(xy))
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
                        var xy = new int[] { x, y };

                        if (HaveAnyUnit(xy))
                        {
                            if (HaveOwner(xy))
                            {
                                if (IsMasterClient(xy))
                                {
                                    if (!IsUnitType(UnitTypes.King, xy))
                                    {
                                        CellUnitWorker.ResetPlayerUnit(xy);
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
                        var xy = new int[] { x, y };

                        if (HaveAnyUnit(xy))
                        {
                            if (HaveOwner(xy))
                            {
                                if (!IsMasterClient(xy))
                                {
                                    if (!IsUnitType(UnitTypes.King, xy))
                                    {
                                        CellUnitWorker.ResetPlayerUnit(xy);

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
