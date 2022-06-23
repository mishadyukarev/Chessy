using Chessy.Model.Values;

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
                    if (!_e.StunUnitC(cellIdxCell).IsStunned)
                    {
                        if (_e.StepUnitC(cellIdxCell).HaveAnySteps)
                        {
                            if (!_e.UnitT(cellIdxCell).IsMelee(_e.MainToolWeaponT(cellIdxCell)))
                            {
                                for (var dir_1 = DirectTypes.None + 1; dir_1 < DirectTypes.End; dir_1++)
                                {
                                    var idx_1 = _e.AroundCellsE(cellIdxCell).IdxCell(dir_1);

                                    var isRight_0 = _e.UnitIsRightArcherC(cellIdxCell).IsRight;

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
                                                            _e.AttackUniqueCellsC(cellIdxCell).Add(idx_1);
                                                        }
                                                        else _e.AttackSimpleCellsC(cellIdxCell).Add(idx_1);
                                                    }
                                                    else
                                                    {
                                                        if (dir_1 == DirectTypes.DownLeft || dir_1 == DirectTypes.LeftUp || dir_1 == DirectTypes.UpRight || dir_1 == DirectTypes.RightDown)
                                                        {
                                                            _e.AttackUniqueCellsC(cellIdxCell).Add(idx_1);
                                                        }
                                                        else _e.AttackSimpleCellsC(cellIdxCell).Add(idx_1);
                                                    }
                                                }
                                                else
                                                {
                                                    _e.AttackSimpleCellsC(cellIdxCell).Add(idx_1);
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
                                                        _e.AttackUniqueCellsC(cellIdxCell).Add(idx_2);
                                                    }

                                                    else _e.AttackSimpleCellsC(cellIdxCell).Add(idx_2);
                                                }
                                                else
                                                {
                                                    if (dir_1 == DirectTypes.Left || dir_1 == DirectTypes.Right || dir_1 == DirectTypes.Down || dir_1 == DirectTypes.Up)
                                                    {
                                                        _e.AttackUniqueCellsC(cellIdxCell).Add(idx_2);
                                                    }

                                                    else _e.AttackSimpleCellsC(cellIdxCell).Add(idx_2);
                                                }
                                            }
                                            else
                                            {
                                                _e.AttackSimpleCellsC(cellIdxCell).Add(idx_2);
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