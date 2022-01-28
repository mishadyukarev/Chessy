using static Game.Game.CellEs;

namespace Game.Game
{
    struct UpdateHealingUnitMS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in Entities.CellEs.Idxs)
            {
                if (Entities.CellEs.UnitEs.Else(idx_0).ConditionC.Is(ConditionUnitTypes.Relaxed))
                {
                    Entities.CellEs.UnitEs.Hp(idx_0).AmountC.Amount = UnitHpValues.MAX_HP;
                }
            }
        }
    }
}