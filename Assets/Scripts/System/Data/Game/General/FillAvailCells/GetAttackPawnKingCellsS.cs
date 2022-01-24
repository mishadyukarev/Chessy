﻿using static Game.Game.CellEnvironmentEs;
using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;

namespace Game.Game
{
    struct GetAttackPawnKingCellsS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in Idxs)
            {
                CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Simple, PlayerTypes.First).Clear();
                CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Simple, PlayerTypes.Second).Clear();
                CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Unique, PlayerTypes.First).Clear();
                CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Unique, PlayerTypes.Second).Clear();

                ref var unit_0 = ref Unit(idx_0);
                ref var level_0 = ref EntitiesPool.UnitElse.Level(idx_0);
                ref var ownUnit_0 = ref EntitiesPool.UnitElse.Owner(idx_0);
                ref var step_0 = ref EntitiesPool.UnitStep.Steps(idx_0);
                ref var stunUnit_0 = ref EntitiesPool.UnitStuns[idx_0].ForExitStun;

                if (!stunUnit_0.Have)
                {
                    if (unit_0.Is(UnitTypes.Pawn) || unit_0.Is(UnitTypes.King))
                    {
                        DirectTypes dir_cur = default;

                        CellSpaceSupport.TryGetXyAround(Cell<XyC>(idx_0).Xy, out var dirs);

                        foreach (var item_1 in dirs)
                        {
                            dir_cur += 1;
                            var idx_1 = IdxCell(item_1.Value);

                            ref var unit_1 = ref Unit(idx_1);
                            ref var own_1 = ref EntitiesPool.UnitElse.Owner(idx_1);

                            if (!Resources(EnvironmentTypes.Mountain, idx_1).Have)
                            {
                                if (EntitiesPool.UnitStep.HaveStepsForDoing(idx_0, idx_1)
                                    || EntitiesPool.UnitStep.HaveMaxSteps(idx_0))
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
