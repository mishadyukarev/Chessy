using Leopotam.Ecs;

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

            ref var build_0 = ref EntityPool.Build<BuildC>(idx_0);
            ref var ownBuild_0 = ref EntityPool.Build<OwnerC>(idx_0);

            ref var curStepUnitC = ref _statUnitF.Get1(idx_0);
            ref var env_0 = ref _envF.Get1(idx_0);
            ref var envRes_0 = ref _envF.Get2(idx_0);


            var whoseMove = WhoseMoveC.WhoseMove;

            if (build == BuildTypes.Mine)
            {
                if (curStepUnitC.HaveMin)
                {
                    if (!build_0.Have || build_0.Is(BuildTypes.Camp))
                    {
                        if (env_0.Have(EnvTypes.Hill) && envRes_0.Have(EnvTypes.Hill))
                        {
                            if (InvResC.CanCreateBuild(whoseMove, build, out var needRes))
                            {
                                RpcSys.SoundToGeneral(sender, ClipTypes.Building);

                                InvResC.BuyBuild(whoseMove, build);

                                
                                build_0.SetNew(build, whoseMove);

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