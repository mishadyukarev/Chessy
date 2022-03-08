namespace Chessy.Game
{
    sealed class VisibleUnitAndBuildingS : SystemAbstract, IEcsRunSystem
    {
        internal VisibleUnitAndBuildingS(in EntitiesModel ents) : base(ents)
        {

        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Start_VALUES.ALL_CELLS_AMOUNT; idx_0++)
            {
                if (E.UnitTC(idx_0).HaveUnit)
                {
                    if (E.UnitPlayerTC(idx_0).Is(PlayerTypes.None))
                    {
                        if (E.IsAnimal(E.UnitTC(idx_0).Unit))
                        {
                            var isVisForFirst = true;
                            var isVisForSecond = true;

                            if (E.AdultForestC(idx_0).HaveAnyResources)
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
                    }
                    else
                    {
                        E.UnitEs(idx_0).ForPlayer(E.UnitPlayerTC(idx_0).Player).IsVisible = true;

                        if (E.AdultForestC(idx_0).HaveAnyResources)
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

                            E.UnitEs(idx_0).ForPlayer(E.NextPlayer(E.UnitPlayerTC(idx_0).Player).Player).IsVisible = isVisibledNextPlayer;
                        }
                        else
                        {
                            E.UnitEs(idx_0).ForPlayer(E.NextPlayer(E.UnitPlayerTC(idx_0).Player).Player).IsVisible = true;
                        }
                    }
                }

                E.BuildEs(idx_0).SetVisible(PlayerTypes.First, true);
                E.BuildEs(idx_0).SetVisible(PlayerTypes.Second, true);

                if (E.BuildingTC(idx_0).HaveBuilding)
                {
                    E.BuildEs(idx_0).SetVisible(E.BuildingPlayerTC(idx_0).Player, true);

                    if (E.AdultForestC(idx_0).HaveAnyResources)
                    {
                        var isVisibledNextPlayer = false;

                        for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                        {
                            var idx_1 = E.CellEs(idx_0).AroundCellE(dirT).IdxC.Idx;

                            if (E.UnitTC(idx_1).HaveUnit)
                            {
                                if (!E.UnitPlayerTC(idx_1).Is(E.BuildingPlayerTC(idx_0).Player))
                                {
                                    isVisibledNextPlayer = true;
                                    break;
                                }
                            }
                        }
                        E.BuildEs(idx_0).SetVisible(E.NextPlayer(E.BuildingPlayerTC(idx_0).Player).Player, isVisibledNextPlayer);
                    }
                    else E.BuildEs(idx_0).SetVisible(E.NextPlayer(E.BuildingPlayerTC(idx_0).Player).Player, true);
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

                            if (E.UnitTC(idx_1).HaveUnit && !E.IsAnimal(E.UnitTC(idx_1).Unit))
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