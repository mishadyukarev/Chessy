﻿using Photon.Pun;
using System;
using static Game.Game.CellEnvironmentEs;
using static Game.Game.CellFireEs;
using static Game.Game.CellUnitEntities;

namespace Game.Game
{
    struct FirePawnMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            IdxDoingMC.Get(out var idx_0);
            var uniq_cur = EntityMPool.UniqueAbilityC.Ability;


            ref var fire_0 = ref Fire<HaveEffectC>(idx_0);


            if (CellUnitEntities.Step(idx_0).AmountC.Amount >= CellUnitStepValues.NeedSteps(uniq_cur))
            {
                if (Resources(EnvironmentTypes.AdultForest, idx_0).Have)
                {
                    EntityPool.Rpc.SoundToGeneral(RpcTarget.All, UniqueAbilityTypes.FirePawn);

                    fire_0.Enable();
                    CellUnitEntities.Step(idx_0).AmountC.Take(CellUnitStepValues.NeedSteps(uniq_cur));
                }
                else
                {
                    throw new Exception();
                }
            }

            else
            {
                EntityPool.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}