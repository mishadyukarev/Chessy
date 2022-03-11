using System;

namespace Chessy.Game.System.Model
{
    public struct GetEffectsForUnitsS
    {
        public GetEffectsForUnitsS(in byte idx_0, in EntitiesModel e)
        {
            if (!e.UnitTC(idx_0).HaveUnit) throw new Exception();

            foreach (var idx_1 in e.CellEs(idx_0).IdxsAround)
            {
                if (e.UnitTC(idx_1).Is(UnitTypes.King))
                {
                    if (e.UnitPlayerTC(idx_1).Is(e.UnitPlayerTC(idx_0).Player))
                    {
                        e.PlayerInfoE(e.UnitPlayerTC(idx_1).Player).WhereKingEffects.Add(idx_0);
                    }
                }
            }
        }
    }
}