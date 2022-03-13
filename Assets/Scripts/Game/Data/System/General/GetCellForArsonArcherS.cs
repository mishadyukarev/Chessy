using System;

namespace Chessy.Game.System.Model
{
    public struct GetCellForArsonArcherS
    {
        public GetCellForArsonArcherS(in byte idx_0, in EntitiesModel e)
        {
            e.UnitEs(idx_0).ForArson.Clear();

            if (!e.UnitEffectStunC(idx_0).IsStunned)
            {
                if (e.UnitTC(idx_0).Is(UnitTypes.Pawn) && e.UnitExtraTWTC(idx_0).Is(ToolWeaponTypes.BowCrossbow))
                {
                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                    {
                        var idx_1 = e.CellEs(idx_0).AroundCellE(dirT).IdxC.Idx;

                        if (!e.EffectEs(idx_1).HaveFire)
                        {
                            if (e.AdultForestC(idx_1).HaveAnyResources)
                            {
                                e.UnitEs(idx_0).ForArson.Add(idx_1);
                            }
                        }
                    }
                }
            }
        }
    }
}