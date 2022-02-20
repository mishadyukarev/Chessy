namespace Game.Game
{
    sealed class VisibElseS : SystemAbstract, IEcsRunSystem
    {
        public VisibElseS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                if (E.UnitTC(idx_0).HaveUnit)
                {
                    if (E.UnitMainE(idx_0).IsAnimal)
                    {
                        var isVisForFirst = true;
                        var isVisForSecond = true;

                        if (E.AdultForestC(idx_0).HaveAny)
                        {
                            isVisForFirst = false;
                            isVisForSecond = false;

                            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                            {
                                var idx_1 = E.CellEs(idx_0).AroundCellE(dirT).IdxC.Idx;

                                if (E.UnitTC(idx_1).HaveUnit)
                                {
                                    if (E.UnitPlayerTC(idx_1).Is(PlayerTypes.First)) isVisForFirst = true;
                                    if (E.UnitPlayerTC(idx_1).Is(PlayerTypes.Second)) isVisForSecond = true;
                                }
                            }
                        }

                        E.UnitEs(idx_0).ForPlayer(PlayerTypes.First).IsVisible = isVisForFirst;
                        E.UnitEs(idx_0).ForPlayer(PlayerTypes.Second).IsVisible = isVisForSecond;
                    }
                    else
                    {
                        E.UnitEs(idx_0).ForPlayer(E.UnitPlayerTC(idx_0).Player).IsVisible = true;

                        if (E.AdultForestC(idx_0).HaveAny)
                        {
                            var isVisibledNextPlayer = false;

                            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                            {
                                var idx_1 = E.CellEs(idx_0).AroundCellE(dirT).IdxC.Idx;

                                if (E.UnitTC(idx_1).HaveUnit)
                                {
                                    if (!E.UnitPlayerTC(idx_1).Is(E.UnitPlayerTC(idx_0).Player))
                                    {
                                        isVisibledNextPlayer = true;
                                        break;
                                    }
                                }
                            }

                            E.UnitEs(idx_0).ForPlayer(E.WhoseMove.NextPlayerFrom(E.UnitPlayerTC(idx_0).Player)).IsVisible = isVisibledNextPlayer;
                        }
                        else
                        {
                            E.UnitEs(idx_0).ForPlayer(E.WhoseMove.NextPlayerFrom(E.UnitPlayerTC(idx_0).Player)).IsVisible = true;
                        }
                    }
                }

                E.BuildE(idx_0).IsVisible(PlayerTypes.First) = true;
                E.BuildE(idx_0).IsVisible(PlayerTypes.Second) = true;

                if (E.BuildTC(idx_0).HaveBuilding)
                {
                    E.BuildE(idx_0).IsVisible(E.BuildPlayerTC(idx_0).Player) = true;

                    if (E.AdultForestC(idx_0).HaveAny)
                    {
                        var isVisibledNextPlayer = false;

                        for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                        {
                            var idx_1 = E.CellEs(idx_0).AroundCellE(dirT).IdxC.Idx;

                            if (E.UnitTC(idx_1).HaveUnit)
                            {
                                if (!E.UnitPlayerTC(idx_1).Is(E.BuildPlayerTC(idx_0).Player))
                                {
                                    isVisibledNextPlayer = true;
                                    break;
                                }
                            }
                        }
                        E.BuildE(idx_0).IsVisible(E.WhoseMove.NextPlayerFrom(E.BuildPlayerTC(idx_0).Player)) = isVisibledNextPlayer;
                    }
                    else E.BuildE(idx_0).IsVisible(E.WhoseMove.NextPlayerFrom(E.BuildPlayerTC(idx_0).Player)) = true;
                }

                for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                {
                    if (E.CellEs(idx_0).TrailHealthC(dirT).Health > 0)
                    {
                        E.CellEs(idx_0).Player(PlayerTypes.First).IsVisibleTrail = false;
                        E.CellEs(idx_0).Player(PlayerTypes.Second).IsVisibleTrail = false;

                        //if (unit_0.Have)
                        //{
                        //if (!unit_0.IsAnimal)
                        //{
                        //    CellTrailEs.IsVisible(ownUnit_0.Player, idx_0).IsVisible = true;
                        //}

                        if (E.UnitTC(idx_0).HaveUnit) E.CellEs(idx_0).Player(E.UnitPlayerTC(idx_0).Player).IsVisibleTrail = true;


                        for (var dir = DirectTypes.None + 1; dir < DirectTypes.End; dir++)
                        {
                            var idx_1 = E.CellEs(idx_0).AroundCellE(dir).IdxC.Idx;

                            if (E.UnitTC(idx_1).HaveUnit && !E.UnitMainE(idx_1).IsAnimal)
                            {
                                E.CellEs(idx_0).Player(E.UnitPlayerTC(idx_1).Player).IsVisibleTrail = true;
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