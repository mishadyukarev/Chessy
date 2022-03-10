namespace Chessy.Game.System.Model
{
    sealed class VisibleUnitAndBuildingS : CellSystem, IEcsRunSystem
    {
        internal VisibleUnitAndBuildingS(in byte idx, in EntitiesModel eM) : base(idx, eM)
        {
        }

        public void Run()
        {
            if (E.UnitTC(Idx).HaveUnit)
            {
                if (E.UnitPlayerTC(Idx).Is(PlayerTypes.None))
                {
                    if (E.IsAnimal(E.UnitTC(Idx).Unit))
                    {
                        var isVisForFirst = true;
                        var isVisForSecond = true;

                        if (E.AdultForestC(Idx).HaveAnyResources)
                        {
                            isVisForFirst = false;
                            isVisForSecond = false;

                            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                            {
                                var idx_1 = E.CellEs(Idx).AroundCellE(dirT).IdxC.Idx;

                                if (E.UnitTC(idx_1).HaveUnit)
                                {
                                    if (E.UnitPlayerTC(idx_1).Is(PlayerTypes.First)) isVisForFirst = true;
                                    if (E.UnitPlayerTC(idx_1).Is(PlayerTypes.Second)) isVisForSecond = true;
                                }
                            }
                        }

                        E.UnitEs(Idx).ForPlayer(PlayerTypes.First).IsVisible = isVisForFirst;
                        E.UnitEs(Idx).ForPlayer(PlayerTypes.Second).IsVisible = isVisForSecond;
                    }
                }
                else
                {
                    E.UnitEs(Idx).ForPlayer(E.UnitPlayerTC(Idx).Player).IsVisible = true;

                    if (E.AdultForestC(Idx).HaveAnyResources)
                    {
                        var isVisibledNextPlayer = false;

                        for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                        {
                            var idx_1 = E.CellEs(Idx).AroundCellE(dirT).IdxC.Idx;

                            if (E.UnitTC(idx_1).HaveUnit)
                            {
                                if (!E.UnitPlayerTC(idx_1).Is(E.UnitPlayerTC(Idx).Player))
                                {
                                    isVisibledNextPlayer = true;
                                    break;
                                }
                            }
                        }

                        E.UnitEs(Idx).ForPlayer(E.NextPlayer(E.UnitPlayerTC(Idx).Player).Player).IsVisible = isVisibledNextPlayer;
                    }
                    else
                    {
                        E.UnitEs(Idx).ForPlayer(E.NextPlayer(E.UnitPlayerTC(Idx).Player).Player).IsVisible = true;
                    }
                }
            }

            E.BuildEs(Idx).SetVisible(PlayerTypes.First, true);
            E.BuildEs(Idx).SetVisible(PlayerTypes.Second, true);

            if (E.BuildingTC(Idx).HaveBuilding)
            {
                E.BuildEs(Idx).SetVisible(E.BuildingPlayerTC(Idx).Player, true);

                if (E.AdultForestC(Idx).HaveAnyResources)
                {
                    var isVisibledNextPlayer = false;

                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                    {
                        var idx_1 = E.CellEs(Idx).AroundCellE(dirT).IdxC.Idx;

                        if (E.UnitTC(idx_1).HaveUnit)
                        {
                            if (!E.UnitPlayerTC(idx_1).Is(E.BuildingPlayerTC(Idx).Player))
                            {
                                isVisibledNextPlayer = true;
                                break;
                            }
                        }
                    }
                    E.BuildEs(Idx).SetVisible(E.NextPlayer(E.BuildingPlayerTC(Idx).Player).Player, isVisibledNextPlayer);
                }
                else E.BuildEs(Idx).SetVisible(E.NextPlayer(E.BuildingPlayerTC(Idx).Player).Player, true);
            }

            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
            {
                if (E.CellEs(Idx).TrailHealthC(dirT).Health > 0)
                {
                    E.CellEs(Idx).Player(PlayerTypes.First).IsVisibleTrail = false;
                    E.CellEs(Idx).Player(PlayerTypes.Second).IsVisibleTrail = false;

                    //if (unit_0.Have)
                    //{
                    //if (!unit_0.IsAnimal)
                    //{
                    //    CellTrailEs.IsVisible(ownUnit_0.Player, Idx).IsVisible = true;
                    //}

                    if (E.UnitTC(Idx).HaveUnit) E.CellEs(Idx).Player(E.UnitPlayerTC(Idx).Player).IsVisibleTrail = true;


                    for (var dir = DirectTypes.None + 1; dir < DirectTypes.End; dir++)
                    {
                        var idx_1 = E.CellEs(Idx).AroundCellE(dir).IdxC.Idx;

                        if (E.UnitTC(idx_1).HaveUnit && !E.IsAnimal(E.UnitTC(idx_1).Unit))
                        {
                            E.CellEs(Idx).Player(E.UnitPlayerTC(idx_1).Player).IsVisibleTrail = true;
                        }
                    }
                    //}

                    break;

                }
            }
        }
    }
}