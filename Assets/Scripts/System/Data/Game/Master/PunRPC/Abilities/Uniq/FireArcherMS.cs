using Photon.Pun;
using static Game.Game.CellFireEs;
using static Game.Game.CellUnitEs;

namespace Game.Game
{
    struct FireArcherMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);


            EntityMPool.FireArcher<IdxFromToC>().Get(out var idx_from, out var idx_to);
            UniqueAbilityMC.Get(out var uniq_cur);

            ref var fire_to = ref Fire<HaveEffectC>(idx_to);

            var whoseMove = WhoseMoveE.WhoseMove<PlayerTC>().Player;

            if (CellUnitStepEs.HaveMaxSteps(idx_from))
            {
                if (CellsForArsonArcherEs.Idxs<IdxsC>(idx_from).Contains(idx_to))
                {
                    EntityPool.Rpc<RpcC>().SoundToGeneral(RpcTarget.All, UniqueAbilityTypes.FireArcher);

                    CellUnitStepEs.Take(idx_from, uniq_cur);
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
