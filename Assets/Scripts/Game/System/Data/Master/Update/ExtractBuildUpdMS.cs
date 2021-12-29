using Leopotam.Ecs;
using static Game.Game.EntityCellPool;

namespace Game.Game
{
    public sealed class ExtractBuildUpdMS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in Idxs)
            {
                ref var buildCell_0 = ref Build<BuildCellC>(idx_0);
                ref var build_0 = ref Build<BuildC>(idx_0);
                ref var ownBuild_0 = ref Build<OwnerC>(idx_0);

                ref var envCell_0 = ref Environment<EnvCellC>(idx_0);
                ref var envRes_0 = ref Environment<EnvResC>(idx_0);


                if (buildCell_0.CanExtract(out var extract, out var env, out var res))
                {
                    envRes_0.Take(env, extract);
                    InvResC.Add(res, ownBuild_0.Owner, extract);

                    if (!envRes_0.Have(env))
                    {
                        buildCell_0.Remove();

                        if (env != EnvTypes.Hill) envCell_0.Remove(env);          

                        if (env == EnvTypes.AdultForest)
                        {
                            if (UnityEngine.Random.Range(0, 100) < 50)
                            {
                                envCell_0.SetNew(EnvTypes.YoungForest);
                            }
                        }
                    }
                }
            }
        }
    }
}