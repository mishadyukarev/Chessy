using Chessy.Game.Entity.Model;
using Chessy.Game.Extensions;
using Chessy.Common.Entity;
using Chessy.Common.Model.System;

namespace Chessy.Game.Model.System
{
    sealed class GetBuildingVisibleS : SystemModelGameAbs
    {
        internal GetBuildingVisibleS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Get(in byte cell_0)
        {
            if (eMG.BuildingTC(cell_0).HaveBuilding)
            {
                eMG.BuildEs(cell_0).SetVisible(eMG.BuildingPlayerTC(cell_0).PlayerT, true);

                if (eMG.AdultForestC(cell_0).HaveAnyResources)
                {
                    var isVisibledNextPlayer = false;

                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                    {
                        var idx_1 = eMG.CellEs(cell_0).AroundCellsEs.AroundCellE(dirT).IdxC.Idx;

                        if (eMG.UnitTC(idx_1).HaveUnit)
                        {
                            if (!eMG.UnitPlayerTC(idx_1).Is(eMG.BuildingPlayerTC(cell_0).PlayerT))
                            {
                                isVisibledNextPlayer = true;
                                break;
                            }
                        }
                    }
                    eMG.BuildEs(cell_0).SetVisible(eMG.BuildingPlayerTC(cell_0).PlayerT.NextPlayer(), isVisibledNextPlayer);
                }
                else eMG.BuildEs(cell_0).SetVisible(eMG.BuildingPlayerTC(cell_0).PlayerT.NextPlayer(), true);


                eMG.BuildEs(cell_0).SetVisible(PlayerTypes.First, true);
                eMG.BuildEs(cell_0).SetVisible(PlayerTypes.Second, true);
            }
        }
    }
}