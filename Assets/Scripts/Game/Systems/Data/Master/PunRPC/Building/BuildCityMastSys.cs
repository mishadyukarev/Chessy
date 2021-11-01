﻿using Leopotam.Ecs;
using Photon.Pun;

namespace Scripts.Game
{
    public sealed class BuildCityMastSys : IEcsRunSystem
    {
        private EcsFilter<ForBuildingMasCom> _forBuilderFilter = default;

        private EcsFilter<XyCellComponent> _xyCellFilt = default;
        private EcsFilter<CellDataC> _cellDataFilt = default;
        private EcsFilter<CellBuildDataC, OwnerCom> _cellBuildFilter = default;
        private EcsFilter<CellUnitDataCom, StepComponent> _cellUnitFilter = default;
        private EcsFilter<CellEnvDataC> _cellEnvFilter = default;
        private EcsFilter<CellFireDataC> _cellFireFilter = default;


        public void Run()
        {
            ref var forBuildMasCom = ref _forBuilderFilter.Get1(0);
            var forBuildType = forBuildMasCom.BuildingTypeForBuidling;


            if (forBuildType == BuildTypes.City)
            {
                var sender = InfoC.Sender(MasGenOthTypes.Master);
                var idxForBuild = forBuildMasCom.IdxForBuild;

                ref var buildC_0 = ref _cellBuildFilter.Get1(idxForBuild);
                ref var ownBuildC_0 = ref _cellBuildFilter.Get2(idxForBuild);

                ref var curUnitDatCom = ref _cellUnitFilter.Get1(idxForBuild);
                ref var curStepUnitC = ref _cellUnitFilter.Get2(idxForBuild);
                ref var curCellEnvCom = ref _cellEnvFilter.Get1(idxForBuild);
                ref var curFireCom = ref _cellFireFilter.Get1(idxForBuild);


                var playerSend = WhoseMoveC.WhoseMove;



                if (curStepUnitC.HaveMinSteps)
                {
                    bool haveNearBorder = false;

                    foreach (var xy in CellSpaceSupport.TryGetXyAround(_xyCellFilt.Get1(idxForBuild).XyCell))
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
                        RpcSys.SoundToGeneral(sender, SoundEffectTypes.Building);

                        if (buildC_0.HaveBuild)
                        {
                            WhereBuildsC.Remove(ownBuildC_0.Owner, buildC_0.BuildType, idxForBuild);
                            buildC_0.Reset();
                        }

                        buildC_0.SetBuild(forBuildType);
                        ownBuildC_0.SetOwner(playerSend);
                        WhereBuildsC.Add(playerSend, forBuildType, idxForBuild);


                        curStepUnitC.ZeroSteps();


                        curFireCom.DisableFire();

                        if (curCellEnvCom.Have(EnvTypes.AdultForest))
                        {
                            curCellEnvCom.Reset(EnvTypes.AdultForest);
                            WhereEnvC.Remove(EnvTypes.AdultForest, idxForBuild);
                        }
                        if (curCellEnvCom.Have(EnvTypes.Fertilizer))
                        {
                            curCellEnvCom.Reset(EnvTypes.Fertilizer);
                            WhereEnvC.Remove(EnvTypes.Fertilizer, idxForBuild);
                        }
                        if (curCellEnvCom.Have(EnvTypes.YoungForest))
                        {
                            curCellEnvCom.Reset(EnvTypes.YoungForest);
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