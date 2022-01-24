using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;

namespace Game.Game
{
    struct UpdateHealingUnitMS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in Idxs)
            {
                if (EntitiesPool.UnitElse.Condition(idx_0).Is(ConditionUnitTypes.Relaxed))
                {
                    EntitiesPool.UnitHps[idx_0].Hp.Amount = UnitHpValues.MAX_HP;
                }
            }
        }
    }
}