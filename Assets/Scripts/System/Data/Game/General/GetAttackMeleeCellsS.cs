namespace Game.Game
{
    sealed class GetAttackMeleeCellsS : SystemAbstract, IEcsRunSystem
    {
        internal GetAttackMeleeCellsS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                Es.UnitEs(idx_0).ForAttack(AttackTypes.Simple).Clear();
                Es.UnitEs(idx_0).ForAttack(AttackTypes.Unique).Clear();

                if (!Es.UnitStunC(idx_0).IsStunned)
                {
                    if (Es.UnitTC(idx_0).HaveUnit && Es.UnitEs(idx_0).IsMelee
                        && !Es.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.BowCrossbow) && !Es.UnitTC(idx_0).Is(UnitTypes.Scout))
                    {
                        DirectTypes dir_cur = default;

                        for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                        {
                            var idx_1 = Es.CellEs(idx_0).AroundCellE(dirT).IdxC.Idx;

                            dir_cur += 1;

                            if (!Es.EnvironmentEs(idx_1).MountainC.HaveAny)
                            {
                                var dir = Es.CellEs(idx_0).Direct(idx_1);

                                var haveMaxSteps = Es.UnitStepC(idx_0).Steps >= CellUnitStatStep_Values.StandartForUnit(Es.UnitTC(idx_0).Unit);

                                if (Es.UnitTC(idx_0).Is(UnitTypes.King))
                                {

                                }

                                if (Es.UnitStepC(idx_0).Steps >= Es.UnitEs(idx_0).NeedSteps(idx_1).Steps || haveMaxSteps)
                                {
                                    if (Es.UnitTC(idx_1).HaveUnit)
                                    {
                                        if (!Es.UnitPlayerTC(idx_1).Is(Es.UnitPlayerTC(idx_0).Player))
                                        {
                                            if (Es.UnitTC(idx_0).Is(UnitTypes.Pawn))
                                            {
                                                if (dir_cur == DirectTypes.Left || dir_cur == DirectTypes.Right
                                               || dir_cur == DirectTypes.Up || dir_cur == DirectTypes.Down)
                                                {
                                                    Es.UnitEs(idx_0).ForAttack(AttackTypes.Simple).Add(idx_1);
                                                }
                                                else Es.UnitEs(idx_0).ForAttack(AttackTypes.Unique).Add(idx_1);
                                            }
                                            else
                                            {
                                                Es.UnitEs(idx_0).ForAttack(AttackTypes.Simple).Add(idx_1);
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
}
