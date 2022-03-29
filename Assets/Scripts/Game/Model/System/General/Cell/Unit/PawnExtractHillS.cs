using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Environment;

namespace Chessy.Game.System.Model
{
    sealed class PawnExtractHillS
    {
        readonly CellEs _cellEs;

        internal PawnExtractHillS(in CellEs cellEs)
        {
            _cellEs = cellEs;
        }

        internal void Get()
        {
            _cellEs.UnitEs.ExtractE.PawnExtractHillE.Resources = 0;

            if (_cellEs.UnitTC.Is(UnitTypes.Pawn) && _cellEs.UnitExtraTWE.ToolWeaponTC.Is(ToolWeaponTypes.Pick))
            {
                if (_cellEs.EnvironmentEs.HillC.HaveAnyResources)
                {
                    var extract = EnvironmentValues.PAWN_PICK_EXTRACT_HILL;


                    if (_cellEs.EnvironmentEs.HillC.Resources < extract) extract = _cellEs.EnvironmentEs.HillC.Resources;


                    _cellEs.UnitEs.ExtractE.PawnExtractHillE.Resources = extract;

                }
            }
        }
    }
}