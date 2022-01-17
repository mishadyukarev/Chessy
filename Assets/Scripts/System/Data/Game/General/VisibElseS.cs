using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;
using static Game.Game.CellTrailEs;
using static Game.Game.CellBuildE;
using static Game.Game.CellEnvironmentEs;
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


                Unit<IsVisibledC>(PlayerTypes.First, idx_0).IsVisibled = true;
                Unit<IsVisibledC>(PlayerTypes.Second, idx_0).IsVisibled = true;

                //if (unit_0.Have)
                //{
                //    Unit<IsVisibledC>(ownUnit_0.Player, idx_0).IsVisibled = true;

                //    if (Environment<HaveEnvironmentC>(EnvTypes.AdultForest, idx_0).Have)
                //    {
                //        var isVisibledNextPlayer = false;

                //        var list = CellSpaceC.XyAround(xy);

                //        foreach (var xy_1 in list)
                //        {
                //            var idxCell_1 = IdxCell(xy_1);

                //            ref var unitCom_1 = ref Unit<UnitTC>(idxCell_1);
                //            ref var ownUnitCom_1 = ref Unit<PlayerTC>(idxCell_1);

                //            if (unitCom_1.Have)
                //            {
                //                if (!ownUnitCom_1.Is(ownUnit_0.Player))
                //                {
                //                    isVisibledNextPlayer = true;
                //                    break;
                //                }
                //            }
                //        }

                //        Unit<IsVisibledC>(WhoseMoveE.NextPlayerFrom(ownUnit_0.Player), idx_0).IsVisibled = isVisibledNextPlayer;
                //    }
                //    else
                //    {
                //        Unit<IsVisibledC>(WhoseMoveE.NextPlayerFrom(ownUnit_0.Player), idx_0).IsVisibled = true;
                //    }

                //}

                ref var build_0 = ref Build<BuildingTC>(idx_0);


                Build<IsVisibledC>(PlayerTypes.First, idx_0).IsVisibled = true;
                Build<IsVisibledC>(PlayerTypes.Second, idx_0).IsVisibled = true;

                if (build_0.Have)
                {
                    ref var ownBuild_0 = ref Build<PlayerTC>(idx_0);


                    //Build<IsVisibledC>(ownBuild_0.Player, idx_0).IsVisibled = true;
                    //Build<IsVisibledC>(PlayerTypes.First, idx_0).IsVisibled = true;
                    //Build<IsVisibledC>(PlayerTypes.Second, idx_0).IsVisibled = true;

                    //if (Environment<HaveEnvironmentC>(EnvTypes.AdultForest, idx_0).Have)
                    //{
                    //    var isVisibledNextPlayer = false;

                    //    var list = CellSpaceC.XyAround(xy);

                    //    foreach (var xy_1 in list)
                    //    {
                    //        var idx_1 = IdxCell(xy_1);

                    //        ref var unit_1 = ref Unit<UnitTC>(idx_1);
                    //        ref var ownUnit_1 = ref Unit<PlayerTC>(idx_1);

                    //        if (unit_1.Have)
                    //        {
                    //            if (!ownUnit_1.Is(ownBuild_0.Player))
                    //            {
                    //                isVisibledNextPlayer = true;
                    //                break;
                    //            }
                    //        }
                    //    }
                    //    Build<IsVisibledC>(WhoseMoveE.NextPlayerFrom(ownBuild_0.Player), idx_0).IsVisibled = isVisibledNextPlayer;
                    //}
                    //else Build<IsVisibledC>(WhoseMoveE.NextPlayerFrom(ownBuild_0.Player), idx_0).IsVisibled = true;
                }


                //ref var trail_0 = ref Trail<TrailCellEC>(idx_0);

                //if (trail_0.HaveAnyTrail)
                //{
                //    var list = CellSpaceC.XyAround(xy);

                //    Trail<IsVisibledC>(WhoseMoveE.NextPlayerFrom(PlayerTypes.First), idx_0).IsVisibled = false;
                //    Trail<IsVisibledC>(WhoseMoveE.NextPlayerFrom(PlayerTypes.Second), idx_0).IsVisibled = false;

                //    if (unit_0.Have) Trail<IsVisibledC>(ownUnit_0.Player, idx_0).IsVisibled = true;

                //    foreach (var xy_1 in list)
                //    {
                //        var idxCell_1 = CellEs.IdxCell(xy_1);

                //        ref var unitCom_1 = ref Unit<UnitTC>(idxCell_1);
                //        ref var ownUnit_1 = ref Unit<PlayerTC>(idxCell_1);


                //        if (unitCom_1.Have)
                //        {
                //            Trail<IsVisibledC>(ownUnit_1.Player, idx_0).IsVisibled = true;
                //        }
                //    }
                //}
            }
        }
    }
}