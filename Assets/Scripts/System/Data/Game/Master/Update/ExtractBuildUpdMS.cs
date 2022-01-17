using static Game.Game.CellEs;
using static Game.Game.CellBuildE;
using static Game.Game.CellEnvironmentEs;

namespace Game.Game
{
    struct ExtractBuildUpdMS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in Idxs)
            {
                ref var buildCell_0 = ref Build<BuildCellEC>(idx_0);
                ref var build_0 = ref Build<BuildingTC>(idx_0);
                ref var ownBuild_0 = ref Build<PlayerTC>(idx_0);

                if (buildCell_0.CanExtract(out var extract, out var env, out var res))
                {
                    Environment<AmountC>(env, idx_0).Amount -= extract;
                    InventorResourcesE.Resource<AmountC>(res, ownBuild_0.Player).Amount += extract;

                    if (!Environment<AmountC>(env, idx_0).Have)
                    {
                        CellBuildE.Remove(idx_0);

                        if (env != EnvTypes.Hill) Remove(env, idx_0);

                        if (env == EnvTypes.AdultForest)
                        {
                            if (UnityEngine.Random.Range(0, 100) < 50)
                            {
                                SetNew(EnvTypes.YoungForest, idx_0);
                            }
                        }
                    }
                }
            }
        }
    }
}