using Leopotam.Ecs;
using System;

namespace Scripts.Game
{
    internal sealed class SyncCellEnvirsVisSystem : IEcsRunSystem
    {
        private EcsFilter<CellEnvDataC, CellEnvironViewCom> _cellEnvFilter = default;

        public void Run()
        {
            foreach (var idx in _cellEnvFilter)
            {
                ref var cellEnvrDataCom = ref _cellEnvFilter.Get1(idx);
                ref var cellEnvrViewCom = ref _cellEnvFilter.Get2(idx);

                for (EnvirTypes curEnvirType = (EnvirTypes)1; curEnvirType < (EnvirTypes)Enum.GetNames(typeof(EnvirTypes)).Length; curEnvirType++)
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
