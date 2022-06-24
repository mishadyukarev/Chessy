﻿using Chessy.Model.Values;

namespace Chessy.Model.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModel
    {
        internal void GetCellsForAttackArcher()
        {
            for (byte cellIdxCell = 0; cellIdxCell < StartValues.CELLS; cellIdxCell++)
            {
                if (_e.UnitT(cellIdxCell).HaveUnit())
                {
                    if (!_e.UnitEffectsC(cellIdxCell).IsStunned)
                    {
                        if (_e.EnergyUnitC(cellIdxCell).HaveAnyEnergy)
                        {
                            if (!_e.UnitT(cellIdxCell).IsMelee(_e.MainToolWeaponT(cellIdxCell)))
                            {
                                for (var dir_1 = DirectTypes.None + 1; dir_1 < DirectTypes.End; dir_1++)
                                {
                                    var idx_1 = _e.AroundCellsE(cellIdxCell).IdxCell(dir_1);

                                    var isRight_0 = _e.IsRightArcherUnit(cellIdxCell);

                                    if (!_e.IsBorder(idx_1) && !_e.MountainC(idx_1).HaveAnyResources)
                                    {
                                        if (_e.UnitT(idx_1).HaveUnit())
                                        {
                                            if (!_e.UnitPlayerT(idx_1).Is(_e.UnitPlayerT(cellIdxCell)))
                                            {
                                                if (_e.UnitT(cellIdxCell).Is(UnitTypes.Pawn) && _e.MainToolWeaponT(cellIdxCell).Is(ToolWeaponTypes.BowCrossbow))
                                                {
                                                    if (isRight_0)
                                                    {
                                                        if (dir_1 == DirectTypes.Left || dir_1 == DirectTypes.Right || dir_1 == DirectTypes.Up || dir_1 == DirectTypes.Down)
                                                        {
                                                            _e.WhereUnitCanAttackUniqueAttackToEnemyC(cellIdxCell).Set(idx_1, true);
                                                        }
                                                        else _e.WhereUnitCanAttackSimpleAttackToEnemyC(cellIdxCell).Set(idx_1, true);
                                                    }
                                                    else
                                                    {
                                                        if (dir_1 == DirectTypes.DownLeft || dir_1 == DirectTypes.LeftUp || dir_1 == DirectTypes.UpRight || dir_1 == DirectTypes.RightDown)
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

                                        var idx_2 = _e.AroundCellsE(idx_1).IdxCell(dir_1);


                                        if (_e.UnitT(idx_2).HaveUnit() && !_e.UnitT(idx_2).IsAnimal()
                                            && _e.UnitVisibleC(idx_2).IsVisible(_e.UnitPlayerT(cellIdxCell))
                                            && !_e.UnitPlayerT(idx_2).Is(_e.UnitPlayerT(cellIdxCell)))
                                        {
                                            if (_e.UnitT(cellIdxCell).Is(UnitTypes.Pawn) && _e.MainToolWeaponT(cellIdxCell).Is(ToolWeaponTypes.BowCrossbow))
                                            {
                                                if (!isRight_0)
                                                {
                                                    if (dir_1 == DirectTypes.DownLeft || dir_1 == DirectTypes.LeftUp || dir_1 == DirectTypes.UpRight || dir_1 == DirectTypes.RightDown)
                                                    {
                                                        _e.WhereUnitCanAttackUniqueAttackToEnemyC(cellIdxCell).Set(idx_2, true);
                                                    }

                                                    else _e.WhereUnitCanAttackSimpleAttackToEnemyC(cellIdxCell).Set(idx_2, true);
                                                }
                                                else
                                                {
                                                    if (dir_1 == DirectTypes.Left || dir_1 == DirectTypes.Right || dir_1 == DirectTypes.Down || dir_1 == DirectTypes.Up)
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
        }
    }
}