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

                if (CellEs.Parent(idx_0).IsActiveSelf.IsActive)
                {
                    if (!CellUnitEs.Stun(idx_0).ForExitStun.Have && Else(idx_0).UnitC.Have && !Else(idx_0).UnitC.IsAnimal)
                    {
                        foreach (var idx_1 in CellSpaceSupport.GetIdxsAround(idx_0))
                        {
                            if (!Environment(EnvironmentTypes.Mountain, idx_1).Resources.Have && !Else(idx_1).UnitC.Have)
                            {
                                var one = CellUnitEs.Step(idx_0).AmountC.Amount >= CellUnitEs.StepsForDoing(idx_0, idx_1);
                                var two = CellUnitEs.Step(idx_0).AmountC.Amount >= CellUnitEs.MaxAmountSteps(idx_0);

                                if (one || two)
                                {
                                    CellsForShiftUnitsEs.CellsForShift<IdxsC>(CellUnitEs.Else(idx_0).OwnerC.Player, idx_0).Add(idx_1);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}