namespace Game.Game
{
    sealed class UpdExtractMineMS : SystemAbstract, IEcsRunSystem
    {
        internal UpdExtractMineMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (BuildEs(idx_0).BuildingE.CanExtractHill(EnvironmentEs(idx_0)))
                {
                    EnvironmentEs(idx_0).Hill.ExtractMine(CellEs(idx_0), Es.BuildingUpgradeEs, Es.InventorResourcesEs);

                    if (!EnvironmentEs(idx_0).Hill.HaveEnvironment)
                    {
                        BuildEs(idx_0).BuildingE.Destroy(BuildEs(idx_0), Es.WhereBuildingEs);
                    }
                }
            }
        }
    }
}