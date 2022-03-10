using Chessy.Game.Values.Cell.Unit.Stats;

namespace Chessy.Game.System.Model
{
    sealed class GetCellsForShiftUnitS : CellSystem, IEcsRunSystem
    {
        internal GetCellsForShiftUnitS(in byte idx, in EntitiesModel eM) : base(idx, eM)
        {
        }

        public void Run()
        {
            E.UnitEs(Idx).ForShift.Clear();
            E.UnitEs(Idx).ForShift.Clear();

            for (byte idx = 0; idx < E.LengthCells; idx++)
            {
                E.UnitEs(Idx).NeedSteps(idx).Steps = 0;
                E.UnitEs(Idx).NeedSteps(idx).Steps = 0;
            }

            if (E.CellEs(Idx).IsActiveParentSelf)
            {
                if (!E.UnitEffectStunC(Idx).IsStunned && E.UnitTC(Idx).HaveUnit && !E.IsAnimal(E.UnitTC(Idx).Unit))
                {
                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                    {
                        var idx_to = E.CellEs(Idx).AroundCellE(dirT).IdxC.Idx;

                        float needSteps = StepValues.FOR_SHIFT_ATTACK_EMPTY_CELL;

                        if (!E.UnitMainE(Idx).IsMelee || E.UnitMainTWTC(Idx).Is(ToolWeaponTypes.Staff))
                        {
                            needSteps /= 2;
                        }

                        if (!E.UnitTC(Idx).Is(UnitTypes.Undead))
                        {
                            if (E.FertilizeC(idx_to).HaveAnyResources) needSteps += StepValues.FERTILIZER;
                            if (E.YoungForestC(idx_to).HaveAnyResources) needSteps += StepValues.YOUNG_FOREST;
                            if (E.AdultForestC(idx_to).HaveAnyResources)
                            {
                                if (!E.UnitMainTWTC(Idx).Is(ToolWeaponTypes.Staff))
                                {
                                    needSteps += StepValues.ADULT_FOREST;
                                }
                            }
                            if (E.HillC(idx_to).HaveAnyResources)
                            {
                                if (!E.UnitMainTWTC(Idx).Is(ToolWeaponTypes.Staff))
                                {
                                    needSteps += StepValues.HILL;
                                }
                            }

                            if (E.CellEs(idx_to).TrailHealthC(dirT.Invert()).IsAlive) needSteps -= StepValues.BONUS_TRAIL;

                        }



                        E.UnitEs(Idx).NeedSteps(idx_to).Steps = needSteps;

                        if (!E.MountainC(idx_to).HaveAnyResources && !E.UnitTC(idx_to).HaveUnit)
                        {
                            if (needSteps <= E.UnitStepC(Idx).Steps || E.UnitStepC(Idx).Steps >= StepValues.MAX)
                            {
                                E.UnitEs(Idx).ForShift.Add(idx_to);

                            }
                        }
                    }
                }
            }
        }
    }
}