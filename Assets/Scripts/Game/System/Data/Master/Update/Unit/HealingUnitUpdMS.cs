using Leopotam.Ecs;
using static Game.Game.EntityCellPool;

namespace Game.Game
{
    public sealed class HealingUnitUpdMS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in Idxs)
            {
                if (Unit<ConditionC>(idx_0).Is(CondUnitTypes.Relaxed))
                {
                    Unit<HpUnitWC>(idx_0).SetMax();
                }
            }
        }
    }
}