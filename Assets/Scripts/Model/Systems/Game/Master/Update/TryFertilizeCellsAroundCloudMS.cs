using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;
using Chessy.Game.Values;

namespace Chessy.Game
{
    sealed class TryFertilizeCellsAroundCloudMS : SystemModel
    {
        internal TryFertilizeCellsAroundCloudMS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
        }

        internal void TryFertilize()
        {
            var cellIdx0 = eMG.WeatherE.CloudC.Center;

            for (var dirT = DirectTypes.None; dirT < DirectTypes.End; dirT++)
            {
                var idx_1 = eMG.AroundCellsE(cellIdx0).IdxCell(dirT);

                if (!eMG.MountainC(idx_1).HaveAnyResources)
                {
                    eMG.FertilizeC(idx_1).Resources = EnvironmentValues.MAX_RESOURCES;
                }
            }
        }
    }
}