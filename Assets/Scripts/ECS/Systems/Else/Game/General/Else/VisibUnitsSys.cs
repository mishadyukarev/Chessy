using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.ECS.Game.General.Components;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class VisibUnitsSys : IEcsRunSystem
{
    private EcsFilter<XyCellComponent> _xyCellFilter = default;
    private EcsFilter<CellEnvironDataCom> _cellEnvFilter = default;
    private EcsFilter<CellUnitDataCom, OwnerOnlineComp, OwnerOfflineCom, OwnerBotComponent> _cellUnitFilter;

    public void Run()
    {
        foreach (byte idxCurCell in _cellUnitFilter)
        {
            var xy = _xyCellFilter.GetXyCell(idxCurCell);

            ref var curUnitDataCom = ref _cellUnitFilter.Get1(idxCurCell);
            ref var curOnUnitCom = ref _cellUnitFilter.Get2(idxCurCell);
            ref var curOffUnitCom = ref _cellUnitFilter.Get3(idxCurCell);

            ref var curEnvDataCom = ref _cellEnvFilter.Get1(idxCurCell);


            curUnitDataCom.SetIsVisibleUnit(true, true);
            curUnitDataCom.SetIsVisibleUnit(false, true);


            if (curUnitDataCom.HaveUnit)
            {
                if (curOnUnitCom.HaveOwner)
                {
                    if (curOnUnitCom.IsMasterClient)
                    {
                        if (curEnvDataCom.HaveEnvir(EnvirTypes.AdultForest))
                        {
                            curUnitDataCom.SetIsVisibleUnit(false, false);

                            var list = CellSpaceSupport.TryGetXyAround(xy);

                            foreach (var xy_1 in list)
                            {
                                var idxCell_1 = _xyCellFilter.GetIdxCell(xy_1);

                                ref var unitDataCom = ref _cellUnitFilter.Get1(idxCell_1);
                                ref var arouOnUnitCom = ref _cellUnitFilter.Get2(idxCell_1);

                                if (unitDataCom.HaveUnit)
                                {
                                    if (arouOnUnitCom.HaveOwner)
                                    {
                                        if (!arouOnUnitCom.IsMasterClient)
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
                        if (curEnvDataCom.HaveEnvir(EnvirTypes.AdultForest))
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
                                        if (ownerUnitCom.IsMasterClient)
                                        {
                                            curUnitDataCom.SetIsVisibleUnit(true, true);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                else if (curOffUnitCom.HaveLocalPlayer)
                {
                    if (curOffUnitCom.IsMainMaster)
                    {
                        if (curEnvDataCom.HaveEnvir(EnvirTypes.AdultForest))
                        {
                            curUnitDataCom.SetIsVisibleUnit(false, false);

                            var list = CellSpaceSupport.TryGetXyAround(xy);

                            foreach (var xy_1 in list)
                            {
                                var idxCell_1 = _xyCellFilter.GetIdxCell(xy_1);

                                ref var unitDataCom = ref _cellUnitFilter.Get1(idxCell_1);
                                ref var aroOffUnitCom = ref _cellUnitFilter.Get3(idxCell_1);

                                if (unitDataCom.HaveUnit)
                                {
                                    if (aroOffUnitCom.HaveLocalPlayer)
                                    {
                                        if (!aroOffUnitCom.IsMainMaster)
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
                        if (curEnvDataCom.HaveEnvir(EnvirTypes.AdultForest))
                        {
                            curUnitDataCom.SetIsVisibleUnit(true, false);

                            var list = CellSpaceSupport.TryGetXyAround(xy);

                            foreach (var xy_1 in list)
                            {
                                var idxCell_1 = _xyCellFilter.GetIdxCell(xy_1);

                                ref var unitDataCom = ref _cellUnitFilter.Get1(idxCell_1);
                                ref var aroOffUnitCom = ref _cellUnitFilter.Get3(idxCell_1);

                                if (unitDataCom.HaveUnit)
                                {
                                    if (aroOffUnitCom.HaveLocalPlayer)
                                    {
                                        if (aroOffUnitCom.IsMainMaster)
                                        {
                                            curUnitDataCom.SetIsVisibleUnit(true, true);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                else if (_cellUnitFilter.Get4(idxCurCell).IsBot)
                {
                    if (curEnvDataCom.HaveEnvir(EnvirTypes.AdultForest))
                    {
                        curUnitDataCom.SetIsVisibleUnit(true, false);

                        var list = CellSpaceSupport.TryGetXyAround(xy);

                        foreach (var xy_1 in list)
                        {
                            var idxCell_1 = _xyCellFilter.GetIdxCell(xy_1);

                            ref var unitDataCom_1 = ref _cellUnitFilter.Get1(idxCell_1);
                            ref var ownerUnitCom_1 = ref _cellUnitFilter.Get2(idxCell_1);
                            ref var offUnitCom_1 = ref _cellUnitFilter.Get3(idxCell_1);

                            if (unitDataCom_1.HaveUnit)
                            {
                                if (ownerUnitCom_1.HaveOwner || offUnitCom_1.HaveLocalPlayer)
                                {
                                    curUnitDataCom.SetIsVisibleUnit(true, true);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}