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

                var ownUnit_0 = Es.UnitOwnerE(idx_0).OwnerC;

                if (!Es.UnitEffectEs(idx_0).StunE.IsStunned)
                {
                    if (Es.UnitTypeE(idx_0).HaveUnit && Es.UnitTypeE(idx_0).IsMelee && !Es.UnitTypeE(idx_0).Is(UnitTypes.Scout))
                    {
                        DirectTypes dir_cur = default;

                        CellWorker.TryGetIdxAround(idx_0, out var dirs);

                        foreach (var item_1 in dirs)
                        {
                            dir_cur += 1;
                            var idx_1 = item_1.Value;

                            var unit_1 = UnitEs(idx_1).TypeE.UnitTC;
                            var own_1 = UnitEs(idx_1).OwnerE.OwnerC;

                            if (!EnvironmentEs(idx_1).Mountain.HaveEnvironment)
                            {
                                CellWorker.TryGetDirect(idx_0, idx_1, out var dir);

                                if (UnitEs(idx_0).StatEs.StepE.CanShift(UnitEs(idx_0).TypeE.UnitTC, dir, CellEs(idx_1))|| UnitStatEs(idx_0).StepE.HaveMax(UnitEs(idx_0).TypeE))
                                {
                                    if (UnitEs(idx_1).TypeE.HaveUnit)
                                    {
                                        if (!own_1.Is(ownUnit_0.Player))
                                        {
                                            if (Es.UnitTypeE(idx_0).Is(UnitTypes.Pawn))
                                            {
                                                if (dir_cur == DirectTypes.Left || dir_cur == DirectTypes.Right
                                               || dir_cur == DirectTypes.Up || dir_cur == DirectTypes.Down)
                                                {
                                                    CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Simple, ownUnit_0.Player).Add(idx_1);
                                                }
                                                else CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Unique, ownUnit_0.Player).Add(idx_1);
                                            }
                                            else
                                            {
                                                CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Simple, ownUnit_0.Player).Add(idx_1);
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
