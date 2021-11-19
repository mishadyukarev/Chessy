using Leopotam.Ecs;
using Game.Common;

namespace Game.Game
{
    public sealed class BuildFarmMastSys : IEcsRunSystem
    {
        private EcsFilter<BuildC, OwnerC> _cellBuildFilter = default;
        private EcsFilter<StepC> _statUnitF = default;
        private EcsFilter<EnvC, EnvResC> _cellEnvFilter = default;

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            BuildDoingMC.Get(out var forBuildType);
            IdxDoingMC.Get(out var idx_0);

            ref var build_0 = ref _cellBuildFilter.Get1(idx_0);
            ref var ownBuildC_0 = ref _cellBuildFilter.Get2(idx_0);

            ref var curStepUnitC = ref _statUnitF.Get1(idx_0);
            ref var env_0 = ref _cellEnvFilter.Get1(idx_0);
            ref var envRes_0 = ref _cellEnvFilter.Get2(idx_0);


            var whoseMove = WhoseMoveC.WhoseMove;

            if (forBuildType == BuildTypes.Farm)
            {
                if (curStepUnitC.HaveMinSteps)
                {
                    if (!build_0.Have || build_0.Is(BuildTypes.Camp))
                    {
                        if (!env_0.Have(EnvTypes.AdultForest))
                        {
                            if (InvResC.CanCreateBuild(whoseMove, forBuildType, out var needRes))
                            {
                                RpcSys.SoundToGeneral(sender, ClipTypes.Building);

                                if (build_0.Have)
                                {
                                    WhereBuildsC.Remove(ownBuildC_0.Owner, build_0.Build, idx_0);
                                    build_0.Remove();
                                }

                                if (env_0.Have(EnvTypes.YoungForest))
                                {
                                    WhereEnvC.Remove(EnvTypes.YoungForest, idx_0);
                                    env_0.Remove(EnvTypes.YoungForest);
                                }

                                if (env_0.Have(EnvTypes.Fertilizer))
                                {
                                    envRes_0.AddMaxAmountRes(EnvTypes.Fertilizer);
                                }
                                else
                                {
                                    env_0.Set(EnvTypes.Fertilizer);
                                    envRes_0.SetNew(EnvTypes.Fertilizer);
                                    WhereEnvC.Add(EnvTypes.Fertilizer, idx_0);
                                }

                                InvResC.BuyBuild(whoseMove, forBuildType);

                                build_0.SetNew(forBuildType);
                                ownBuildC_0.SetOwner(whoseMove);
                                WhereBuildsC.Add(ownBuildC_0.Owner, build_0.Build, idx_0);

                                curStepUnitC.TakeSteps();
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
