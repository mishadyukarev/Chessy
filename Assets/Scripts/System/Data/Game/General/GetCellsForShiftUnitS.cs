using static Game.Game.CellEnvironmentEs;
using static Game.Game.CellUnitEntities;
using static Game.Game.EntitiesPool;

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
                    if (!CellUnitEntities.Stun(idx_0).ForExitStun.Have && Else(idx_0).UnitC.Have && !Else(idx_0).UnitC.IsAnimal)
                    {
                        foreach (var idx_1 in CellSpaceSupport.GetIdxsAround(idx_0))
                        {
                            if (!Resources(EnvironmentTypes.Mountain, idx_1).Have && !Else(idx_1).UnitC.Have)
                            {
                                var one = CellUnitEntities.Step(idx_0).AmountC.Amount >= CellUnitEntities.StepsForDoing(idx_0, idx_1);
                                var two = CellUnitEntities.Step(idx_0).AmountC.Amount >= CellUnitEntities.MaxAmountSteps(idx_0);

                                if (one || two)
                                {
                                    CellsForShiftUnitsEs.CellsForShift<IdxsC>(CellUnitEntities.Else(idx_0).OwnerC.Player, idx_0).Add(idx_1);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}