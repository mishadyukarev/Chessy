﻿using Leopotam.Ecs;

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
                ref var build_0 = ref EntityDataPool.GetBuildCellC<BuildC>(idx_0);
                ref var ownBuild_0 = ref EntityDataPool.GetBuildCellC<OwnerC>(idx_0);

                ref var curStepUnitC = ref _statUnitF.Get1(idx_0);
                ref var curCellEnvCom = ref _envF.Get1(idx_0);
                ref var curFireCom = ref _fireF.Get1(idx_0);


                var whoseMove = WhoseMoveC.WhoseMove;


                if (curStepUnitC.HaveMinSteps)
                {
                    bool haveNearBorder = false;

                    foreach (var xy in CellSpace.GetXyAround(EntityDataPool.GetCellC<XyC>(idx_0).Xy))
                    {
                        var curIdx = EntityDataPool.GetIdxCell(xy);

                        if (!EntityDataPool.GetCellC<CellC>(curIdx).IsActiveCell)
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
