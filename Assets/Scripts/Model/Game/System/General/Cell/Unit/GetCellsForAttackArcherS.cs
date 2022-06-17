using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModel
    {
        internal void GetCellsForAttackArcher(in byte cell_0)
        {
            if (eMG.UnitTC(cell_0).HaveUnit)
            {
                if (!eMG.StunUnitC(cell_0).IsStunned)
                {
                    if (eMG.StepUnitC(cell_0).HaveAnySteps)
                    {
                        if (!eMG.UnitTC(cell_0).IsMelee(eMG.MainToolWeaponTC(cell_0).ToolWeaponT))
                        {
                            for (var dir_1 = DirectTypes.None + 1; dir_1 < DirectTypes.End; dir_1++)
                            {
                                var idx_1 = eMG.AroundCellsE(cell_0).IdxCell(dir_1);

                                var isRight_0 = eMG.UnitIsRightArcherC(cell_0).IsRight;

                                if (!eMG.IsBorder(idx_1) && !eMG.MountainC(idx_1).HaveAnyResources)
                                {
                                    if (eMG.UnitTC(idx_1).HaveUnit)
                                    {
                                        if (!eMG.UnitPlayerTC(idx_1).Is(eMG.UnitPlayerTC(cell_0).PlayerT))
                                        {
                                            if (eMG.UnitTC(cell_0).Is(UnitTypes.Pawn) && eMG.MainToolWeaponTC(cell_0).Is(ToolWeaponTypes.BowCrossbow))
                                            {
                                                if (isRight_0)
                                                {
                                                    if (dir_1 == DirectTypes.Left || dir_1 == DirectTypes.Right || dir_1 == DirectTypes.Up || dir_1 == DirectTypes.Down)
                                                    {
                                                        eMG.AttackUniqueCellsC(cell_0).Add(idx_1);
                                                    }
                                                    else eMG.AttackSimpleCellsC(cell_0).Add(idx_1);
                                                }
                                                else
                                                {
                                                    if (dir_1 == DirectTypes.DownLeft || dir_1 == DirectTypes.LeftUp || dir_1 == DirectTypes.UpRight || dir_1 == DirectTypes.RightDown)
                                                    {
                                                        eMG.AttackUniqueCellsC(cell_0).Add(idx_1);
                                                    }
                                                    else eMG.AttackSimpleCellsC(cell_0).Add(idx_1);
                                                }
                                            }
                                            else
                                            {
                                                eMG.AttackSimpleCellsC(cell_0).Add(idx_1);
                                            }
                                        }
                                    }

                                    var idx_2 = eMG.AroundCellsE(idx_1).IdxCell(dir_1);


                                    if (eMG.UnitTC(idx_2).HaveUnit && !eMG.UnitTC(idx_2).IsAnimal
                                        && eMG.UnitVisibleC(idx_2).IsVisible(eMG.UnitPlayerTC(cell_0).PlayerT)
                                        && !eMG.UnitPlayerTC(idx_2).Is(eMG.UnitPlayerTC(cell_0).PlayerT))
                                    {
                                        if (eMG.UnitTC(cell_0).Is(UnitTypes.Pawn) && eMG.MainToolWeaponTC(cell_0).Is(ToolWeaponTypes.BowCrossbow))
                                        {
                                            if (!isRight_0)
                                            {
                                                if (dir_1 == DirectTypes.DownLeft || dir_1 == DirectTypes.LeftUp || dir_1 == DirectTypes.UpRight || dir_1 == DirectTypes.RightDown)
                                                {
                                                    eMG.AttackUniqueCellsC(cell_0).Add(idx_2);
                                                }

                                                else eMG.AttackSimpleCellsC(cell_0).Add(idx_2);
                                            }
                                            else
                                            {
                                                if (dir_1 == DirectTypes.Left || dir_1 == DirectTypes.Right || dir_1 == DirectTypes.Down || dir_1 == DirectTypes.Up)
                                                {
                                                    eMG.AttackUniqueCellsC(cell_0).Add(idx_2);
                                                }

                                                else eMG.AttackSimpleCellsC(cell_0).Add(idx_2);
                                            }
                                        }
                                        else
                                        {
                                            eMG.AttackSimpleCellsC(cell_0).Add(idx_2);
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