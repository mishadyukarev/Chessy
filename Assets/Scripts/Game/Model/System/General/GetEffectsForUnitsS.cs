using Chessy.Game.Entity.Model;

namespace Chessy.Game.System.Model
{
    public struct GetEffectsForUnitsS
    {
        public GetEffectsForUnitsS(in byte cell_0, in Chessy.Game.Entity.Model.EntitiesModelGame e)
        {
            e.UnitEffectsE(cell_0).HaveKingEffect = false;

            if (e.CellEs(cell_0).IsActiveParentSelf)
            {
                foreach (var idx_1 in e.CellEs(cell_0).IdxsAround)
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