﻿using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModel
    {
        internal void GetCellForArsonArcher(in byte cell_0)
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