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
                    if (Es.UnitEs(idx_0).IsAnimal)
                    {
                        var isVisForFirst = true;
                        var isVisForSecond = true;

                        if (Es.AdultForestC(idx_0).HaveAny)
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

                        Es.UnitEs(idx_0).ForPlayer(PlayerTypes.First).IsVisibleC = isVisForFirst;
                        Es.UnitEs(idx_0).ForPlayer(PlayerTypes.Second).IsVisibleC = isVisForSecond;
                    }
                    else
                    {
                        Es.UnitEs(idx_0).ForPlayer(Es.UnitPlayerTC(idx_0).Player).IsVisibleC = true;

                        if (Es.AdultForestC(idx_0).HaveAny)
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

                            Es.UnitEs(idx_0).ForPlayer(Es.WhoseMove.NextPlayerFrom(Es.UnitPlayerTC(idx_0).Player)).IsVisibleC = isVisibledNextPlayer;
                        }
                        else
                        {
                            Es.UnitEs(idx_0).ForPlayer(Es.WhoseMove.NextPlayerFrom(Es.UnitPlayerTC(idx_0).Player)).IsVisibleC = true;
                        }
                    }
                }

                Es.BuildE(idx_0).IsVisible(PlayerTypes.First) = true;
                Es.BuildE(idx_0).IsVisible(PlayerTypes.Second) = true;

                if (Es.BuildTC(idx_0).HaveBuilding)
                {
                    Es.BuildE(idx_0).IsVisible(Es.BuildPlayerTC(idx_0).Player) = true;

                    if (Es.AdultForestC(idx_0).HaveAny)
                    {
                        var isVisibledNextPlayer = false;

                        foreach (var idx_1 in CellWorker.GetIdxsAround(idx_0))
                        {
                            if (Es.UnitTC(idx_1).HaveUnit)
                            {
                                if (!Es.UnitPlayerTC(idx_1).Is(Es.BuildPlayerTC(idx_0).Player))
                                {
                                    isVisibledNextPlayer = true;
                                    break;
                                }
                            }
                        }
                        Es.BuildE(idx_0).IsVisible(Es.WhoseMove.NextPlayerFrom(Es.BuildPlayerTC(idx_0).Player)) = isVisibledNextPlayer;
                    }
                    else Es.BuildE(idx_0).IsVisible(Es.WhoseMove.NextPlayerFrom(Es.BuildPlayerTC(idx_0).Player)) = true;
                }

                for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                {
                    if (Es.TrailEs(idx_0).Trail(dirT).HealthC.Health > 0)
                    {
                        Es.TrailEs(idx_0).IsVisible(PlayerTypes.First).IsVisibleC = false;
                        Es.TrailEs(idx_0).IsVisible(PlayerTypes.Second).IsVisibleC = false;

                        //if (unit_0.Have)
                        //{
                        //if (!unit_0.IsAnimal)
                        //{
                        //    CellTrailEs.IsVisible(ownUnit_0.Player, idx_0).IsVisible = true;
                        //}


                        foreach (var idx_1 in CellWorker.GetIdxsAround(idx_0))
                        {
                            if (Es.UnitTC(idx_1).HaveUnit && !Es.UnitEs(idx_1).IsAnimal)
                            {
                                Es.TrailEs(idx_0).IsVisible(Es.UnitPlayerTC(idx_1).Player).IsVisibleC = true;
                            }
                        }
                        //}

                        break;

                    }
                }  
            }
        }
    }
}