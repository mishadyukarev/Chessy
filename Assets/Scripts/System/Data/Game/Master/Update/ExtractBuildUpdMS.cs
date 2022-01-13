using static Game.Game.EntityCellPool;
using static Game.Game.EntityCellBuildPool;
using static Game.Game.EntityCellEnvPool;

namespace Game.Game
{
    struct ExtractBuildUpdMS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in Idxs)
            {
                ref var buildCell_0 = ref Build<BuildCellEC>(idx_0);
                ref var build_0 = ref Build<BuildingC>(idx_0);
                ref var ownBuild_0 = ref Build<PlayerC>(idx_0);

                if (buildCell_0.CanExtract(out var extract, out var env, out var res))
                {
                    Environment<AmountResourcesC>(env, idx_0).Resources -= extract;
                    EntInventorResources.Resource<AmountC>(res, ownBuild_0.Player).Amount += extract;

                    if (!Environment<AmountResourcesC>(env, idx_0).Have)
                    {
                        buildCell_0.Remove();

                        if (env != EnvTypes.Hill) Environment<EnvCellEC>(env, idx_0).Remove();

                        if (env == EnvTypes.AdultForest)
                        {
                            if (UnityEngine.Random.Range(0, 100) < 50)
                            {
                                Environment<EnvCellEC>(EnvTypes.YoungForest, idx_0).SetNew();
                            }
                        }
                    }
                }
            }
        }
    }
}