using Leopotam.Ecs;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public sealed class BuildFarmMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            BuildDoingMC.Get(out var buildType);
            IdxDoingMC.Get(out var idx_0);

            ref var build_0 = ref Build<BuildC>(idx_0);
            ref var ownBuild_0 = ref Build<OwnerC>(idx_0);

            ref var step_0 = ref Unit<StepC>(idx_0);

            ref var env_0 = ref Environment<EnvC>(idx_0);
            ref var envRes_0 = ref Environment<EnvResC>(idx_0);


            var whoseMove = WhoseMoveC.WhoseMove;



            if (buildType == BuildTypes.Farm)
            {
                if (build_0.CanBuild(buildType, whoseMove, out var mistake, out var needRes))
                {
                    RpcSys.SoundToGeneral(sender, ClipTypes.Building);

                    env_0.Remove(EnvTypes.YoungForest);

                    if (env_0.Have(EnvTypes.Fertilizer))
                    {
                        envRes_0.AddMax(EnvTypes.Fertilizer);
                    }
                    else
                    {
                        env_0.SetNew(EnvTypes.Fertilizer);
                    }

                    InvResC.BuyBuild(whoseMove, buildType);


                    build_0.SetNew(buildType, whoseMove);

                    step_0.TakeSteps();
                }
                               
                else
                {
                    if(mistake == MistakeTypes.Economy) RpcSys.MistakeEconomyToGeneral(sender, needRes);
                    else RpcSys.SimpleMistakeToGeneral(mistake, sender);
                }
            }
        }
    }
}
