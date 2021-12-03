using Leopotam.Ecs;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public sealed class HealingUnitUpdMS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in Idxs)
            {
                if (UnitEffects<ConditionC>(idx_0).Is(CondUnitTypes.Relaxed))
                {
                    UnitStat<HpC>(idx_0).SetMax();
                }
            }
        }
    }
}