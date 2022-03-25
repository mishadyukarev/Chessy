using Chessy.Game.Values.Cell;
using Chessy.Game.Values.Cell.Environment;

namespace Chessy.Game
{
    sealed class RiverFertilizeAroundUpdateMS : SystemModelGameAbs, IEcsRunSystem
    {
        internal RiverFertilizeAroundUpdateMS(in Chessy.Game.Entity.Model.EntitiesModelGame ents) : base(ents)
        {

        }

        public void Run()
        {
            for (byte cell_0 = 0; cell_0 < eMGame.LengthCells; cell_0++)
            {
                if (eMGame.RiverEs(cell_0).RiverTC.HaveRiverNear)
                {
                    if (!eMGame.MountainC(cell_0).HaveAnyResources)
                    {
                        eMGame.FertilizeC(cell_0).Resources = EnvironmentValues.MAX_RESOURCES;
                    }
                }
            }
        }
    }
}