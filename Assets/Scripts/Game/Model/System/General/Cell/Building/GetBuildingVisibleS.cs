using Chessy.Game.Entity.Model;

namespace Chessy.Game.System.Model
{
    sealed class GetBuildingVisibleS : SystemModelGameAbs
    {
        internal GetBuildingVisibleS(in EntitiesModelGame eMGame) : base(eMGame) { }

        internal void Get(in byte cell_0)
        {
            if (eMGame.BuildingTC(cell_0).HaveBuilding)
            {
                eMGame.BuildEs(cell_0).SetVisible(eMGame.BuildingPlayerTC(cell_0).Player, true);

                if (eMGame.AdultForestC(cell_0).HaveAnyResources)
                {
                    var isVisibledNextPlayer = false;

                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                    {
                        var idx_1 = eMGame.CellEs(cell_0).AroundCellsEs.AroundCellE(dirT).IdxC.Idx;

                        if (eMGame.UnitTC(idx_1).HaveUnit)
                        {
                            if (!eMGame.UnitPlayerTC(idx_1).Is(eMGame.BuildingPlayerTC(cell_0).Player))
                            {
                                isVisibledNextPlayer = true;
                                break;
                            }
                        }
                    }
                    eMGame.BuildEs(cell_0).SetVisible(eMGame.NextPlayer(eMGame.BuildingPlayerTC(cell_0).Player).Player, isVisibledNextPlayer);
                }
                else eMGame.BuildEs(cell_0).SetVisible(eMGame.NextPlayer(eMGame.BuildingPlayerTC(cell_0).Player).Player, true);


                eMGame.BuildEs(cell_0).SetVisible(PlayerTypes.First, true);
                eMGame.BuildEs(cell_0).SetVisible(PlayerTypes.Second, true);
            }
        }
    }
}