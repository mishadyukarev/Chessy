using static Game.Game.EntityCellPool;
using static Game.Game.EntityCellUnitPool;

namespace Game.Game
{
    struct HealingUnitUpdMS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in Idxs)
            {
                if (Unit<ConditionC>(idx_0).Is(ConditionUnitTypes.Relaxed))
                {
                    Unit<UnitCellEC>(idx_0).SetMaxHp();
                }
            }
        }
    }
}