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
                if (cellCs[cellIdxCell_0].IsBorder) continue;
                if (effectsUnitCs[cellIdxCell_0].IsStunned) continue;

                var curUnitC = unitCs[cellIdxCell_0];
                var curUnitT = curUnitC.UnitT;

                if (!curUnitT.HaveUnit() && shiftingUnitCs[cellIdxCell_0].IsShifting) continue;





                if (!curUnitT.IsMelee(mainTWC[cellIdxCell_0].ToolWeaponT))
                {
                    foreach (var idx_1 in idxsAroundCellCs[cellIdxCell_0].IdxCellsAroundArray)
                    {
                        var directShotingT = cellAroundCs[cellIdxCell_0, idx_1].DirectT;

                        var isRight_0 = unitCs[cellIdxCell_0].IsArcherDirectedToRight;

                        if (!cellCs[idx_1].IsBorder && !environmentCs[idx_1].HaveEnvironment(EnvironmentTypes.Mountain))
                        {
                            if (unitCs[idx_1].HaveUnit && !shiftingUnitCs[idx_1].IsShifting)
                            {
                                if (unitCs[idx_1].PlayerT != unitCs[cellIdxCell_0].PlayerT)
                                {
                                    if (unitCs[cellIdxCell_0].UnitT == UnitTypes.Pawn && mainTWC[cellIdxCell_0].ToolWeaponT == ToolsWeaponsWarriorTypes.BowCrossbow)
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

                            var idx_2 = cellsByDirectAroundC[idx_1].Get(directShotingT);


                            if (unitCs[idx_2].HaveUnit && !shiftingUnitCs[idx_2].IsShifting && !unitCs[idx_2].UnitT.IsAnimal()
                                && _unitVisibleCs[idx_2].IsVisible(unitCs[cellIdxCell_0].PlayerT)
                                && unitCs[idx_2].PlayerT != unitCs[cellIdxCell_0].PlayerT)
                            {
                                if (unitCs[cellIdxCell_0].UnitT == UnitTypes.Pawn && mainTWC[cellIdxCell_0].ToolWeaponT == ToolsWeaponsWarriorTypes.BowCrossbow)
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