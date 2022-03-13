namespace Chessy.Game.System.Model
{
    public struct GetVisibleUnitS
    {
        public GetVisibleUnitS(in byte idx_0, in EntitiesModel e)
        {
            if (e.UnitTC(idx_0).HaveUnit)
            {
                if (e.UnitPlayerTC(idx_0).Is(PlayerTypes.None))
                {
                    if (e.IsAnimal(e.UnitTC(idx_0).Unit))
                    {
                        var isVisForFirst = true;
                        var isVisForSecond = true;

                        if (e.AdultForestC(idx_0).HaveAnyResources)
                        {
                            isVisForFirst = false;
                            isVisForSecond = false;

                            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                            {
                                var idx_1 = e.CellEs(idx_0).AroundCellE(dirT).IdxC.Idx;

                                if (e.UnitTC(idx_1).HaveUnit)
                                {
                                    if (e.UnitPlayerTC(idx_1).Is(PlayerTypes.First)) isVisForFirst = true;
                                    if (e.UnitPlayerTC(idx_1).Is(PlayerTypes.Second)) isVisForSecond = true;
                                }
                            }
                        }

                        e.UnitEs(idx_0).ForPlayer(PlayerTypes.First).IsVisible = isVisForFirst;
                        e.UnitEs(idx_0).ForPlayer(PlayerTypes.Second).IsVisible = isVisForSecond;
                    }
                }
                else
                {
                    e.UnitEs(idx_0).ForPlayer(e.UnitPlayerTC(idx_0).Player).IsVisible = true;

                    if (e.AdultForestC(idx_0).HaveAnyResources)
                    {
                        var isVisibledNextPlayer = false;

                        for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                        {
                            var idx_1 = e.CellEs(idx_0).AroundCellE(dirT).IdxC.Idx;

                            if (e.UnitTC(idx_1).HaveUnit)
                            {
                                if (!e.UnitPlayerTC(idx_1).Is(e.UnitPlayerTC(idx_0).Player))
                                {
                                    isVisibledNextPlayer = true;
                                    break;
                                }
                            }
                        }

                        e.UnitEs(idx_0).ForPlayer(e.NextPlayer(e.UnitPlayerTC(idx_0).Player).Player).IsVisible = isVisibledNextPlayer;
                    }
                    else
                    {
                        e.UnitEs(idx_0).ForPlayer(e.NextPlayer(e.UnitPlayerTC(idx_0).Player).Player).IsVisible = true;
                    }
                }
            }
        }
    }
}