﻿using Photon.Pun;
using System;
using static Game.Game.CellEnvironmentEs;
using static Game.Game.CellFireEs;
using static Game.Game.CellUnitEs;

namespace Game.Game
{
    struct FirePawnMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            IdxDoingMC.Get(out var idx_0);
            UniqueAbilityMC.Get(out var uniq_cur);


            ref var stepUnit_0 = ref Unit<UnitCellEC>(idx_0);
            ref var fire_0 = ref Fire<HaveEffectC>(idx_0);


            if (CellUnitStepEs.Have(idx_0, uniq_cur))
            {
                if (Environment<HaveEnvironmentC>(EnvTypes.AdultForest, idx_0).Have)
                {
                    EntityPool.Rpc<RpcC>().SoundToGeneral(RpcTarget.All, UniqueAbilityTypes.FirePawn);

                    fire_0.Enable();
                    stepUnit_0.Take(uniq_cur);
                }
                else
                {
                    throw new Exception();
                }
            }

            else
            {
                EntityPool.Rpc<RpcC>().SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}