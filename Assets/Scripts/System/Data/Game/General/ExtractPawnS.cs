namespace Game.Game
{
    sealed class ExtractPawnS : SystemAbstract, IEcsRunSystem
    {
        internal ExtractPawnS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < StartValues.ALL_CELLS_AMOUNT; idx_0++)
            {
                Es.UnitEs(idx_0).ExtractPawnE.CanExtract = false;

                if (Es.AdultForestC(idx_0).HaveAny)
                {
                    if (Es.UnitTC(idx_0).Is(UnitTypes.Pawn) && Es.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Relaxed))
                    {
                        Es.UnitExtractPawnE(idx_0).CanExtract = true;
                        Es.UnitExtractPawnE(idx_0).ResourcesC.Resources = 0.1f;
                    }
                }
            }
        }
    }
}