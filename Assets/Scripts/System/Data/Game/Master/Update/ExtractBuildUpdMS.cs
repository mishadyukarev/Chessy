using static Game.Game.CellBuildEs;
using static Game.Game.CellEnvironmentEs;
using static Game.Game.CellEs;

namespace Game.Game
{
    struct ExtractBuildUpdMS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in Entities.CellEs.Idxs)
            {
                ref var build_0 = ref Entities.CellEs.BuildEs.Build(idx_0).BuildTC;
                ref var ownBuild_0 = ref Entities.CellEs.BuildEs.Build(idx_0).PlayerTC;

                if (Entities.CellEs.BuildEs.CanExtract(idx_0, out var extract, out var env, out var res))
                {
                    Entities.CellEs.EnvironmentEs.Environment(env, idx_0).Resources.Amount -= extract;
                    InventorResourcesE.Resource(res, ownBuild_0.Player).Amount += extract;

                    if (!Entities.CellEs.EnvironmentEs.Environment(env, idx_0).Resources.Have)
                    {
                        Entities.WhereBuildingEs.HaveBuild(Entities.CellEs.BuildEs.Build(idx_0), idx_0).HaveBuilding.Have = false;
                        Entities.CellEs.BuildEs.Build(idx_0).Remove();


                        if (env != EnvironmentTypes.Hill) Entities.CellEs.EnvironmentEs.Environment(env, idx_0).Remove();

                        if (env == EnvironmentTypes.AdultForest)
                        {
                            if (UnityEngine.Random.Range(0, 100) < 50)
                            {
                                Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.YoungForest, idx_0).SetNew();
                            }
                        }
                    }
                }
            }
        }
    }
}