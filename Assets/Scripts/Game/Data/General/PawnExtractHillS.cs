using Chessy.Game.Values.Cell.Environment;

namespace Chessy.Game.System.Model
{
    public struct PawnExtractHillS
    {
        public PawnExtractHillS(in byte idx_0, in EntitiesModel e)
        {
            e.PawnExtractHillE(idx_0).Resources = 0;

            if (e.UnitTC(idx_0).Is(UnitTypes.Pawn) && e.UnitExtraTWTC(idx_0).Is(ToolWeaponTypes.Pick))
            {
                if (e.HillC(idx_0).HaveAnyResources)
                {
                    var extract = EnvironmentValues.PAWN_PICK_EXTRACT_HILL;


                    if (e.HillC(idx_0).Resources < extract) extract = e.HillC(idx_0).Resources;


                    e.PawnExtractHillE(idx_0).Resources = extract;

                }
            }
        }
    }
}