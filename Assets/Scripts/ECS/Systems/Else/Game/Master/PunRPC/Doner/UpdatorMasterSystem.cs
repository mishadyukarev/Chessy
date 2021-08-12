using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.Game;
using Assets.Scripts.ECS.Component.View.Else.Game.General.Cell;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Assets.Scripts.Workers.Game.Else.Economy;
using Leopotam.Ecs;
using Photon.Pun;
using System;

internal sealed class UpdatorMasterSystem : IEcsRunSystem
{
    private EcsFilter<XyCellComponent> _xyCellFilter = default;
    private EcsFilter<CellViewComponent> _cellViewFilter = default;
    private EcsFilter<CellUnitDataComponent, OwnerComponent> _cellUnitFilter = default;
    private EcsFilter<CellFireDataComponent> _cellFireDataFilter = default;
    private EcsFilter<CellEnvironDataCom> _cellEnvDataFilter = default;
    private EcsFilter<CellBuildDataComponent, OwnerComponent> _cellBuildDataFilter = default;

    private EcsFilter<UpgradesBuildingsComponent> _upgradeBuildsFilter = default;
    private EcsFilter<InventorResourcesComponent> _invResFilt = default;
    private EcsFilter<MotionsDataUIComponent> _motionsUIFilter = default;
    private EcsFilter<DonerDataUIComponent> _donerUIFilter = default;

    private EcsFilter<UnitsInGameInfoComponent> _idxUnitsFilter = default;
    private EcsFilter<UnitsInConditionInGameCom> _idxUnitsInCondFilter = default;
    private EcsFilter<BuildsInGameComponent> _idxBuildsFilter = default;

    public void Run()
    {
        ref var inventorResCom = ref _invResFilt.Get1(0);

        ref var idxUnitsCom = ref _idxUnitsFilter.Get1(0);
        ref var idxUnitsInCondCom = ref _idxUnitsInCondFilter.Get1(0);
        ref var idxBuildsCom = ref _idxBuildsFilter.Get1(0);

        ref var amountUpgradesCom = ref _upgradeBuildsFilter.Get1(0);

        int minus;
        bool isMasterKey;

        for (byte isMasterByte = 0; isMasterByte <= 1; isMasterByte++)
        {
            isMasterKey = true;
            if (isMasterByte == 1) isMasterKey = false;


            for (byte idxCellList = 0; idxCellList < idxBuildsCom.GetAmountBuild(BuildingTypes.Farm, isMasterKey); idxCellList++)
            {
                var curIdxCell = idxBuildsCom.GetIdxCellByIndexList(BuildingTypes.Farm, isMasterKey, idxCellList);

                ref var cellEnvrDataCom = ref _cellEnvDataFilter.Get1(curIdxCell);
                ref var cellBuildDataCom = ref _cellBuildDataFilter.Get1(curIdxCell);

                minus = ExtractionInfoSupport.GetExtractionOneBuilding(BuildingTypes.Farm, amountUpgradesCom.GetAmountUpgrades(BuildingTypes.Farm, isMasterKey));

                cellEnvrDataCom.TakeAmountResources(EnvironmentTypes.Fertilizer, minus);
                inventorResCom.AddAmountResources(ResourceTypes.Food, isMasterKey, minus);

                if (!cellEnvrDataCom.HaveResources(EnvironmentTypes.Fertilizer))
                {
                    cellEnvrDataCom.ResetEnvironment(EnvironmentTypes.Fertilizer);

                    idxBuildsCom.RemoveIdxBuild(BuildingTypes.Farm, isMasterKey, curIdxCell);

                    cellBuildDataCom.BuildingType = default;
                    _cellBuildDataFilter.Get2(curIdxCell).ResetOwner();
                }
            }


            for (byte idxList = 0; idxList < idxBuildsCom.GetAmountBuild(BuildingTypes.Woodcutter, isMasterKey); idxList++)
            {
                var curIdxCell = idxBuildsCom.GetIdxCellByIndexList(BuildingTypes.Woodcutter, isMasterKey, idxList);

                ref var curCellEnvDataCom = ref _cellEnvDataFilter.Get1(curIdxCell);
                ref var curCellBuildDataCom = ref _cellBuildDataFilter.Get1(curIdxCell);
                ref var curCellFireDataCom = ref _cellFireDataFilter.Get1(curIdxCell);

                minus = ExtractionInfoSupport.GetExtractionOneBuilding(BuildingTypes.Woodcutter, amountUpgradesCom.GetAmountUpgrades(BuildingTypes.Woodcutter, isMasterKey));

                curCellEnvDataCom.TakeAmountResources(EnvironmentTypes.AdultForest, minus);
                inventorResCom.AddAmountResources(ResourceTypes.Wood, isMasterKey, minus);

                if (!curCellEnvDataCom.HaveResources(EnvironmentTypes.AdultForest))
                {
                    curCellEnvDataCom.ResetEnvironment(EnvironmentTypes.AdultForest);

                    idxBuildsCom.RemoveIdxBuild(BuildingTypes.Woodcutter, isMasterKey, curIdxCell);
                    curCellBuildDataCom.ResetBuildType();

                    if (curCellFireDataCom.HaveFire)
                    {
                        curCellFireDataCom.HaveFire = false;
                    }
                }
            }


            for (byte idxList = 0; idxList < idxBuildsCom.GetAmountBuild(BuildingTypes.Mine, isMasterKey); idxList++)
            {
                var curIdxCell = idxBuildsCom.GetIdxCellByIndexList(BuildingTypes.Mine, isMasterKey, idxList);

                ref var curCellEnvDataCom = ref _cellEnvDataFilter.Get1(curIdxCell);
                ref var curCellBuildDataCom = ref _cellBuildDataFilter.Get1(curIdxCell);
                ref var curOwnerCellBuildCom = ref _cellBuildDataFilter.Get2(curIdxCell);


                minus = ExtractionInfoSupport.GetExtractionOneBuilding(BuildingTypes.Mine, amountUpgradesCom.GetAmountUpgrades(BuildingTypes.Mine, isMasterKey));

                curCellEnvDataCom.TakeAmountResources(EnvironmentTypes.Hill, minus);
                inventorResCom.AddAmountResources(ResourceTypes.Ore, isMasterKey, minus);

                if (!curCellEnvDataCom.HaveResources(EnvironmentTypes.Hill))
                {
                    if (curOwnerCellBuildCom.HaveOwner)
                    {
                        idxBuildsCom.RemoveIdxBuild(BuildingTypes.Mine, isMasterKey, curIdxCell);
                    }
                    curCellBuildDataCom.ResetBuildType();
                }
            }


            for (UnitTypes curUnitType = UnitTypes.King; (byte)curUnitType < Enum.GetNames(typeof(UnitTypes)).Length; curUnitType++)
            {
                var curConditionType = ConditionUnitTypes.Relaxed;

                for (int idxCell = 0; idxCell < idxUnitsInCondCom.GetAmountUnitsInCondition(curConditionType, curUnitType, isMasterKey); idxCell++)
                {
                    var curIdxCell = idxUnitsInCondCom.GetIdxInConditionByIndex(curConditionType, curUnitType, isMasterKey, idxCell);

                    ref var curCellUnitDataCom = ref _cellUnitFilter.Get1(curIdxCell);
                    ref var curOwnerCellUnitCom = ref _cellUnitFilter.Get2(curIdxCell);
                    ref var curCellBuildDataCom = ref _cellBuildDataFilter.Get1(curIdxCell);
                    ref var curOwnerCellBuildCom = ref _cellBuildDataFilter.Get2(curIdxCell);
                    ref var curCellFireDataCom = ref _cellFireDataFilter.Get1(curIdxCell);
                    ref var curCellEnvDataCom = ref _cellEnvDataFilter.Get1(curIdxCell);

                    if (curCellFireDataCom.HaveFire)
                    {
                        idxUnitsInCondCom.ReplaceCondition(curConditionType, ConditionUnitTypes.None, curUnitType, isMasterKey, curIdxCell);
                        curCellUnitDataCom.ConditionType = ConditionUnitTypes.None;
                    }

                    else
                    {
                        if (curCellUnitDataCom.AmountHealth == curCellUnitDataCom.MaxAmountHealth)
                        {
                            if (curUnitType == UnitTypes.Pawn)
                            {
                                if (curCellEnvDataCom.HaveEnvironment(EnvironmentTypes.AdultForest))
                                {
                                    inventorResCom.AddAmountResources(ResourceTypes.Wood, isMasterKey);
                                    curCellEnvDataCom.TakeAmountResources(EnvironmentTypes.AdultForest);

                                    if (curCellBuildDataCom.HaveBuild)
                                    {
                                        if (curCellBuildDataCom.IsBuildType(BuildingTypes.Woodcutter))
                                        {
                                            if (!curCellEnvDataCom.HaveResources(EnvironmentTypes.AdultForest))
                                            {
                                                idxBuildsCom.RemoveIdxBuild(BuildingTypes.Woodcutter, isMasterKey, curIdxCell);

                                                curCellBuildDataCom.BuildingType = default;
                                                curOwnerCellBuildCom.ResetOwner();
                                            }
                                        }
                                        else
                                        {
                                            idxUnitsInCondCom.ReplaceCondition(curConditionType, ConditionUnitTypes.Protected, curUnitType, isMasterKey, curIdxCell);
                                            curCellUnitDataCom.ConditionType = ConditionUnitTypes.Protected;
                                        }
                                    }
                                    else
                                    {
                                        if (curCellEnvDataCom.HaveResources(EnvironmentTypes.AdultForest))
                                        {
                                            curCellBuildDataCom.BuildingType = BuildingTypes.Woodcutter;
                                            curOwnerCellBuildCom.SetOwner(curOwnerCellUnitCom.Owner);
                                            idxBuildsCom.AddIdxBuild(BuildingTypes.Woodcutter, isMasterKey, curIdxCell);
                                        }
                                        else
                                        {
                                            curCellEnvDataCom.ResetEnvironment(EnvironmentTypes.AdultForest);
                                        }
                                    }
                                }

                                else if (curCellEnvDataCom.HaveEnvironment(EnvironmentTypes.Hill))
                                {
                                    if (curCellEnvDataCom.GetAmountResources(EnvironmentTypes.Hill) < curCellEnvDataCom.MaxAmountResources(EnvironmentTypes.Hill))
                                    {
                                        curCellEnvDataCom.AddAmountResources(EnvironmentTypes.Hill);
                                    }
                                    else
                                    {
                                        curCellUnitDataCom.ConditionType = ConditionUnitTypes.Protected;
                                        idxUnitsInCondCom.ReplaceCondition(curConditionType, ConditionUnitTypes.Protected, curUnitType, isMasterKey, curIdxCell);
                                    }
                                }

                                else
                                {
                                    curCellUnitDataCom.ConditionType = ConditionUnitTypes.Protected;
                                    idxUnitsInCondCom.ReplaceCondition(curConditionType, ConditionUnitTypes.Protected, curUnitType, isMasterKey, curIdxCell);
                                }
                            }

                            else
                            {
                                idxUnitsInCondCom.ReplaceCondition(curConditionType, ConditionUnitTypes.Protected, curUnitType, isMasterKey, curIdxCell);
                                curCellUnitDataCom.ConditionType = ConditionUnitTypes.Protected;
                            }
                        }

                        else
                        {
                            curCellUnitDataCom.AddStandartHeal();
                            if (curCellUnitDataCom.AmountHealth > curCellUnitDataCom.MaxAmountHealth)
                            {
                                curCellUnitDataCom.AmountHealth = curCellUnitDataCom.MaxAmountHealth;
                            }
                        }
                    }
                }



                curConditionType = ConditionUnitTypes.None;

                for (int index = 0; index < idxUnitsInCondCom.GetAmountUnitsInCondition(curConditionType, curUnitType, isMasterKey); index++)
                {
                    var idxUnitInCond = idxUnitsInCondCom.GetIdxInConditionByIndex(curConditionType, curUnitType, isMasterKey, index);

                    ref var curCellUnitDataCom = ref _cellUnitFilter.Get1(idxUnitInCond);

                    if (curCellUnitDataCom.HaveMaxAmountSteps)
                    {
                        curCellUnitDataCom.ConditionType = ConditionUnitTypes.Protected;
                        idxUnitsInCondCom.ReplaceCondition(curConditionType, ConditionUnitTypes.Protected, curUnitType, isMasterKey, idxUnitInCond);
                    }
                }
            }

            var amountUnits = idxUnitsCom.GetAmountUnitsInGame(isMasterKey, new[]
            {
                UnitTypes.Pawn, UnitTypes.Rook, UnitTypes.RookCrossbow,
                UnitTypes.Bishop, UnitTypes.BishopCrossbow
            });

            inventorResCom.TakeAmountResources(ResourceTypes.Food, isMasterKey, amountUnits);
            inventorResCom.AddAmountResources(ResourceTypes.Food, isMasterKey);




            if (inventorResCom.GetAmountResources(ResourceTypes.Food, isMasterKey) < 0)
            {
                for (UnitTypes unitType = (UnitTypes)Enum.GetNames(typeof(UnitTypes)).Length - 1; unitType != UnitTypes.None; unitType--)
                {
                    amountUnits = idxUnitsCom.GetAmountUnitsInGame(unitType, isMasterKey);

                    if (amountUnits > 0)
                    {
                        var curIdxCell = idxUnitsCom.GetIdxUnitInGameByIndexList(unitType, isMasterKey, amountUnits - 1);

                        ref var curCellUnitDataCom = ref _cellUnitFilter.Get1(curIdxCell);

                        idxUnitsCom.RemoveAmountUnitsInGame(unitType, isMasterKey, curIdxCell);
                        idxUnitsInCondCom.RemoveUnitInCondition(curCellUnitDataCom.ConditionType, unitType, isMasterKey, curIdxCell);
                        curCellUnitDataCom.UnitType = default;
                        break;
                    }
                }

                inventorResCom.SetAmountResources(ResourceTypes.Food, isMasterKey, 0);
            }


            _donerUIFilter.Get1(0).SetDoned(isMasterKey, false);
        }

        foreach (byte curIdxCell in _xyCellFilter)
        {
            ref var curCellUnitDataCom = ref _cellUnitFilter.Get1(curIdxCell);
            ref var curOwnerCellUnitDataCom = ref _cellUnitFilter.Get2(curIdxCell);
            ref var curCellBuildDataCom = ref _cellBuildDataFilter.Get1(curIdxCell);
            ref var curOwnerBuildCom = ref _cellBuildDataFilter.Get2(curIdxCell);
            ref var curCellFireDataCom = ref _cellFireDataFilter.Get1(curIdxCell);
            ref var curCellEnvrDataCom = ref _cellEnvDataFilter.Get1(curIdxCell);

            if (curCellFireDataCom.HaveFire)
            {
                curCellEnvrDataCom.TakeAmountResources(EnvironmentTypes.AdultForest, 2);

                if (curCellUnitDataCom.HaveUnit)
                {
                    curCellUnitDataCom.TakeAmountHealth(40);

                    if (!curCellUnitDataCom.HaveAmountHealth)
                    {
                        idxUnitsCom.RemoveAmountUnitsInGame(curCellUnitDataCom.UnitType, curOwnerCellUnitDataCom.IsMasterClient, curIdxCell);
                        idxUnitsInCondCom.RemoveUnitInCondition(curCellUnitDataCom.ConditionType, curCellUnitDataCom.UnitType, curOwnerCellUnitDataCom.IsMasterClient, curIdxCell);

                        curCellUnitDataCom.ResetUnit();
                        curOwnerCellUnitDataCom.ResetOwner();
                    }
                }



                if (!curCellEnvrDataCom.HaveResources(EnvironmentTypes.AdultForest))
                {
                    if (curCellBuildDataCom.HaveBuild)
                    {
                        idxBuildsCom.RemoveIdxBuild(curCellBuildDataCom.BuildingType, curOwnerBuildCom.IsMasterClient, curIdxCell);
                        curCellBuildDataCom.BuildingType = default;
                        curOwnerBuildCom.ResetOwner();
                    }

                    curCellEnvrDataCom.ResetEnvironment(EnvironmentTypes.AdultForest);

                    curCellFireDataCom.HaveFire = false;


                    var aroundXYList = CellSpaceSupport.TryGetXyAround(_xyCellFilter.GetXyCell(curIdxCell));
                    foreach (var xy1 in aroundXYList)
                    {
                        var curIdxCell1 = _xyCellFilter.GetIndexCell(xy1);

                        if (_cellViewFilter.Get1(curIdxCell1).IsActiveParent)
                        {
                            if (_cellEnvDataFilter.Get1(curIdxCell1).HaveEnvironment(EnvironmentTypes.AdultForest))
                            {
                                _cellFireDataFilter.Get1(curIdxCell1).HaveFire = true;
                            }
                        }
                    }
                }
            }
        }



        foreach (var curIdxCell in _xyCellFilter)
        {
            if (_cellUnitFilter.Get1(curIdxCell).HaveUnit)
            {
                _cellUnitFilter.Get1(curIdxCell).RefreshAmountSteps();
            }
        }

        _motionsUIFilter.Get1(0).AmountMotions += 1;


        int amountAdultForest = 0;

        foreach (var curIdxCell in _xyCellFilter)
        {
            ref var curCellEnvDataCom = ref _cellEnvDataFilter.Get1(curIdxCell);
            ref var curCellViewCom =ref _cellViewFilter.Get1(curIdxCell);

            if (curCellEnvDataCom.HaveEnvironment(EnvironmentTypes.AdultForest) && curCellViewCom.IsActiveParent)
            {
                ++amountAdultForest;
            }
        }

        if (amountAdultForest <= 3)
        {
            RPCGameSystem.SoundToGeneral(RpcTarget.All, SoundEffectTypes.Truce);
            GameMasterSystemManager.TruceSystems.Run();
        }
    }
}

