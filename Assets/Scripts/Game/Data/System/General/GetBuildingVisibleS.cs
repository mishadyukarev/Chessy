using System;

namespace Chessy.Game.System.Model
{
    public struct GetBuildingVisibleS
    {
        public GetBuildingVisibleS(in byte idx_0, in Chessy.Game.Entity.Model.EntitiesModel e)
        {
            if (e.BuildingTC(idx_0).HaveBuilding)
            {
                e.BuildEs(idx_0).SetVisible(e.BuildingPlayerTC(idx_0).Player, true);

                if (e.AdultForestC(idx_0).HaveAnyResources)
                {
                    var isVisibledNextPlayer = false;

                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                    {
                        var idx_1 = e.CellEs(idx_0).AroundCellE(dirT).IdxC.Idx;

                        if (e.UnitTC(idx_1).HaveUnit)
                        {
                            if (!e.UnitPlayerTC(idx_1).Is(e.BuildingPlayerTC(idx_0).Player))
                            {
                                isVisibledNextPlayer = true;
                                break;
                            }
                        }
                    }
                    e.BuildEs(idx_0).SetVisible(e.NextPlayer(e.BuildingPlayerTC(idx_0).Player).Player, isVisibledNextPlayer);
                }
                else e.BuildEs(idx_0).SetVisible(e.NextPlayer(e.BuildingPlayerTC(idx_0).Player).Player, true);


                e.BuildEs(idx_0).SetVisible(PlayerTypes.First, true);
                e.BuildEs(idx_0).SetVisible(PlayerTypes.Second, true);
            }
        }
    }
}