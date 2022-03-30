using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Environment;

namespace Chessy.Game.System.Model
{
    sealed class PawnExtractHillS : SystemModelGameAbs
    {
        internal PawnExtractHillS(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        internal void Get(in byte cell_0)
        {
            e.PawnExtractHillE(cell_0).Resources = 0;

            if (e.UnitTC(cell_0).Is(UnitTypes.Pawn) && e.UnitExtraTWTC(cell_0).Is(ToolWeaponTypes.Pick))
            {
                if (e.HillC(cell_0).HaveAnyResources)
                {
                    var extract = EnvironmentValues.PAWN_PICK_EXTRACT_HILL;


                    if (e.HillC(cell_0).Resources < extract) extract = e.HillC(cell_0).Resources;


                    e.PawnExtractHillE(cell_0).Resources = extract;

                }
            }
        }
    }
}