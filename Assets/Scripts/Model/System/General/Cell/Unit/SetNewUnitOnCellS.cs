using Chessy.Model.Entity;
using Chessy.Model.Values;

namespace Chessy.Model.System
{
    sealed class SetNewUnitOnCellS : SystemModelAbstract
    {
        internal SetNewUnitOnCellS(in SystemsModel sM, EntitiesModel eM) : base(sM, eM)
        {
        }

        internal void Set(in UnitTypes unitT, in PlayerTypes playerT, in byte forSettingCellIdx)
        {
            _unitCs[forSettingCellIdx].Set((unitT, LevelTypes.First, playerT, ConditionUnitTypes.None, false));

            _hpUnitCs[forSettingCellIdx].Health = HpUnitValues.MAX;
            _unitWaterCs[forSettingCellIdx].Water = ValuesChessy.MAX_WATER_FOR_ANY_UNIT;

            _extraTWC[forSettingCellIdx].Set(ToolsWeaponsWarriorTypes.None, LevelTypes.None, 0);
            _effectsUnitCs[forSettingCellIdx].Set(0, 0, false);

            if (_unitCs[forSettingCellIdx].UnitT == UnitTypes.Pawn)
            {
                PawnPeopleInfoC(playerT).AmountInGame++;
            }



            if (unitT == UnitTypes.Pawn)
            {
                PawnPeopleInfoC(playerT).PeopleInCity--;

                _mainTWC[forSettingCellIdx].Set(ToolsWeaponsWarriorTypes.Axe, LevelTypes.First);
            }

            else
            {
                if (unitT.Is(UnitTypes.Tree)) AboutGameC.HaveTreeUnitInGame = true;


                if (unitT.IsGod())
                {
                    GodInfoC(playerT).HaveGodInInventor = false;
                }
                else if (unitT == UnitTypes.King)
                {
                    PlayerInfoC(playerT).HaveKingInInventor = false;
                }

                _mainTWC[forSettingCellIdx].Set(ToolsWeaponsWarriorTypes.None, LevelTypes.None);
            }

            for (byte currentCellIdx = 0; currentCellIdx < IndexCellsValues.CELLS; currentCellIdx++)
            {
                if (_cellCs[currentCellIdx].IsBorder) continue;

                if (!_unitWhereViewDataCs[currentCellIdx].HaveDataReference)
                {
                    _unitWhereViewDataCs[currentCellIdx].DataIdxCell = forSettingCellIdx;
                    _unitWhereViewDataCs[forSettingCellIdx].ViewIdxCell = currentCellIdx;


                    //var pos_0 = _e.CellE(currentCellIdx).StartPositionC.Possition;
                    //var pos_1 = _e.CellE(forSettingCellIdx).StartPositionC.Possition;

                    //var t = _e.ShiftingInfoForUnitC(cell_0).DistanceForShiftingOnOtherCell / _e.HowManyDistanceNeedForShiftingUnitC(cell_0).HowMany(cell_1);

                    //_unitCs[currentCellIdx).Possition = Vector3.Lerp(pos_0, pos_1, 1);


                    //_e.UnitPossitionOnCellC(currentCellIdx).Position = _e.CellE(forSettingCellIdx).PositionC.Position;

                    break;
                }
            }
        }
    }
}