namespace Game.Game
{
    sealed class GetCellsForShiftUnitS : SystemAbstract, IEcsRunSystem
    {
        internal GetCellsForShiftUnitS(in EntitiesModel ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
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

                            float needSteps = CellUnitStatStep_Values.FOR_SHIFT_ATTACK_EMPTY_CELL;

                            if (!E.UnitTC(idx_0).Is(UnitTypes.Undead))
                            {
                                needSteps += CellUnitStatStep_Values.NeedStepsShiftAttackUnit(E.FertilizeC(idx_to).HaveAny, E.YoungForestC(idx_to).HaveAny, E.AdultForestC(idx_to).HaveAny, E.HillC(idx_to).HaveAny);

                                if (E.CellEs(idx_to).TrailHealthC(dirT.Invert()).IsAlive) needSteps -= CellUnitStatStep_Values.BONUS_TRAIL;
                            }

                            E.UnitEs(idx_0).NeedSteps(idx_to).Steps = needSteps;

                            if (!E.MountainC(idx_to).HaveAny && !E.UnitTC(idx_to).HaveUnit)
                            {
                                if (needSteps <= E.UnitStepC(idx_0).Steps || E.UnitStepC(idx_0).Steps >= CellUnitStatStep_Values.StandartForUnit(E.UnitTC(idx_0).Unit))
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