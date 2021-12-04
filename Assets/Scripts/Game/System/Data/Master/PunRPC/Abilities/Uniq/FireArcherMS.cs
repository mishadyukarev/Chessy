using Leopotam.Ecs;
using Photon.Pun;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public sealed class FireArcherMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            FromToDoingMC.Get(out var idx_from, out var idx_to);
            UniqueAbilityMC.Get(out var uniq_cur);


            ref var stepUnit_from = ref Unit<StepUnitC>(idx_from);

            ref var fire_to = ref Fire<FireC>(idx_to);


            var whoseMove = WhoseMoveC.WhoseMove;


            if (Unit<StepUnitC>(idx_from).HaveMaxSteps)
            {
                if (ArsonCellsC.ContainIdx(whoseMove, idx_from, idx_to))
                {
                    RpcSys.SoundToGeneral(RpcTarget.All, UniqueAbilTypes.FireArcher);

                    stepUnit_from.Take(uniq_cur);
                    fire_to.Enable();
                }
            }

            else
            {
                RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}
