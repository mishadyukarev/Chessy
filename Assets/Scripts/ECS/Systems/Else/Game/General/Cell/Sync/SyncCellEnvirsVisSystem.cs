using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.View.Else.Game.General.Cell;
using Leopotam.Ecs;
using System;

namespace Assets.Scripts.ECS.Game.General.Systems.SyncCellVision
{
    internal sealed class SyncCellEnvirsVisSystem : IEcsRunSystem
    {
        private EcsFilter<CellEnvironDataCom, CellEnvironViewCom> _cellEnvFilter = default;

        public void Run()
        {
            foreach (var idx in _cellEnvFilter)
            {
                ref var cellEnvrDataCom = ref _cellEnvFilter.Get1(idx);
                ref var cellEnvrViewCom = ref _cellEnvFilter.Get2(idx);

                for (EnvironmentTypes curEnvirType = (EnvironmentTypes)1; curEnvirType < (EnvironmentTypes)Enum.GetNames(typeof(EnvironmentTypes)).Length; curEnvirType++)
                {
                    if (cellEnvrDataCom.HaveEnvironment(curEnvirType))
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
