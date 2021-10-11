using Leopotam.Ecs;
using Photon.Pun;
using System;

namespace Scripts.Game
{
    internal sealed class BuildMineMastSys : IEcsRunSystem
    {
        private EcsFilter<InfoCom> _infoFilter = default;
        private EcsFilter<ForBuildingMasCom> _forBuilderFilter = default;

        private EcsFilter<CellBuildDataComponent, OwnerCom> _cellBuildFilter = default;
        private EcsFilter<CellUnitDataCom> _cellUnitFilter = default;
        private EcsFilter<CellEnvironDataCom> _cellEnvFilter = default;

        private EcsFilter<InventResourCom> _amountResFilt = default;
        private EcsFilter<BuildsInGameCom> _buildsFilt = default;

        public void Run()
        {
            ref var infoMasCom = ref _infoFilter.Get1(0);
            ref var invResCom = ref _amountResFilt.Get1(0);
            ref var forBuildMasCom = ref _forBuilderFilter.Get1(0);
            ref var buildsInGameCom = ref _buildsFilt.Get1(0);

            var sender = infoMasCom.FromInfo.Sender;
            var idxForBuild = forBuildMasCom.IdxForBuild;
            var forBuildType = forBuildMasCom.BuildingTypeForBuidling;

            ref var curBuildDatCom = ref _cellBuildFilter.Get1(idxForBuild);
            ref var curOwnBuildCom = ref _cellBuildFilter.Get2(idxForBuild);

            ref var curUnitDatCom = ref _cellUnitFilter.Get1(idxForBuild);
            ref var curCellEnvCom = ref _cellEnvFilter.Get1(idxForBuild);


            PlayerTypes playerTypeSender = default;
            if (PhotonNetwork.OfflineMode) playerTypeSender = WhoseMoveCom.WhoseMoveOffline;
            else playerTypeSender = sender.GetPlayerType();

            if (forBuildType == BuildingTypes.Mine)
            {
                if (curBuildDatCom.HaveBuild)
                {
                    RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                }

                else
                {
                    if (curUnitDatCom.HaveMinAmountSteps)
                    {
                        if (curCellEnvCom.HaveEnvir(EnvirTypes.Hill) && curCellEnvCom.HaveResources(EnvirTypes.Hill))
                        {
                            if (invResCom.CanCreateBuild(playerTypeSender, forBuildType, out bool[] haves))
                            {
                                RpcSys.SoundToGeneral(sender, SoundEffectTypes.Building);

                                invResCom.BuyBuild(playerTypeSender, forBuildType);

                                curBuildDatCom.BuildType = forBuildType;
                                curOwnBuildCom.PlayerType = playerTypeSender;

                                curUnitDatCom.TakeAmountSteps();

                            }
                            else
                            {
                                RpcSys.MistakeEconomyToGeneral(sender, haves);
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