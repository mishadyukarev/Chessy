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
                CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Simple, PlayerTypes.First).Clear();
                CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Simple, PlayerTypes.Second).Clear();
                CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Unique, PlayerTypes.First).Clear();
                CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Unique, PlayerTypes.Second).Clear();

                var ownUnit_0 = Es.UnitE(idx_0).Owner;

                if (!Es.UnitE(idx_0).IsStunned)
                {
                    if (Es.UnitE(idx_0).HaveUnit && Es.UnitE(idx_0).IsMelee(Es.MainTWE(idx_0)) 
                        && !Es.MainTWE(idx_0).Is(ToolWeaponTypes.BowCrossbow) && !Es.UnitE(idx_0).Is(UnitTypes.Scout))
                    {
                        DirectTypes dir_cur = default;

                        CellWorker.TryGetIdxAround(idx_0, out var dirs);

                        foreach (var item_1 in dirs)
                        {
                            dir_cur += 1;
                            var idx_1 = item_1.Value;

                            var own_1 = Es.UnitE(idx_1).Owner;

                            if (!EnvironmentEs(idx_1).Mountain.HaveEnvironment)
                            {
                                CellWorker.TryGetDirect(idx_0, idx_1, out var dir);

                                if (Es.UnitE(idx_0).CanShift(Es.UnitE(idx_0).Unit, dir, CellEs(idx_1))|| Es.UnitE(idx_0).HaveMaxSteps)
                                {
                                    if (UnitEs(idx_1).UnitE.HaveUnit)
                                    {
                                        if (!Es.UnitE(idx_1).Is(ownUnit_0))
                                        {
                                            if (Es.UnitE(idx_0).Is(UnitTypes.Pawn))
                                            {
                                                if (dir_cur == DirectTypes.Left || dir_cur == DirectTypes.Right
                                               || dir_cur == DirectTypes.Up || dir_cur == DirectTypes.Down)
                                                {
                                                    CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Simple, ownUnit_0).Add(idx_1);
                                                }
                                                else CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Unique, ownUnit_0).Add(idx_1);
                                            }
                                            else
                                            {
                                                CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Simple, ownUnit_0).Add(idx_1);
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
