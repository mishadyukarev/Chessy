using Chessy.Game.Entity.Model;

namespace Chessy.Game.System.Model
{
    sealed class GetEffectsForUnitsS : SystemModelGameAbs
    {
        internal GetEffectsForUnitsS(in EntitiesModelGame eMGame) : base(eMGame) { }

        internal void Get(in byte cell_0)
        {
            eMGame.UnitEffectsE(cell_0).HaveKingEffect = false;

            if (eMGame.CellEs(cell_0).IsActiveParentSelf)
            {
                foreach (var idx_1 in eMGame.CellEs(cell_0).AroundCellsEs.IdxsAround)
                {
                    if (eMGame.UnitTC(idx_1).Is(UnitTypes.King))
                    {
                        if (eMGame.UnitPlayerTC(idx_1).Is(eMGame.UnitPlayerTC(cell_0).Player))
                        {
                            eMGame.PlayerInfoE(eMGame.UnitPlayerTC(idx_1).Player).WhereKingEffects.Add(cell_0);
                            eMGame.UnitEffectsE(cell_0).HaveKingEffect = true;
                        }
                    }
                }
            }
        }
    }
}