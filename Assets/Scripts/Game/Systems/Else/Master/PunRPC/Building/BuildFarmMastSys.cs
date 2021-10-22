using Leopotam.Ecs;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Game
{
    internal sealed class BuildFarmMastSys : IEcsRunSystem
    {
        private EcsFilter<InfoCom> _infoFilter = default;
        private EcsFilter<ForBuildingMasCom> _forBuilderFilter = default;

        private EcsFilter<XyCellComponent> _xyCellFilt = default;
        private EcsFilter<CellViewComponent> _cellViewFilt = default;
        private EcsFilter<CellBuildDataComponent, OwnerCom> _cellBuildFilter = default;
        private EcsFilter<CellUnitDataCom> _cellUnitFilter = default;
        private EcsFilter<CellEnvironDataCom> _cellEnvFilter = default;
        private EcsFilter<CellFireDataComponent> _cellFireFilter = default;

        private EcsFilter<InventResourCom> _amountResFilt = default;
        private EcsFilter<BuildsInGameCom> _buildsFilt = default;

        public void Run()
        {
            ref var infoMasCom = ref _infoFilter.Get1(0);
            ref var forBuildMasCom = ref _forBuilderFilter.Get1(0);
            ref var buildsInGameCom = ref _buildsFilt.Get1(0);
            ref var invResCom = ref _amountResFilt.Get1(0);

            var sender = infoMasCom.FromInfo.Sender;
            var idxForBuild = forBuildMasCom.IdxForBuild;
            var forBuildType = forBuildMasCom.BuildingTypeForBuidling;

            ref var curBuildDatCom = ref _cellBuildFilter.Get1(idxForBuild);
            ref var curOwnBuildCom = ref _cellBuildFilter.Get2(idxForBuild);

            ref var curUnitDatCom = ref _cellUnitFilter.Get1(idxForBuild);
            ref var curCellEnvCom = ref _cellEnvFilter.Get1(idxForBuild);
            ref var curFireCom = ref _cellFireFilter.Get1(idxForBuild);


            PlayerTypes playerTypeSender = default;
            if (PhotonNetwork.OfflineMode) playerTypeSender = WhoseMoveCom.WhoseMoveOffline;
            else playerTypeSender = sender.GetPlayerType();


            if (forBuildType == BuildingTypes.Farm)
            {
                if (curBuildDatCom.HaveBuild)
                {
                    RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                }

                else
                {
                    if (curUnitDatCom.HaveMinAmountSteps)
                    {
                        if (!curCellEnvCom.Have(EnvirTypes.AdultForest) && !curCellEnvCom.Have(EnvirTypes.YoungForest))
                        {
                            if (invResCom.CanCreateBuild(playerTypeSender, forBuildType, out var needRes))
                            {
                                RpcSys.SoundToGeneral(sender, SoundEffectTypes.Building);

                                if (curCellEnvCom.Have(EnvirTypes.Fertilizer))
                                {
                                    curCellEnvCom.AddAmountRes(EnvirTypes.Fertilizer, curCellEnvCom.MaxAmountRes(EnvirTypes.Fertilizer));
                                }
                                else
                                {
                                    curCellEnvCom.SetNewEnvir(EnvirTypes.Fertilizer);
                                }

                                invResCom.BuyBuild(playerTypeSender, forBuildType);

                                curBuildDatCom.BuildType = forBuildType;
                                curOwnBuildCom.PlayerType = playerTypeSender;

                                curUnitDatCom.TakeAmountSteps();

                            }
                            else
                            {
                                RpcSys.MistakeEconomyToGeneral(sender, needRes);
                            }
                        }
                        else
                        {
                            RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
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
