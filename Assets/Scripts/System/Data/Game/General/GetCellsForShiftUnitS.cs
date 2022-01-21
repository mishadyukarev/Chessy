using static Game.Game.CellEnvironmentEs;
using static Game.Game.CellUnitEs;

namespace Game.Game
{
    public struct GetCellsForShiftUnitS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in CellEs.Idxs)
            {
                CellsForShiftUnitsEs.CellsForShift<IdxsC>(PlayerTypes.First, idx_0).Clear();
                CellsForShiftUnitsEs.CellsForShift<IdxsC>(PlayerTypes.Second, idx_0).Clear();

                if (CellEs.IsActiveC(idx_0).IsActive)
                {
                    if (!CellUnitStunEs.ForExitStun(idx_0).Have && Unit(idx_0).Have && !Unit(idx_0).IsAnimal)
                    {
                        foreach (var idx_1 in CellSpaceSupport.GetIdxsAround(idx_0))
                        {
                            if (!Resources(EnvironmentTypes.Mountain, idx_1).Have && !Unit(idx_1).Have)
                            {
                                var one = CellUnitStepEs.HaveStepsForDoing(idx_0, idx_1);
                                var two = CellUnitStepEs.HaveMaxSteps(idx_0);

                                if (one || two)
                                {
                                    CellsForShiftUnitsEs.CellsForShift<IdxsC>(CellUnitElseEs.Owner(idx_0).Player, idx_0).Add(idx_1);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}