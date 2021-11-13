using Chessy.Common;
using Leopotam.Ecs;
using Photon.Pun;
using System;

namespace Chessy.Game
{
    public sealed class FirePawnMS : IEcsRunSystem
    {
        private EcsFilter<UnitC, StepC> _cellUnitFilter = default;
        private EcsFilter<FireC> _cellFireFilter = default;
        private EcsFilter<EnvC> _cellEnvFilter = default;


        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            IdxDoingMC.Get(out var idx_0);

            ref var stepUnit_0 = ref _cellUnitFilter.Get2(idx_0);
            ref var fire_0 = ref _cellFireFilter.Get1(idx_0);
            ref var env_0 = ref _cellEnvFilter.Get1(idx_0);


            var whoseMove = WhoseMoveC.WhoseMove;


            if (stepUnit_0.HaveMinSteps)
            {
                if (env_0.Have(EnvTypes.AdultForest))
                {
                    RpcSys.SoundToGeneral(RpcTarget.All, UniqAbilTypes.FirePawn);

                    fire_0.Enable();
                    stepUnit_0.TakeSteps();
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