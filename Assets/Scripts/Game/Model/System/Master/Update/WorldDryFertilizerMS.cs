using Chessy.Game.Values.Cell;
using Chessy.Game.Values.Cell.Environment;

namespace Chessy.Game
{
    sealed class WorldDryFertilizerMS : SystemAbstract, IEcsRunSystem
    {
        internal WorldDryFertilizerMS(in Chessy.Game.Entity.Model.EntitiesModelGame ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                if (E.FertilizeC(idx_0).HaveAnyResources)
                {
                    E.FertilizeC(idx_0).Resources -= EnvironmentValues.DRY_FERTILIZE;
                }
            }
        }
    }
}