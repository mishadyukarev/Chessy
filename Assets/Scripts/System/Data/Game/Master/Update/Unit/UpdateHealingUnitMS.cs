namespace Game.Game
{
    sealed class UpdateHealingUnitMS : SystemAbstract, IEcsRunSystem
    {
        public UpdateHealingUnitMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            foreach (var idx_0 in Es.CellEs.Idxs)
            {
                if (Es.CellEs.UnitEs.Main(idx_0).ConditionC.Is(ConditionUnitTypes.Relaxed))
                {
                    Es.CellEs.UnitEs.StatEs.Hp(idx_0).Health.Amount = CellUnitHpValues.MAX_HP;
                }
            }
        }
    }
}