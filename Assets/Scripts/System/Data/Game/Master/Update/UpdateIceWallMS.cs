namespace Game.Game
{
    sealed class UpdateIceWallMS : SystemAbstract, IEcsRunSystem
    {
        public UpdateIceWallMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (BuildEs(idx_0).BuildingE.HaveBuilding && BuildEs(idx_0).BuildingE.BuildTC.Is(BuildingTypes.IceWall))
                {
                    BuildEs(idx_0).BuildingE.Defrost(BuildEs(idx_0), Es.WhereBuildingEs);
                }
            }
        }
    }
}