﻿using Leopotam.Ecs;
using Chessy.Common;

namespace Chessy.Game
{
    public sealed class BuildMineMastSys : IEcsRunSystem
    {
        private EcsFilter<ForBuildingMasCom> _forBuilderFilter = default;

        private EcsFilter<BuildC, OwnerC> _cellBuildFilter = default;
        private EcsFilter<StepC> _statUnitF = default;
        private EcsFilter<EnvC, EnvResC> _cellEnvFilter = default;

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

            ref var curStepUnitC = ref _statUnitF.Get1(idx_0);
            ref var env_0 = ref _cellEnvFilter.Get1(idx_0);
            ref var envRes_0 = ref _cellEnvFilter.Get2(idx_0);


            var whoseMove = WhoseMoveC.WhoseMove;

            if (forBuildType == BuildTypes.Mine)
            {
                if (curStepUnitC.HaveMinSteps)
                {
                    if (!build_0.Have || build_0.Is(BuildTypes.Camp))
                    {
                        if (env_0.Have(EnvTypes.Hill) && envRes_0.HaveRes(EnvTypes.Hill))
                        {
                            if (InvResC.CanCreateBuild(whoseMove, forBuildType, out var needRes))
                            {
                                if (build_0.Have)
                                {
                                    WhereBuildsC.Remove(ownBuildC_0.Owner, build_0.Build, idx_0);
                                    build_0.Remove();
                                }

                                RpcSys.SoundToGeneral(sender, ClipTypes.Building);

                                InvResC.BuyBuild(whoseMove, forBuildType);

                                build_0.SetNew(forBuildType);
                                ownBuildC_0.SetOwner(whoseMove);
                                WhereBuildsC.Add(ownBuildC_0.Owner, build_0.Build, idx_0);

                                curStepUnitC.TakeSteps();
                            }

                            else RpcSys.MistakeEconomyToGeneral(sender, needRes);
                        }

                        else RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                    }
                    else RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                }
                else
                {
                    RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
        }
    }
}