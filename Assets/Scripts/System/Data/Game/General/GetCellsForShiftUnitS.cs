using UnityEditor;
using UnityEngine;
using static Game.Game.CellUnitEs;
using static Game.Game.CellEnvironmentEs;

namespace Game.Game
{
    public struct GetCellsForShiftUnitS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_from in CellEs.Idxs)
            {
                if (CellEs.Cell<IsActiveC>(idx_from).IsActive)
                {
                    for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                    {
                        CellsForShiftUnitsEs.CellsForShift<IdxsC>(player, idx_from).Clear();


                        ref var stepUnit_from = ref Unit<UnitCellEC>(idx_from);

                        if (!Unit<NeedStepsForExitStunC>(idx_from).IsStunned && Unit<UnitTC>(idx_from).Have 
                            && Unit<PlayerTC>(idx_from).Is(player))
                        {
                            CellSpaceC.TryGetIdxAround(idx_from, out var directs);

                            foreach (var item_1 in directs)
                            {
                                var idx_to = item_1.Value;

                                if (idx_to == item_1.Value && !Environment<HaveEnvironmentC>(EnvTypes.Mountain, idx_to).Have && !Unit<UnitTC>(idx_to).Have)
                                {
                                    var one = stepUnit_from.HaveStepsForDoing(idx_to);
                                    var two = stepUnit_from.HaveMaxSteps;

                                    if (one || two)
                                    {
                                        CellsForShiftUnitsEs.CellsForShift<IdxsC>(player, idx_from).Add(idx_to);
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