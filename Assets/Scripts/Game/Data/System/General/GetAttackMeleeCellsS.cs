using Chessy.Game.Values.Cell.Unit.Stats;
using System;

namespace Chessy.Game.System.Model
{
    public struct GetAttackMeleeCellsS
    {
        public GetAttackMeleeCellsS(in byte idx_0, in Chessy.Game.Entity.Model.EntitiesModel e)
        {
            e.UnitEs(idx_0).ForAttack(AttackTypes.Simple).Clear();
            e.UnitEs(idx_0).ForAttack(AttackTypes.Unique).Clear();

            if (!e.UnitEffectStunC(idx_0).IsStunned)
            {
                if (e.UnitTC(idx_0).HaveUnit && e.IsMelee(idx_0) && !e.IsAnimal(e.UnitTC(idx_0).Unit)
                    && !e.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.BowCrossbow))
                {
                    DirectTypes dir_cur = default;

                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                    {
                        var idx_1 = e.CellEs(idx_0).AroundCellE(dirT).IdxC.Idx;

                        dir_cur += 1;

                        if (!e.MountainC(idx_1).HaveAnyResources)
                        {
                            var haveMaxSteps = e.UnitStepC(idx_0).Steps >= StepValues.MAX;

                            if (e.UnitStepC(idx_0).Steps >= e.UnitEs(idx_0).NeedSteps(idx_1).Steps || haveMaxSteps)
                            {
                                if (e.UnitTC(idx_1).HaveUnit)
                                {
                                    if (!e.UnitPlayerTC(idx_1).Is(e.UnitPlayerTC(idx_0).Player))
                                    {
                                        if (e.UnitTC(idx_0).Is(UnitTypes.Pawn))
                                        {
                                            if (dir_cur == DirectTypes.Left || dir_cur == DirectTypes.Right
                                           || dir_cur == DirectTypes.Up || dir_cur == DirectTypes.Down)
                                            {
                                                e.UnitEs(idx_0).ForAttack(AttackTypes.Simple).Add(idx_1);
                                            }
                                            else e.UnitEs(idx_0).ForAttack(AttackTypes.Unique).Add(idx_1);
                                        }
                                        else
                                        {
                                            e.UnitEs(idx_0).ForAttack(AttackTypes.Simple).Add(idx_1);
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
