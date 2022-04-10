using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values;

namespace Chessy.Game.Model.System
{
    sealed class PawnExtractHillS : SystemModel
    {
        internal PawnExtractHillS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        internal void Get(in byte cell_0)
        {
            eMG.PawnExtractHillC(cell_0).Resources = 0;

            if (eMG.UnitTC(cell_0).Is(UnitTypes.Pawn) && eMG.ExtraToolWeaponTC(cell_0).Is(ToolWeaponTypes.Pick) && eMG.UnitConditionT(cell_0) == ConditionUnitTypes.Relaxed)
            {
                if (eMG.HillC(cell_0).HaveAnyResources)
                {
                    var extract = ExtractPawnValues.PAWN_PICK_EXTRACT_HILL;


                    if (eMG.HillC(cell_0).Resources < extract) extract = eMG.HillC(cell_0).Resources;


                    eMG.PawnExtractHillC(cell_0).Resources = extract;

                }
            }
        }
    }
}