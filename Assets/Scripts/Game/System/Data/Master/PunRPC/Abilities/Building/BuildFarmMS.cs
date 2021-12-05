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

            ref var buildCell_0 = ref Build<BuildCellC>(idx_0);
            ref var build_0 = ref Build<BuildC>(idx_0);
            ref var ownBuild_0 = ref Build<OwnerC>(idx_0);

            ref var stepUnit_0 = ref Unit<StepUnitWC>(idx_0);

            ref var env_0 = ref Environment<EnvC>(idx_0);
            ref var envCell_0 = ref Environment<EnvCellC>(idx_0);
            ref var envRes_0 = ref Environment<EnvResC>(idx_0);


            var whoseMove = WhoseMoveC.WhoseMove;



            if (buildType == BuildTypes.Farm)
            {
                if (buildCell_0.CanBuild(buildType, whoseMove, out var mistake, out var needRes))
                {
                    RpcSys.SoundToGeneral(sender, ClipTypes.Building);

                    envCell_0.Remove(EnvTypes.YoungForest);

                    if (env_0.Have(EnvTypes.Fertilizer))
                    {
                        envRes_0.AddMax(EnvTypes.Fertilizer);
                    }
                    else
                    {
                        envCell_0.SetNew(EnvTypes.Fertilizer);
                    }

                    InvResC.BuyBuild(whoseMove, buildType);


                    buildCell_0.SetNew(buildType, whoseMove);

                    stepUnit_0.Take(buildType);
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
