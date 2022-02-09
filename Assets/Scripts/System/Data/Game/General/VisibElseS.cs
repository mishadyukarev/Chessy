namespace Game.Game
{
    sealed class VisibElseS : SystemAbstract, IEcsRunSystem
    {
        public VisibElseS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                var unit_0 = UnitEs(idx_0).TypeE.UnitTC;
                var ownUnit_0 = UnitEs(idx_0).OwnerE.OwnerC;


                if (UnitEs(idx_0).TypeE.HaveUnit)
                {
                    if (unit_0.IsAnimal)
                    {
                        var isVisForFirst = true;
                        var isVisForSecond = true;

                        if (EnvironmentEs(idx_0).AdultForest.HaveEnvironment)
                        {
                            isVisForFirst = false;
                            isVisForSecond = false;

                            foreach (var idx_1 in CellWorker.GetIdxsAround(idx_0))
                            {
                                if (UnitEs(idx_1).TypeE.HaveUnit)
                                {
                                    if (UnitEs(idx_1).OwnerE.OwnerC.Is(PlayerTypes.First)) isVisForFirst = true;
                                    if (UnitEs(idx_1).OwnerE.OwnerC.Is(PlayerTypes.Second)) isVisForSecond = true;
                                }
                            }
                        }

                        UnitEs(idx_0).VisibleE(PlayerTypes.First).IsVisibleC.IsVisible = isVisForFirst;
                        UnitEs(idx_0).VisibleE(PlayerTypes.Second).IsVisibleC.IsVisible = isVisForSecond;
                    }
                    else
                    {
                        UnitEs(idx_0).VisibleE(ownUnit_0.Player).IsVisibleC.IsVisible = true;

                        if (EnvironmentEs(idx_0).AdultForest.HaveEnvironment)
                        {
                            var isVisibledNextPlayer = false;

                            foreach (var idx_1 in CellWorker.GetIdxsAround(idx_0))
                            {
                                var unit_1 = UnitEs(idx_1).TypeE.UnitTC;
                                var ownUnit_1 = UnitEs(idx_1).OwnerE.OwnerC;

                                if (UnitEs(idx_1).TypeE.HaveUnit)
                                {
                                    if (!ownUnit_1.Is(ownUnit_0.Player))
                                    {
                                        isVisibledNextPlayer = true;
                                        break;
                                    }
                                }
                            }

                            UnitEs(idx_0).VisibleE(Es.WhoseMoveE.NextPlayerFrom(ownUnit_0.Player)).IsVisibleC.IsVisible = isVisibledNextPlayer;
                        }
                        else
                        {
                            UnitEs(idx_0).VisibleE(Es.WhoseMoveE.NextPlayerFrom(ownUnit_0.Player)).IsVisibleC.IsVisible = true;
                        }
                    }
                }



                var build_0 = BuildEs(idx_0).BuildingE.BuildTC;

                BuildEs(idx_0).BuildingVisE(PlayerTypes.First).IsVisibleC.IsVisible = true;
                BuildEs(idx_0).BuildingVisE(PlayerTypes.Second).IsVisibleC.IsVisible = true;

                if (build_0.Have)
                {
                    var ownBuild_0 = BuildEs(idx_0).BuildingE.OwnerC;

                    BuildEs(idx_0).BuildingVisE(ownBuild_0.Player).IsVisibleC.IsVisible = true;

                    if (EnvironmentEs(idx_0).AdultForest.HaveEnvironment)
                    {
                        var isVisibledNextPlayer = false;

                        foreach (var idx_1 in CellWorker.GetIdxsAround(idx_0))
                        {
                            var unit_1 = UnitEs(idx_1).TypeE.UnitTC;
                            var ownUnit_1 = UnitEs(idx_1).OwnerE.OwnerC;

                            if (UnitEs(idx_1).TypeE.HaveUnit)
                            {
                                if (!ownUnit_1.Is(ownBuild_0.Player))
                                {
                                    isVisibledNextPlayer = true;
                                    break;
                                }
                            }
                        }
                        BuildEs(idx_0).BuildingVisE(Es.WhoseMoveE.NextPlayerFrom(ownBuild_0.Player)).IsVisibleC.IsVisible = isVisibledNextPlayer;
                    }
                    else BuildEs(idx_0).BuildingVisE(Es.WhoseMoveE.NextPlayerFrom(ownBuild_0.Player)).IsVisibleC.IsVisible = true;
                }

                if (TrailEs(idx_0).HaveAnyTrail)
                {
                    TrailEs(idx_0).IsVisible(PlayerTypes.First).SetVisible(false);
                    TrailEs(idx_0).IsVisible(PlayerTypes.Second).SetVisible(false);

                    //if (unit_0.Have)
                    //{
                    //if (!unit_0.IsAnimal)
                    //{
                    //    CellTrailEs.IsVisible(ownUnit_0.Player, idx_0).IsVisible = true;
                    //}


                    foreach (var idx_1 in CellWorker.GetIdxsAround(idx_0))
                    {
                        var unit_1 = UnitEs(idx_1).TypeE.UnitTC;
                        var ownUnit_1 = UnitEs(idx_1).OwnerE.OwnerC;


                        if (UnitEs(idx_1).TypeE.HaveUnit && !unit_1.IsAnimal)
                        {
                            TrailEs(idx_0).IsVisible(ownUnit_1.Player).SetVisible(true);
                        }
                    }
                    //}
                }
            }
        }
    }
}