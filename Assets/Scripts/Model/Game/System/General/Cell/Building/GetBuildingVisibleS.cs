using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using Chessy.Game.Extensions;

namespace Chessy.Game.Model.System
{
    sealed class GetBuildingVisibleS : SystemModel
    {
        internal GetBuildingVisibleS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        internal void Get(in byte cell_0)
        {
            if (eMG.BuildingTC(cell_0).HaveBuilding)
            {
                eMG.BuildingVisibleC(cell_0).Set(eMG.BuildingPlayerTC(cell_0).PlayerT, true);

                if (eMG.AdultForestC(cell_0).HaveAnyResources)
                {
                    var isVisibledNextPlayer = false;

                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                    {
                        var idx_1 = eMG.AroundCellsE(cell_0).IdxCell(dirT);

                        if (eMG.UnitTC(idx_1).HaveUnit)
                        {
                            if (!eMG.UnitPlayerTC(idx_1).Is(eMG.BuildingPlayerTC(cell_0).PlayerT))
                            {
                                isVisibledNextPlayer = true;
                                break;
                            }
                        }
                    }
                    eMG.BuildingVisibleC(cell_0).Set(eMG.BuildingPlayerTC(cell_0).PlayerT.NextPlayer(), isVisibledNextPlayer);
                }
                else eMG.BuildingVisibleC(cell_0).Set(eMG.BuildingPlayerTC(cell_0).PlayerT.NextPlayer(), true);


                eMG.BuildingVisibleC(cell_0).Set(PlayerTypes.First, true);
                eMG.BuildingVisibleC(cell_0).Set(PlayerTypes.Second, true);
            }
        }
    }
}