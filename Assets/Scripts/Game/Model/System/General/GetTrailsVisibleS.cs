using Chessy.Game.Entity.Model;

namespace Chessy.Game.System.Model
{
    sealed class GetTrailsVisibleS : SystemModelGameAbs
    {
        internal GetTrailsVisibleS(in EntitiesModelGame eMGame) : base(eMGame) { }

        internal void Get(in byte cell_0)
        {
            if (eMGame.CellEs(cell_0).IsActiveParentSelf)
            {
                for (var dir_0 = DirectTypes.None + 1; dir_0 < DirectTypes.End; dir_0++)
                {
                    eMGame.CellEs(cell_0).Player(PlayerTypes.First).IsVisibleTrail = false;
                    eMGame.CellEs(cell_0).Player(PlayerTypes.Second).IsVisibleTrail = false;

                    if (eMGame.UnitTC(cell_0).HaveUnit) eMGame.CellEs(cell_0).Player(eMGame.UnitPlayerTC(cell_0).Player).IsVisibleTrail = true;


                    for (var dir = DirectTypes.None + 1; dir < DirectTypes.End; dir++)
                    {
                        var idx_1 = eMGame.CellEs(cell_0).AroundCellsEs.AroundCellE(dir).IdxC.Idx;

                        if (eMGame.UnitTC(idx_1).HaveUnit && !eMGame.UnitTC(cell_0).IsAnimal)
                        {
                            eMGame.CellEs(cell_0).Player(eMGame.UnitPlayerTC(idx_1).Player).IsVisibleTrail = true;
                        }
                    }
                }
            }
        }
    }
}