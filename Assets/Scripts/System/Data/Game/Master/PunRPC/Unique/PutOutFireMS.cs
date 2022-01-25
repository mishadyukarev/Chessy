using static Game.Game.CellFireEs;
using static Game.Game.CellUnitEntities;

namespace Game.Game
{
    struct PutOutFireMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            IdxDoingMC.Get(out var idx_0);

            ref var fire_0 = ref Fire<HaveEffectC>(idx_0);


            if (CellUnitEntities.Step(idx_0).AmountC.Have)
            {
                fire_0.Disable();

                CellUnitEntities.Step(idx_0).AmountC.Take();
            }

            else
            {
                EntityPool.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}