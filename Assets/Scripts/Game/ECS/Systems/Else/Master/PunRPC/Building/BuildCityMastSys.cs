using Leopotam.Ecs;
using Photon.Pun;

namespace Scripts.Game
{
    internal sealed class BuildCityMastSys : IEcsRunSystem
    {
        private EcsFilter<InfoCom> _infoFilter = default;
        private EcsFilter<ForBuildingMasCom> _forBuilderFilter = default;

        private EcsFilter<XyCellComponent> _xyCellFilt = default;
        private EcsFilter<CellViewComponent> _cellViewFilt = default;
        private EcsFilter<CellBuildDataComponent, OwnerCom> _cellBuildFilter = default;
        private EcsFilter<CellUnitDataCom> _cellUnitFilter = default;
        private EcsFilter<CellEnvironDataCom> _cellEnvFilter = default;
        private EcsFilter<CellFireDataComponent> _cellFireFilter = default;

        private EcsFilter<BuildsInGameCom> _buildsFilt = default;

        public void Run()
        {
            ref var forBuildMasCom = ref _forBuilderFilter.Get1(0);
            var forBuildType = forBuildMasCom.BuildingTypeForBuidling;


            if (forBuildType == BuildingTypes.City)
            {
                ref var infoMasCom = ref _infoFilter.Get1(0);
                ref var buildsInGameCom = ref _buildsFilt.Get1(0);

                var sender = infoMasCom.FromInfo.Sender;
                var idxForBuild = forBuildMasCom.IdxForBuild;

                ref var curBuildDatCom = ref _cellBuildFilter.Get1(idxForBuild);
                ref var curOwnBuildCom = ref _cellBuildFilter.Get2(idxForBuild);

                ref var curUnitDatCom = ref _cellUnitFilter.Get1(idxForBuild);
                ref var curCellEnvCom = ref _cellEnvFilter.Get1(idxForBuild);
                ref var curFireCom = ref _cellFireFilter.Get1(idxForBuild);


                PlayerTypes playerTypeSender = default;
                if (PhotonNetwork.OfflineMode) playerTypeSender = WhoseMoveCom.WhoseMoveOffline;
                else playerTypeSender = sender.GetPlayerType();


                if (curBuildDatCom.HaveBuild)
                {
                    RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                }
                else
                {
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

                            curBuildDatCom.BuildType = forBuildType;
                            curOwnBuildCom.PlayerType = playerTypeSender;

                            buildsInGameCom.Add(playerTypeSender, forBuildType, idxForBuild);

                            curUnitDatCom.ResetAmountSteps();

                            curFireCom.DisableFire();

                            if (curCellEnvCom.HaveEnvir(EnvirTypes.AdultForest)) curCellEnvCom.ResetEnvironment(EnvirTypes.AdultForest);
                            if (curCellEnvCom.HaveEnvir(EnvirTypes.Fertilizer)) curCellEnvCom.ResetEnvironment(EnvirTypes.Fertilizer);
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
}
