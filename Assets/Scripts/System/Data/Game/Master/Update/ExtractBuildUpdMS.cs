using static Game.Game.CellEs;
using static Game.Game.CellBuildEs;
using static Game.Game.CellEnvironmentEs;

namespace Game.Game
{
    struct ExtractBuildUpdMS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in Idxs)
            {
                ref var build_0 = ref CellBuildEs.Build(idx_0).BuildTC;
                ref var ownBuild_0 = ref CellBuildEs.Build(idx_0).PlayerTC;

                if (CellBuildEs.CanExtract(idx_0, out var extract, out var env, out var res))
                {
                    Environment(env, idx_0).Resources.Amount -= extract;
                    InventorResourcesE.Resource(res, ownBuild_0.Player).Amount += extract;

                    if (!Environment(env, idx_0).Resources.Have)
                    {
                        CellBuildEs.Remove(idx_0);

                        if (env != EnvironmentTypes.Hill) Remove(env, idx_0);

                        if (env == EnvironmentTypes.AdultForest)
                        {
                            if (UnityEngine.Random.Range(0, 100) < 50)
                            {
                                SetNew(EnvironmentTypes.YoungForest, idx_0);
                            }
                        }
                    }
                }
            }
        }
    }
}