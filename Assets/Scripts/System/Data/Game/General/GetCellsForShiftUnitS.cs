using static Game.Game.CellEnvironmentEs;
using static Game.Game.CellUnitEs;

namespace Game.Game
{
    public struct GetCellsForShiftUnitS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_from in CellEs.Idxs)
            {
                if (CellEs.CellParent<IsActiveC>(idx_from).IsActive)
                {
                    for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                    {
                        CellsForShiftUnitsEs.CellsForShift<IdxsC>(player, idx_from).Clear();

                        if (!CellUnitStunEs.StepsForExitStun(idx_from).Have && Unit<UnitTC>(idx_from).Have
                            && Unit<PlayerTC>(idx_from).Is(player))
                        {
                            CellSpaceC.TryGetIdxAround(idx_from, out var directs);

                            foreach (var item_1 in directs)
                            {
                                var idx_to = item_1.Value;

                                if (idx_to == item_1.Value && !Resources(EnvironmentTypes.Mountain, idx_to).Have && !Unit<UnitTC>(idx_to).Have)
                                {
                                    var one = CellUnitStepEs.HaveStepsForDoing(idx_from, idx_to);
                                    var two = CellUnitStepEs.HaveMaxSteps(idx_from);

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