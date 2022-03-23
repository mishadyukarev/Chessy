using Chessy.Game.System.Model;
using Chessy.Game.Values;

namespace Chessy.Game
{
    public struct WorldMeltIceWallUpdateMS
    {
        public void Run(in Chessy.Game.Entity.Model.EntitiesModelGame e)
        {
            for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
            {
                if (e.BuildingTC(idx_0).Is(BuildingTypes.IceWall))
                {
                    new DestroyBuildingS(0.5f, e.NextPlayer(e.BuildingPlayerTC(idx_0).Player).Player, idx_0, e);
                }
            }
        }
    }
}