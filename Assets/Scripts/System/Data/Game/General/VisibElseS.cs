namespace Game.Game
{
    sealed class VisibElseS : SystemAbstract, IEcsRunSystem
    {
        public VisibElseS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            foreach (byte idx_0 in Es.CellEs.Idxs)
            {
                ref var unit_0 = ref Es.CellEs.UnitEs.Main(idx_0).UnitC;
                ref var ownUnit_0 = ref Es.CellEs.UnitEs.Main(idx_0).OwnerC;


                if (unit_0.Have)
                {
                    if (unit_0.IsAnimal)
                    {
                        var isVisForFirst = true;
                        var isVisForSecond = true;

                        if (Es.CellEs.EnvironmentEs.AdultForest( idx_0).HaveEnvironment)
                        {
                            isVisForFirst = false;
                            isVisForSecond = false;

                            foreach (var idx_1 in Es.CellEs.GetIdxsAround(idx_0))
                            {
                                if (Es.CellEs.UnitEs.Main(idx_1).UnitC.Have)
                                {
                                    if (Es.CellEs.UnitEs.Main(idx_1).OwnerC.Is(PlayerTypes.First)) isVisForFirst = true;
                                    if (Es.CellEs.UnitEs.Main(idx_1).OwnerC.Is(PlayerTypes.Second)) isVisForSecond = true;
                                }
                            }
                        }

                        Es.CellEs.UnitEs.VisibleE(PlayerTypes.First, idx_0).VisibleC.IsVisible = isVisForFirst;
                        Es.CellEs.UnitEs.VisibleE(PlayerTypes.Second, idx_0).VisibleC.IsVisible = isVisForSecond;
                    }
                    else
                    {
                        Es.CellEs.UnitEs.VisibleE(ownUnit_0.Player, idx_0).VisibleC.IsVisible = true;

                        if (Es.CellEs.EnvironmentEs.AdultForest( idx_0).HaveEnvironment)
                        {
                            var isVisibledNextPlayer = false;

                            foreach (var idx_1 in Es.CellEs.GetIdxsAround(idx_0))
                            {
                                ref var unit_1 = ref Es.CellEs.UnitEs.Main(idx_1).UnitC;
                                ref var ownUnit_1 = ref Es.CellEs.UnitEs.Main(idx_1).OwnerC;

                                if (unit_1.Have)
                                {
                                    if (!ownUnit_1.Is(ownUnit_0.Player))
                                    {
                                        isVisibledNextPlayer = true;
                                        break;
                                    }
                                }
                            }

                            Es.CellEs.UnitEs.VisibleE(Es.WhoseMove.NextPlayerFrom(ownUnit_0.Player), idx_0).VisibleC.IsVisible = isVisibledNextPlayer;
                        }
                        else
                        {
                            Es.CellEs.UnitEs.VisibleE(Es.WhoseMove.NextPlayerFrom(ownUnit_0.Player), idx_0).VisibleC.IsVisible = true;
                        }
                    }
                }



                ref var build_0 = ref Es.CellEs.BuildEs.Build(idx_0).BuildTC;

                Es.CellEs.BuildEs.IsVisible(PlayerTypes.First, idx_0).IsVisibleC.IsVisible = true;
                Es.CellEs.BuildEs.IsVisible(PlayerTypes.Second, idx_0).IsVisibleC.IsVisible = true;

                if (build_0.Have)
                {
                    ref var ownBuild_0 = ref Es.CellEs.BuildEs.Build(idx_0).PlayerTC;

                    Es.CellEs.BuildEs.IsVisible(ownBuild_0.Player, idx_0).IsVisibleC.IsVisible = true;

                    if (Es.CellEs.EnvironmentEs.AdultForest( idx_0).HaveEnvironment)
                    {
                        var isVisibledNextPlayer = false;

                        foreach (var idx_1 in Es.CellEs.GetIdxsAround(idx_0))
                        {
                            ref var unit_1 = ref Es.CellEs.UnitEs.Main(idx_1).UnitC;
                            ref var ownUnit_1 = ref Es.CellEs.UnitEs.Main(idx_1).OwnerC;

                            if (unit_1.Have)
                            {
                                if (!ownUnit_1.Is(ownBuild_0.Player))
                                {
                                    isVisibledNextPlayer = true;
                                    break;
                                }
                            }
                        }
                        Es.CellEs.BuildEs.IsVisible(Es.WhoseMove.NextPlayerFrom(ownBuild_0.Player), idx_0).IsVisibleC.IsVisible = isVisibledNextPlayer;
                    }
                    else Es.CellEs.BuildEs.IsVisible(Es.WhoseMove.NextPlayerFrom(ownBuild_0.Player), idx_0).IsVisibleC.IsVisible = true;
                }

                if (Es.CellEs.TrailEs.HaveAnyTrail(idx_0))
                {
                    Es.CellEs.TrailEs.IsVisible(PlayerTypes.First, idx_0).IsVisibleC.IsVisible = false;
                    Es.CellEs.TrailEs.IsVisible(PlayerTypes.Second, idx_0).IsVisibleC.IsVisible = false;

                    //if (unit_0.Have)
                    //{
                    //if (!unit_0.IsAnimal)
                    //{
                    //    CellTrailEs.IsVisible(ownUnit_0.Player, idx_0).IsVisible = true;
                    //}


                    foreach (var idx_1 in Es.CellEs.GetIdxsAround(idx_0))
                    {
                        ref var unit_1 = ref Es.CellEs.UnitEs.Main(idx_1).UnitC;
                        ref var ownUnit_1 = ref Es.CellEs.UnitEs.Main(idx_1).OwnerC;


                        if (unit_1.Have && !unit_1.IsAnimal)
                        {
                            Es.CellEs.TrailEs.IsVisible(ownUnit_1.Player, idx_0).IsVisibleC.IsVisible = true;
                        }
                    }
                    //}
                }
            }
        }
    }
}