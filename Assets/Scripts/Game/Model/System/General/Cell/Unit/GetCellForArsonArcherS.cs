using Chessy.Game.Entity.Model;

namespace Chessy.Game.System.Model
{
    sealed class GetCellForArsonArcherS : SystemModelGameAbs
    {
        internal GetCellForArsonArcherS(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        internal void Get(in byte cell_0)
        {
            e.UnitEs(cell_0).ForArson.Clear();

            if (!e.UnitEffectStunC(cell_0).IsStunned)
            {
                if (e.UnitTC(cell_0).Is(UnitTypes.Pawn) && e.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.BowCrossbow))
                {
                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                    {
                        var idx_1 = e.CellEs(cell_0).AroundCellsEs.AroundCellE(dirT).IdxC.Idx;

                        if (!e.EffectEs(idx_1).HaveFire)
                        {
                            if (e.AdultForestC(idx_1).HaveAnyResources)
                            {
                                e.UnitEs(cell_0).ForArson.Add(idx_1);
                            }
                        }
                    }
                }
            }
        }
    }
}