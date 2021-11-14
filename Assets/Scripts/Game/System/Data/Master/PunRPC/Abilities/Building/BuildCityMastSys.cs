using Leopotam.Ecs;
using Photon.Pun;
using Chessy.Common;

namespace Chessy.Game
{
    public sealed class BuildCityMastSys : IEcsRunSystem
    {
        private EcsFilter<ForBuildingMasCom> _forBuilderFilter = default;

        private EcsFilter<XyC> _xyCellFilt = default;
        private EcsFilter<CellC> _cellDataFilt = default;
        private EcsFilter<BuildC, OwnerC> _cellBuildFilter = default;
        private EcsFilter<EnvC> _cellEnvFilter = default;
        private EcsFilter<FireC> _cellFireFilter = default;

        private EcsFilter<StepC> _statUnitF = default;


        public void Run()
        {
            ref var forBuildMasCom = ref _forBuilderFilter.Get1(0);
            var forBuildType = forBuildMasCom.BuildingTypeForBuidling;


            if (forBuildType == BuildTypes.City)
            {
                var sender = InfoC.Sender(MGOTypes.Master);
                var idxForBuild = forBuildMasCom.IdxForBuild;

                ref var build_0 = ref _cellBuildFilter.Get1(idxForBuild);
                ref var ownBuild_0 = ref _cellBuildFilter.Get2(idxForBuild);

                ref var curStepUnitC = ref _statUnitF.Get1(idxForBuild);
                ref var curCellEnvCom = ref _cellEnvFilter.Get1(idxForBuild);
                ref var curFireCom = ref _cellFireFilter.Get1(idxForBuild);


                var whoseMove = WhoseMoveC.WhoseMove;


                if (curStepUnitC.HaveMinSteps)
                {
                    bool haveNearBorder = false;

                    foreach (var xy in CellSpace.GetXyAround(_xyCellFilt.Get1(idxForBuild).Xy))
                    {
                        var curIdx = _xyCellFilt.GetIdxCell(xy);

                        if (!_cellDataFilt.Get1(curIdx).IsActiveCell)
                        {
                            haveNearBorder = true;
                            break;
                        }
                    }

                    if (!haveNearBorder)
                    {
                        RpcSys.SoundToGeneral(sender, ClipGameTypes.Building);
                        RpcSys.SoundToGeneral(sender, ClipGameTypes.AfterBuildTown);

                        if (build_0.Have)
                        {
                            WhereBuildsC.Remove(ownBuild_0.Owner, build_0.Build, idxForBuild);
                            build_0.Remove();
                        }

                        build_0.SetNew(forBuildType);
                        ownBuild_0.SetOwner(whoseMove);
                        WhereBuildsC.Add(whoseMove, forBuildType, idxForBuild);


                        curStepUnitC.DefSteps();


                        curFireCom.Disable();

                        if (curCellEnvCom.Have(EnvTypes.AdultForest))
                        {
                            curCellEnvCom.Remove(EnvTypes.AdultForest);
                            WhereEnvC.Remove(EnvTypes.AdultForest, idxForBuild);
                        }
                        if (curCellEnvCom.Have(EnvTypes.Fertilizer))
                        {
                            curCellEnvCom.Remove(EnvTypes.Fertilizer);
                            WhereEnvC.Remove(EnvTypes.Fertilizer, idxForBuild);
                        }
                        if (curCellEnvCom.Have(EnvTypes.YoungForest))
                        {
                            curCellEnvCom.Remove(EnvTypes.YoungForest);
                            WhereEnvC.Remove(EnvTypes.YoungForest, idxForBuild);
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
