using Leopotam.Ecs;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public sealed class BuildFarmMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            BuildDoingMC.Get(out var forBuildType);
            IdxDoingMC.Get(out var idx_0);

            ref var build_0 = ref Build<BuildC>(idx_0);
            ref var ownBuild_0 = ref Build<OwnerC>(idx_0);

            ref var step_0 = ref Unit<StepC>(idx_0);

            ref var env_0 = ref Environment<EnvC>(idx_0);
            ref var envRes_0 = ref Environment<EnvResC>(idx_0);


            var whoseMove = WhoseMoveC.WhoseMove;

            if (forBuildType == BuildTypes.Farm)
            {
                if (step_0.HaveMinSteps)
                {
                    if (!build_0.Have || build_0.Is(BuildTypes.Camp))
                    {
                        if (!env_0.Have(EnvTypes.AdultForest))
                        {
                            if (InvResC.CanCreateBuild(whoseMove, forBuildType, out var needRes))
                            {
                                RpcSys.SoundToGeneral(sender, ClipTypes.Building);

                                build_0.Remove();

                                env_0.Remove(EnvTypes.YoungForest);

                                if (env_0.Have(EnvTypes.Fertilizer))
                                {
                                    envRes_0.AddMax(EnvTypes.Fertilizer);
                                }
                                else
                                {
                                    env_0.SetNew(EnvTypes.Fertilizer);
                                }

                                InvResC.BuyBuild(whoseMove, forBuildType);


                                ownBuild_0.SetOwner(whoseMove);
                                build_0.SetNew(forBuildType, ownBuild_0.Owner);

                                step_0.TakeSteps();
                            }
                            else
                            {
                                RpcSys.MistakeEconomyToGeneral(sender, needRes);
                            }
                        }
                        else
                        {
                            RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                        }
                    }
                    else
                    {
                        RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                    }
                }
                else
                {
                    RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
        }
    }
}
