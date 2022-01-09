using static Game.Game.EntityCellPool;

namespace Game.Game
{
    sealed class HealingUnitUpdMS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in Idxs)
            {
                if (Unit<ConditionC>(idx_0).Is(CondUnitTypes.Relaxed))
                {
                    Unit<UnitCellEC>(idx_0).SetMaxHp();
                }
            }
        }
    }
}