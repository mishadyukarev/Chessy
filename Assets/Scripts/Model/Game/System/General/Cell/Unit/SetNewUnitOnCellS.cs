using Chessy.Game.Values.Cell.Unit.Stats;

namespace Chessy.Game.Model.System
{
    sealed partial class SystemsModelGame
    {
        internal void SetNewUnitOnCellS(in UnitTypes unitT, in PlayerTypes playerT, in byte cellIdxForSetting)
        {
            _e.UnitMainE(cellIdxForSetting).Set(unitT, LevelTypes.First, playerT, ConditionUnitTypes.None, false);
            _e.StatsUnitE(cellIdxForSetting).Set(HpValues.MAX, StepValues.MAX, WaterValues.MAX);
            _e.UnitExtraTWE(cellIdxForSetting).Set(ToolWeaponTypes.None, LevelTypes.None, 0);
            _e.UnitEffectsE(cellIdxForSetting).Set(0, 0, 0, false);

            if (_e.UnitT(cellIdxForSetting).Is(UnitTypes.Pawn))
            {
                _e.PlayerInfoE(playerT).PawnInfoC.SetPawn();
            }



            if (unitT == UnitTypes.Pawn)
            {
                _e.PlayerInfoE(playerT).PawnInfoC.PeopleInCity--;

                _e.MainToolWeaponE(cellIdxForSetting).Set(ToolWeaponTypes.Axe, LevelTypes.First);
            }

            else
            {
                if (unitT.Is(UnitTypes.Tree)) _e.HaveTreeUnit = true;


                if (unitT.IsGod())
                {
                    _e.PlayerInfoE(playerT).GodInfoE.HaveHeroInInventor = false;
                }
                else if (unitT == UnitTypes.King)
                {
                    _e.PlayerInfoE(playerT).KingInfoE.CellKing = cellIdxForSetting;
                    _e.PlayerInfoE(playerT).KingInfoE.HaveInInventor = false;
                }

                _e.MainToolWeaponE(cellIdxForSetting).Set(ToolWeaponTypes.None, LevelTypes.None);
            }
        }
    }
}