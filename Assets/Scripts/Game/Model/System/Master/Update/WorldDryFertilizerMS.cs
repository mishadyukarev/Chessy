using Chessy.Game.Values.Cell;
using Chessy.Game.Values.Cell.Environment;

namespace Chessy.Game
{
    sealed class WorldDryFertilizerMS : SystemModelGameAbs, IEcsRunSystem
    {
        internal WorldDryFertilizerMS(in Chessy.Game.Entity.Model.EntitiesModelGame ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte cell_0 = 0; cell_0 < eMGame.LengthCells; cell_0++)
            {
                if (eMGame.FertilizeC(cell_0).HaveAnyResources)
                {
                    eMGame.FertilizeC(cell_0).Resources -= EnvironmentValues.DRY_FERTILIZE;
                }
            }
        }
    }
}