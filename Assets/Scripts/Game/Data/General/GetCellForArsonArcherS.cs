namespace Chessy.Game.System.Model
{
    sealed class GetCellForArsonArcherS : CellSystem
    {
        internal GetCellForArsonArcherS(in byte idx, in EntitiesModel ents) : base(idx, ents)
        {

        }

        public void Run()
        {
            E.UnitEs(Idx).ForArson.Clear();

            if (!E.UnitEffectStunC(Idx).IsStunned)
            {
                if (E.UnitTC(Idx).HaveUnit && E.UnitExtraTWTC(Idx).Is(ToolWeaponTypes.BowCrossbow))
                {
                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                    {
                        var idx_1 = E.CellEs(Idx).AroundCellE(dirT).IdxC.Idx;

                        if (!E.EffectEs(idx_1).HaveFire)
                        {
                            if (E.AdultForestC(idx_1).HaveAnyResources)
                            {
                                E.UnitEs(Idx).ForArson.Add(idx_1);
                            }
                        }
                    }
                }
            }
        }
    }
}