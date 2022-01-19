using static Game.Game.CellBuildE;
using static Game.Game.CellEnvironmentEs;
using static Game.Game.CellEs;
using static Game.Game.CellTrailEs;
using static Game.Game.CellUnitEs;

namespace Game.Game
{
    struct VisibElseS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in Idxs)
            {
                ref var unit_0 = ref Unit<UnitTC>(idx_0);
                ref var ownUnit_0 = ref Unit<PlayerTC>(idx_0);


                if (unit_0.Have)
                {
                    CellUnitVisibleEs.Visible<IsVisibleC>(ownUnit_0.Player, idx_0).IsVisible = true;


                    if (Resources(EnvironmentTypes.AdultForest, idx_0).Have)
                    {
                        var isVisibledNextPlayer = false;

                        foreach (var idx_1 in CellSpaceC.IdxAround(idx_0))
                        {
                            ref var unit_1 = ref Unit<UnitTC>(idx_1);
                            ref var ownUnit_1 = ref Unit<PlayerTC>(idx_1);

                            if (unit_1.Have)
                            {
                                if (!ownUnit_1.Is(ownUnit_0.Player))
                                {
                                    isVisibledNextPlayer = true;
                                    break;
                                }
                            }
                        }

                        CellUnitVisibleEs.Visible<IsVisibleC>(WhoseMoveE.NextPlayerFrom(ownUnit_0.Player), idx_0).IsVisible = isVisibledNextPlayer;
                    }
                    else
                    {
                        CellUnitVisibleEs.Visible<IsVisibleC>(WhoseMoveE.NextPlayerFrom(ownUnit_0.Player), idx_0).IsVisible = true;
                    }

                }



                ref var build_0 = ref Build<BuildingTC>(idx_0);

                CellBuildE.IsVisible<IsVisibleC>(PlayerTypes.First, idx_0).IsVisible = true;
                CellBuildE.IsVisible<IsVisibleC>(PlayerTypes.Second, idx_0).IsVisible = true;

                if (build_0.Have)
                {
                    ref var ownBuild_0 = ref Build<PlayerTC>(idx_0);

                    CellBuildE.IsVisible<IsVisibleC>(ownBuild_0.Player, idx_0).IsVisible = true;

                    if (Resources(EnvironmentTypes.AdultForest, idx_0).Have)
                    {
                        var isVisibledNextPlayer = false;

                        foreach (var idx_1 in CellSpaceC.IdxAround(idx_0))
                        {
                            ref var unit_1 = ref Unit<UnitTC>(idx_1);
                            ref var ownUnit_1 = ref Unit<PlayerTC>(idx_1);

                            if (unit_1.Have)
                            {
                                if (!ownUnit_1.Is(ownBuild_0.Player))
                                {
                                    isVisibledNextPlayer = true;
                                    break;
                                }
                            }
                        }
                        CellBuildE.IsVisible<IsVisibleC>(WhoseMoveE.NextPlayerFrom(ownBuild_0.Player), idx_0).IsVisible = isVisibledNextPlayer;
                    }
                    else CellBuildE.IsVisible<IsVisibleC>(WhoseMoveE.NextPlayerFrom(ownBuild_0.Player), idx_0).IsVisible = true;
                }

                if (HaveAnyTrail(idx_0))
                {
                    CellTrailEs.IsVisible(PlayerTypes.First, idx_0).IsVisible = false;
                    CellTrailEs.IsVisible(PlayerTypes.Second, idx_0).IsVisible = false;

                    if (unit_0.Have) CellTrailEs.IsVisible(ownUnit_0.Player, idx_0).IsVisible = true;


                    foreach (var idx_1 in CellSpaceC.IdxAround(idx_0))
                    {
                        ref var unit_1 = ref Unit<UnitTC>(idx_1);
                        ref var ownUnit_1 = ref Unit<PlayerTC>(idx_1);


                        if (unit_1.Have)
                        {
                            CellTrailEs.IsVisible(ownUnit_1.Player, idx_0).IsVisible = true;
                        }
                    }
                }
            }
        }
    }
}