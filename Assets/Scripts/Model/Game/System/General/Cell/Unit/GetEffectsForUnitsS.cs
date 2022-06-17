using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModel
    {
        internal void GetEffectsForUnits(in byte cell_0)
        {
            eMG.HaveKingEffect(cell_0) = false;

            if (!eMG.IsBorder(cell_0))
            {
                foreach (var idx_1 in eMG.AroundCellsE(cell_0).CellsAround)
                {
                    if (eMG.UnitTC(idx_1).Is(UnitTypes.King))
                    {
                        if (eMG.UnitPlayerTC(idx_1).Is(eMG.UnitPlayerTC(cell_0).PlayerT))
                        {
                            eMG.PlayerInfoE(eMG.UnitPlayerTC(idx_1).PlayerT).WhereKingEffects.Add(cell_0);
                            eMG.HaveKingEffect(cell_0) = true;
                        }
                    }
                }
            }
        }
    }
}