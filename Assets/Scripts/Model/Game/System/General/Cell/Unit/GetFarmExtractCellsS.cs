﻿using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Environment;
using Chessy.Common.Entity;
using Chessy.Common.Model.System;

namespace Chessy.Game.Model.System
{
    sealed class GetFarmExtractCellsS : SystemModelGameAbs
    {
        internal GetFarmExtractCellsS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Get(in byte cell_0)
        {
            eMG.FarmExtractFertilizeE(cell_0).Resources = 0;

            if (eMG.BuildingTC(cell_0).Is(BuildingTypes.Farm))
            {
                if (eMG.FertilizeC(cell_0).HaveAnyResources)
                {
                    var extract = EnvironmentValues.FARM_EXTRACT;

                    //if (E.BuildingsInfo(E.BuildingMainE(Idx)).HaveCenterUpgrade)
                    //{
                    //    extract += Environment_Values.FARM_CENTER_UPGRADE;
                    //}

                    if (eMG.FertilizeC(cell_0).Resources < extract) extract = eMG.FertilizeC(cell_0).Resources;

                    eMG.FarmExtractFertilizeE(cell_0).Resources = extract;
                }
            }
        }
    }
}