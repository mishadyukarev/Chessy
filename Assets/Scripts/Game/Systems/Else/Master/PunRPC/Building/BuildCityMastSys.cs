using Leopotam.Ecs;
using Photon.Pun;

namespace Scripts.Game
{
    internal sealed class BuildCityMastSys : IEcsRunSystem
    {
        private EcsFilter<ForBuildingMasCom> _forBuilderFilter = default;

        private EcsFilter<XyCellComponent> _xyCellFilt = default;
        private EcsFilter<CellViewComponent> _cellViewFilt = default;
        private EcsFilter<CellBuildDataCom, OwnerCom> _cellBuildFilter = default;
        private EcsFilter<CellUnitDataCom> _cellUnitFilter = default;
        private EcsFilter<CellEnvironmentDataC> _cellEnvFilter = default;
        private EcsFilter<CellFireDataComponent> _cellFireFilter = default;

        private EcsFilter<BuildsInGameC> _buildsFilt = default;

        public void Run()
        {
            ref var forBuildMasCom = ref _forBuilderFilter.Get1(0);
            var forBuildType = forBuildMasCom.BuildingTypeForBuidling;


            if (forBuildType == BuildingTypes.City)
            {
                ref var buildsInGameCom = ref _buildsFilt.Get1(0);

                var sender = InfoC.Sender(MasGenOthTypes.Master);
                var idxForBuild = forBuildMasCom.IdxForBuild;

                ref var curBuildCom = ref _cellBuildFilter.Get1(idxForBuild);
                ref var curOwnBuildCom = ref _cellBuildFilter.Get2(idxForBuild);

                ref var curUnitDatCom = ref _cellUnitFilter.Get1(idxForBuild);
                ref var curCellEnvCom = ref _cellEnvFilter.Get1(idxForBuild);
                ref var curFireCom = ref _cellFireFilter.Get1(idxForBuild);


                var playerSend = WhoseMoveC.WhoseMove;



                if (curUnitDatCom.HaveMinAmountSteps)
                {
                    bool haveNearBorder = false;

                    foreach (var xy in CellSpaceSupport.TryGetXyAround(_xyCellFilt.GetXyCell(idxForBuild)))
                    {
                        var curIdx = _xyCellFilt.GetIdxCell(xy);

                        if (!_cellViewFilt.Get1(curIdx).IsActiveParent)
                        {
                            haveNearBorder = true;
                            break;
                        }
                    }

                    if (!haveNearBorder)
                    {
                        RpcSys.SoundToGeneral(sender, SoundEffectTypes.Building);

                        curBuildCom.BuildType = forBuildType;
                        curOwnBuildCom.PlayerType = playerSend;

                        BuildsInGameC.Add(playerSend, forBuildType, idxForBuild);

                        curUnitDatCom.DefAmountSteps();

                        curFireCom.DisableFire();

                        if (curCellEnvCom.Have(EnvirTypes.AdultForest))
                        {
                            curCellEnvCom.Reset(EnvirTypes.AdultForest);
                            WhereEnvironmentC.Remove(EnvirTypes.AdultForest, idxForBuild);
                        }
                        if (curCellEnvCom.Have(EnvirTypes.Fertilizer))
                        {
                            curCellEnvCom.Reset(EnvirTypes.Fertilizer);
                            WhereEnvironmentC.Remove(EnvirTypes.Fertilizer, idxForBuild);
                        }
                        if (curCellEnvCom.Have(EnvirTypes.YoungForest))
                        {
                            curCellEnvCom.Reset(EnvirTypes.YoungForest);
                            WhereEnvironmentC.Remove(EnvirTypes.YoungForest, idxForBuild);
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
