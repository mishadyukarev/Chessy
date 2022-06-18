using Chessy.Game.Model.Entity;
using Chessy.Game.Values;

namespace Chessy.Game.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModel
    {
        internal void GetCellsForAttackArcher()
        {
            for (byte cellIdxCell = 0; cellIdxCell < StartValues.CELLS; cellIdxCell++)
            {
                if (_eMG.UnitTC(cellIdxCell).HaveUnit)
                {
                    if (!_eMG.StunUnitC(cellIdxCell).IsStunned)
                    {
                        if (_eMG.StepUnitC(cellIdxCell).HaveAnySteps)
                        {
                            if (!_eMG.UnitTC(cellIdxCell).IsMelee(_eMG.MainToolWeaponTC(cellIdxCell).ToolWeaponT))
                            {
                                for (var dir_1 = DirectTypes.None + 1; dir_1 < DirectTypes.End; dir_1++)
                                {
                                    var idx_1 = _eMG.AroundCellsE(cellIdxCell).IdxCell(dir_1);

                                    var isRight_0 = _eMG.UnitIsRightArcherC(cellIdxCell).IsRight;

                                    if (!_eMG.IsBorder(idx_1) && !_eMG.MountainC(idx_1).HaveAnyResources)
                                    {
                                        if (_eMG.UnitTC(idx_1).HaveUnit)
                                        {
                                            if (!_eMG.UnitPlayerTC(idx_1).Is(_eMG.UnitPlayerTC(cellIdxCell).PlayerT))
                                            {
                                                if (_eMG.UnitTC(cellIdxCell).Is(UnitTypes.Pawn) && _eMG.MainToolWeaponTC(cellIdxCell).Is(ToolWeaponTypes.BowCrossbow))
                                                {
                                                    if (isRight_0)
                                                    {
                                                        if (dir_1 == DirectTypes.Left || dir_1 == DirectTypes.Right || dir_1 == DirectTypes.Up || dir_1 == DirectTypes.Down)
                                                        {
                                                            _eMG.AttackUniqueCellsC(cellIdxCell).Add(idx_1);
                                                        }
                                                        else _eMG.AttackSimpleCellsC(cellIdxCell).Add(idx_1);
                                                    }
                                                    else
                                                    {
                                                        if (dir_1 == DirectTypes.DownLeft || dir_1 == DirectTypes.LeftUp || dir_1 == DirectTypes.UpRight || dir_1 == DirectTypes.RightDown)
                                                        {
                                                            _eMG.AttackUniqueCellsC(cellIdxCell).Add(idx_1);
                                                        }
                                                        else _eMG.AttackSimpleCellsC(cellIdxCell).Add(idx_1);
                                                    }
                                                }
                                                else
                                                {
                                                    _eMG.AttackSimpleCellsC(cellIdxCell).Add(idx_1);
                                                }
                                            }
                                        }

                                        var idx_2 = _eMG.AroundCellsE(idx_1).IdxCell(dir_1);


                                        if (_eMG.UnitTC(idx_2).HaveUnit && !_eMG.UnitTC(idx_2).IsAnimal
                                            && _eMG.UnitVisibleC(idx_2).IsVisible(_eMG.UnitPlayerTC(cellIdxCell).PlayerT)
                                            && !_eMG.UnitPlayerTC(idx_2).Is(_eMG.UnitPlayerTC(cellIdxCell).PlayerT))
                                        {
                                            if (_eMG.UnitTC(cellIdxCell).Is(UnitTypes.Pawn) && _eMG.MainToolWeaponTC(cellIdxCell).Is(ToolWeaponTypes.BowCrossbow))
                                            {
                                                if (!isRight_0)
                                                {
                                                    if (dir_1 == DirectTypes.DownLeft || dir_1 == DirectTypes.LeftUp || dir_1 == DirectTypes.UpRight || dir_1 == DirectTypes.RightDown)
                                                    {
                                                        _eMG.AttackUniqueCellsC(cellIdxCell).Add(idx_2);
                                                    }

                                                    else _eMG.AttackSimpleCellsC(cellIdxCell).Add(idx_2);
                                                }
                                                else
                                                {
                                                    if (dir_1 == DirectTypes.Left || dir_1 == DirectTypes.Right || dir_1 == DirectTypes.Down || dir_1 == DirectTypes.Up)
                                                    {
                                                        _eMG.AttackUniqueCellsC(cellIdxCell).Add(idx_2);
                                                    }

                                                    else _eMG.AttackSimpleCellsC(cellIdxCell).Add(idx_2);
                                                }
                                            }
                                            else
                                            {
                                                _eMG.AttackSimpleCellsC(cellIdxCell).Add(idx_2);
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