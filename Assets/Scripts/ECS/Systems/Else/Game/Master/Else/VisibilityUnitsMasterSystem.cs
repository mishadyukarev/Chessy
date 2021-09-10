using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Game.General.Components;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Leopotam.Ecs;
using Photon.Pun;
using System.Collections.Generic;

internal sealed class VisibilityUnitsMasterSystem : IEcsRunSystem
{
    private EcsFilter<XyCellComponent> _xyCellFilter = default;
    private EcsFilter<CellEnvironDataCom> _cellEnvFilter = default;
    private EcsFilter<CellUnitDataComponent, OwnerComponent, OwnerBotComponent> _cellUnitFilter;

    public void Run()
    {
        foreach (byte idxCurCell in _cellUnitFilter)
        {
            var xy = _xyCellFilter.GetXyCell(idxCurCell);

            ref var curUnitDataCom = ref _cellUnitFilter.Get1(idxCurCell);
            ref var curOwnerUnitCom = ref _cellUnitFilter.Get2(idxCurCell);

            ref var curEnvDataCom = ref _cellEnvFilter.Get1(idxCurCell);


            curUnitDataCom.SetIsVisibleUnit(true, true);
            curUnitDataCom.SetIsVisibleUnit(false, true);


            if (curUnitDataCom.HaveUnit)
            {
                if (curOwnerUnitCom.HaveOwner)
                {
                    if (curOwnerUnitCom.IsHim(PhotonNetwork.MasterClient))
                    {
                        if (curEnvDataCom.HaveEnvironment(EnvironmentTypes.AdultForest))
                        {
                            curUnitDataCom.SetIsVisibleUnit(false, false);

                            var list = CellSpaceSupport.TryGetXyAround(xy);

                            foreach (var xy_1 in list)
                            {
                                var idxCell_1 = _xyCellFilter.GetIdxCell(xy_1);

                                ref var unitDataCom = ref _cellUnitFilter.Get1(idxCell_1);
                                ref var ownerUnitCom = ref _cellUnitFilter.Get2(idxCell_1);

                                if (unitDataCom.HaveUnit)
                                {
                                    if (ownerUnitCom.HaveOwner)
                                    {
                                        if (!ownerUnitCom.IsHim(PhotonNetwork.MasterClient))
                                        {
                                            curUnitDataCom.SetIsVisibleUnit(false, true);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (curEnvDataCom.HaveEnvironment(EnvironmentTypes.AdultForest))
                        {
                            curUnitDataCom.SetIsVisibleUnit(true, false);

                            var list = CellSpaceSupport.TryGetXyAround(xy);

                            foreach (var xy_1 in list)
                            {
                                var idxCell_1 = _xyCellFilter.GetIdxCell(xy_1);

                                ref var unitDataCom = ref _cellUnitFilter.Get1(idxCell_1);
                                ref var ownerUnitCom = ref _cellUnitFilter.Get2(idxCell_1);

                                if (unitDataCom.HaveUnit)
                                {
                                    if (ownerUnitCom.HaveOwner)
                                    {
                                        if (!ownerUnitCom.IsHim(PhotonNetwork.MasterClient))
                                        {
                                            curUnitDataCom.SetIsVisibleUnit(true, true);
                                            break;
                                        }
                                    }
                                }
                            }
                        }


                        //if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                        //{
                        //    CellUnitsDataSystem.SetIsVisibleUnit(true, false, xy);

                        //    List<int[]> list = TryGetXyAround(xy);
                        //    foreach (var xy1 in list)
                        //    {
                        //        if (CellUnitsDataSystem.HaveAnyUnit(xy1))
                        //        {
                        //            if (CellUnitsDataSystem.HaveOwner(xy1))
                        //            {
                        //                if (CellUnitsDataSystem.IsHim(PhotonNetwork.MasterClient, xy1))
                        //                {
                        //                    CellUnitsDataSystem.SetIsVisibleUnit(true, true, xy);
                        //                    break;
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                    }
                }

                else if (_cellUnitFilter.Get3(idxCurCell).IsBot)
                {
                    if (curEnvDataCom.HaveEnvironment(EnvironmentTypes.AdultForest))
                    {
                        curUnitDataCom.SetIsVisibleUnit(true, false);

                        var list = CellSpaceSupport.TryGetXyAround(xy);

                        foreach (var xy_1 in list)
                        {
                            var idxCell_1 = _xyCellFilter.GetIdxCell(xy_1);

                            ref var unitDataCom_1 = ref _cellUnitFilter.Get1(idxCell_1);
                            ref var ownerUnitCom_1 = ref _cellUnitFilter.Get2(idxCell_1);

                            if (unitDataCom_1.HaveUnit)
                            {
                                if (ownerUnitCom_1.HaveOwner)
                                {
                                    curUnitDataCom.SetIsVisibleUnit(true, true);
                                    break;
                                }
                            }
                        }
                    }

                    //if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                    //{
                    //    CellUnitsDataSystem.SetIsVisibleUnit(true, false, xy);

                    //    List<int[]> list = TryGetXyAround(xy);
                    //    foreach (var xy1 in list)
                    //    {
                    //        if (CellUnitsDataSystem.HaveAnyUnit(xy1))
                    //        {
                    //            if (CellUnitsDataSystem.HaveOwner(xy1))
                    //            {
                    //                if (CellUnitsDataSystem.IsHim(PhotonNetwork.MasterClient, xy1))
                    //                {
                    //                    CellUnitsDataSystem.SetIsVisibleUnit(true, true, xy);
                    //                    break;
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                }
            }
        }
    }
}