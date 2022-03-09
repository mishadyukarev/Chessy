namespace Chessy.Game
{
    internal class WorldMeltIceWallUpdateMS : SystemAbstract, IEcsRunSystem
    {
        internal WorldMeltIceWallUpdateMS(in EntitiesModel ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < StartValues.ALL_CELLS_AMOUNT; idx_0++)
            {
                if (E.BuildingTC(idx_0).Is(BuildingTypes.IceWall))
                {
                    E.BuildEs(idx_0).MainE.AttackBuildingC.Damage += 0.5f;
                }
            }
        }
    }
}