using Chessy.Game.Entity.Model;

namespace Chessy.Game.System.Model
{
    public struct GetEffectsForUnitsS
    {
        public GetEffectsForUnitsS(in byte idx_0, in Chessy.Game.Entity.Model.EntitiesModel e)
        {
            e.UnitEffectsE(idx_0).HaveKingEffect = false;

            if (e.CellEs(idx_0).IsActiveParentSelf)
            {
                foreach (var idx_1 in e.CellEs(idx_0).IdxsAround)
                {
                    if (e.UnitTC(idx_1).Is(UnitTypes.King))
                    {
                        if (e.UnitPlayerTC(idx_1).Is(e.UnitPlayerTC(idx_0).Player))
                        {
                            e.PlayerInfoE(e.UnitPlayerTC(idx_1).Player).WhereKingEffects.Add(idx_0);
                            e.UnitEffectsE(idx_0).HaveKingEffect = true;
                        }
                    }
                }
            }
        }
    }
}