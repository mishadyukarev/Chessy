using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Environment;
using Chessy.Common.Entity;
using Chessy.Common.Model.System;

namespace Chessy.Game.Model.System
{
    sealed class PawnExtractHillS : SystemModelGameAbs
    {
        internal PawnExtractHillS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Get(in byte cell_0)
        {
            eMG.PawnExtractHillE(cell_0).Resources = 0;

            if (eMG.UnitTC(cell_0).Is(UnitTypes.Pawn) && eMG.UnitExtraTWTC(cell_0).Is(ToolWeaponTypes.Pick))
            {
                if (eMG.HillC(cell_0).HaveAnyResources)
                {
                    var extract = EnvironmentValues.PAWN_PICK_EXTRACT_HILL;


                    if (eMG.HillC(cell_0).Resources < extract) extract = eMG.HillC(cell_0).Resources;


                    eMG.PawnExtractHillE(cell_0).Resources = extract;

                }
            }
        }
    }
}