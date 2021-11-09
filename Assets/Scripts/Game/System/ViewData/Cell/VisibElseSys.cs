using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class VisibElseSys : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellEnvDataC> _cellEnvFilter = default;
        private EcsFilter<CellTrailDataC, VisibleC> _cellTrailFilt = default;

        private EcsFilter<CellUnitDataC, OwnerC, VisibleC> _cellUnitFilter = default;
        private EcsFilter<CellBuildDataC, OwnerC, VisibleC> _cellBuildFilt = default;

        public void Run()
        {
            foreach (byte idx_0 in _cellUnitFilter)
            {
                var xy = _xyCellFilter.Get1(idx_0).XyCell;

                ref var env_0 = ref _cellEnvFilter.Get1(idx_0);
                ref var unit_0 = ref _cellUnitFilter.Get1(idx_0);
                ref var ownUnit_0 = ref _cellUnitFilter.Get2(idx_0);

                if (unit_0.HaveUnit)
                {
                    ref var visUnit_0 = ref _cellUnitFilter.Get3(idx_0);

                    visUnit_0.SetVisibled(ownUnit_0.Owner, true);

                    if (env_0.Have(EnvTypes.AdultForest))
                    {
                        var isVisibledNextPlayer = false;

                        var list = CellSpaceSupport.GetXyAround(xy);

                        foreach (var xy_1 in list)
                        {
                            var idxCell_1 = _xyCellFilter.GetIdxCell(xy_1);

                            ref var unitCom_1 = ref _cellUnitFilter.Get1(idxCell_1);
                            ref var ownUnitCom_1 = ref _cellUnitFilter.Get2(idxCell_1);

                            if (unitCom_1.HaveUnit)
                            {
                                if (!ownUnitCom_1.Is(ownUnit_0.Owner))
                                {
                                    isVisibledNextPlayer = true;
                                    break;
                                }
                            }
                        }

                        visUnit_0.SetVisibled(WhoseMoveC.NextPlayerFrom(ownUnit_0.Owner), isVisibledNextPlayer);
                    }
                    else
                    {
                        visUnit_0.SetVisibled(WhoseMoveC.NextPlayerFrom(ownUnit_0.Owner), true);
                    }

                }

                ref var curBuildCom = ref _cellBuildFilt.Get1(idx_0);

                if (curBuildCom.HaveBuild)
                {
                    ref var curOwnBuildCom = ref _cellBuildFilt.Get2(idx_0);
                    ref var curVisBuildCom = ref _cellBuildFilt.Get3(idx_0);

                    curVisBuildCom.SetVisibled(curOwnBuildCom.Owner, true);

                    if (env_0.Have(EnvTypes.AdultForest))
                    {
                        var isVisibledNextPlayer = false;

                        var list = CellSpaceSupport.GetXyAround(xy);

                        foreach (var xy_1 in list)
                        {
                            var idxCell_1 = _xyCellFilter.GetIdxCell(xy_1);

                            ref var aroUnitDataCom = ref _cellUnitFilter.Get1(idxCell_1);
                            ref var arouOnUnitCom = ref _cellUnitFilter.Get2(idxCell_1);

                            if (aroUnitDataCom.HaveUnit)
                            {
                                if (!arouOnUnitCom.Is(curOwnBuildCom.Owner))
                                {
                                    isVisibledNextPlayer = true;
                                    break;
                                }
                            }
                        }

                        curVisBuildCom.SetVisibled(WhoseMoveC.NextPlayerFrom(curOwnBuildCom.Owner), isVisibledNextPlayer);
                    }
                    else curVisBuildCom.SetVisibled(WhoseMoveC.NextPlayerFrom(curOwnBuildCom.Owner), true);
                }


                ref var trail_0 = ref _cellTrailFilt.Get1(idx_0);

                if (trail_0.HaveAnyTrail)
                {
                    ref var trailVis_0 = ref _cellTrailFilt.Get2(idx_0);

                    var list = CellSpaceSupport.GetXyAround(xy);

                    trailVis_0.SetVisibled(WhoseMoveC.NextPlayerFrom(PlayerTypes.First), false);
                    trailVis_0.SetVisibled(WhoseMoveC.NextPlayerFrom(PlayerTypes.Second), false);
        
                    if (unit_0.HaveUnit) trailVis_0.SetVisibled(ownUnit_0.Owner, true);

                    foreach (var xy_1 in list)
                    {
                        var idxCell_1 = _xyCellFilter.GetIdxCell(xy_1);

                        ref var unitCom_1 = ref _cellUnitFilter.Get1(idxCell_1);
                        ref var ownUnit_1 = ref _cellUnitFilter.Get2(idxCell_1);


                        if (unitCom_1.HaveUnit)
                        {
                            trailVis_0.SetVisibled(ownUnit_1.Owner, true);
                        }
                    }
                }
            }
        }
    }
}