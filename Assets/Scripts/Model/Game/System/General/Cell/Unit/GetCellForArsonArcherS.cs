using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Entity.Model;

namespace Chessy.Game.Model.System
{
    sealed class GetCellForArsonArcherS : SystemModelGameAbs
    {
        internal GetCellForArsonArcherS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Get(in byte cell_0)
        {
            eMG.UnitEs(cell_0).ForArson.Clear();

            if (!eMG.UnitEffectStunC(cell_0).IsStunned)
            {
                if (eMG.UnitTC(cell_0).Is(UnitTypes.Pawn) && eMG.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.BowCrossbow))
                {
                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                    {
                        var idx_1 = eMG.CellEs(cell_0).AroundCellsEs.AroundCellE(dirT).IdxC.Idx;

                        if (!eMG.EffectEs(idx_1).HaveFire)
                        {
                            if (eMG.AdultForestC(idx_1).HaveAnyResources)
                            {
                                eMG.UnitEs(cell_0).ForArson.Add(idx_1);
                            }
                        }
                    }
                }
            }
        }
    }
}