using Leopotam.Ecs;
using Photon.Pun;
using Scripts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Game
{
    internal sealed class BuildFarmMastSys : IEcsRunSystem
    {
        private EcsFilter<ForBuildingMasCom> _forBuilderFilter = default;

        private EcsFilter<CellBuildDataCom, OwnerCom> _cellBuildFilter = default;
        private EcsFilter<CellUnitDataCom> _cellUnitFilter = default;
        private EcsFilter<CellEnvironmentDataC> _cellEnvFilter = default;

        private EcsFilter<BuildsInGameC> _buildsFilt = default;

        public void Run()
        {
            ref var forBuildMasCom = ref _forBuilderFilter.Get1(0);
            ref var buildsInGameCom = ref _buildsFilt.Get1(0);

            var sender = InfoC.Sender(MasGenOthTypes.Master);
            var idxForBuild = forBuildMasCom.IdxForBuild;
            var forBuildType = forBuildMasCom.BuildingTypeForBuidling;

            ref var curBuildDatCom = ref _cellBuildFilter.Get1(idxForBuild);
            ref var curOwnBuildCom = ref _cellBuildFilter.Get2(idxForBuild);

            ref var curUnitDatCom = ref _cellUnitFilter.Get1(idxForBuild);
            ref var curCellEnvCom = ref _cellEnvFilter.Get1(idxForBuild);


            var playerSend = WhoseMoveC.WhoseMove;

            if (forBuildType == BuildingTypes.Farm)
            {
                if (curUnitDatCom.HaveMinAmountSteps)
                {
                    if (!curCellEnvCom.Have(EnvirTypes.AdultForest) && !curCellEnvCom.Have(EnvirTypes.YoungForest))
                    {
                        if (InventResourcesC.CanCreateBuild(playerSend, forBuildType, out var needRes))
                        {
                            RpcSys.SoundToGeneral(sender, SoundEffectTypes.Building);

                            if (curCellEnvCom.Have(EnvirTypes.Fertilizer))
                            {
                                curCellEnvCom.AddMaxAmountRes(EnvirTypes.Fertilizer);
                            }
                            else
                            {
                                curCellEnvCom.SetNew(EnvirTypes.Fertilizer);
                                WhereEnvironmentC.Add(EnvirTypes.Fertilizer, idxForBuild);
                            }

                            InventResourcesC.BuyBuild(playerSend, forBuildType);

                            curBuildDatCom.BuildType = forBuildType;
                            curOwnBuildCom.PlayerType = playerSend;

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
