namespace Chessy.Game.System.Model
{
    sealed class UnitGetEffectsS : SystemAbstract, IEcsRunSystem
    {
        internal UnitGetEffectsS(in EntitiesModel eM) : base(eM) { }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
            {
                if (E.UnitTC(idx_0).Is(UnitTypes.King))
                {
                    foreach (var idx_1 in E.CellEs(idx_0).IdxsAround)
                    {
                        if (E.UnitTC(idx_1).HaveUnit)
                        {
                            if (E.UnitPlayerTC(idx_1).Is(E.UnitPlayerTC(idx_0).Player))
                            {
                                E.PlayerInfoE(E.UnitPlayerTC(idx_1).Player).WhereKingEffects.Add(idx_1);
                            }
                        }
                    }
                }
            }
        }
    }
}