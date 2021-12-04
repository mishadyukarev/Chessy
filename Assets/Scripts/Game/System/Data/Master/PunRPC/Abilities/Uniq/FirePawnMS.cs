using Leopotam.Ecs;
using Photon.Pun;
using System;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public sealed class FirePawnMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            IdxDoingMC.Get(out var idx_0);
            UniqueAbilityMC.Get(out var uniq_cur);


            ref var stepUnit_0 = ref Unit<StepUnitC>(idx_0);
            ref var fire_0 = ref Fire<FireC>(idx_0);
            ref var env_0 = ref Environment<EnvC>(idx_0);


            var whoseMove = WhoseMoveC.WhoseMove;


            if (stepUnit_0.Have(uniq_cur))
            {
                if (env_0.Have(EnvTypes.AdultForest))
                {
                    RpcSys.SoundToGeneral(RpcTarget.All, UniqueAbilTypes.FirePawn);

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
                RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}