namespace Chessy.Game
{
    sealed class GetCellsForShiftUnitS : SystemAbstract, IEcsRunSystem
    {
        internal GetCellsForShiftUnitS(in EntitiesModel ents) : base(ents)
        {

        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Start_VALUES.ALL_CELLS_AMOUNT; idx_0++)
            {
                E.UnitEs(idx_0).ForShift.Clear();
                E.UnitEs(idx_0).ForShift.Clear();

                for (byte idx = 0; idx < E.LengthCells; idx++)
                {
                    E.UnitEs(idx_0).NeedSteps(idx).Steps = 0;
                    E.UnitEs(idx_0).NeedSteps(idx).Steps = 0;
                }

                if (E.CellEs(idx_0).IsActiveParentSelf)
                {
                    if (!E.UnitEffectStunC(idx_0).IsStunned && E.UnitTC(idx_0).HaveUnit && !E.UnitMainE(idx_0).IsAnimal)
                    {
                        for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                        {
                            var idx_to = E.CellEs(idx_0).AroundCellE(dirT).IdxC.Idx;

                            float needSteps = UnitStep_Values.FOR_SHIFT_ATTACK_EMPTY_CELL;

                            if (!E.UnitTC(idx_0).Is(UnitTypes.Undead))
                            {
                                if (E.FertilizeC(idx_to).HaveAnyResources) needSteps += UnitStep_Values.FERTILIZER;
                                if (E.YoungForestC(idx_to).HaveAnyResources) needSteps += UnitStep_Values.YOUNG_FOREST;
                                if (E.AdultForestC(idx_to).HaveAnyResources) needSteps += UnitStep_Values.ADULT_FOREST;
                                if (E.HillC(idx_to).HaveAnyResources) needSteps += UnitStep_Values.HILL;

                                if (E.CellEs(idx_to).TrailHealthC(dirT.Invert()).IsAlive) needSteps -= UnitStep_Values.BONUS_TRAIL;
                            }

                            E.UnitEs(idx_0).NeedSteps(idx_to).Steps = needSteps;

                            if (!E.MountainC(idx_to).HaveAnyResources && !E.UnitTC(idx_to).HaveUnit)
                            {
                                if (needSteps <= E.UnitStepC(idx_0).Steps || E.UnitStepC(idx_0).Steps >= E.UnitStatsE(idx_0).MaxStepsC.Steps)
                                {
                                    E.UnitEs(idx_0).ForShift.Add(idx_to);

                                }
                            }
                        }
                    }
                }
            }
        }
    }
}