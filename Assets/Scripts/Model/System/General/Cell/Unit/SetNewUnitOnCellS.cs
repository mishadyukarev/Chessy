﻿using Chessy.Model.Values;
namespace Chessy.Model.System
{
    sealed partial class SystemsModel
    {
        internal void SetNewUnitOnCellS(in UnitTypes unitT, in PlayerTypes playerT, in byte cellIdxForSetting)
        {
            _e.UnitMainC(cellIdxForSetting).Set(unitT, LevelTypes.First, playerT, ConditionUnitTypes.None, false);
            _e.UnitE(cellIdxForSetting).SetStats(HpValues.MAX, StepValues.MAX, WaterValues.MAX);
            _e.UnitExtraTWE(cellIdxForSetting).Set(ToolWeaponTypes.None, LevelTypes.None, 0);
            _e.UnitEffectsC(cellIdxForSetting).Set(0, 0, 0);

            if (_e.UnitT(cellIdxForSetting).Is(UnitTypes.Pawn))
            {
                _e.PawnPeopleInfoC(playerT).AmountInGame++;
            }



            if (unitT == UnitTypes.Pawn)
            {
                _e.PawnPeopleInfoC(playerT).PeopleInCity--;

                _e.MainToolWeaponE(cellIdxForSetting).Set(ToolWeaponTypes.Axe, LevelTypes.First);
            }

            else
            {
                if (unitT.Is(UnitTypes.Tree)) _e.HaveTreeUnit = true;


                if (unitT.IsGod())
                {
                    _e.GodInfoC(playerT).HaveGodInInventor = false;
                }
                else if (unitT == UnitTypes.King)
                {
                    _e.PlayerInfoC(playerT).HaveKingInInventor = false;
                }

                _e.MainToolWeaponE(cellIdxForSetting).Set(ToolWeaponTypes.None, LevelTypes.None);
            }
        }
    }
}