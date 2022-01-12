using static Game.Game.EntityCellPool;
using static Game.Game.EntCellUnit;
using static Game.Game.EntityCellTrailPool;
using static Game.Game.EntityCellBuildPool;
using static Game.Game.EntityCellEnvPool;

namespace Game.Game
{
    struct VisibElseS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in Idxs)
            {
                var xy = Cell<XyC>(idx_0).Xy;

                ref var unit_0 = ref Unit<UnitC>(idx_0);
                ref var ownUnit_0 = ref Unit<PlayerC>(idx_0);

                if (unit_0.Have)
                {
                    Unit<VisibledC>(ownUnit_0.Player, idx_0).IsVisibled = true;

                    if (Environment<HaveEnvironmentC>(EnvTypes.AdultForest, idx_0).Have)
                    {
                        var isVisibledNextPlayer = false;

                        var list = CellSpaceC.XyAround(xy);

                        foreach (var xy_1 in list)
                        {
                            var idxCell_1 = IdxCell(xy_1);

                            ref var unitCom_1 = ref Unit<UnitC>(idxCell_1);
                            ref var ownUnitCom_1 = ref Unit<PlayerC>(idxCell_1);

                            if (unitCom_1.Have)
                            {
                                if (!ownUnitCom_1.Is(ownUnit_0.Player))
                                {
                                    isVisibledNextPlayer = true;
                                    break;
                                }
                            }
                        }

                        Unit<VisibledC>(WhoseMoveC.NextPlayerFrom(ownUnit_0.Player), idx_0).IsVisibled = isVisibledNextPlayer;
                    }
                    else
                    {
                        Unit<VisibledC>(WhoseMoveC.NextPlayerFrom(ownUnit_0.Player), idx_0).IsVisibled = true;
                    }

                }

                ref var curBuildCom = ref Build<BuildC>(idx_0);

                if (curBuildCom.Have)
                {
                    ref var curOwnBuildCom = ref Build<PlayerC>(idx_0);
                    ref var curVisBuildCom = ref Build<VisibledC>(curOwnBuildCom.Player, idx_0);

                    curVisBuildCom.IsVisibled = true;

                    if (Environment<HaveEnvironmentC>(EnvTypes.AdultForest, idx_0).Have)
                    {
                        var isVisibledNextPlayer = false;

                        var list = CellSpaceC.XyAround(xy);

                        foreach (var xy_1 in list)
                        {
                            var idxCell_1 = IdxCell(xy_1);

                            ref var aroUnitDataCom = ref Unit<UnitC>(idxCell_1);
                            ref var arouOnUnitCom = ref Unit<PlayerC>(idxCell_1);

                            if (aroUnitDataCom.Have)
                            {
                                if (!arouOnUnitCom.Is(curOwnBuildCom.Player))
                                {
                                    isVisibledNextPlayer = true;
                                    break;
                                }
                            }
                        }
                        Build<VisibledC>(WhoseMoveC.NextPlayerFrom(curOwnBuildCom.Player), idx_0).IsVisibled = isVisibledNextPlayer;
                    }
                    else Build<VisibledC>(WhoseMoveC.NextPlayerFrom(curOwnBuildCom.Player), idx_0).IsVisibled = true;
                }


                ref var trail_0 = ref Trail<TrailCellEC>(idx_0);

                if (trail_0.HaveAnyTrail)
                {
                    var list = CellSpaceC.XyAround(xy);

                    Trail<VisibledC>(WhoseMoveC.NextPlayerFrom(PlayerTypes.First), idx_0).IsVisibled = false;
                    Trail<VisibledC>(WhoseMoveC.NextPlayerFrom(PlayerTypes.Second), idx_0).IsVisibled = false;

                    if (unit_0.Have) Trail<VisibledC>(ownUnit_0.Player, idx_0).IsVisibled = true;

                    foreach (var xy_1 in list)
                    {
                        var idxCell_1 = EntityCellPool.IdxCell(xy_1);

                        ref var unitCom_1 = ref Unit<UnitC>(idxCell_1);
                        ref var ownUnit_1 = ref Unit<PlayerC>(idxCell_1);


                        if (unitCom_1.Have)
                        {
                            Trail<VisibledC>(ownUnit_1.Player, idx_0).IsVisibled = true;
                        }
                    }
                }
            }
        }
    }
}