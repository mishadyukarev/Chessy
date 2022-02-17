namespace Game.Game
{
    sealed class UpdExtractWoodcutterMS : SystemAbstract, IEcsRunSystem
    {
        internal UpdExtractWoodcutterMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                //if (Es.AdultForestC(idx_0).CanExtractWoodcutter(Es.BuildEs(idx_0)))
                //{
                //    var extract = Es.AdultForestC(idx_0).AmountExtractBuilding(Es.BuildingUpgradeEs, Es.BuildingE(idx_0));

                //    Es.InventorResourcesEs.Resource(ResourceTypes.Wood, Es.BuildingE(idx_0).Owner).ResourceC.Resources += extract;

                //    Es.AdultForestC(idx_0).Resources -= extract;
                //    if (!Es.AdultForestC(idx_0).HaveAny) Es.TrailEs(idx_0).DestroyAll();

                //    if (!Es.AdultForestC(idx_0).HaveAny)
                //    {
                //        Es.BuildingE(idx_0).Destroy(Es);

                //        if (UnityEngine.Random.Range(0, 100) < 30)
                //        {
                //            //Es.YoungForestC(idx_0).SetRandomResources();
                //        }
                //    }
                //}
            }
        }
    }
}