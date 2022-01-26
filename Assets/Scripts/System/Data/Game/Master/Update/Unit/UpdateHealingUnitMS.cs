using static Game.Game.CellEs;

namespace Game.Game
{
    struct UpdateHealingUnitMS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in Idxs)
            {
                if (CellUnitEs.Else(idx_0).ConditionC.Is(ConditionUnitTypes.Relaxed))
                {
                    CellUnitEs.Hp(idx_0).AmountC.Amount = UnitHpValues.MAX_HP;
                }
            }
        }
    }
}