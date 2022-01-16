using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;

namespace Game.Game
{
    struct HealingUnitUpdMS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in Idxs)
            {
                if (Unit<ConditionUnitC>(idx_0).Is(ConditionUnitTypes.Relaxed))
                {
                    Unit<UnitCellEC>(idx_0).SetMaxHp();
                }
            }
        }
    }
}