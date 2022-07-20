using Chessy.Model.Component;
using Chessy.Model.Entity;
using Chessy.Model.Values;
namespace Chessy.Model.System
{
    sealed class GetCellsForAttackArcherS : SystemModelAbstract
    {
        internal GetCellsForAttackArcherS(in SystemsModel sM, EntitiesModel eM) : base(sM, eM)
        {
        }

        internal void Get()
        {
            for (byte cellIdxCell_0 = 0; cellIdxCell_0 < IndexCellsValues.CELLS; cellIdxCell_0++)
            {
                if (_cellCs[cellIdxCell_0].IsBorder) continue;
                if (_effectsUnitCs[cellIdxCell_0].IsStunned) continue;

                var curUnitC = _unitCs[cellIdxCell_0];
                var curUnitT = curUnitC.UnitT;

                if (!curUnitT.HaveUnit() && _e.ShiftingInfoForUnitC(cellIdxCell_0).IsShifting) continue;





                if (!curUnitT.IsMelee(_e.MainToolWeaponT(cellIdxCell_0)))
                {
                    foreach (var idx_1 in _e.IdxsCellsAround(cellIdxCell_0))
                    {
                        var directShotingT = _e.DirectionAround(cellIdxCell_0, idx_1);

                        var isRight_0 = _e.IsRightArcherUnit(cellIdxCell_0);

                        if (!_cellCs[idx_1].IsBorder && !_e.MountainC(idx_1).HaveAnyResources)
                        {
                            if (_e.UnitT(idx_1).HaveUnit() && !_e.ShiftingInfoForUnitC(idx_1).IsShifting)
                            {
                                if (!_e.UnitPlayerT(idx_1).Is(_e.UnitPlayerT(cellIdxCell_0)))
                                {
                                    if (_e.UnitT(cellIdxCell_0).Is(UnitTypes.Pawn) && _e.MainToolWeaponT(cellIdxCell_0).Is(ToolsWeaponsWarriorTypes.BowCrossbow))
                                    {
                                        if (isRight_0)
                                        {
                                            if (directShotingT == DirectTypes.Left || directShotingT == DirectTypes.Right || directShotingT == DirectTypes.Up || directShotingT == DirectTypes.Down)
                                            {
                                                _e.WhereUnitCanAttackUniqueAttackToEnemyC(cellIdxCell_0).Set(idx_1, true);
                                            }
                                            else _e.WhereUnitCanAttackSimpleAttackToEnemyC(cellIdxCell_0).Set(idx_1, true);
                                        }
                                        else
                                        {
                                            if (directShotingT == DirectTypes.DownLeft || directShotingT == DirectTypes.LeftUp || directShotingT == DirectTypes.UpRight || directShotingT == DirectTypes.RightDown)
                                            {
                                                _e.WhereUnitCanAttackUniqueAttackToEnemyC(cellIdxCell_0).Set(idx_1, true);
                                            }
                                            else _e.WhereUnitCanAttackSimpleAttackToEnemyC(cellIdxCell_0).Set(idx_1, true);
                                        }
                                    }
                                    else
                                    {
                                        _e.WhereUnitCanAttackSimpleAttackToEnemyC(cellIdxCell_0).Set(idx_1, true);
                                    }
                                }
                            }

                            var idx_2 = _e.GetIdxCellByDirectAround(idx_1, directShotingT);


                            if (_e.UnitT(idx_2).HaveUnit() && !_e.ShiftingInfoForUnitC(idx_2).IsShifting && !_e.UnitT(idx_2).IsAnimal()
                                && _e.UnitVisibleC(idx_2).IsVisible(_e.UnitPlayerT(cellIdxCell_0))
                                && !_e.UnitPlayerT(idx_2).Is(_e.UnitPlayerT(cellIdxCell_0)))
                            {
                                if (_e.UnitT(cellIdxCell_0).Is(UnitTypes.Pawn) && _e.MainToolWeaponT(cellIdxCell_0).Is(ToolsWeaponsWarriorTypes.BowCrossbow))
                                {
                                    if (!isRight_0)
                                    {
                                        if (directShotingT == DirectTypes.DownLeft || directShotingT == DirectTypes.LeftUp || directShotingT == DirectTypes.UpRight || directShotingT == DirectTypes.RightDown)
                                        {
                                            _e.WhereUnitCanAttackUniqueAttackToEnemyC(cellIdxCell_0).Set(idx_2, true);
                                        }

                                        else _e.WhereUnitCanAttackSimpleAttackToEnemyC(cellIdxCell_0).Set(idx_2, true);
                                    }
                                    else
                                    {
                                        if (directShotingT == DirectTypes.Left || directShotingT == DirectTypes.Right || directShotingT == DirectTypes.Down || directShotingT == DirectTypes.Up)
                                        {
                                            _e.WhereUnitCanAttackUniqueAttackToEnemyC(cellIdxCell_0).Set(idx_2, true);
                                        }

                                        else _e.WhereUnitCanAttackSimpleAttackToEnemyC(cellIdxCell_0).Set(idx_2, true);
                                    }
                                }
                                else
                                {
                                    _e.WhereUnitCanAttackSimpleAttackToEnemyC(cellIdxCell_0).Set(idx_2, true);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}