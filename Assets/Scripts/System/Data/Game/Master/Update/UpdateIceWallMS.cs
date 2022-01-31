namespace Game.Game
{
    sealed class UpdateIceWallMS : SystemAbstract, IEcsRunSystem
    {
        public UpdateIceWallMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < CellEs.Count; idx_0++)
            {
                if (BuildEs.BuildingE(idx_0).BuildTC.Is(BuildingTypes.IceWall))
                {
                    BuildEs.BuildingE(idx_0).Defrost(BuildEs, Es.WhereBuildingEs);
                }
            }
        }
    }
}