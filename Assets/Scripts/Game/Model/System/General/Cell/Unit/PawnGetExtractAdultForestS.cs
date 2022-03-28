using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Environment;

namespace Chessy.Game.System.Model
{
    sealed class PawnGetExtractAdultForestS : SystemModelGameAbs
    {
        readonly CellEs _cellEs;

        internal PawnGetExtractAdultForestS(in CellEs cellEs, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _cellEs = cellEs;
        }

        internal void Get()
        {
            _cellEs.UnitEs.ExtractE.PawnExtractAdultForestE.Resources = 0;

            if (_cellEs.EnvironmentEs.AdultForestC.HaveAnyResources)
            {
                if (_cellEs.UnitEs.MainE.UnitTC.Is(UnitTypes.Pawn) && _cellEs.UnitEs.MainE.ConditionTC.Is(ConditionUnitTypes.Relaxed) 
                    && !_cellEs.UnitEs.MainToolWeaponE.ToolWeaponTC.Is(ToolWeaponTypes.BowCrossbow, ToolWeaponTypes.Staff))
                {
                    var extract = EnvironmentValues.EXTRACT_PAWM_ADULT_FOREST;

                    if (e.PlayerInfoE(_cellEs.UnitEs.MainE.PlayerTC.Player).MyHeroTC.Is(UnitTypes.Elfemale))
                    {
                        if (_cellEs.UnitEs.MainE.UnitTC.Is(UnitTypes.Pawn))
                        {
                            extract *= 2;
                        }
                    }



                    if (_cellEs.UnitEs.MainToolWeaponE.ToolWeaponTC.Is(ToolWeaponTypes.Axe))
                    {
                        if (_cellEs.UnitEs.MainToolWeaponE.LevelTC.Is(LevelTypes.Second))
                        {
                            extract *= EnvironmentValues.PAWN_TOOL_WEAPON_AXE_LEVEL_SECOND_FOR_EXTACT;
                        }
                    }

                    if (_cellEs.EnvironmentEs.AdultForestC.Resources < extract) extract = _cellEs.EnvironmentEs.AdultForestC.Resources;

                    _cellEs.UnitEs.ExtractE.PawnExtractAdultForestE.Resources = extract;
                }
            }
        }
    }
}