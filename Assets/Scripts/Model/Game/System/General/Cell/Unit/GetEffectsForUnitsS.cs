using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    sealed class GetEffectsForUnitsS : SystemModelGameAbs
    {
        internal GetEffectsForUnitsS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Get(in byte cell_0)
        {
            eMG.UnitEffectsE(cell_0).HaveKingEffect = false;

            if (eMG.CellE(cell_0).IsActiveParentSelf)
            {
                foreach (var idx_1 in eMG.AroundCellsE(cell_0).CellsAround)
                {
                    if (eMG.UnitTC(idx_1).Is(UnitTypes.King))
                    {
                        if (eMG.UnitPlayerTC(idx_1).Is(eMG.UnitPlayerTC(cell_0).PlayerT))
                        {
                            eMG.PlayerInfoE(eMG.UnitPlayerTC(idx_1).PlayerT).WhereKingEffects.Add(cell_0);
                            eMG.UnitEffectsE(cell_0).HaveKingEffect = true;
                        }
                    }
                }
            }
        }
    }
}