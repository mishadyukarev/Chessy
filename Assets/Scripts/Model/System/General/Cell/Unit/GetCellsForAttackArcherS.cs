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

                if (!curUnitT.HaveUnit() && _shiftingUnitCs[cellIdxCell_0].IsShifting) continue;





                if (!curUnitT.IsMelee(_mainTWC[cellIdxCell_0].ToolWeaponT))
                {
                    foreach (var idx_1 in _e.IdxsCellsAround(cellIdxCell_0))
                    {
                        var directShotingT = _e.CellAroundC(cellIdxCell_0, idx_1).DirectT;

                        var isRight_0 = _unitCs[cellIdxCell_0].IsArcherDirectedToRight;

                        if (!_cellCs[idx_1].IsBorder && !_e.MountainC(idx_1).HaveAnyResources)
                        {
                            if (_e.UnitT(idx_1).HaveUnit() && !_shiftingUnitCs[idx_1].IsShifting)
                            {
                                if (_unitCs[idx_1].PlayerT != _unitCs[cellIdxCell_0].PlayerT)
                                {
                                    if (_e.UnitT(cellIdxCell_0).Is(UnitTypes.Pawn) && _mainTWC[cellIdxCell_0].ToolWeaponT == ToolsWeaponsWarriorTypes.BowCrossbow)
                                    {
                                        if (isRight_0)
                                        {
                                            if (directShotingT == DirectTypes.Left || directShotingT == DirectTypes.Right || directShotingT == DirectTypes.Up || directShotingT == DirectTypes.Down)
                                            {
                                                _whereUniqueAttackCs[cellIdxCell_0].Set(idx_1, true);
                                            }
                                            else _whereSimpleAttackCs[cellIdxCell_0].Set(idx_1, true);
                                        }
                                        else
                                        {
                                            if (directShotingT == DirectTypes.DownLeft || directShotingT == DirectTypes.LeftUp || directShotingT == DirectTypes.UpRight || directShotingT == DirectTypes.RightDown)
                                            {
                                                _whereUniqueAttackCs[cellIdxCell_0].Set(idx_1, true);
                                            }
                                            else _whereSimpleAttackCs[cellIdxCell_0].Set(idx_1, true);
                                        }
                                    }
                                    else
                                    {
                                        _whereSimpleAttackCs[cellIdxCell_0].Set(idx_1, true);
                                    }
                                }
                            }

                            var idx_2 = _e.GetIdxCellByDirectAround(idx_1, directShotingT);


                            if (_e.UnitT(idx_2).HaveUnit() && !_shiftingUnitCs[idx_2].IsShifting && !_e.UnitT(idx_2).IsAnimal()
                                && _unitVisibleCs[idx_2].IsVisible(_unitCs[cellIdxCell_0].PlayerT)
                                && _unitCs[idx_2].PlayerT != _unitCs[cellIdxCell_0].PlayerT)
                            {
                                if (_e.UnitT(cellIdxCell_0).Is(UnitTypes.Pawn) && _mainTWC[cellIdxCell_0].ToolWeaponT == ToolsWeaponsWarriorTypes.BowCrossbow)
                                {
                                    if (!isRight_0)
                                    {
                                        if (directShotingT == DirectTypes.DownLeft || directShotingT == DirectTypes.LeftUp || directShotingT == DirectTypes.UpRight || directShotingT == DirectTypes.RightDown)
                                        {
                                            _whereUniqueAttackCs[cellIdxCell_0].Set(idx_2, true);
                                        }

                                        else _whereSimpleAttackCs[cellIdxCell_0].Set(idx_2, true);
                                    }
                                    else
                                    {
                                        if (directShotingT == DirectTypes.Left || directShotingT == DirectTypes.Right || directShotingT == DirectTypes.Down || directShotingT == DirectTypes.Up)
                                        {
                                            _whereUniqueAttackCs[cellIdxCell_0].Set(idx_2, true);
                                        }

                                        else _whereSimpleAttackCs[cellIdxCell_0].Set(idx_2, true);
                                    }
                                }
                                else
                                {
                                    _whereSimpleAttackCs[cellIdxCell_0].Set(idx_2, true);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}