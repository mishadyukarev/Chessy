using Chessy.Model.Values;
using UnityEngine;

namespace Chessy.Model.System
{
    public partial class SystemsModel
    {
        internal void SetNewUnitOnCellS(in UnitTypes unitT, in PlayerTypes playerT, in byte forSettingCellIdx)
        {
            _e.UnitMainC(forSettingCellIdx).Set((unitT, LevelTypes.First, playerT, ConditionUnitTypes.None, false));
            _e.UnitE(forSettingCellIdx).SetStats(HpUnitValues.MAX, ValuesChessy.MAX_WATER_FOR_ANY_UNIT);
            _e.UnitExtraTWC(forSettingCellIdx).Set(ToolsWeaponsWarriorTypes.None, LevelTypes.None, 0);
            _e.UnitEffectsC(forSettingCellIdx).Set(0, 0, false);

            if (_e.UnitT(forSettingCellIdx).Is(UnitTypes.Pawn))
            {
                _e.PawnPeopleInfoC(playerT).AmountInGame++;
            }



            if (unitT == UnitTypes.Pawn)
            {
                _e.PawnPeopleInfoC(playerT).PeopleInCity--;

                _e.MainToolWeaponC(forSettingCellIdx).Set(ToolsWeaponsWarriorTypes.Axe, LevelTypes.First);
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

                _e.MainToolWeaponC(forSettingCellIdx).Set(ToolsWeaponsWarriorTypes.None, LevelTypes.None);
            }

            for (byte currentCellIdx = 0; currentCellIdx < IndexCellsValues.CELLS; currentCellIdx++)
            {
                if (_cellCs[currentCellIdx].IsBorder) continue;

                if (!_e.WhereViewDataUnitC(currentCellIdx).HaveDataReference)
                {
                    _e.WhereViewDataUnitC(currentCellIdx).DataIdxCell = forSettingCellIdx;
                    _e.WhereViewDataUnitC(forSettingCellIdx).ViewIdxCell = currentCellIdx;


                    //var pos_0 = _e.CellE(currentCellIdx).StartPositionC.Possition;
                    //var pos_1 = _e.CellE(forSettingCellIdx).StartPositionC.Possition;

                    //var t = _e.ShiftingInfoForUnitC(cell_0).DistanceForShiftingOnOtherCell / _e.HowManyDistanceNeedForShiftingUnitC(cell_0).HowMany(cell_1);

                    //_e.UnitMainC(currentCellIdx).Possition = Vector3.Lerp(pos_0, pos_1, 1);


                    //_e.UnitPossitionOnCellC(currentCellIdx).Position = _e.CellE(forSettingCellIdx).PositionC.Position;

                    break;
                }
            }
        }
    }
}