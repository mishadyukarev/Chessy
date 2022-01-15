using static Game.Game.CellE;
using static Game.Game.CellUnitE;

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