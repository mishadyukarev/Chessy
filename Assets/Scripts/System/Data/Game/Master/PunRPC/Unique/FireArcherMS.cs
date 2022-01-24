﻿using Photon.Pun;
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
            var uniq_cur = EntityMPool.UniqueAbilityC.Ability;

            ref var fire_to = ref Fire<HaveEffectC>(idx_to);

            var whoseMove = WhoseMoveE.WhoseMove.Player;

            if (EntitiesPool.UnitStep.Steps(idx_from).Amount >= 2)
            {
                if (CellsForArsonArcherEs.Idxs<IdxsC>(idx_from).Contains(idx_to))
                {
                    EntityPool.Rpc.SoundToGeneral(RpcTarget.All, UniqueAbilityTypes.FireArcher);

                    EntitiesPool.UnitStep.Take(idx_from, uniq_cur);
                    fire_to.Enable();
                }
            }

            else
            {
                EntityPool.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}
