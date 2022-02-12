namespace Game.Game
{
    sealed class UpdateHealingUnitMS : SystemAbstract, IEcsRunSystem
    {
        internal UpdateHealingUnitMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (Es.UnitE(idx_0).Is(ConditionUnitTypes.Relaxed))
                {
                    Es.UnitE(idx_0).SetMaxHp();
                }
            }
        }
    }
}