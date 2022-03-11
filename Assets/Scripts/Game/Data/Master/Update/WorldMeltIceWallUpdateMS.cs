using Chessy.Game.System.Model;

namespace Chessy.Game
{
    internal class WorldMeltIceWallUpdateMS : SystemAbstract, IEcsRunSystem
    {
        internal WorldMeltIceWallUpdateMS(in EntitiesModel ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
            {
                if (E.BuildingTC(idx_0).Is(BuildingTypes.IceWall))
                {
                    new DestroyBuildingS(0.5f, E.NextPlayer(E.BuildingPlayerTC(idx_0).Player).Player, idx_0, E);
                }
            }
        }
    }
}