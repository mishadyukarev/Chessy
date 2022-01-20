using static Game.Game.CellFireEs;
using static Game.Game.CellUnitEs;

namespace Game.Game
{
    struct PutOutFireMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            IdxDoingMC.Get(out var idx_0);

            ref var fire_0 = ref Fire<HaveEffectC>(idx_0);


            if (CellUnitStepEs.HaveMin(idx_0))
            {
                fire_0.Disable();

                CellUnitStepEs.TakeMin(idx_0);
            }

            else
            {
                EntityPool.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}