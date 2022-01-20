using static Game.Game.CellEnvironmentEs;
using static Game.Game.CellUnitEs;

namespace Game.Game
{
    public struct GetCellsForShiftUnitS : IEcsRunSystem
    {
        public void Run()
        {
            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                foreach (var idx_0 in CellEs.Idxs)
                {
                    if (CellEs.IsActiveC(idx_0).IsActive)
                    {
                        CellsForShiftUnitsEs.CellsForShift<IdxsC>(player, idx_0).Clear();

                        if (!CellUnitStunEs.StepsForExitStun(idx_0).Have && Unit(idx_0).Have
                            && CellUnitElseEs.Owner(idx_0).Is(player))
                        {
                            foreach (var idx_1 in CellSpaceSupport.GetIdxAround(idx_0))
                            {
                                if (!Resources(EnvironmentTypes.Mountain, idx_1).Have && !Unit(idx_1).Have)
                                {
                                    var one = CellUnitStepEs.HaveStepsForDoing(idx_0, idx_1);
                                    var two = CellUnitStepEs.HaveMaxSteps(idx_0);

                                    if (one || two)
                                    {
                                        CellsForShiftUnitsEs.CellsForShift<IdxsC>(player, idx_0).Add(idx_1);
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