using Chessy.Game.Values.Cell.Unit.Stats;
using System;

namespace Chessy.Game.System.Model
{
    public struct GetAttackMeleeCellsS
    {
        public GetAttackMeleeCellsS(in byte cell_0, in Chessy.Game.Entity.Model.EntitiesModelGame e)
        {
            e.UnitEs(cell_0).SimpleAttack.Clear();
            e.UnitEs(cell_0).UniqueAttack.Clear();

            if (!e.UnitEffectStunC(cell_0).IsStunned)
            {
                if (e.UnitTC(cell_0).HaveUnit && e.UnitTC(cell_0).IsMelee(e.UnitMainTWTC(cell_0).ToolWeapon) && !e.UnitTC(cell_0).IsAnimal)
                {
                    DirectTypes dir_cur = default;

                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                    {
                        var idx_1 = e.CellEs(cell_0).AroundCellE(dirT).IdxC.Idx;

                        dir_cur += 1;

                        if (!e.MountainC(idx_1).HaveAnyResources)
                        {
                            var haveMaxSteps = e.UnitStepC(cell_0).Steps >= StepValues.MAX;

                            if (e.UnitStepC(cell_0).Steps >= e.UnitEs(cell_0).NeedSteps(idx_1).Steps || haveMaxSteps)
                            {
                                if (e.UnitTC(idx_1).HaveUnit)
                                {
                                    if (!e.UnitPlayerTC(idx_1).Is(e.UnitPlayerTC(cell_0).Player))
                                    {
                                        if (e.UnitTC(cell_0).Is(UnitTypes.Pawn))
                                        {
                                            if (dir_cur == DirectTypes.Left || dir_cur == DirectTypes.Right
                                           || dir_cur == DirectTypes.Up || dir_cur == DirectTypes.Down)
                                            {
                                                e.UnitEs(cell_0).SimpleAttack.Add(idx_1);
                                            }
                                            else e.UnitEs(cell_0).UniqueAttack.Add(idx_1);
                                        }
                                        else
                                        {
                                            e.UnitEs(cell_0).SimpleAttack.Add(idx_1);
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
