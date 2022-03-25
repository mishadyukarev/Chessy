using Chessy.Game.Values.Cell;
using Chessy.Game.Values.Cell.Environment;

namespace Chessy.Game
{
    sealed class CloudFertilizeUpdMS : SystemModelGameAbs, IEcsRunSystem
    {
        internal CloudFertilizeUpdMS(in Chessy.Game.Entity.Model.EntitiesModelGame ents) : base(ents)
        {
        }

        public void Run()
        {
            var cell_0 = eMGame.WeatherE.CloudC.Center;

            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
            {
                var idx_1 = eMGame.CellEs(cell_0).AroundCellE(dirT).IdxC.Idx;

                if (!eMGame.MountainC(idx_1).HaveAnyResources)
                {
                    eMGame.FertilizeC(idx_1).Resources = EnvironmentValues.MAX_RESOURCES;
                }
            }
        }
    }
}