using Chessy.Game.Values.Cell;
using Chessy.Game.Values.Cell.Environment;

namespace Chessy.Game
{
    sealed class IceWallFertilizeAroundUpdateMS : SystemAbstract, IEcsRunSystem
    {
        internal IceWallFertilizeAroundUpdateMS(in Chessy.Game.Entity.Model.EntitiesModelGame ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                if (E.BuildingTC(idx_0).HaveBuilding && E.BuildingTC(idx_0).Is(BuildingTypes.IceWall))
                {
                    var aroundIdxs = E.CellEs(idx_0).IdxsAroundHashSet;
                    aroundIdxs.Add(idx_0);

                    foreach (var idx_0_1 in aroundIdxs)
                    {
                        if (!E.BuildingTC(idx_0_1).Is(BuildingTypes.City) && !E.MountainC(idx_0_1).HaveAnyResources)
                        {
                            if (!E.HillC(idx_0_1).HaveAnyResources)
                            {
                                E.FertilizeC(idx_0_1).Resources = EnvironmentValues.ADDING_FROM_ICE_WALL;
                            }
                        }
                    }
                }
            }
        }
    }
}