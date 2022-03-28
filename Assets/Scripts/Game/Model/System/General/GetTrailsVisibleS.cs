using Chessy.Game.Entity.Model;

namespace Chessy.Game.System.Model
{
    sealed class GetTrailsVisibleS : SystemModelGameAbs
    {
        internal GetTrailsVisibleS(in EntitiesModelGame eMGame) : base(eMGame) { }

        internal void Get(in byte cell_0)
        {
            if (e.CellEs(cell_0).IsActiveParentSelf)
            {
                for (var dir_0 = DirectTypes.None + 1; dir_0 < DirectTypes.End; dir_0++)
                {
                    e.CellEs(cell_0).Player(PlayerTypes.First).IsVisibleTrail = false;
                    e.CellEs(cell_0).Player(PlayerTypes.Second).IsVisibleTrail = false;

                    if (e.UnitTC(cell_0).HaveUnit) e.CellEs(cell_0).Player(e.UnitPlayerTC(cell_0).Player).IsVisibleTrail = true;


                    for (var dir = DirectTypes.None + 1; dir < DirectTypes.End; dir++)
                    {
                        var idx_1 = e.CellEs(cell_0).AroundCellsEs.AroundCellE(dir).IdxC.Idx;

                        if (e.UnitTC(idx_1).HaveUnit && !e.UnitTC(cell_0).IsAnimal)
                        {
                            e.CellEs(cell_0).Player(e.UnitPlayerTC(idx_1).Player).IsVisibleTrail = true;
                        }
                    }
                }
            }
        }
    }
}