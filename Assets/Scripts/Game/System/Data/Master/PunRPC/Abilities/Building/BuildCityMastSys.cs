using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class BuildCityMastSys : IEcsRunSystem
    {
        private EcsFilter<XyC> _xyF = default;
        private EcsFilter<CellC> _cellF = default;
        private EcsFilter<BuildC, OwnerC> _buildF = default;
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
                ref var build_0 = ref _buildF.Get1(idx_0);
                ref var ownBuild_0 = ref _buildF.Get2(idx_0);

                ref var curStepUnitC = ref _statUnitF.Get1(idx_0);
                ref var curCellEnvCom = ref _envF.Get1(idx_0);
                ref var curFireCom = ref _fireF.Get1(idx_0);


                var whoseMove = WhoseMoveC.WhoseMove;


                if (curStepUnitC.HaveMinSteps)
                {
                    bool haveNearBorder = false;

                    foreach (var xy in CellSpace.GetXyAround(_xyF.Get1(idx_0).Xy))
                    {
                        var curIdx = _xyF.GetIdxCell(xy);

                        if (!_cellF.Get1(curIdx).IsActiveCell)
                        {
                            haveNearBorder = true;
                            break;
                        }
                    }

                    if (!haveNearBorder)
                    {
                        RpcSys.SoundToGeneral(sender, ClipTypes.Building);
                        RpcSys.SoundToGeneral(sender, ClipTypes.AfterBuildTown);

                        if (build_0.Have)
                        {
                            build_0.Remove(ownBuild_0.Owner);
                        }

                        
                        ownBuild_0.SetOwner(whoseMove);
                        build_0.SetNew(forBuildType, ownBuild_0.Owner);


                        curStepUnitC.DefSteps();


                        curFireCom.Disable();

                        if (curCellEnvCom.Have(EnvTypes.AdultForest))
                        {
                            curCellEnvCom.Remove(EnvTypes.AdultForest);
                        }
                        if (curCellEnvCom.Have(EnvTypes.Fertilizer))
                        {
                            curCellEnvCom.Remove(EnvTypes.Fertilizer);
                        }
                        if (curCellEnvCom.Have(EnvTypes.YoungForest))
                        {
                            curCellEnvCom.Remove(EnvTypes.YoungForest);
                        }
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
