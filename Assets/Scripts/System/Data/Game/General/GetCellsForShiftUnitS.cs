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
                Es.UnitEs(idx_0).ForPlayer(PlayerTypes.First).ForShift.Clear();
                Es.UnitEs(idx_0).ForPlayer(PlayerTypes.Second).ForShift.Clear();

                if (Es.CellEs(idx_0).IsActiveParentSelf)
                {
                    if (!Es.UnitStunC(idx_0).IsStunned && Es.UnitTC(idx_0).HaveUnit && !Es.UnitEs(idx_0).IsAnimal)
                    {
                        foreach (var idx_to in CellWorker.GetIdxsAround(idx_0))
                        {
                            if (!Es.MountainC(idx_to).HaveAny && !Es.UnitTC(idx_to).HaveUnit)
                            {
                                CellWorker.TryGetDirect(idx_0, idx_to, out var dir);


                                float needSteps = CellUnitStatStep_Values.FOR_SHIFT_ATTACK_EMPTY_CELL;

                                if (!Es.UnitTC(idx_0).Is(UnitTypes.Undead))
                                {
                                    needSteps += CellUnitStatStep_Values.NeedStepsShiftAttackUnit(Es.FertilizeC(idx_to).HaveAny, Es.YoungForestC(idx_to).HaveAny, Es.AdultForestC(idx_to).HaveAny, Es.HillC(idx_to).HaveAny);

                                    if (Es.TrailEs(idx_to).Trail(dir.Invert()).HealthC.IsAlive) needSteps -= CellUnitStatStep_Values.BONUS_TRAIL;
                                }

                                if (needSteps <= Es.UnitStepC(idx_0).Steps || Es.UnitStepC(idx_0).Steps >= CellUnitStatStep_Values.StandartForUnit(Es.UnitTC(idx_0).Unit))
                                {
                                    Es.UnitEs(idx_0).ForPlayer(Es.UnitPlayerTC(idx_0).Player).ForShift.Add(idx_to);
                                    Es.UnitEs(idx_0).ForPlayer(Es.UnitPlayerTC(idx_0).Player).NeedStepsForShift[idx_to] = needSteps;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}