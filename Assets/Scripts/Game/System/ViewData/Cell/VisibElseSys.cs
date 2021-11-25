using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class VisibElseSys : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in EntityPool.Idxs)
            {
                var xy = EntityPool.CellC<XyC>(idx_0).Xy;

                ref var env_0 = ref EntityPool.EnvCellC<EnvC>(idx_0);
                ref var unit_0 = ref EntityPool.UnitCellC<UnitC>(idx_0);
                ref var ownUnit_0 = ref EntityPool.UnitCellC<OwnerC>(idx_0);

                if (unit_0.HaveUnit)
                {
                    ref var visUnit_0 = ref EntityPool.UnitCellC<VisibleC>(idx_0);

                    visUnit_0.SetVisibled(ownUnit_0.Owner, true);

                    if (env_0.Have(EnvTypes.AdultForest))
                    {
                        var isVisibledNextPlayer = false;

                        var list = CellSpaceC.XyAround(xy);

                        foreach (var xy_1 in list)
                        {
                            var idxCell_1 = EntityPool.IdxCell(xy_1);

                            ref var unitCom_1 = ref EntityPool.UnitCellC<UnitC>(idxCell_1);
                            ref var ownUnitCom_1 = ref EntityPool.UnitCellC<OwnerC>(idxCell_1);

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

                ref var curBuildCom = ref EntityPool.BuildCellC<BuildC>(idx_0);

                if (curBuildCom.Have)
                {
                    ref var curOwnBuildCom = ref EntityPool.BuildCellC<OwnerC>(idx_0);
                    ref var curVisBuildCom = ref EntityPool.BuildCellC<VisibleC>(idx_0);

                    curVisBuildCom.SetVisibled(curOwnBuildCom.Owner, true);

                    if (env_0.Have(EnvTypes.AdultForest))
                    {
                        var isVisibledNextPlayer = false;

                        var list = CellSpaceC.XyAround(xy);

                        foreach (var xy_1 in list)
                        {
                            var idxCell_1 = EntityPool.IdxCell(xy_1);

                            ref var aroUnitDataCom = ref EntityPool.UnitCellC<UnitC>(idxCell_1);
                            ref var arouOnUnitCom = ref EntityPool.UnitCellC<OwnerC>(idxCell_1);

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


                ref var trail_0 = ref EntityPool.GetTrailCellC<TrailC>(idx_0);

                if (trail_0.HaveAnyTrail)
                {
                    ref var trailVis_0 = ref EntityPool.GetTrailCellC<VisibleC>(idx_0);

                    var list = CellSpaceC.XyAround(xy);

                    trailVis_0.SetVisibled(WhoseMoveC.NextPlayerFrom(PlayerTypes.First), false);
                    trailVis_0.SetVisibled(WhoseMoveC.NextPlayerFrom(PlayerTypes.Second), false);
        
                    if (unit_0.HaveUnit) trailVis_0.SetVisibled(ownUnit_0.Owner, true);

                    foreach (var xy_1 in list)
                    {
                        var idxCell_1 = EntityPool.IdxCell(xy_1);

                        ref var unitCom_1 = ref EntityPool.UnitCellC<UnitC>(idxCell_1);
                        ref var ownUnit_1 = ref EntityPool.UnitCellC<OwnerC>(idxCell_1);


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