using System;

namespace Chessy.Game.System.Model
{
    public struct GetBuildingVisibleS
    {
        public GetBuildingVisibleS(in byte cell_0, in Chessy.Game.Entity.Model.EntitiesModelGame e)
        {
            if (e.BuildingTC(cell_0).HaveBuilding)
            {
                e.BuildEs(cell_0).SetVisible(e.BuildingPlayerTC(cell_0).Player, true);

                if (e.AdultForestC(cell_0).HaveAnyResources)
                {
                    var isVisibledNextPlayer = false;

                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                    {
                        var idx_1 = e.CellEs(cell_0).AroundCellE(dirT).IdxC.Idx;

                        if (e.UnitTC(idx_1).HaveUnit)
                        {
                            if (!e.UnitPlayerTC(idx_1).Is(e.BuildingPlayerTC(cell_0).Player))
                            {
                                isVisibledNextPlayer = true;
                                break;
                            }
                        }
                    }
                    e.BuildEs(cell_0).SetVisible(e.NextPlayer(e.BuildingPlayerTC(cell_0).Player).Player, isVisibledNextPlayer);
                }
                else e.BuildEs(cell_0).SetVisible(e.NextPlayer(e.BuildingPlayerTC(cell_0).Player).Player, true);


                e.BuildEs(cell_0).SetVisible(PlayerTypes.First, true);
                e.BuildEs(cell_0).SetVisible(PlayerTypes.Second, true);
            }
        }
    }
}