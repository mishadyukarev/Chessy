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
                ref var cellEnvrDataCom = ref _envF.Get1(idx);
                ref var cellEnvrViewCom = ref _envViewF.Get1(idx);

                for (EnvTypes curEnvirType = (EnvTypes)1; curEnvirType < (EnvTypes)Enum.GetNames(typeof(EnvTypes)).Length; curEnvirType++)
                {
                    if (cellEnvrDataCom.Have(curEnvirType))
                    {
                        cellEnvrViewCom.EnableSR(curEnvirType);
                    }
                    else
                    {
                        cellEnvrViewCom.DisableSR(curEnvirType);
                    }
                }
            }
        }
    }
}
