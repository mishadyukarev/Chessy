namespace Chessy.Game
{
    internal class WorldMeltIceWallUpdateMS : SystemAbstract, IEcsRunSystem
    {
        internal WorldMeltIceWallUpdateMS(in EntitiesModel ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Start_Values.ALL_CELLS_AMOUNT; idx_0++)
            {
                if (E.BuildingTC(idx_0).Is(BuildingTypes.IceWall))
                {
                    E.BuildHpC(idx_0).Health -= 0.1f;
                }
            }
        }
    }
}