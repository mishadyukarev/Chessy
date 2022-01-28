namespace Game.Game
{
    struct VisibElseS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in Entities.CellEs.Idxs)
            {
                ref var unit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).UnitC;
                ref var ownUnit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).OwnerC;


                if (unit_0.Have)
                {
                    if (unit_0.IsAnimal)
                    {
                        var isVisForFirst = true;
                        var isVisForSecond = true;

                        if (Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.AdultForest, idx_0).Resources.Have)
                        {
                            isVisForFirst = false;
                            isVisForSecond = false;

                            foreach (var idx_1 in CellSpaceSupport.GetIdxsAround(idx_0))
                            {
                                if (Entities.CellEs.UnitEs.Else(idx_1).UnitC.Have)
                                {
                                    if (Entities.CellEs.UnitEs.Else(idx_1).OwnerC.Is(PlayerTypes.First)) isVisForFirst = true;
                                    if (Entities.CellEs.UnitEs.Else(idx_1).OwnerC.Is(PlayerTypes.Second)) isVisForSecond = true;
                                }
                            }
                        }

                        Entities.CellEs.UnitEs.VisibleE(PlayerTypes.First, idx_0).VisibleC.IsVisible = isVisForFirst;
                        Entities.CellEs.UnitEs.VisibleE(PlayerTypes.Second, idx_0).VisibleC.IsVisible = isVisForSecond;
                    }
                    else
                    {
                        Entities.CellEs.UnitEs.VisibleE(ownUnit_0.Player, idx_0).VisibleC.IsVisible = true;

                        if (Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.AdultForest, idx_0).Resources.Have)
                        {
                            var isVisibledNextPlayer = false;

                            foreach (var idx_1 in CellSpaceSupport.GetIdxsAround(idx_0))
                            {
                                ref var unit_1 = ref Entities.CellEs.UnitEs.Else(idx_1).UnitC;
                                ref var ownUnit_1 = ref Entities.CellEs.UnitEs.Else(idx_1).OwnerC;

                                if (unit_1.Have)
                                {
                                    if (!ownUnit_1.Is(ownUnit_0.Player))
                                    {
                                        isVisibledNextPlayer = true;
                                        break;
                                    }
                                }
                            }

                            Entities.CellEs.UnitEs.VisibleE(Entities.WhoseMove.NextPlayerFrom(ownUnit_0.Player), idx_0).VisibleC.IsVisible = isVisibledNextPlayer;
                        }
                        else
                        {
                            Entities.CellEs.UnitEs.VisibleE(Entities.WhoseMove.NextPlayerFrom(ownUnit_0.Player), idx_0).VisibleC.IsVisible = true;
                        }
                    }
                }



                ref var build_0 = ref Entities.CellEs.BuildEs.Build(idx_0).BuildTC;

                Entities.CellEs.BuildEs.IsVisible(PlayerTypes.First, idx_0).IsVisibleC.IsVisible = true;
                Entities.CellEs.BuildEs.IsVisible(PlayerTypes.Second, idx_0).IsVisibleC.IsVisible = true;

                if (build_0.Have)
                {
                    ref var ownBuild_0 = ref Entities.CellEs.BuildEs.Build(idx_0).PlayerTC;

                    Entities.CellEs.BuildEs.IsVisible(ownBuild_0.Player, idx_0).IsVisibleC.IsVisible = true;

                    if (Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.AdultForest, idx_0).Resources.Have)
                    {
                        var isVisibledNextPlayer = false;

                        foreach (var idx_1 in CellSpaceSupport.GetIdxsAround(idx_0))
                        {
                            ref var unit_1 = ref Entities.CellEs.UnitEs.Else(idx_1).UnitC;
                            ref var ownUnit_1 = ref Entities.CellEs.UnitEs.Else(idx_1).OwnerC;

                            if (unit_1.Have)
                            {
                                if (!ownUnit_1.Is(ownBuild_0.Player))
                                {
                                    isVisibledNextPlayer = true;
                                    break;
                                }
                            }
                        }
                        Entities.CellEs.BuildEs.IsVisible(Entities.WhoseMove.NextPlayerFrom(ownBuild_0.Player), idx_0).IsVisibleC.IsVisible = isVisibledNextPlayer;
                    }
                    else Entities.CellEs.BuildEs.IsVisible(Entities.WhoseMove.NextPlayerFrom(ownBuild_0.Player), idx_0).IsVisibleC.IsVisible = true;
                }

                if (Entities.CellEs.TrailEs.HaveAnyTrail(idx_0))
                {
                    Entities.CellEs.TrailEs.IsVisible(PlayerTypes.First, idx_0).IsVisibleC.IsVisible = false;
                    Entities.CellEs.TrailEs.IsVisible(PlayerTypes.Second, idx_0).IsVisibleC.IsVisible = false;

                    //if (unit_0.Have)
                    //{
                    //if (!unit_0.IsAnimal)
                    //{
                    //    CellTrailEs.IsVisible(ownUnit_0.Player, idx_0).IsVisible = true;
                    //}


                    foreach (var idx_1 in CellSpaceSupport.GetIdxsAround(idx_0))
                    {
                        ref var unit_1 = ref Entities.CellEs.UnitEs.Else(idx_1).UnitC;
                        ref var ownUnit_1 = ref Entities.CellEs.UnitEs.Else(idx_1).OwnerC;


                        if (unit_1.Have && !unit_1.IsAnimal)
                        {
                            Entities.CellEs.TrailEs.IsVisible(ownUnit_1.Player, idx_0).IsVisibleC.IsVisible = true;
                        }
                    }
                    //}
                }
            }
        }
    }
}