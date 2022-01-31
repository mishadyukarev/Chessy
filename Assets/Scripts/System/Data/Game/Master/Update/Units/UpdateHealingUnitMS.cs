namespace Game.Game
{
    sealed class UpdateHealingUnitMS : SystemAbstract, IEcsRunSystem
    {
        public UpdateHealingUnitMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            foreach (var idx_0 in CellEs.Idxs)
            {
                if (UnitEs.Main(idx_0).ConditionTC.Is(ConditionUnitTypes.Relaxed))
                {
                    UnitEs.StatEs.Hp(idx_0).Health.Amount = CellUnitHpValues.MAX_HP;
                }
            }
        }
    }
}