using Chessy.Game.Values.Cell.Unit.Stats;
using System;

namespace Chessy.Game.System.Model
{
    public struct GetCellsForShiftUnitS
    {
        public GetCellsForShiftUnitS(in byte idx_0, in EntitiesModel e)
        {
            if (!e.UnitTC(idx_0).HaveUnit) throw new Exception();


            e.UnitEs(idx_0).ForShift.Clear();
            e.UnitEs(idx_0).ForShift.Clear();


            for (byte idx = 0; idx < e.LengthCells; idx++)
            {
                e.UnitEs(idx_0).NeedSteps(idx).Steps = 0;
                e.UnitEs(idx_0).NeedSteps(idx).Steps = 0;
            }

            if (e.CellEs(idx_0).IsActiveParentSelf)
            {
                if (!e.UnitEffectStunC(idx_0).IsStunned && e.UnitTC(idx_0).HaveUnit && !e.IsAnimal(e.UnitTC(idx_0).Unit))
                {
                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                    {
                        var idx_to = e.CellEs(idx_0).AroundCellE(dirT).IdxC.Idx;

                        float needSteps = StepValues.FOR_SHIFT_ATTACK_EMPTY_CELL;

                        if (!e.IsMelee(idx_0) || e.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.Staff))
                        {
                            needSteps /= 2;
                        }

                        if (!e.UnitTC(idx_0).Is(UnitTypes.Undead))
                        {
                            if (e.FertilizeC(idx_to).HaveAnyResources) needSteps += StepValues.FERTILIZER;
                            if (e.YoungForestC(idx_to).HaveAnyResources) needSteps += StepValues.YOUNG_FOREST;
                            if (e.AdultForestC(idx_to).HaveAnyResources)
                            {
                                if (!e.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.Staff))
                                {
                                    needSteps += StepValues.ADULT_FOREST;
                                }
                            }
                            if (e.HillC(idx_to).HaveAnyResources)
                            {
                                if (!e.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.Staff))
                                {
                                    needSteps += StepValues.HILL;
                                }
                            }

                            if (e.CellEs(idx_to).TrailHealthC(dirT.Invert()).IsAlive) needSteps -= StepValues.BONUS_TRAIL;

                        }



                        e.UnitEs(idx_0).NeedSteps(idx_to).Steps = needSteps;

                        if (!e.MountainC(idx_to).HaveAnyResources && !e.UnitTC(idx_to).HaveUnit)
                        {
                            if (needSteps <= e.UnitStepC(idx_0).Steps || e.UnitStepC(idx_0).Steps >= StepValues.MAX)
                            {
                                e.UnitEs(idx_0).ForShift.Add(idx_to);

                            }
                        }
                    }
                }
            }
        }
    }
}