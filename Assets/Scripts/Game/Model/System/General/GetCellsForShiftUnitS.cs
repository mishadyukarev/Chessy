using Chessy.Game.Values.Cell.Unit.Stats;
using System;

namespace Chessy.Game.System.Model
{
    public struct GetCellsForShiftUnitS
    {
        public GetCellsForShiftUnitS(in byte cell_0, in Chessy.Game.Entity.Model.EntitiesModelGame e)
        {
            e.UnitEs(cell_0).ForShift.Clear();

            for (byte idx = 0; idx < e.LengthCells; idx++) 
                e.UnitEs(cell_0).NeedSteps(idx).Steps = 0;

            if (e.CellEs(cell_0).IsActiveParentSelf)
            {
                if (!e.UnitEffectStunC(cell_0).IsStunned && e.UnitTC(cell_0).HaveUnit && !e.UnitTC(cell_0).IsAnimal)
                {
                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                    {
                        var idx_to = e.CellEs(cell_0).AroundCellE(dirT).IdxC.Idx;

                        float needSteps = StepValues.FOR_SHIFT_ATTACK_EMPTY_CELL;

                        if (e.UnitTC(cell_0).Is(UnitTypes.Tree))
                        {
                            needSteps = 1;
                        }
                        else
                        {
                            if (!e.UnitTC(cell_0).Is(UnitTypes.Undead))
                            {
                                if (e.UnitTC(cell_0).Is(UnitTypes.Pawn) && e.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.Staff))
                                {
                                    needSteps /= 2;
                                }
                                else if (e.UnitTC(cell_0).Is(UnitTypes.Snowy))
                                {
                                    if (e.FertilizeC(idx_to).HaveAnyResources)
                                    {
                                        needSteps = StepValues.FOR_SHIFT_ATTACK_EMPTY_CELL / 2;
                                    }
                                }


                                if (e.AdultForestC(idx_to).HaveAnyResources)
                                {
                                    if (e.UnitTC(cell_0).Is(UnitTypes.Pawn))
                                    {
                                        if (!e.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.Staff))
                                        {
                                            needSteps += StepValues.ADULT_FOREST;

                                            if (e.CellEs(idx_to).TrailHealthC(dirT.Invert()).IsAlive) needSteps -= StepValues.BONUS_TRAIL;
                                        }
                                    }
                                    else if (e.UnitTC(cell_0).Is(UnitTypes.Elfemale))
                                    {
                                        needSteps /= 2;
                                    }
                                    else
                                    {
                                        needSteps += StepValues.ADULT_FOREST;

                                        if (e.CellEs(idx_to).TrailHealthC(dirT.Invert()).IsAlive) needSteps -= StepValues.BONUS_TRAIL;
                                    }
                                }
                                else
                                {
                                    if (!e.UnitTC(cell_0).Is(UnitTypes.Elfemale))
                                    {

                                    }
                                }

                                if (e.HillC(idx_to).HaveAnyResources)
                                {
                                    if (!e.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.Staff))
                                    {
                                        needSteps += StepValues.HILL;
                                    }
                                }
                            }
                        }



                        e.UnitEs(cell_0).NeedSteps(idx_to).Steps = needSteps;

                        if (!e.MountainC(idx_to).HaveAnyResources && !e.UnitTC(idx_to).HaveUnit)
                        {
                            if (needSteps <= e.UnitStepC(cell_0).Steps || e.UnitStepC(cell_0).Steps >= StepValues.MAX)
                            {
                                e.UnitEs(cell_0).ForShift.Add(idx_to);

                            }
                        }
                    }
                }
            }
        }
    }
}