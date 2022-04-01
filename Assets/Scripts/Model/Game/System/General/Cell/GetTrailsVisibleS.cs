using Chessy.Game.Entity.Model;
using Chessy.Common.Entity;
using Chessy.Common.Model.System;

namespace Chessy.Game.Model.System
{
    sealed class GetTrailsVisibleS : SystemModelGameAbs
    {
        internal GetTrailsVisibleS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Get(in byte cell_0)
        {
            if (eMG.CellEs(cell_0).IsActiveParentSelf)
            {
                for (var dir_0 = DirectTypes.None + 1; dir_0 < DirectTypes.End; dir_0++)
                {
                    eMG.CellEs(cell_0).Player(PlayerTypes.First).IsVisibleTrail = false;
                    eMG.CellEs(cell_0).Player(PlayerTypes.Second).IsVisibleTrail = false;

                    if (eMG.UnitTC(cell_0).HaveUnit) eMG.CellEs(cell_0).Player(eMG.UnitPlayerTC(cell_0).PlayerT).IsVisibleTrail = true;


                    for (var dir = DirectTypes.None + 1; dir < DirectTypes.End; dir++)
                    {
                        var idx_1 = eMG.CellEs(cell_0).AroundCellsEs.AroundCellE(dir).IdxC.Idx;

                        if (eMG.UnitTC(idx_1).HaveUnit && !eMG.UnitTC(cell_0).IsAnimal)
                        {
                            eMG.CellEs(cell_0).Player(eMG.UnitPlayerTC(idx_1).PlayerT).IsVisibleTrail = true;
                        }
                    }
                }
            }
        }
    }
}