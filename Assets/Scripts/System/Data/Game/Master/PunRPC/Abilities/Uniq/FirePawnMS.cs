using Photon.Pun;
using System;
using static Game.Game.EntityCellPool;

namespace Game.Game
{
    public sealed class FirePawnMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            IdxDoingMC.Get(out var idx_0);
            UniqueAbilityMC.Get(out var uniq_cur);


            ref var stepUnit_0 = ref Unit<UnitCellEC>(idx_0);
            ref var fire_0 = ref Fire<HaveEffectC>(idx_0);
            ref var env_0 = ref Environment<EnvironmentC>(idx_0);


            if (stepUnit_0.Have(uniq_cur))
            {
                if (env_0.Have(EnvTypes.AdultForest))
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