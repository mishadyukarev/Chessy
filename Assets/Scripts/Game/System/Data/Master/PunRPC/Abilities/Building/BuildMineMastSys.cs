﻿using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class BuildMineMastSys : IEcsRunSystem
    {
        private EcsFilter<StepC> _statUnitF = default;
        private EcsFilter<EnvC, EnvResC> _envF = default;

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            BuildDoingMC.Get(out var build);
            IdxDoingMC.Get(out var idx_0);

            ref var build_0 = ref EntityPool.BuildCellC<BuildC>(idx_0);
            ref var ownBuild_0 = ref EntityPool.BuildCellC<OwnerC>(idx_0);

            ref var curStepUnitC = ref _statUnitF.Get1(idx_0);
            ref var env_0 = ref _envF.Get1(idx_0);
            ref var envRes_0 = ref _envF.Get2(idx_0);


            var whoseMove = WhoseMoveC.WhoseMove;

            if (build == BuildTypes.Mine)
            {
                if (curStepUnitC.HaveMinSteps)
                {
                    if (!build_0.Have || build_0.Is(BuildTypes.Camp))
                    {
                        if (env_0.Have(EnvTypes.Hill) && envRes_0.HaveRes(EnvTypes.Hill))
                        {
                            if (InvResC.CanCreateBuild(whoseMove, build, out var needRes))
                            {
                                if (build_0.Have)
                                {
                                    build_0.Remove(ownBuild_0.Owner);
                                }

                                RpcSys.SoundToGeneral(sender, ClipTypes.Building);

                                InvResC.BuyBuild(whoseMove, build);

                                
                                ownBuild_0.SetOwner(whoseMove);
                                build_0.SetNew(build, ownBuild_0.Owner);

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