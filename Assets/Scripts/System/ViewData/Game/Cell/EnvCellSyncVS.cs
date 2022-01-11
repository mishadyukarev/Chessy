using static Game.Game.EntityCellPool;
using static Game.Game.EntityCellEnvPool;

namespace Game.Game
{
    struct EnvCellSyncVS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx in Idxs)
            {
                ref var envV_0 = ref EntityCellVPool.EnvCellVC<EnvVC>(idx);

                for (var env_0 = EnvTypes.First; env_0 < EnvTypes.End; env_0++)
                {
                    if (Environment<HaveEnvironmentC>(env_0, idx).Have)
                    {
                        envV_0.EnableSR(env_0);
                    }
                    else
                    {
                        envV_0.DisableSR(env_0);
                    }
                }
            }
        }
    }
}
