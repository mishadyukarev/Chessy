using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class BuildCityMastSys : IEcsRunSystem
    {
        private EcsFilter<EnvC> _envF = default;
        private EcsFilter<FireC> _fireF = default;

        private EcsFilter<StepC> _statUnitF = default;


        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            BuildDoingMC.Get(out var forBuildType);
            IdxDoingMC.Get(out var idx_0);


            if (forBuildType == BuildTypes.City)
            {
                ref var build_0 = ref EntityPool.Build<BuildC>(idx_0);
                ref var ownBuild_0 = ref EntityPool.Build<OwnerC>(idx_0);

                ref var step_0 = ref _statUnitF.Get1(idx_0);
                ref var env_0 = ref _envF.Get1(idx_0);
                ref var fire_0 = ref _fireF.Get1(idx_0);


                var whoseMove = WhoseMoveC.WhoseMove;


                if (step_0.HaveMin)
                {
                    bool haveNearBorder = false;

                    foreach (var xy in CellSpaceC.XyAround(EntityPool.Cell<XyC>(idx_0).Xy))
                    {
                        var curIdx = EntityPool.IdxCell(xy);

                        if (!EntityPool.Cell<CellC>(curIdx).IsActiveCell)
                        {
                            haveNearBorder = true;
                            break;
                        }
                    }

                    if (!haveNearBorder)
                    {
                        RpcSys.SoundToGeneral(sender, ClipTypes.Building);
                        RpcSys.SoundToGeneral(sender, ClipTypes.AfterBuildTown);


                        build_0.SetNew(forBuildType, whoseMove);


                        step_0.DefSteps();


                        fire_0.Disable();


                        env_0.Remove(EnvTypes.AdultForest);
                        env_0.Remove(EnvTypes.Fertilizer);
                        env_0.Remove(EnvTypes.YoungForest);
                    }

                    else
                    {
                        RpcSys.SimpleMistakeToGeneral(MistakeTypes.NearBorder, sender);
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
