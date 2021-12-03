using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class PutOutFireMS : IEcsRunSystem
    {
        private EcsFilter<StepC> _statUnitF = default;
        private EcsFilter<FireC> _cellFireFilter = default;

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            IdxDoingMC.Get(out var idx_0);

            ref var stepUnit_0 = ref _statUnitF.Get1(idx_0);
            ref var fire_0 = ref _cellFireFilter.Get1(idx_0);

            var whoseMove = WhoseMoveC.WhoseMove;

            if (stepUnit_0.HaveMin)
            {
                fire_0.Disable();

                stepUnit_0.Take();
            }

            else
            {
                RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}