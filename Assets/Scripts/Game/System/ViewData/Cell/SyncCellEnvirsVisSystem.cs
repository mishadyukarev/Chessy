using Leopotam.Ecs;
using System;

namespace Chessy.Game
{
    public sealed class SyncCellEnvirsVisSystem : IEcsRunSystem
    {
        private EcsFilter<EnvC> _envF = default;
        private EcsFilter<EnvVC> _envViewF = default;

        public void Run()
        {
            foreach (var idx in _envViewF)
            {
                ref var envD_0 = ref _envF.Get1(idx);
                ref var envV_0 = ref _envViewF.Get1(idx);

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
