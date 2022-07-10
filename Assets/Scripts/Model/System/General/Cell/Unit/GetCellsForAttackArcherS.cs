using Chessy.Model.Enum;
using Chessy.Model.Values;
namespace Chessy.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModelAbstract
    {
        internal void GetCellsForAttackArcher()
        {
            for (byte cellIdxCell = 0; cellIdxCell < IndexCellsValues.CELLS; cellIdxCell++)
            {
                if (_e.UnitEffectsC(cellIdxCell).IsStunned) continue;
                if (!_e.UnitT(cellIdxCell).HaveUnit() && _e.ShiftingInfoForUnitC(cellIdxCell).IsShifting) continue;


                if (!_e.UnitT(cellIdxCell).IsMelee(_e.MainToolWeaponT(cellIdxCell)))
                {
                    foreach (var idx_1 in _e.IdxsCellsAround(cellIdxCell, DistanceFromCellTypes.First))
                    {
                        var directShotingT = _e.DirectionAround(cellIdxCell, idx_1);

                        var isRight_0 = _e.IsRightArcherUnit(cellIdxCell);

                        if (!_e.IsBorder(idx_1) && !_e.MountainC(idx_1).HaveAnyResources)
                        {
                            if (_e.UnitT(idx_1).HaveUnit() && !_e.ShiftingInfoForUnitC(idx_1).IsShifting)
                            {
                                if (!_e.UnitPlayerT(idx_1).Is(_e.UnitPlayerT(cellIdxCell)))
                                {
                                    if (_e.UnitT(cellIdxCell).Is(UnitTypes.Pawn) && _e.MainToolWeaponT(cellIdxCell).Is(ToolsWeaponsWarriorTypes.BowCrossbow))
                                    {
                                        if (isRight_0)
                                        {
                                            if (directShotingT == DirectTypes.Left || directShotingT == DirectTypes.Right || directShotingT == DirectTypes.Up || directShotingT == DirectTypes.Down)
                                            {
                                                _e.WhereUnitCanAttackUniqueAttackToEnemyC(cellIdxCell).Set(idx_1, true);
                                            }
                                            else _e.WhereUnitCanAttackSimpleAttackToEnemyC(cellIdxCell).Set(idx_1, true);
                                        }
                                        else
                                        {
                                            if (directShotingT == DirectTypes.DownLeft || directShotingT == DirectTypes.LeftUp || directShotingT == DirectTypes.UpRight || directShotingT == DirectTypes.RightDown)
                                            {
                                                _e.WhereUnitCanAttackUniqueAttackToEnemyC(cellIdxCell).Set(idx_1, true);
                                            }
                                            else _e.WhereUnitCanAttackSimpleAttackToEnemyC(cellIdxCell).Set(idx_1, true);
                                        }
                                    }
                                    else
                                    {
                                        _e.WhereUnitCanAttackSimpleAttackToEnemyC(cellIdxCell).Set(idx_1, true);
                                    }
                                }
                            }

                            var idx_2 = _e.GetIdxCellByDirect(idx_1, DistanceFromCellTypes.First, directShotingT);


                            if (_e.UnitT(idx_2).HaveUnit() && !_e.ShiftingInfoForUnitC(idx_2).IsShifting && !_e.UnitT(idx_2).IsAnimal()
                                && _e.UnitVisibleC(idx_2).IsVisible(_e.UnitPlayerT(cellIdxCell))
                                && !_e.UnitPlayerT(idx_2).Is(_e.UnitPlayerT(cellIdxCell)))
                            {
                                if (_e.UnitT(cellIdxCell).Is(UnitTypes.Pawn) && _e.MainToolWeaponT(cellIdxCell).Is(ToolsWeaponsWarriorTypes.BowCrossbow))
                                {
                                    if (!isRight_0)
                                    {
                                        if (directShotingT == DirectTypes.DownLeft || directShotingT == DirectTypes.LeftUp || directShotingT == DirectTypes.UpRight || directShotingT == DirectTypes.RightDown)
                                        {
                                            _e.WhereUnitCanAttackUniqueAttackToEnemyC(cellIdxCell).Set(idx_2, true);
                                        }

                                        else _e.WhereUnitCanAttackSimpleAttackToEnemyC(cellIdxCell).Set(idx_2, true);
                                    }
                                    else
                                    {
                                        if (directShotingT == DirectTypes.Left || directShotingT == DirectTypes.Right || directShotingT == DirectTypes.Down || directShotingT == DirectTypes.Up)
                                        {
                                            _e.WhereUnitCanAttackUniqueAttackToEnemyC(cellIdxCell).Set(idx_2, true);
                                        }

                                        else _e.WhereUnitCanAttackSimpleAttackToEnemyC(cellIdxCell).Set(idx_2, true);
                                    }
                                }
                                else
                                {
                                    _e.WhereUnitCanAttackSimpleAttackToEnemyC(cellIdxCell).Set(idx_2, true);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}