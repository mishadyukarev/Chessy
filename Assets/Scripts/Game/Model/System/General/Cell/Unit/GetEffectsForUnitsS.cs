using Chessy.Game.Entity.Model;

namespace Chessy.Game.System.Model
{
    sealed class GetEffectsForUnitsS : SystemModelGameAbs
    {
        internal GetEffectsForUnitsS(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        internal void Get(in byte cell_0)
        {
            e.UnitEffectsE(cell_0).HaveKingEffect = false;

            if (e.CellEs(cell_0).IsActiveParentSelf)
            {
                foreach (var idx_1 in e.CellEs(cell_0).AroundCellsEs.IdxsAround)
                {
                    if (e.UnitTC(idx_1).Is(UnitTypes.King))
                    {
                        if (e.UnitPlayerTC(idx_1).Is(e.UnitPlayerTC(cell_0).Player))
                        {
                            e.PlayerInfoE(e.UnitPlayerTC(idx_1).Player).WhereKingEffects.Add(cell_0);
                            e.UnitEffectsE(cell_0).HaveKingEffect = true;
                        }
                    }
                }
            }
        }
    }
}