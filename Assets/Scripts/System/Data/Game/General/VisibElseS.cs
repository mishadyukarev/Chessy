namespace Game.Game
{
    sealed class VisibElseS : SystemAbstract, IEcsRunSystem
    {
        public VisibElseS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            foreach (byte idx_0 in CellEs.Idxs)
            {
                var unit_0 = UnitEs.Main(idx_0).UnitTC;
                var ownUnit_0 = UnitEs.Main(idx_0).OwnerC;


                if (unit_0.Have)
                {
                    if (unit_0.IsAnimal)
                    {
                        var isVisForFirst = true;
                        var isVisForSecond = true;

                        if (CellEs.EnvironmentEs.AdultForest( idx_0).HaveEnvironment)
                        {
                            isVisForFirst = false;
                            isVisForSecond = false;

                            foreach (var idx_1 in CellEs.GetIdxsAround(idx_0))
                            {
                                if (UnitEs.Main(idx_1).UnitTC.Have)
                                {
                                    if (UnitEs.Main(idx_1).OwnerC.Is(PlayerTypes.First)) isVisForFirst = true;
                                    if (UnitEs.Main(idx_1).OwnerC.Is(PlayerTypes.Second)) isVisForSecond = true;
                                }
                            }
                        }

                        UnitEs.VisibleE(PlayerTypes.First, idx_0).IsVisibleC.IsVisible = isVisForFirst;
                        UnitEs.VisibleE(PlayerTypes.Second, idx_0).IsVisibleC.IsVisible = isVisForSecond;
                    }
                    else
                    {
                        UnitEs.VisibleE(ownUnit_0.Player, idx_0).IsVisibleC.IsVisible = true;

                        if (CellEs.EnvironmentEs.AdultForest( idx_0).HaveEnvironment)
                        {
                            var isVisibledNextPlayer = false;

                            foreach (var idx_1 in CellEs.GetIdxsAround(idx_0))
                            {
                                var unit_1 = UnitEs.Main(idx_1).UnitTC;
                                var ownUnit_1 = UnitEs.Main(idx_1).OwnerC;

                                if (unit_1.Have)
                                {
                                    if (!ownUnit_1.Is(ownUnit_0.Player))
                                    {
                                        isVisibledNextPlayer = true;
                                        break;
                                    }
                                }
                            }

                            UnitEs.VisibleE(Es.WhoseMove.NextPlayerFrom(ownUnit_0.Player), idx_0).IsVisibleC.IsVisible = isVisibledNextPlayer;
                        }
                        else
                        {
                            UnitEs.VisibleE(Es.WhoseMove.NextPlayerFrom(ownUnit_0.Player), idx_0).IsVisibleC.IsVisible = true;
                        }
                    }
                }



                var build_0 = BuildEs.BuildingE(idx_0).BuildTC;

                BuildEs.BuildingE(PlayerTypes.First, idx_0).IsVisibleC.IsVisible = true;
                BuildEs.BuildingE(PlayerTypes.Second, idx_0).IsVisibleC.IsVisible = true;

                if (build_0.Have)
                {
                    var ownBuild_0 = BuildEs.BuildingE(idx_0).Owner;

                    BuildEs.BuildingE(ownBuild_0.Player, idx_0).IsVisibleC.IsVisible = true;

                    if (CellEs.EnvironmentEs.AdultForest( idx_0).HaveEnvironment)
                    {
                        var isVisibledNextPlayer = false;

                        foreach (var idx_1 in CellEs.GetIdxsAround(idx_0))
                        {
                            var unit_1 = UnitEs.Main(idx_1).UnitTC;
                            var ownUnit_1 = UnitEs.Main(idx_1).OwnerC;

                            if (unit_1.Have)
                            {
                                if (!ownUnit_1.Is(ownBuild_0.Player))
                                {
                                    isVisibledNextPlayer = true;
                                    break;
                                }
                            }
                        }
                        BuildEs.BuildingE(Es.WhoseMove.NextPlayerFrom(ownBuild_0.Player), idx_0).IsVisibleC.IsVisible = isVisibledNextPlayer;
                    }
                    else BuildEs.BuildingE(Es.WhoseMove.NextPlayerFrom(ownBuild_0.Player), idx_0).IsVisibleC.IsVisible = true;
                }

                if (CellEs.TrailEs.HaveAnyTrail(idx_0))
                {
                    CellEs.TrailEs.IsVisible(PlayerTypes.First, idx_0).IsVisibleC.IsVisible = false;
                    CellEs.TrailEs.IsVisible(PlayerTypes.Second, idx_0).IsVisibleC.IsVisible = false;

                    //if (unit_0.Have)
                    //{
                    //if (!unit_0.IsAnimal)
                    //{
                    //    CellTrailEs.IsVisible(ownUnit_0.Player, idx_0).IsVisible = true;
                    //}


                    foreach (var idx_1 in CellEs.GetIdxsAround(idx_0))
                    {
                        var unit_1 = UnitEs.Main(idx_1).UnitTC;
                        var ownUnit_1 = UnitEs.Main(idx_1).OwnerC;


                        if (unit_1.Have && !unit_1.IsAnimal)
                        {
                            CellEs.TrailEs.IsVisible(ownUnit_1.Player, idx_0).IsVisibleC.IsVisible = true;
                        }
                    }
                    //}
                }
            }
        }
    }
}