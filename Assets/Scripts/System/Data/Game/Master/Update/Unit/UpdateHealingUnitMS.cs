using static Game.Game.CellEs;
using static Game.Game.CellUnitEntities;

namespace Game.Game
{
    struct UpdateHealingUnitMS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in Idxs)
            {
                if (CellUnitEntities.Else(idx_0).ConditionC.Is(ConditionUnitTypes.Relaxed))
                {
                    CellUnitEntities.Hp(idx_0).AmountC.Amount = UnitHpValues.MAX_HP;
                }
            }
        }
    }
}