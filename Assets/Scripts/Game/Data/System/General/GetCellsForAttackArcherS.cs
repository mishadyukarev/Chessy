using System;

namespace Chessy.Game.System.Model
{
    public struct GetCellsForAttackArcherS
    {
        public GetCellsForAttackArcherS(in byte idx_0, in EntitiesModel e)
        {
            if (e.UnitTC(idx_0).HaveUnit)
            {
                if (!e.UnitEffectStunC(idx_0).IsStunned)
            {
                if (e.UnitStepC(idx_0).HaveAnySteps)
                {
                        if (!e.IsMelee(idx_0))
                        {
                            for (var dir_1 = DirectTypes.None + 1; dir_1 < DirectTypes.End; dir_1++)
                            {
                                var idx_1 = e.CellEs(idx_0).AroundCellE(dir_1).IdxC.Idx;

                                var isRight_0 = e.UnitIsRightArcherC(idx_0).IsRight;

                                if (e.CellEs(idx_1).IsActiveParentSelf && !e.MountainC(idx_1).HaveAnyResources)
                                {
                                    if (e.UnitTC(idx_1).HaveUnit)
                                    {
                                        if (!e.UnitPlayerTC(idx_1).Is(e.UnitPlayerTC(idx_0).Player))
                                        {
                                            if (e.UnitTC(idx_0).Is(UnitTypes.Pawn) && e.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.BowCrossbow))
                                            {
                                                if (isRight_0)
                                                {
                                                    if (dir_1 == DirectTypes.Left || dir_1 == DirectTypes.Right || dir_1 == DirectTypes.Up || dir_1 == DirectTypes.Down)
                                                    {
                                                        e.UnitEs(idx_0).ForAttack(AttackTypes.Unique).Add(idx_1);
                                                    }
                                                    else e.UnitEs(idx_0).ForAttack(AttackTypes.Simple).Add(idx_1);
                                                }
                                                else
                                                {
                                                    if (dir_1 == DirectTypes.DownLeft || dir_1 == DirectTypes.UpLeft || dir_1 == DirectTypes.UpRight || dir_1 == DirectTypes.DownRight)
                                                    {
                                                        e.UnitEs(idx_0).ForAttack(AttackTypes.Unique).Add(idx_1);
                                                    }
                                                    else e.UnitEs(idx_0).ForAttack(AttackTypes.Simple).Add(idx_1);
                                                }
                                            }
                                            else
                                            {
                                                e.UnitEs(idx_0).ForAttack(AttackTypes.Simple).Add(idx_1);
                                            }
                                        }
                                    }

                                    var idx_2 = e.CellEs(idx_1).AroundCellE(dir_1).IdxC.Idx;


                                    if (e.UnitTC(idx_2).HaveUnit && !e.IsAnimal(e.UnitTC(idx_2).Unit)
                                        && e.UnitEs(idx_2).ForPlayer(e.UnitPlayerTC(idx_0).Player).IsVisible
                                        && !e.UnitPlayerTC(idx_2).Is(e.UnitPlayerTC(idx_0).Player))
                                    {
                                        if (e.UnitTC(idx_0).Is(UnitTypes.Pawn) && e.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.BowCrossbow))
                                        {
                                            if (!isRight_0)
                                            {
                                                if (dir_1 == DirectTypes.DownLeft || dir_1 == DirectTypes.UpLeft || dir_1 == DirectTypes.UpRight || dir_1 == DirectTypes.DownRight)
                                                {
                                                    e.UnitEs(idx_0).ForAttack(AttackTypes.Unique).Add(idx_2);
                                                }

                                                else e.UnitEs(idx_0).ForAttack(AttackTypes.Simple).Add(idx_2);
                                            }
                                            else
                                            {
                                                if (dir_1 == DirectTypes.Left || dir_1 == DirectTypes.Right || dir_1 == DirectTypes.Down || dir_1 == DirectTypes.Up)
                                                {
                                                    e.UnitEs(idx_0).ForAttack(AttackTypes.Unique).Add(idx_2);
                                                }

                                                else e.UnitEs(idx_0).ForAttack(AttackTypes.Simple).Add(idx_2);
                                            }
                                        }
                                        else
                                        {
                                            e.UnitEs(idx_0).ForAttack(AttackTypes.Simple).Add(idx_2);
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