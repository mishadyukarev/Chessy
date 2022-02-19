namespace Game.Game
{
    sealed class GetCellsForShiftUnitS : SystemAbstract, IEcsRunSystem
    {
        internal GetCellsForShiftUnitS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                Es.UnitEs(idx_0).ForShift.Clear();
                Es.UnitEs(idx_0).ForShift.Clear();

                for (byte idx = 0; idx < Es.LengthCells; idx++)
                {
                    Es.UnitEs(idx_0).NeedSteps(idx).Steps = 0;
                    Es.UnitEs(idx_0).NeedSteps(idx).Steps = 0;
                }
                    
                if (Es.CellEs(idx_0).IsActiveParentSelf)
                {
                    if (!Es.UnitStunC(idx_0).IsStunned && Es.UnitTC(idx_0).HaveUnit && !Es.UnitEs(idx_0).IsAnimal)
                    {
                        for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                        {
                            var idx_to = Es.CellEs(idx_0).AroundCellE(dirT).IdxC.Idx;

                            float needSteps = CellUnitStatStep_Values.FOR_SHIFT_ATTACK_EMPTY_CELL;

                            if (!Es.UnitTC(idx_0).Is(UnitTypes.Undead))
                            {
                                needSteps += CellUnitStatStep_Values.NeedStepsShiftAttackUnit(Es.FertilizeC(idx_to).HaveAny, Es.YoungForestC(idx_to).HaveAny, Es.AdultForestC(idx_to).HaveAny, Es.HillC(idx_to).HaveAny);

                                if (Es.CellEs(idx_to).TrailHealthC(dirT.Invert()).IsAlive) needSteps -= CellUnitStatStep_Values.BONUS_TRAIL;
                            }

                            Es.UnitEs(idx_0).NeedSteps(idx_to).Steps = needSteps;

                            if (!Es.MountainC(idx_to).HaveAny && !Es.UnitTC(idx_to).HaveUnit)
                            {
                                if (needSteps <= Es.UnitStepC(idx_0).Steps || Es.UnitStepC(idx_0).Steps >= CellUnitStatStep_Values.StandartForUnit(Es.UnitTC(idx_0).Unit))
                                {
                                    Es.UnitEs(idx_0).ForShift.Add(idx_to);
                                    
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}