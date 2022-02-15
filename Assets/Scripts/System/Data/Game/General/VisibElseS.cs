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
                if (Es.UnitTC(idx_0).HaveUnit)
                {
                    if (Es.UnitTC(idx_0).IsAnimal)
                    {
                        var isVisForFirst = true;
                        var isVisForSecond = true;

                        if (Es.EnvironmentEs(idx_0).AdultForest.HaveEnvironment)
                        {
                            isVisForFirst = false;
                            isVisForSecond = false;

                            foreach (var idx_1 in CellWorker.GetIdxsAround(idx_0))
                            {
                                if (Es.UnitTC(idx_1).HaveUnit)
                                {
                                    if (Es.UnitPlayerTC(idx_1).Is(PlayerTypes.First)) isVisForFirst = true;
                                    if (Es.UnitPlayerTC(idx_1).Is(PlayerTypes.Second)) isVisForSecond = true;
                                }
                            }
                        }

                        Es.UnitEs(idx_0).VisibleE(PlayerTypes.First).SetVisible(isVisForFirst);
                        Es.UnitEs(idx_0).VisibleE(PlayerTypes.Second).SetVisible(isVisForSecond);
                    }
                    else
                    {
                        Es.UnitEs(idx_0).VisibleE(Es.UnitPlayerTC(idx_0).Player).SetVisible(true);

                        if (Es.EnvironmentEs(idx_0).AdultForest.HaveEnvironment)
                        {
                            var isVisibledNextPlayer = false;

                            foreach (var idx_1 in CellWorker.GetIdxsAround(idx_0))
                            {
                                if (Es.UnitTC(idx_1).HaveUnit)
                                {
                                    if (!Es.UnitPlayerTC(idx_1).Is(Es.UnitPlayerTC(idx_0).Player))
                                    {
                                        isVisibledNextPlayer = true;
                                        break;
                                    }
                                }
                            }

                            Es.UnitEs(idx_0).VisibleE(Es.WhoseMovePlayerTC.NextPlayerFrom(Es.UnitPlayerTC(idx_0).Player)).SetVisible(isVisibledNextPlayer);
                        }
                        else
                        {
                            Es.UnitEs(idx_0).VisibleE(Es.WhoseMovePlayerTC.NextPlayerFrom(Es.UnitPlayerTC(idx_0).Player)).SetVisible(true);
                        }
                    }
                }

                Es.BuildEs(idx_0).BuildingVisE(PlayerTypes.First).IsVisibleC.IsVisible = true;
                Es.BuildEs(idx_0).BuildingVisE(PlayerTypes.Second).IsVisibleC.IsVisible = true;

                if (Es.BuildingE(idx_0).HaveBuilding)
                {
                    Es.BuildEs(idx_0).BuildingVisE(Es.BuildingE(idx_0).Owner).IsVisibleC.IsVisible = true;

                    if (Es.EnvironmentEs(idx_0).AdultForest.HaveEnvironment)
                    {
                        var isVisibledNextPlayer = false;

                        foreach (var idx_1 in CellWorker.GetIdxsAround(idx_0))
                        {
                            if (Es.UnitTC(idx_1).HaveUnit)
                            {
                                if (!Es.UnitPlayerTC(idx_1).Is(Es.BuildingE(idx_0).Owner))
                                {
                                    isVisibledNextPlayer = true;
                                    break;
                                }
                            }
                        }
                        Es.BuildEs(idx_0).BuildingVisE(Es.WhoseMovePlayerTC.NextPlayerFrom(Es.BuildingE(idx_0).Owner)).IsVisibleC.IsVisible = isVisibledNextPlayer;
                    }
                    else Es.BuildEs(idx_0).BuildingVisE(Es.WhoseMovePlayerTC.NextPlayerFrom(Es.BuildingE(idx_0).Owner)).IsVisibleC.IsVisible = true;
                }

                if (Es.TrailEs(idx_0).HaveAnyTrail)
                {
                    Es.TrailEs(idx_0).IsVisible(PlayerTypes.First).SetVisible(false);
                    Es.TrailEs(idx_0).IsVisible(PlayerTypes.Second).SetVisible(false);

                    //if (unit_0.Have)
                    //{
                    //if (!unit_0.IsAnimal)
                    //{
                    //    CellTrailEs.IsVisible(ownUnit_0.Player, idx_0).IsVisible = true;
                    //}


                    foreach (var idx_1 in CellWorker.GetIdxsAround(idx_0))
                    {
                        if (Es.UnitTC(idx_1).HaveUnit && !Es.UnitTC(idx_1).IsAnimal)
                        {
                            Es.TrailEs(idx_0).IsVisible(Es.UnitPlayerTC(idx_1).Player).SetVisible(true);
                        }
                    }
                    //}
                }
            }
        }
    }
}