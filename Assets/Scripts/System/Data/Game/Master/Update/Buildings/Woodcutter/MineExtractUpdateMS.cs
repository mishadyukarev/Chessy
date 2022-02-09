namespace Game.Game
{
    sealed class MineExtractUpdateMS : SystemAbstract, IEcsRunSystem
    {
        internal MineExtractUpdateMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            //for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            //{
            //    if (Es.BuildE(idx_0).Is(BuildingTypes.Mine))
            //    {
            //        if (Es.EnvHillE(idx_0).CanExtractMine(BuildEs(idx_0)))
            //        {
            //            Es.EnvHillE(idx_0).ExtractMine(CellEs(idx_0), Es.BuildingUpgradeEs, Es.InventorResourcesEs);

            //            if (!Es.EnvHillE(idx_0).HaveEnvironment)
            //            {
            //                Es.BuildE(idx_0).Destroy(Es);
            //            }
            //        }
            //        else
            //        {
            //            Es.BuildE(idx_0).Destroy(Es);
            //        }
            //    }
            //}
        }
    }
}