using static Game.Game.CellBuildEs;
using static Game.Game.CellEnvironmentEs;
using static Game.Game.CellEs;
using static Game.Game.CellTrailEs;

namespace Game.Game
{
    struct VisibElseS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in Idxs)
            {
                ref var unit_0 = ref CellUnitEs.Else(idx_0).UnitC;
                ref var ownUnit_0 = ref CellUnitEs.Else(idx_0).OwnerC;


                if (unit_0.Have)
                {
                    if (unit_0.IsAnimal)
                    {
                        var isVisForFirst = true;
                        var isVisForSecond = true;

                        if (Environment(EnvironmentTypes.AdultForest, idx_0).Resources.Have)
                        {
                            isVisForFirst = false;
                            isVisForSecond = false;

                            foreach (var idx_1 in CellSpaceSupport.GetIdxsAround(idx_0))
                            {
                                if (CellUnitEs.Else(idx_1).UnitC.Have)
                                {
                                    if (CellUnitEs.Else(idx_1).OwnerC.Is(PlayerTypes.First)) isVisForFirst = true;
                                    if (CellUnitEs.Else(idx_1).OwnerC.Is(PlayerTypes.Second)) isVisForSecond = true;
                                }
                            }
                        }

                        CellUnitEs.VisibleE(PlayerTypes.First, idx_0).VisibleC.IsVisible = isVisForFirst;
                        CellUnitEs.VisibleE(PlayerTypes.Second, idx_0).VisibleC.IsVisible = isVisForSecond;
                    }
                    else
                    {
                        CellUnitEs.VisibleE(ownUnit_0.Player, idx_0).VisibleC.IsVisible = true;

                        if (Environment(EnvironmentTypes.AdultForest, idx_0).Resources.Have)
                        {
                            var isVisibledNextPlayer = false;

                            foreach (var idx_1 in CellSpaceSupport.GetIdxsAround(idx_0))
                            {
                                ref var unit_1 = ref CellUnitEs.Else(idx_1).UnitC;
                                ref var ownUnit_1 = ref CellUnitEs.Else(idx_1).OwnerC;

                                if (unit_1.Have)
                                {
                                    if (!ownUnit_1.Is(ownUnit_0.Player))
                                    {
                                        isVisibledNextPlayer = true;
                                        break;
                                    }
                                }
                            }

                            CellUnitEs.VisibleE(Entities.WhoseMoveE.NextPlayerFrom(ownUnit_0.Player), idx_0).VisibleC.IsVisible = isVisibledNextPlayer;
                        }
                        else
                        {
                            CellUnitEs.VisibleE(Entities.WhoseMoveE.NextPlayerFrom(ownUnit_0.Player), idx_0).VisibleC.IsVisible = true;
                        }
                    }
                }



                ref var build_0 = ref CellBuildEs.Build(idx_0).BuildTC;

                CellBuildEs.IsVisible(PlayerTypes.First, idx_0).IsVisibleC.IsVisible = true;
                CellBuildEs.IsVisible(PlayerTypes.Second, idx_0).IsVisibleC.IsVisible = true;

                if (build_0.Have)
                {
                    ref var ownBuild_0 = ref CellBuildEs.Build(idx_0).PlayerTC;

                    CellBuildEs.IsVisible(ownBuild_0.Player, idx_0).IsVisibleC.IsVisible = true;

                    if (Environment(EnvironmentTypes.AdultForest, idx_0).Resources.Have)
                    {
                        var isVisibledNextPlayer = false;

                        foreach (var idx_1 in CellSpaceSupport.GetIdxsAround(idx_0))
                        {
                            ref var unit_1 = ref CellUnitEs.Else(idx_1).UnitC;
                            ref var ownUnit_1 = ref CellUnitEs.Else(idx_1).OwnerC;

                            if (unit_1.Have)
                            {
                                if (!ownUnit_1.Is(ownBuild_0.Player))
                                {
                                    isVisibledNextPlayer = true;
                                    break;
                                }
                            }
                        }
                        CellBuildEs.IsVisible(Entities.WhoseMoveE.NextPlayerFrom(ownBuild_0.Player), idx_0).IsVisibleC.IsVisible = isVisibledNextPlayer;
                    }
                    else CellBuildEs.IsVisible(Entities.WhoseMoveE.NextPlayerFrom(ownBuild_0.Player), idx_0).IsVisibleC.IsVisible = true;
                }

                if (HaveAnyTrail(idx_0))
                {
                    CellTrailEs.IsVisible(PlayerTypes.First, idx_0).IsVisibleC.IsVisible = false;
                    CellTrailEs.IsVisible(PlayerTypes.Second, idx_0).IsVisibleC.IsVisible = false;

                    //if (unit_0.Have)
                    //{
                    //if (!unit_0.IsAnimal)
                    //{
                    //    CellTrailEs.IsVisible(ownUnit_0.Player, idx_0).IsVisible = true;
                    //}


                    foreach (var idx_1 in CellSpaceSupport.GetIdxsAround(idx_0))
                    {
                        ref var unit_1 = ref CellUnitEs.Else(idx_1).UnitC;
                        ref var ownUnit_1 = ref CellUnitEs.Else(idx_1).OwnerC;


                        if (unit_1.Have && !unit_1.IsAnimal)
                        {
                            CellTrailEs.IsVisible(ownUnit_1.Player, idx_0).IsVisibleC.IsVisible = true;
                        }
                    }
                    //}
                }
            }
        }
    }
}