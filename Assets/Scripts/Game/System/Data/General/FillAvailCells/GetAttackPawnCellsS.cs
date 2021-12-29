using Leopotam.Ecs;
using static Game.Game.EntityCellPool;

namespace Game.Game
{
    public sealed class GetAttackPawnCellsS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in Idxs)
            {
                ref var unit_0 = ref Unit<UnitC>(idx_0);
                ref var level_0 = ref Unit<LevelC>(idx_0);
                ref var ownUnit_0 = ref Unit<OwnerC>(idx_0);
                ref var step_0 = ref Unit<StepC>(idx_0);
                ref var effUnit_0 = ref Unit<EffectsC>(idx_0); 
                ref var stunUnit_0 = ref Unit<StunC>(idx_0);

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

                            ref var env_1 = ref Environment<EnvC>(idx_1);
                            ref var unit_1 = ref Unit<UnitC>(idx_1);
                            ref var own_1 = ref Unit<OwnerC>(idx_1);

                            ref var trail_1 = ref Trail<TrailC>(idx_1);


                            if (!env_1.Have(EnvTypes.Mountain))
                            {
                                if (Unit<StepUnitWC>(idx_0).HaveStepsForDoing(idx_1)
                                    || Unit<StepUnitWC>(idx_0).HaveMaxSteps)
                                {
                                    if (unit_1.Have)
                                    {
                                        if (!own_1.Is(ownUnit_0.Owner))
                                        {
                                            if (dir_cur == DirectTypes.Left || dir_cur == DirectTypes.Right
                                                || dir_cur == DirectTypes.Up || dir_cur == DirectTypes.Down)
                                            {
                                                AttackCellsC.Add(AttackTypes.Simple, ownUnit_0.Owner,  idx_0, idx_1);
                                            }
                                            else AttackCellsC.Add(AttackTypes.Unique, ownUnit_0.Owner,  idx_0, idx_1);
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
