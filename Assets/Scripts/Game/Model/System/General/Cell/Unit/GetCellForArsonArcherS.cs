using Chessy.Game.Entity.Model;

namespace Chessy.Game.System.Model
{
    sealed class GetCellForArsonArcherS : SystemModelGameAbs
    {
        internal GetCellForArsonArcherS(in EntitiesModelGame eMGame) : base(eMGame) { }

        internal void Get(in byte cell_0)
        {
            eMGame.UnitEs(cell_0).ForArson.Clear();

            if (!eMGame.UnitEffectStunC(cell_0).IsStunned)
            {
                if (eMGame.UnitTC(cell_0).Is(UnitTypes.Pawn) && eMGame.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.BowCrossbow))
                {
                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                    {
                        var idx_1 = eMGame.CellEs(cell_0).AroundCellsEs.AroundCellE(dirT).IdxC.Idx;

                        if (!eMGame.EffectEs(idx_1).HaveFire)
                        {
                            if (eMGame.AdultForestC(idx_1).HaveAnyResources)
                            {
                                eMGame.UnitEs(cell_0).ForArson.Add(idx_1);
                            }
                        }
                    }
                }
            }
        }
    }
}