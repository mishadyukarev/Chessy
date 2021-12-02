using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class VisibElseS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in EntityPool.Idxs)
            {
                var xy = EntityPool.Cell<XyC>(idx_0).Xy;

                ref var env_0 = ref EntityPool.Environment<EnvC>(idx_0);
                ref var unit_0 = ref EntityPool.Unit<UnitC>(idx_0);
                ref var ownUnit_0 = ref EntityPool.Unit<OwnerC>(idx_0);

                if (unit_0.Have)
                {
                    ref var visUnit_0 = ref EntityPool.Unit<VisibleC>(idx_0);

                    visUnit_0.SetVisibled(ownUnit_0.Owner, true);

                    if (env_0.Have(EnvTypes.AdultForest))
                    {
                        var isVisibledNextPlayer = false;

                        var list = CellSpaceC.XyAround(xy);

                        foreach (var xy_1 in list)
                        {
                            var idxCell_1 = EntityPool.IdxCell(xy_1);

                            ref var unitCom_1 = ref EntityPool.Unit<UnitC>(idxCell_1);
                            ref var ownUnitCom_1 = ref EntityPool.Unit<OwnerC>(idxCell_1);

                            if (unitCom_1.Have)
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

                ref var curBuildCom = ref EntityPool.Build<BuildC>(idx_0);

                if (curBuildCom.Have)
                {
                    ref var curOwnBuildCom = ref EntityPool.Build<OwnerC>(idx_0);
                    ref var curVisBuildCom = ref EntityPool.Build<VisibleC>(idx_0);

                    curVisBuildCom.SetVisibled(curOwnBuildCom.Owner, true);

                    if (env_0.Have(EnvTypes.AdultForest))
                    {
                        var isVisibledNextPlayer = false;

                        var list = CellSpaceC.XyAround(xy);

                        foreach (var xy_1 in list)
                        {
                            var idxCell_1 = EntityPool.IdxCell(xy_1);

                            ref var aroUnitDataCom = ref EntityPool.Unit<UnitC>(idxCell_1);
                            ref var arouOnUnitCom = ref EntityPool.Unit<OwnerC>(idxCell_1);

                            if (aroUnitDataCom.Have)
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


                ref var trail_0 = ref EntityPool.Trail<TrailC>(idx_0);

                if (trail_0.HaveAnyTrail)
                {
                    ref var trailVis_0 = ref EntityPool.Trail<VisibleC>(idx_0);

                    var list = CellSpaceC.XyAround(xy);

                    trailVis_0.SetVisibled(WhoseMoveC.NextPlayerFrom(PlayerTypes.First), false);
                    trailVis_0.SetVisibled(WhoseMoveC.NextPlayerFrom(PlayerTypes.Second), false);
        
                    if (unit_0.Have) trailVis_0.SetVisibled(ownUnit_0.Owner, true);

                    foreach (var xy_1 in list)
                    {
                        var idxCell_1 = EntityPool.IdxCell(xy_1);

                        ref var unitCom_1 = ref EntityPool.Unit<UnitC>(idxCell_1);
                        ref var ownUnit_1 = ref EntityPool.Unit<OwnerC>(idxCell_1);


                        if (unitCom_1.Have)
                        {
                            trailVis_0.SetVisibled(ownUnit_1.Owner, true);
                        }
                    }
                }
            }
        }
    }
}