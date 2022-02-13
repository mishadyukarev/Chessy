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
                if (Es.UnitEs(idx_0).UnitE.HaveUnit)
                {
                    if (Es.UnitE(idx_0).IsAnimal)
                    {
                        var isVisForFirst = true;
                        var isVisForSecond = true;

                        if (EnvironmentEs(idx_0).AdultForest.HaveEnvironment)
                        {
                            isVisForFirst = false;
                            isVisForSecond = false;

                            foreach (var idx_1 in CellWorker.GetIdxsAround(idx_0))
                            {
                                if (UnitEs(idx_1).UnitE.HaveUnit)
                                {
                                    if (Es.UnitE(idx_1).Is(PlayerTypes.First)) isVisForFirst = true;
                                    if (Es.UnitE(idx_1).Is(PlayerTypes.Second)) isVisForSecond = true;
                                }
                            }
                        }

                        UnitEs(idx_0).VisibleE(PlayerTypes.First).IsVisibleC.IsVisible = isVisForFirst;
                        UnitEs(idx_0).VisibleE(PlayerTypes.Second).IsVisibleC.IsVisible = isVisForSecond;
                    }
                    else
                    {
                        UnitEs(idx_0).VisibleE(Es.UnitE(idx_0).Owner).IsVisibleC.IsVisible = true;

                        if (EnvironmentEs(idx_0).AdultForest.HaveEnvironment)
                        {
                            var isVisibledNextPlayer = false;

                            foreach (var idx_1 in CellWorker.GetIdxsAround(idx_0))
                            {
                                if (UnitEs(idx_1).UnitE.HaveUnit)
                                {
                                    if (!Es.UnitE(idx_1).Is(Es.UnitE(idx_0).Owner))
                                    {
                                        isVisibledNextPlayer = true;
                                        break;
                                    }
                                }
                            }

                            UnitEs(idx_0).VisibleE(Es.WhoseMoveE.NextPlayerFrom(Es.UnitE(idx_0).Owner)).IsVisibleC.IsVisible = isVisibledNextPlayer;
                        }
                        else
                        {
                            UnitEs(idx_0).VisibleE(Es.WhoseMoveE.NextPlayerFrom(Es.UnitE(idx_0).Owner)).IsVisibleC.IsVisible = true;
                        }
                    }
                }

                BuildEs(idx_0).BuildingVisE(PlayerTypes.First).IsVisibleC.IsVisible = true;
                BuildEs(idx_0).BuildingVisE(PlayerTypes.Second).IsVisibleC.IsVisible = true;

                if (Es.BuildingE(idx_0).HaveBuilding)
                {
                    BuildEs(idx_0).BuildingVisE(Es.BuildingE(idx_0).Owner).IsVisibleC.IsVisible = true;

                    if (EnvironmentEs(idx_0).AdultForest.HaveEnvironment)
                    {
                        var isVisibledNextPlayer = false;

                        foreach (var idx_1 in CellWorker.GetIdxsAround(idx_0))
                        {
                            if (UnitEs(idx_1).UnitE.HaveUnit)
                            {
                                if (!Es.UnitE(idx_1).Is(Es.BuildingE(idx_0).Owner))
                                {
                                    isVisibledNextPlayer = true;
                                    break;
                                }
                            }
                        }
                        BuildEs(idx_0).BuildingVisE(Es.WhoseMoveE.NextPlayerFrom(Es.BuildingE(idx_0).Owner)).IsVisibleC.IsVisible = isVisibledNextPlayer;
                    }
                    else BuildEs(idx_0).BuildingVisE(Es.WhoseMoveE.NextPlayerFrom(Es.BuildingE(idx_0).Owner)).IsVisibleC.IsVisible = true;
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
                        if (UnitEs(idx_1).UnitE.HaveUnit && !Es.UnitE(idx_1).IsAnimal)
                        {
                            TrailEs(idx_0).IsVisible(Es.UnitE(idx_1).Owner).SetVisible(true);
                        }
                    }
                    //}
                }
            }
        }
    }
}