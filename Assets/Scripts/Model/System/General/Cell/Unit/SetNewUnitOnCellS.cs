﻿using Chessy.Model.Values;
namespace Chessy.Model.System
{
    public partial class SystemsModel
    {
        internal void SetNewUnitOnCellS(in UnitTypes unitT, in PlayerTypes playerT, in byte cellIdxForSetting)
        {
            _e.UnitMainC(cellIdxForSetting).Set(unitT, LevelTypes.First, playerT, ConditionUnitTypes.None, false);
            _e.UnitE(cellIdxForSetting).SetStats(HpValues.MAX, 1, ValuesChessy.MAX_WATER_FOR_ANY_UNIT);
            _e.UnitExtraTWC(cellIdxForSetting).Set(ToolsWeaponsWarriorTypes.None, LevelTypes.None, 0);
            _e.UnitEffectsC(cellIdxForSetting).Set(0, 0, false);

            if (_e.UnitT(cellIdxForSetting).Is(UnitTypes.Pawn))
            {
                _e.PawnPeopleInfoC(playerT).AmountInGame++;
            }



            if (unitT == UnitTypes.Pawn)
            {
                _e.PawnPeopleInfoC(playerT).PeopleInCity--;

                _e.MainToolWeaponC(cellIdxForSetting).Set(ToolsWeaponsWarriorTypes.Axe, LevelTypes.First);
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

                _e.MainToolWeaponC(cellIdxForSetting).Set(ToolsWeaponsWarriorTypes.None, LevelTypes.None);
            }
        }
    }
}