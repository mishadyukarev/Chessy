using Leopotam.Ecs;

namespace Scripts.Game
{
    public sealed class BuildFarmMastSys : IEcsRunSystem
    {
        private EcsFilter<ForBuildingMasCom> _forBuilderFilter = default;

        private EcsFilter<CellBuildDataC, OwnerCom> _cellBuildFilter = default;
        private EcsFilter<CellUnitDataCom, StepComponent> _cellUnitFilter = default;
        private EcsFilter<CellEnvDataC, CellEnvResC> _cellEnvFilter = default;

        private EcsFilter<WhereBuildsC> _buildsFilt = default;

        public void Run()
        {
            ref var forBuildMasCom = ref _forBuilderFilter.Get1(0);
            ref var buildsInGameCom = ref _buildsFilt.Get1(0);

            var sender = InfoC.Sender(MGOTypes.Master);
            var idx_0 = forBuildMasCom.IdxForBuild;
            var forBuildType = forBuildMasCom.BuildingTypeForBuidling;

            ref var build_0 = ref _cellBuildFilter.Get1(idx_0);
            ref var ownBuildC_0 = ref _cellBuildFilter.Get2(idx_0);

            ref var curUnitDatCom = ref _cellUnitFilter.Get1(idx_0);
            ref var curStepUnitC = ref _cellUnitFilter.Get2(idx_0);
            ref var env_0 = ref _cellEnvFilter.Get1(idx_0);
            ref var envRes_0 = ref _cellEnvFilter.Get2(idx_0);


            var playerSend = WhoseMoveC.WhoseMove;

            if (forBuildType == BuildTypes.Farm)
            {
                if (curStepUnitC.HaveMinSteps)
                {
                    if (!build_0.HaveBuild || build_0.Is(BuildTypes.Camp))
                    {
                        if (!env_0.Have(EnvTypes.AdultForest))
                        {
                            if (InventResC.CanCreateBuild(playerSend, forBuildType, out var needRes))
                            {
                                RpcSys.SoundToGeneral(sender, SoundEffectTypes.Building);

                                if (build_0.HaveBuild)
                                {
                                    WhereBuildsC.Remove(ownBuildC_0.Owner, build_0.BuildType, idx_0);
                                    build_0.Reset();
                                }

                                if (env_0.Have(EnvTypes.YoungForest))
                                {
                                    WhereEnvC.Remove(EnvTypes.YoungForest, idx_0);
                                    env_0.Reset(EnvTypes.YoungForest);
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

                                InventResC.BuyBuild(playerSend, forBuildType);

                                build_0.SetBuild(forBuildType);
                                ownBuildC_0.SetOwner(playerSend);
                                WhereBuildsC.Add(ownBuildC_0.Owner, build_0.BuildType, idx_0);

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
