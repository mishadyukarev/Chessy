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

    private EcsFilter<IdxUnitsComponent> _idxUnitsFilter = default;
    private EcsFilter<IdxUnitsInConditionCom> _idxUnitsInCondFilter = default;
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


            foreach (var curIdxCell in idxBuildsCom.GetListIdxBuild(BuildingTypes.Farm, isMasterKey))
            {
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





            //        for (int xyIndex = 0; xyIndex < MainGameSystem.XyBuildingsCom.GetAmountBuild(BuildingTypes.Woodcutter, isMasterKey); xyIndex++)
            //        {
            //            var xy = MainGameSystem.XyBuildingsCom.GetXyBuildByIndex(BuildingTypes.Woodcutter, isMasterKey, xyIndex);

            //            minus = ExtractionInfoSupport.GetExtractionOneBuilding(BuildingTypes.Woodcutter, GetAmountUpgrades(BuildingTypes.Woodcutter, isMasterKey));

            //            CellEnvrDataSystem.TakeAmountResources(EnvironmentTypes.AdultForest, xy, minus);
            //            inventorResCom.AddAmountResources(ResourceTypes.Wood, isMasterKey, minus);

            //            if (!CellEnvrDataSystem.HaveResources(EnvironmentTypes.AdultForest, xy))
            //            {
            //                CellEnvrDataSystem.ResetEnvironment(EnvironmentTypes.AdultForest, xy);

            //                MainGameSystem.XyBuildingsCom.RemoveXyBuild(BuildingTypes.Woodcutter, isMasterKey, xyIndex);
            //                CellBuildDataSystem.ResetBuild(xy);

            //                if (CellFireDataSystem.HaveFireCom(xy).HaveFire)
            //                {
            //                    CellFireDataSystem.HaveFireCom(xy).Disable();
            //                }
            //            }
            //        }


            //        for (int xyIndex = 0; xyIndex < MainGameSystem.XyBuildingsCom.GetAmountBuild(BuildingTypes.Mine, isMasterKey); xyIndex++)
            //        {
            //            var xy = MainGameSystem.XyBuildingsCom.GetXyBuildByIndex(BuildingTypes.Mine, isMasterKey, xyIndex);

            //            minus = ExtractionInfoSupport.GetExtractionOneBuilding(BuildingTypes.Mine, GetAmountUpgrades(BuildingTypes.Mine, isMasterKey));

            //            CellEnvrDataSystem.TakeAmountResources(EnvironmentTypes.Hill, xy, minus);
            //            inventorResCom.AddAmountResources(ResourceTypes.Ore, isMasterKey, minus);


            //            CellBuildDataSystem.TimeStepsCom(xy).Add(minus);

            //            if (CellBuildDataSystem.TimeStepsCom(xy).TimeSteps > 9
            //                || !CellEnvrDataSystem.HaveResources(EnvironmentTypes.Hill, xy))
            //            {
            //                if (CellBuildDataSystem.OwnerCom(xy).HaveOwner)
            //                {
            //                    MainGameSystem.XyBuildingsCom.RemoveXyBuild(BuildingTypes.Mine, isMasterKey, xyIndex);
            //                }
            //                CellBuildDataSystem.ResetBuild(xy);

            //                CellBuildDataSystem.TimeStepsCom(xy).Reset();
            //            }
            //        }




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
                            if (curUnitType == UnitTypes.Pawn || curUnitType == UnitTypes.PawnSword)
                            {
                                if (curCellEnvDataCom.HaveEnvironment(EnvironmentTypes.AdultForest))
                                {
                                    inventorResCom.AddAmountResources(ResourceTypes.Wood, isMasterKey);
                                    curCellEnvDataCom.TakeAmountResources(EnvironmentTypes.AdultForest);

                                    if (curCellBuildDataCom.HaveBuild)
                                    {
                                        if (curCellBuildDataCom.IsBuildType(BuildingTypes.Woodcutter))
                                        {

                                        }
                                        else
                                        {
                                            idxUnitsInCondCom.ReplaceCondition(curConditionType, ConditionUnitTypes.Protected, curUnitType, isMasterKey, curIdxCell);
                                            curCellUnitDataCom.ConditionType = ConditionUnitTypes.Protected;
                                        }
                                    }
                                    else
                                    {
                                        curCellBuildDataCom.BuildingType = BuildingTypes.Woodcutter;
                                        curOwnerCellBuildCom.SetOwner(curOwnerCellUnitCom.Owner);
                                        idxBuildsCom.AddIdxBuild(BuildingTypes.Woodcutter, isMasterKey, curIdxCell);
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
                UnitTypes.Pawn, UnitTypes.PawnSword,
                UnitTypes.Rook, UnitTypes.RookCrossbow,
                UnitTypes.Bishop, UnitTypes.BishopCrossbow
            });

            inventorResCom.TakeAmountResources(ResourceTypes.Food, isMasterKey, amountUnits);
            inventorResCom.AddAmountResources(ResourceTypes.Food, isMasterKey);
        }





        //    TryRemoveUnit(true, ref xyUnitsCom, ref inventorResCom);
        //    TryRemoveUnit(false, ref xyUnitsCom, ref inventorResCom);


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
                        curCellUnitDataCom.ResetStandartValuesUnit();
                        curOwnerCellUnitDataCom.ResetOwner();

                        idxUnitsCom.RemoveAmountUnitsInGame(curCellUnitDataCom.UnitType, curOwnerCellUnitDataCom.IsMasterClient, curIdxCell);
                        idxUnitsInCondCom.RemoveUnitInCondition(curCellUnitDataCom.ConditionType, curCellUnitDataCom.UnitType, curOwnerCellUnitDataCom.IsMasterClient, curIdxCell);
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



        //    for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
        //        for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
        //        {
        //            var xy = new int[] { x, y };

        //            if (CellUnitsDataSystem.HaveAnyUnit(xy))
        //            {
        //                CellUnitsDataSystem.RefreshAmountSteps(xy);
        //            }
        //        }


        //    _donerUIFilter.Get1(0).SetDoned(true, false);
        //    _donerUIFilter.Get1(0).SetDoned(false, false);

        //    _motionsUIFilter.Get1(0).AmountMotions += 1;


        //    int amountAdultForest = 0;

        //    for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
        //        for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
        //        {
        //            var xy = new int[] { x, y };

        //            if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, xy)
        //                && CellViewSystem.IsActiveSelfParentCell(xy))
        //            {
        //                ++amountAdultForest;
        //            }
        //        }

        //    if (amountAdultForest <= 3)
        //    {
        //        RPCGameSystem.SoundToGeneral(RpcTarget.All, SoundEffectTypes.Truce);
        //        GameMasterSystemManager.TruceSystems.Run();
        //    }



        //}


        //private void TryRemoveUnit(bool isMaster, ref XyUnitsComponent xyUnitsCom, ref InventorResourcesComponent invResCom)
        //{
        //    if (invResCom.GetAmountResources(ResourceTypes.Food, isMaster) < 0)
        //    {
        //        for (UnitTypes unitType = (UnitTypes)Enum.GetNames(typeof(UnitTypes)).Length - 1; unitType != UnitTypes.None; unitType--)
        //        {
        //            var amountUnits = xyUnitsCom.GetAmountUnitsInGame(unitType, isMaster);

        //            if (amountUnits > 0)
        //            {
        //                var xyUnit = xyUnitsCom.GetXyUnitInGame(unitType, isMaster, amountUnits - 1);


        //                xyUnitsCom.RemoveAmountUnitsInGame(unitType, isMaster, xyUnit);
        //                MainGameSystem.XyUnitsContitionCom.RemoveUnitInCondition(CellUnitsDataSystem.ConditionType(xyUnit), unitType, isMaster, xyUnit);

        //                CellUnitsDataSystem.ResetUnit(xyUnit);
        //                break;
        //            }
        //        }

        //        invResCom.SetAmountResources(ResourceTypes.Food, isMaster, 0);
    }
}

