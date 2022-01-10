using static Game.Game.EntityCellPool;

namespace Game.Game
{
    public struct VisibElseS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in Idxs)
            {
                var xy = Cell<XyC>(idx_0).Xy;

                ref var unit_0 = ref Unit<UnitC>(idx_0);
                ref var ownUnit_0 = ref Unit<OwnerC>(idx_0);

                if (unit_0.Have)
                {
                    Unit<VisibledC>(ownUnit_0.Owner, idx_0).IsVisibled = true;

                    if (Environment<HaveEnvironmentC>(EnvTypes.AdultForest, idx_0).Have)
                    {
                        var isVisibledNextPlayer = false;

                        var list = CellSpaceC.XyAround(xy);

                        foreach (var xy_1 in list)
                        {
                            var idxCell_1 = IdxCell(xy_1);

                            ref var unitCom_1 = ref Unit<UnitC>(idxCell_1);
                            ref var ownUnitCom_1 = ref Unit<OwnerC>(idxCell_1);

                            if (unitCom_1.Have)
                            {
                                if (!ownUnitCom_1.Is(ownUnit_0.Owner))
                                {
                                    isVisibledNextPlayer = true;
                                    break;
                                }
                            }
                        }

                        Unit<VisibledC>(WhoseMoveC.NextPlayerFrom(ownUnit_0.Owner), idx_0).IsVisibled = isVisibledNextPlayer;
                    }
                    else
                    {
                        Unit<VisibledC>(WhoseMoveC.NextPlayerFrom(ownUnit_0.Owner), idx_0).IsVisibled = true;
                    }

                }

                ref var curBuildCom = ref Build<BuildC>(idx_0);

                if (curBuildCom.Have)
                {
                    ref var curOwnBuildCom = ref Build<OwnerC>(idx_0);
                    ref var curVisBuildCom = ref Build<VisibledC>(curOwnBuildCom.Owner, idx_0);

                    curVisBuildCom.IsVisibled = true;

                    if (Environment<HaveEnvironmentC>(EnvTypes.AdultForest, idx_0).Have)
                    {
                        var isVisibledNextPlayer = false;

                        var list = CellSpaceC.XyAround(xy);

                        foreach (var xy_1 in list)
                        {
                            var idxCell_1 = IdxCell(xy_1);

                            ref var aroUnitDataCom = ref Unit<UnitC>(idxCell_1);
                            ref var arouOnUnitCom = ref Unit<OwnerC>(idxCell_1);

                            if (aroUnitDataCom.Have)
                            {
                                if (!arouOnUnitCom.Is(curOwnBuildCom.Owner))
                                {
                                    isVisibledNextPlayer = true;
                                    break;
                                }
                            }
                        }
                        Build<VisibledC>(WhoseMoveC.NextPlayerFrom(curOwnBuildCom.Owner), idx_0).IsVisibled = isVisibledNextPlayer;
                    }
                    else Build<VisibledC>(WhoseMoveC.NextPlayerFrom(curOwnBuildCom.Owner), idx_0).IsVisibled = true;
                }


                ref var trail_0 = ref EntityCellPool.Trail<TrailCellEC>(idx_0);

                if (trail_0.HaveAnyTrail)
                {
                    ref var trailVis_0 = ref EntityCellPool.Trail<VisibleC>(idx_0);

                    var list = CellSpaceC.XyAround(xy);

                    trailVis_0.SetVisibled(WhoseMoveC.NextPlayerFrom(PlayerTypes.First), false);
                    trailVis_0.SetVisibled(WhoseMoveC.NextPlayerFrom(PlayerTypes.Second), false);

                    if (unit_0.Have) trailVis_0.SetVisibled(ownUnit_0.Owner, true);

                    foreach (var xy_1 in list)
                    {
                        var idxCell_1 = EntityCellPool.IdxCell(xy_1);

                        ref var unitCom_1 = ref EntityCellPool.Unit<UnitC>(idxCell_1);
                        ref var ownUnit_1 = ref EntityCellPool.Unit<OwnerC>(idxCell_1);


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