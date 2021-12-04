using Leopotam.Ecs;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public sealed class PutOutFireMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            IdxDoingMC.Get(out var idx_0);

            ref var stepUnit_0 = ref Unit<StepUnitC>(idx_0);
            ref var fire_0 = ref Fire<FireC>(idx_0);

            var whoseMove = WhoseMoveC.WhoseMove;

            if (stepUnit_0.HaveMin)
            {
                fire_0.Disable();

                stepUnit_0.TakeMin();
            }

            else
            {
                RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}