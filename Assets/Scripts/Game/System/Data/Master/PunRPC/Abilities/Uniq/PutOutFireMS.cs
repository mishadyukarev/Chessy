using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class PutOutFireMS : IEcsRunSystem
    {
        private EcsFilter<UnitC, StepC> _cellUnitFilter = default;
        private EcsFilter<FireC> _cellFireFilter = default;

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            IdxDoingMC.Get(out var idx_0);

            ref var stepUnit_0 = ref _cellUnitFilter.Get2(idx_0);
            ref var fire_0 = ref _cellFireFilter.Get1(idx_0);

            var whoseMove = WhoseMoveC.WhoseMove;

            if (stepUnit_0.HaveMinSteps)
            {
                fire_0.Disable();

                stepUnit_0.TakeSteps();
            }

            else
            {
                RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}