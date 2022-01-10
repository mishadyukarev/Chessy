using Photon.Pun;
using static Game.Game.EntityCellPool;

namespace Game.Game
{
    public sealed class FireArcherMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            FromToDoingMC.Get(out var idx_from, out var idx_to);
            UniqueAbilityMC.Get(out var uniq_cur);


            ref var stepUnit_from = ref Unit<UnitCellEC>(idx_from);

            ref var fire_to = ref Fire<HaveEffectC>(idx_to);

            var whoseMove = WhoseMoveC.WhoseMove;

            if (Unit<UnitCellEC>(idx_from).HaveMaxSteps)
            {
                if (Unit<UnitCellEC>(idx_from).CanArson(whoseMove, idx_to))
                {
                    EntityPool.Rpc<RpcC>().SoundToGeneral(RpcTarget.All, UniqueAbilityTypes.FireArcher);

                    stepUnit_from.Take(uniq_cur);
                    fire_to.Enable();
                }
            }

            else
            {
                EntityPool.Rpc<RpcC>().SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}
