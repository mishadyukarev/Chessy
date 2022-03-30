using Chessy.Game.Entity.Model;

namespace Chessy.Game.System.Model
{
    sealed class GetBuildingVisibleS : SystemModelGameAbs
    {
        internal GetBuildingVisibleS(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        internal void Get(in byte cell_0)
        {
            if (e.BuildingTC(cell_0).HaveBuilding)
            {
                e.BuildEs(cell_0).SetVisible(e.BuildingPlayerTC(cell_0).Player, true);

                if (e.AdultForestC(cell_0).HaveAnyResources)
                {
                    var isVisibledNextPlayer = false;

                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                    {
                        var idx_1 = e.CellEs(cell_0).AroundCellsEs.AroundCellE(dirT).IdxC.Idx;

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