namespace Chessy.Game.System.Model
{
    sealed class UnitGetEffectsS : CellSystem, IEcsRunSystem
    {
        internal UnitGetEffectsS(in byte idx, in EntitiesModel eM) : base(idx, eM)
        {
        }

        public void Run()
        {
            if (E.UnitTC(Idx).Is(UnitTypes.King))
            {
                foreach (var idx_1 in E.CellEs(Idx).IdxsAround)
                {
                    if (E.UnitTC(idx_1).HaveUnit)
                    {
                        if (E.UnitPlayerTC(idx_1).Is(E.UnitPlayerTC(Idx).Player))
                        {
                            E.PlayerInfoE(E.UnitPlayerTC(idx_1).Player).WhereKingEffects.Add(idx_1);
                        }
                    }
                }
            }
        }
    }
}