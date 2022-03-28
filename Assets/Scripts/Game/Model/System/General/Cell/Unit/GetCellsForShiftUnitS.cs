using Chessy.Game.Entity.Model;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Stats;

namespace Chessy.Game.System.Model
{
    sealed class GetCellsForShiftUnitS : SystemModelGameAbs
    {
        internal GetCellsForShiftUnitS(in EntitiesModelGame eMGame) : base(eMGame) { }

        internal void Get(in byte cell_0)
        {
            eMGame.UnitEs(cell_0).ForShift.Clear();

            for (byte idx = 0; idx < StartValues.CELLS; idx++)
                eMGame.UnitEs(cell_0).NeedSteps(idx).Steps = 0;

            if (eMGame.CellEs(cell_0).IsActiveParentSelf)
            {
                if (!eMGame.UnitEffectStunC(cell_0).IsStunned && eMGame.UnitTC(cell_0).HaveUnit && !eMGame.UnitTC(cell_0).IsAnimal)
                {
                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                    {
                        var idx_to = eMGame.CellEs(cell_0).AroundCellsEs.AroundCellE(dirT).IdxC.Idx;

                        float needSteps = StepValues.FOR_SHIFT_ATTACK_EMPTY_CELL;

                        if (eMGame.UnitTC(cell_0).Is(UnitTypes.Tree))
                        {
                            needSteps = 1;
                        }
                        else
                        {
                            if (!eMGame.UnitTC(cell_0).Is(UnitTypes.Undead))
                            {
                                if (eMGame.UnitTC(cell_0).Is(UnitTypes.Pawn) && eMGame.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.Staff))
                                {
                                    needSteps /= 2;
                                }
                                else if (eMGame.UnitTC(cell_0).Is(UnitTypes.Snowy))
                                {
                                    if (eMGame.FertilizeC(idx_to).HaveAnyResources)
                                    {
                                        needSteps = StepValues.FOR_SHIFT_ATTACK_EMPTY_CELL / 2;
                                    }
                                }


                                if (eMGame.AdultForestC(idx_to).HaveAnyResources)
                                {
                                    if (eMGame.UnitTC(cell_0).Is(UnitTypes.Pawn))
                                    {
                                        if (!eMGame.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.Staff))
                                        {
                                            needSteps += StepValues.ADULT_FOREST;

                                            if (eMGame.CellEs(idx_to).TrailHealthC(dirT.Invert()).IsAlive) needSteps -= StepValues.BONUS_TRAIL;
                                        }
                                    }
                                    else if (eMGame.UnitTC(cell_0).Is(UnitTypes.Elfemale))
                                    {
                                        needSteps /= 2;
                                    }
                                    else
                                    {
                                        needSteps += StepValues.ADULT_FOREST;

                                        if (eMGame.CellEs(idx_to).TrailHealthC(dirT.Invert()).IsAlive) needSteps -= StepValues.BONUS_TRAIL;
                                    }
                                }
                                else
                                {
                                    if (!eMGame.UnitTC(cell_0).Is(UnitTypes.Elfemale))
                                    {

                                    }
                                }

                                if (eMGame.HillC(idx_to).HaveAnyResources)
                                {
                                    if (!eMGame.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.Staff))
                                    {
                                        needSteps += StepValues.HILL;
                                    }
                                }
                            }
                        }



                        eMGame.UnitEs(cell_0).NeedSteps(idx_to).Steps = needSteps;

                        if (!eMGame.MountainC(idx_to).HaveAnyResources && !eMGame.UnitTC(idx_to).HaveUnit)
                        {
                            if (needSteps <= eMGame.UnitStepC(cell_0).Steps || eMGame.UnitStepC(cell_0).Steps >= StepValues.MAX)
                            {
                                eMGame.UnitEs(cell_0).ForShift.Add(idx_to);

                            }
                        }
                    }
                }
            }
        }
    }
}