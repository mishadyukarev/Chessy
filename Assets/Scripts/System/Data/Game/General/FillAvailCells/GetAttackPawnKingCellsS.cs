namespace Game.Game
{
    sealed class GetAttackPawnKingCellsS : SystemAbstract, IEcsRunSystem
    {
        public GetAttackPawnKingCellsS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            foreach (byte idx_0 in CellEsWorker.Idxs)
            {
                CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Simple, PlayerTypes.First).Clear();
                CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Simple, PlayerTypes.Second).Clear();
                CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Unique, PlayerTypes.First).Clear();
                CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Unique, PlayerTypes.Second).Clear();

                var unit_0 = UnitEs(idx_0).MainE.UnitTC;
                var ownUnit_0 = UnitEs(idx_0).MainE.OwnerC;
                var step_0 = UnitStatEs(idx_0).StepE.Steps;

                if (!UnitEffectEs(idx_0).StunE.IsStunned)
                {
                    if (unit_0.Is(UnitTypes.Pawn) || unit_0.Is(UnitTypes.King))
                    {
                        DirectTypes dir_cur = default;

                        CellEsWorker.TryGetIdxAround(idx_0, out var dirs);

                        foreach (var item_1 in dirs)
                        {
                            dir_cur += 1;
                            var idx_1 = item_1.Value;

                            var unit_1 = UnitEs(idx_1).MainE.UnitTC;
                            var own_1 = UnitEs(idx_1).MainE.OwnerC;

                            if (!EnvironmentEs(idx_1).Mountain.HaveEnvironment)
                            {
                                if (UnitStatEs(idx_0).StepE.Steps.Amount >=
                                    UnitEs(idx_1).MainE.StepsForShiftOrAttack(CellEsWorker.GetDirect(idx_0, idx_1), CellEs(idx_1).EnvironmentEs, CellEs(idx_1).TrailEs)

                                    || UnitStatEs(idx_0).StepE.HaveMax(UnitEs(idx_0).MainE))
                                {
                                    if (UnitEs(idx_1).MainE.HaveUnit(UnitStatEs(idx_1)))
                                    {
                                        if (!own_1.Is(ownUnit_0.Player))
                                        {
                                            if (unit_0.Is(UnitTypes.King))
                                            {
                                                CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Simple, ownUnit_0.Player).Add(idx_1);
                                            }
                                            else
                                            {
                                                if (dir_cur == DirectTypes.Left || dir_cur == DirectTypes.Right
                                                || dir_cur == DirectTypes.Up || dir_cur == DirectTypes.Down)
                                                {
                                                    CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Simple, ownUnit_0.Player).Add(idx_1);
                                                }
                                                else CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Unique, ownUnit_0.Player).Add(idx_1);
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
