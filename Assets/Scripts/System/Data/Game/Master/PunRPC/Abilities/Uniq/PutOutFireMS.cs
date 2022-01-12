using static Game.Game.EntityCellFirePool;
using static Game.Game.EntCellUnit;

namespace Game.Game
{
    struct PutOutFireMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            IdxDoingMC.Get(out var idx_0);

            ref var stepUnit_0 = ref Unit<UnitCellEC>(idx_0);
            ref var fire_0 = ref Fire<HaveEffectC>(idx_0);

            var whoseMove = WhoseMoveC.WhoseMove;

            if (stepUnit_0.HaveMin)
            {
                fire_0.Disable();

                stepUnit_0.TakeMin();
            }

            else
            {
                EntityPool.Rpc<RpcC>().SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}