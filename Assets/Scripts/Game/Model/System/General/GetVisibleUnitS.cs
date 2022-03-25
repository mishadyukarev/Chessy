using Chessy.Game.Entity.Model;

namespace Chessy.Game.System.Model
{
    public struct GetVisibleUnitS
    {
        public GetVisibleUnitS(in byte cell_0, in EntitiesModelGame e)
        {
            if (e.UnitTC(cell_0).HaveUnit)
            {
                if (e.UnitPlayerTC(cell_0).Is(PlayerTypes.None))
                {
                    if (e.UnitTC(cell_0).IsAnimal)
                    {
                        var isVisForFirst = true;
                        var isVisForSecond = true;

                        if (e.AdultForestC(cell_0).HaveAnyResources)
                        {
                            isVisForFirst = false;
                            isVisForSecond = false;

                            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                            {
                                var idx_1 = e.CellEs(cell_0).AroundCellE(dirT).IdxC.Idx;

                                if (e.UnitTC(idx_1).HaveUnit)
                                {
                                    if (e.UnitPlayerTC(idx_1).Is(PlayerTypes.First)) isVisForFirst = true;
                                    if (e.UnitPlayerTC(idx_1).Is(PlayerTypes.Second)) isVisForSecond = true;
                                }
                            }
                        }

                        e.UnitEs(cell_0).ForPlayer(PlayerTypes.First).IsVisible = isVisForFirst;
                        e.UnitEs(cell_0).ForPlayer(PlayerTypes.Second).IsVisible = isVisForSecond;
                    }
                }
                else
                {
                    e.UnitEs(cell_0).ForPlayer(e.UnitPlayerTC(cell_0).Player).IsVisible = true;

                    if (e.AdultForestC(cell_0).HaveAnyResources)
                    {
                        var isVisibledNextPlayer = false;

                        for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                        {
                            var idx_1 = e.CellEs(cell_0).AroundCellE(dirT).IdxC.Idx;

                            if (e.UnitTC(idx_1).HaveUnit)
                            {
                                if (!e.UnitPlayerTC(idx_1).Is(e.UnitPlayerTC(cell_0).Player))
                                {
                                    isVisibledNextPlayer = true;
                                    break;
                                }
                            }
                        }

                        e.UnitEs(cell_0).ForPlayer(e.NextPlayer(e.UnitPlayerTC(cell_0).Player).Player).IsVisible = isVisibledNextPlayer;
                    }
                    else
                    {
                        e.UnitEs(cell_0).ForPlayer(e.NextPlayer(e.UnitPlayerTC(cell_0).Player).Player).IsVisible = true;
                    }
                }
            }
        }
    }
}