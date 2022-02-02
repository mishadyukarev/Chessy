namespace Game.Game
{
    sealed class UpdateHealingUnitMS : SystemAbstract, IEcsRunSystem
    {
        public UpdateHealingUnitMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            foreach (var idx_0 in CellWorker.Idxs)
            {
                if (UnitEs(idx_0).MainE.ConditionTC.Is(ConditionUnitTypes.Relaxed))
                {
                    UnitStatEs(idx_0).Hp.SetMax();
                }
            }
        }
    }
}