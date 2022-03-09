namespace Chessy.Game
{
    sealed class UnitGetEffectsS : SystemAbstract, IEcsRunSystem
    {
        internal UnitGetEffectsS(in EntitiesModel ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < StartValues.ALL_CELLS_AMOUNT; idx_0++)
            {
                E.UnitEffectsE(idx_0).HaveKingEffect = false;
            }

            for (byte idx_0 = 0; idx_0 < StartValues.ALL_CELLS_AMOUNT; idx_0++)
            {
                if (E.UnitTC(idx_0).Is(UnitTypes.King))
                {
                    foreach (var idx_1 in E.CellEs(idx_0).IdxsAround)
                    {
                        if (E.UnitTC(idx_1).HaveUnit)
                        {
                            if (E.UnitPlayerTC(idx_1).Is(E.UnitPlayerTC(idx_0).Player))
                            {
                                E.UnitEffectsE(idx_1).HaveKingEffect = true;
                            }
                        }
                    }
                }
            }
        }
    }
}