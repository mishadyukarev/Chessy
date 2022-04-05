using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    sealed class GetCellForArsonArcherS : SystemModelGameAbs
    {
        internal GetCellForArsonArcherS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Get(in byte cell_0)
        {
            eMG.UnitForArsonC(cell_0).Clear();

            if (!eMG.StunUnitC(cell_0).IsStunned)
            {
                if (eMG.UnitTC(cell_0).Is(UnitTypes.Pawn) && eMG.MainToolWeaponTC(cell_0).Is(ToolWeaponTypes.BowCrossbow))
                {
                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                    {
                        var idx_1 = eMG.AroundCellsE(cell_0).IdxCell(dirT);

                        if (!eMG.HaveFire(idx_1))
                        {
                            if (eMG.AdultForestC(idx_1).HaveAnyResources)
                            {
                                eMG.UnitForArsonC(cell_0).Add(idx_1);
                            }
                        }
                    }
                }
            }
        }
    }
}