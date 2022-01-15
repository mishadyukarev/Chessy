using static Game.Game.CellE;
using static Game.Game.CellUnitE;
using static Game.Game.EntityCellTrailPool;
using static Game.Game.CellBuildE;
using static Game.Game.CellEnvironmentE;
using Game.Common;

namespace Game.Game
{
    struct VisibElseS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in Idxs)
            {
                var xy = Cell<XyC>(idx_0).Xy;

                ref var unit_0 = ref Unit<UnitTC>(idx_0);
                ref var ownUnit_0 = ref Unit<PlayerTC>(idx_0);

                if (unit_0.Have)
                {
                    Unit<IsVisibledC>(ownUnit_0.Player, idx_0).IsVisibled = true;

                    if (Environment<HaveEnvironmentC>(EnvTypes.AdultForest, idx_0).Have)
                    {
                        var isVisibledNextPlayer = false;

                        var list = CellSpaceC.XyAround(xy);

                        foreach (var xy_1 in list)
                        {
                            var idxCell_1 = IdxCell(xy_1);

                            ref var unitCom_1 = ref Unit<UnitTC>(idxCell_1);
                            ref var ownUnitCom_1 = ref Unit<PlayerTC>(idxCell_1);

                            if (unitCom_1.Have)
                            {
                                if (!ownUnitCom_1.Is(ownUnit_0.Player))
                                {
                                    isVisibledNextPlayer = true;
                                    break;
                                }
                            }
                        }

                        Unit<IsVisibledC>(WhoseMoveE.NextPlayerFrom(ownUnit_0.Player), idx_0).IsVisibled = isVisibledNextPlayer;
                    }
                    else
                    {
                        Unit<IsVisibledC>(WhoseMoveE.NextPlayerFrom(ownUnit_0.Player), idx_0).IsVisibled = true;
                    }

                }

                ref var curBuildCom = ref Build<BuildingC>(idx_0);

                if (curBuildCom.Have)
                {
                    ref var curOwnBuildCom = ref Build<PlayerTC>(idx_0);
                    ref var curVisBuildCom = ref Build<IsVisibledC>(curOwnBuildCom.Player, idx_0);

                    curVisBuildCom.IsVisibled = true;

                    if (Environment<HaveEnvironmentC>(EnvTypes.AdultForest, idx_0).Have)
                    {
                        var isVisibledNextPlayer = false;

                        var list = CellSpaceC.XyAround(xy);

                        foreach (var xy_1 in list)
                        {
                            var idxCell_1 = IdxCell(xy_1);

                            ref var aroUnitDataCom = ref Unit<UnitTC>(idxCell_1);
                            ref var arouOnUnitCom = ref Unit<PlayerTC>(idxCell_1);

                            if (aroUnitDataCom.Have)
                            {
                                if (!arouOnUnitCom.Is(curOwnBuildCom.Player))
                                {
                                    isVisibledNextPlayer = true;
                                    break;
                                }
                            }
                        }
                        Build<IsVisibledC>(WhoseMoveE.NextPlayerFrom(curOwnBuildCom.Player), idx_0).IsVisibled = isVisibledNextPlayer;
                    }
                    else Build<IsVisibledC>(WhoseMoveE.NextPlayerFrom(curOwnBuildCom.Player), idx_0).IsVisibled = true;
                }


                ref var trail_0 = ref Trail<TrailCellEC>(idx_0);

                if (trail_0.HaveAnyTrail)
                {
                    var list = CellSpaceC.XyAround(xy);

                    Trail<IsVisibledC>(WhoseMoveE.NextPlayerFrom(PlayerTypes.First), idx_0).IsVisibled = false;
                    Trail<IsVisibledC>(WhoseMoveE.NextPlayerFrom(PlayerTypes.Second), idx_0).IsVisibled = false;

                    if (unit_0.Have) Trail<IsVisibledC>(ownUnit_0.Player, idx_0).IsVisibled = true;

                    foreach (var xy_1 in list)
                    {
                        var idxCell_1 = CellE.IdxCell(xy_1);

                        ref var unitCom_1 = ref Unit<UnitTC>(idxCell_1);
                        ref var ownUnit_1 = ref Unit<PlayerTC>(idxCell_1);


                        if (unitCom_1.Have)
                        {
                            Trail<IsVisibledC>(ownUnit_1.Player, idx_0).IsVisibled = true;
                        }
                    }
                }
            }
        }
    }
}