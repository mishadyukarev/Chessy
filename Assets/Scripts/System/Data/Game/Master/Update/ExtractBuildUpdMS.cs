namespace Game.Game
{
    sealed class ExtractBuildUpdMS : SystemCellAbstract, IEcsRunSystem
    {
        internal ExtractBuildUpdMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            foreach (var idx_0 in CellEs.Idxs)
            {
                ref var build_0 = ref Es.CellEs.BuildEs.Build(idx_0).BuildTC;
                ref var ownBuild_0 = ref Es.CellEs.BuildEs.Build(idx_0).PlayerTC;

                if (CellEs.BuildEs.CanExtract(idx_0, out var extract, out var env, out var res))
                {
                    //CellEs.EnvironmentEs.Environment(env, idx_0).Resources.Amount -= extract;
                    //Es.InventorResourcesEs.Resource(res, ownBuild_0.Player).Resources.Amount += extract;

                    //if (!CellEs.EnvironmentEs.Environment(env, idx_0).HaveEnvironment)
                    //{
                    //    Es.WhereBuildingEs.HaveBuild(Es.CellEs.BuildEs.Build(idx_0), idx_0).HaveBuilding.Have = false;
                    //    CellEs.BuildEs.Build(idx_0).Remove();


                    //    if (env != EnvironmentTypes.Hill) Es.CellEs.EnvironmentEs.Environment(env, idx_0).Destroy(TrailEs.Trails(idx_0));

                    //    if (env == EnvironmentTypes.AdultForest)
                    //    {
                    //        if (UnityEngine.Random.Range(0, 100) < 50)
                    //        {
                    //            CellEs.EnvironmentEs.YoungForest( idx_0).SetNew();
                    //        }
                    //    }
                    //}
                }
            }
        }
    }
}