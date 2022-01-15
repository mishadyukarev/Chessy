using static Game.Game.CellE;
using static Game.Game.CellUnitE;
using static Game.Game.EntityCellTrailPool;
using static Game.Game.CellEnvironmentE;

namespace Game.Game
{
    struct GetAttackPawnCellsS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in Idxs)
            {
                ref var unit_0 = ref Unit<UnitTC>(idx_0);
                ref var level_0 = ref Unit<LevelTC>(idx_0);
                ref var ownUnit_0 = ref Unit<PlayerTC>(idx_0);
                ref var step_0 = ref Unit<StepC>(idx_0);
                ref var stunUnit_0 = ref Unit<NeedStepsForExitStunC>(idx_0);

                if (!stunUnit_0.IsStunned)
                {
                    if (unit_0.Is(UnitTypes.Pawn))
                    {
                        DirectTypes dir_cur = default;

                        CellSpaceC.TryGetXyAround(Cell<XyC>(idx_0).Xy, out var dirs);

                        foreach (var item_1 in dirs)
                        {
                            dir_cur += 1;
                            var idx_1 = IdxCell(item_1.Value);

                            ref var unit_1 = ref Unit<UnitTC>(idx_1);
                            ref var own_1 = ref Unit<PlayerTC>(idx_1);

                            ref var trail_1 = ref Trail<TrailCellEC>(idx_1);


                            if (!Environment<HaveEnvironmentC>(EnvTypes.Mountain, idx_1).Have)
                            {
                                if (Unit<UnitCellEC>(idx_0).HaveStepsForDoing(idx_1)
                                    || Unit<UnitCellEC>(idx_0).HaveMaxSteps)
                                {
                                    if (unit_1.Have)
                                    {
                                        if (!own_1.Is(ownUnit_0.Player))
                                        {
                                            //if (dir_cur == DirectTypes.Left || dir_cur == DirectTypes.Right
                                            //    || dir_cur == DirectTypes.Up || dir_cur == DirectTypes.Down)
                                            //{
                                            //    AttackCellsC.Add(AttackTypes.Simple, ownUnit_0.Player, idx_0, idx_1);
                                            //}
                                            //else AttackCellsC.Add(AttackTypes.Unique, ownUnit_0.Player, idx_0, idx_1);
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
