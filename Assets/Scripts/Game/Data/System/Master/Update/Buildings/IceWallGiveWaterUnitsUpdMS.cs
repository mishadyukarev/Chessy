using Chessy.Game.Values.Cell.Unit.Stats;

namespace Chessy.Game
{
    sealed class IceWallGiveWaterUnitsUpdMS : SystemAbstract, IEcsRunSystem
    {
        internal IceWallGiveWaterUnitsUpdMS(in EntitiesModel ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                if (E.BuildingTC(idx_0).HaveBuilding && E.BuildingTC(idx_0).Is(BuildingTypes.IceWall))
                {
                    var idxs_01 = E.CellEs(idx_0).IdxsAroundHashSet;
                    idxs_01.Add(idx_0);

                    foreach (var idx_01 in E.CellEs(idx_0).IdxsAround)
                    {
                        if (E.UnitTC(idx_01).HaveUnit && E.UnitPlayerTC(idx_01).Is(E.BuildingPlayerTC(idx_0).Player))
                        {
                            E.UnitWaterC(idx_01).Water = WaterValues.MAX;
                        }
                    }
                }
            }
        }
    }
}