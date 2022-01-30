namespace Game.Game
{
    sealed class GetAttackPawnKingCellsS : SystemAbstract, IEcsRunSystem
    {
        public GetAttackPawnKingCellsS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            foreach (byte idx_0 in Es.CellEs.Idxs)
            {
                CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Simple, PlayerTypes.First).Clear();
                CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Simple, PlayerTypes.Second).Clear();
                CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Unique, PlayerTypes.First).Clear();
                CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Unique, PlayerTypes.Second).Clear();

                ref var unit_0 = ref Es.CellEs.UnitEs.Main(idx_0).UnitC;
                ref var level_0 = ref Es.CellEs.UnitEs.Main(idx_0).LevelC;
                ref var ownUnit_0 = ref Es.CellEs.UnitEs.Main(idx_0).OwnerC;
                ref var step_0 = ref Es.CellEs.UnitEs.StatEs.Step(idx_0).Steps;
                ref var stunUnit_0 = ref Es.CellEs.UnitEs.Stun(idx_0).ForExitStun;

                if (!stunUnit_0.Have)
                {
                    if (unit_0.Is(UnitTypes.Pawn) || unit_0.Is(UnitTypes.King))
                    {
                        DirectTypes dir_cur = default;

                        Es.CellEs.TryGetIdxAround(idx_0, out var dirs);

                        foreach (var item_1 in dirs)
                        {
                            dir_cur += 1;
                            var idx_1 = item_1.Value;

                            ref var unit_1 = ref Es.CellEs.UnitEs.Main(idx_1).UnitC;
                            ref var own_1 = ref Es.CellEs.UnitEs.Main(idx_1).OwnerC;

                            if (!Es.CellEs.EnvironmentEs.Mountain(idx_1).HaveEnvironment)
                            {
                                if (Es.CellEs.UnitEs.StatEs.Step(idx_0).Steps.Amount >=
                                    Es.CellEs.UnitEs.StepsForShiftOrAttack(idx_1, Es.CellEs.GetDirect(idx_0, idx_1), Es.CellEs.EnvironmentEs, Es.CellEs.TrailEs)

                                    || Es.CellEs.UnitEs.StatEs.Step(idx_0).HaveMax(Es.CellEs.UnitEs.Main(idx_0)))
                                {
                                    if (unit_1.Have)
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
