namespace Game.Game
{
    sealed class GetAttackMeleeCellsS : SystemAbstract, IEcsRunSystem
    {
        internal GetAttackMeleeCellsS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            foreach (byte idx_0 in CellWorker.Idxs)
            {
                CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Simple, PlayerTypes.First).Clear();
                CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Simple, PlayerTypes.Second).Clear();
                CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Unique, PlayerTypes.First).Clear();
                CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Unique, PlayerTypes.Second).Clear();

                var unit_0 = UnitEs(idx_0).TypeE.UnitTC;
                var ownUnit_0 = UnitEs(idx_0).OwnerE.OwnerC;
                var step_0 = UnitStatEs(idx_0).StepE.StepsC;

                if (!UnitEffectEs(idx_0).StunE.IsStunned)
                {
                    if (unit_0.Is(UnitTypes.Pawn, UnitTypes.King, UnitTypes.Undead, UnitTypes.Hell))
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
                                            if (unit_0.Is(UnitTypes.Pawn))
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
