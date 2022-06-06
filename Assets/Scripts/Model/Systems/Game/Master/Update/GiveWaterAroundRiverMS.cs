using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;
using Chessy.Game.Values;

namespace Chessy.Game
{
    sealed class GiveWaterAroundRiverMS : SystemModel
    {
        internal GiveWaterAroundRiverMS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
        }

        internal void GiveWater()
        {
            for (byte cellIdx0 = 0; cellIdx0 < StartValues.CELLS; cellIdx0++)
            {
                if (eMG.RiverTC(cellIdx0).HaveRiverNear)
                {
                    if (!eMG.MountainC(cellIdx0).HaveAnyResources)
                    {
                        eMG.FertilizeC(cellIdx0).Resources = EnvironmentValues.MAX_RESOURCES;
                    }
                }
            }
        }
    }
}