using Chessy.Game.Entity.Model;
using Chessy.Common.Entity;
using Chessy.Common.Model.System;

namespace Chessy.Game.Model.System
{
    sealed class GetCellsForAttackArcherS : SystemModelGameAbs
    {
        internal GetCellsForAttackArcherS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Get(in byte cell_0)
        {
            if (eMG.UnitTC(cell_0).HaveUnit)
            {
                if (!eMG.UnitEffectStunC(cell_0).IsStunned)
                {
                    if (eMG.UnitStepC(cell_0).HaveAnySteps)
                    {
                        if (!eMG.UnitTC(cell_0).IsMelee(eMG.UnitMainTWE(cell_0).ToolWeaponTC.ToolWeaponT))
                        {
                            for (var dir_1 = DirectTypes.None + 1; dir_1 < DirectTypes.End; dir_1++)
                            {
                                var idx_1 = eMG.CellEs(cell_0).AroundCellsEs.AroundCellE(dir_1).IdxC.Idx;

                                var isRight_0 = eMG.UnitIsRightArcherC(cell_0).IsRight;

                                if (eMG.CellEs(idx_1).IsActiveParentSelf && !eMG.MountainC(idx_1).HaveAnyResources)
                                {
                                    if (eMG.UnitTC(idx_1).HaveUnit)
                                    {
                                        if (!eMG.UnitPlayerTC(idx_1).Is(eMG.UnitPlayerTC(cell_0).PlayerT))
                                        {
                                            if (eMG.UnitTC(cell_0).Is(UnitTypes.Pawn) && eMG.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.BowCrossbow))
                                            {
                                                if (isRight_0)
                                                {
                                                    if (dir_1 == DirectTypes.Left || dir_1 == DirectTypes.Right || dir_1 == DirectTypes.Up || dir_1 == DirectTypes.Down)
                                                    {
                                                        eMG.UnitEs(cell_0).UniqueAttack.Add(idx_1);
                                                    }
                                                    else eMG.UnitEs(cell_0).SimpleAttack.Add(idx_1);
                                                }
                                                else
                                                {
                                                    if (dir_1 == DirectTypes.DownLeft || dir_1 == DirectTypes.UpLeft || dir_1 == DirectTypes.UpRight || dir_1 == DirectTypes.DownRight)
                                                    {
                                                        eMG.UnitEs(cell_0).UniqueAttack.Add(idx_1);
                                                    }
                                                    else eMG.UnitEs(cell_0).SimpleAttack.Add(idx_1);
                                                }
                                            }
                                            else
                                            {
                                                eMG.UnitEs(cell_0).SimpleAttack.Add(idx_1);
                                            }
                                        }
                                    }

                                    var idx_2 = eMG.CellEs(idx_1).AroundCellsEs.AroundCellE(dir_1).IdxC.Idx;


                                    if (eMG.UnitTC(idx_2).HaveUnit && !eMG.UnitTC(idx_2).IsAnimal
                                        && eMG.UnitEs(idx_2).ForPlayer(eMG.UnitPlayerTC(cell_0).PlayerT).IsVisible
                                        && !eMG.UnitPlayerTC(idx_2).Is(eMG.UnitPlayerTC(cell_0).PlayerT))
                                    {
                                        if (eMG.UnitTC(cell_0).Is(UnitTypes.Pawn) && eMG.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.BowCrossbow))
                                        {
                                            if (!isRight_0)
                                            {
                                                if (dir_1 == DirectTypes.DownLeft || dir_1 == DirectTypes.UpLeft || dir_1 == DirectTypes.UpRight || dir_1 == DirectTypes.DownRight)
                                                {
                                                    eMG.UnitEs(cell_0).UniqueAttack.Add(idx_2);
                                                }

                                                else eMG.UnitEs(cell_0).SimpleAttack.Add(idx_2);
                                            }
                                            else
                                            {
                                                if (dir_1 == DirectTypes.Left || dir_1 == DirectTypes.Right || dir_1 == DirectTypes.Down || dir_1 == DirectTypes.Up)
                                                {
                                                    eMG.UnitEs(cell_0).UniqueAttack.Add(idx_2);
                                                }

                                                else eMG.UnitEs(cell_0).SimpleAttack.Add(idx_2);
                                            }
                                        }
                                        else
                                        {
                                            eMG.UnitEs(cell_0).SimpleAttack.Add(idx_2);
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