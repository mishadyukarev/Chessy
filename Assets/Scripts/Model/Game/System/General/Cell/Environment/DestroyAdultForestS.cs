using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values;

namespace Chessy.Game.Model.System
{
    sealed class DestroyAdultForestS : SystemModel
    {
        internal DestroyAdultForestS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG)
        {
        }

        internal void Destroy(in byte cell)
        {
            eMG.AdultForestC(cell).Resources = 0;
            if (UnityEngine.Random.Range(0f, 1f) < EnvironmentValues.PERCENT_SPAWN_FOR_YOUNG_FOREST_AFTER_FIRE)
                eMG.YoungForestC(cell).Resources = EnvironmentValues.MAX_RESOURCES;
            sMG.DestroyAllTrailS.Destroy(cell);
        }
    }
}