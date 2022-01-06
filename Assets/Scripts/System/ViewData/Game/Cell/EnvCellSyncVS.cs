using Leopotam.Ecs;
using System;

namespace Game.Game
{
    public sealed class EnvCellSyncVS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx in EntityCellPool.Idxs)
            {
                ref var envD_0 = ref EntityCellPool.Environment<EnvironmentC>(idx);
                ref var envV_0 = ref EntityCellVPool.EnvCellVC<EnvVC>(idx);

                for (var env_0 = EnvTypes.First; env_0 < EnvTypes.End; env_0++)
                {
                    if (envD_0.Have(env_0))
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
