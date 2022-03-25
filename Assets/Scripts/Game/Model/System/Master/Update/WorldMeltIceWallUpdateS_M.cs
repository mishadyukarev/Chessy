using Chessy.Game.Entity.Model;
using Chessy.Game.System.Model;
using Chessy.Game.Values;

namespace Chessy.Game
{
    public sealed class WorldMeltIceWallUpdateS_M : SystemModelGameAbs, IEcsRunSystem
    {
        public WorldMeltIceWallUpdateS_M(in EntitiesModelGame eMGame) : base(eMGame)
        {
        }

        public void Run()
        {
            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                if (eMGame.BuildingTC(cell_0).Is(BuildingTypes.IceWall))
                {
                    new DestroyBuildingS(0.5f, eMGame.NextPlayer(eMGame.BuildingPlayerTC(cell_0).Player).Player, cell_0, eMGame);
                }
            }
        }
    }
}