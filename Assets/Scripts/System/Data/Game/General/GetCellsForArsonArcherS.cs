namespace Game.Game
{
    sealed class GetCellsForArsonArcherS : SystemAbstract, IEcsRunSystem
    {
        public GetCellsForArsonArcherS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                Es.UnitEs(idx_0).ForArson.Clear();

                if (!Es.UnitStunC(idx_0).IsStunned)
                {
                    if (Es.UnitTC(idx_0).HaveUnit && Es.UnitEs(idx_0).ExtraToolWeaponTC.Is(ToolWeaponTypes.BowCrossbow))
                    {
                        foreach (var idx_1 in CellWorker.GetIdxsAround(idx_0))
                        {
                            if (!Es.EffectEs(idx_1).HaveFire)
                            {
                                if (Es.AdultForestC(idx_1).HaveAny)
                                {
                                    Es.UnitEs(idx_0).ForArson.Add(idx_1);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}