using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Stats;

namespace Chessy.Game.Model.System
{
    sealed class GetCellsForShiftUnitS : SystemModelGameAbs
    {
        internal GetCellsForShiftUnitS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Get(in byte cell_0)
        {
            eMG.UnitShiftE(cell_0).ForShift.Clear();

            for (byte idx = 0; idx < StartValues.CELLS; idx++)
                eMG.UnitShiftE(cell_0).Set(idx, 0);

            if (eMG.CellE(cell_0).IsActiveParentSelf)
            {
                if (!eMG.UnitEffectStunC(cell_0).IsStunned && eMG.UnitTC(cell_0).HaveUnit && !eMG.UnitTC(cell_0).IsAnimal)
                {
                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                    {
                        var idx_to = eMG.AroundCellsE(cell_0).IdxCell(dirT);

                        float needSteps = StepValues.FOR_SHIFT_ATTACK_EMPTY_CELL;

                        if (eMG.UnitTC(cell_0).Is(UnitTypes.Tree))
                        {
                            needSteps = 1;
                        }
                        else
                        {
                            if (!eMG.UnitTC(cell_0).Is(UnitTypes.Undead))
                            {
                                if (eMG.UnitTC(cell_0).Is(UnitTypes.Pawn) && eMG.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.Staff))
                                {
                                    needSteps /= 2;
                                }
                                else if (eMG.UnitTC(cell_0).Is(UnitTypes.Snowy))
                                {
                                    if (eMG.FertilizeC(idx_to).HaveAnyResources)
                                    {
                                        needSteps = StepValues.FOR_SHIFT_ATTACK_EMPTY_CELL / 2;
                                    }
                                }


                                if (eMG.AdultForestC(idx_to).HaveAnyResources)
                                {
                                    if (eMG.UnitTC(cell_0).Is(UnitTypes.Pawn))
                                    {
                                        if (!eMG.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.Staff))
                                        {
                                            needSteps += StepValues.ADULT_FOREST;

                                            if (eMG.HealthTrail(idx_to).IsAlive(dirT.Invert())) needSteps -= StepValues.BONUS_TRAIL;
                                        }
                                    }
                                    else if (eMG.UnitTC(cell_0).Is(UnitTypes.Elfemale))
                                    {
                                        needSteps /= 2;
                                    }
                                    else
                                    {
                                        needSteps += StepValues.ADULT_FOREST;

                                        if (eMG.HealthTrail(idx_to).IsAlive(dirT.Invert())) needSteps -= StepValues.BONUS_TRAIL;
                                    }
                                }
                                else
                                {
                                    if (!eMG.UnitTC(cell_0).Is(UnitTypes.Elfemale))
                                    {

                                    }
                                }

                                if (eMG.HillC(idx_to).HaveAnyResources)
                                {
                                    if (!eMG.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.Staff))
                                    {
                                        needSteps += StepValues.HILL;
                                    }
                                }
                            }
                        }



                        eMG.UnitShiftE(cell_0).Set(idx_to, needSteps);

                        if (!eMG.MountainC(idx_to).HaveAnyResources && !eMG.UnitTC(idx_to).HaveUnit)
                        {
                            if (needSteps <= eMG.UnitStepC(cell_0).Steps || eMG.UnitStepC(cell_0).Steps >= StepValues.MAX)
                            {
                                eMG.UnitShiftE(cell_0).ForShift.Add(idx_to);

                            }
                        }
                    }
                }
            }
        }
    }
}