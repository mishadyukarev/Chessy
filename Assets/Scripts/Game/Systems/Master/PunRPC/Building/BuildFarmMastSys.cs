﻿using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class BuildFarmMastSys : IEcsRunSystem
    {
        private EcsFilter<ForBuildingMasCom> _forBuilderFilter = default;

        private EcsFilter<CellBuildDataC, OwnerCom> _cellBuildFilter = default;
        private EcsFilter<CellUnitDataCom, StepComponent> _cellUnitFilter = default;
        private EcsFilter<CellEnvDataC> _cellEnvFilter = default;

        private EcsFilter<WhereBuildsC> _buildsFilt = default;

        public void Run()
        {
            ref var forBuildMasCom = ref _forBuilderFilter.Get1(0);
            ref var buildsInGameCom = ref _buildsFilt.Get1(0);

            var sender = InfoC.Sender(MasGenOthTypes.Master);
            var idxForBuild = forBuildMasCom.IdxForBuild;
            var forBuildType = forBuildMasCom.BuildingTypeForBuidling;

            ref var buildC_0 = ref _cellBuildFilter.Get1(idxForBuild);
            ref var ownBuildC_0 = ref _cellBuildFilter.Get2(idxForBuild);

            ref var curUnitDatCom = ref _cellUnitFilter.Get1(idxForBuild);
            ref var curStepUnitC = ref _cellUnitFilter.Get2(idxForBuild);
            ref var curCellEnvCom = ref _cellEnvFilter.Get1(idxForBuild);


            var playerSend = WhoseMoveC.WhoseMove;

            if (forBuildType == BuildTypes.Farm)
            {
                if (curStepUnitC.HaveMinSteps)
                {
                    if (!curCellEnvCom.Have(EnvirTypes.AdultForest) && !curCellEnvCom.Have(EnvirTypes.YoungForest))
                    {
                        if (InventResC.CanCreateBuild(playerSend, forBuildType, out var needRes))
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

                            InventResC.BuyBuild(playerSend, forBuildType);

                            buildC_0.SetBuild(forBuildType);
                            ownBuildC_0.SetOwner(playerSend);
                            WhereBuildsC.Add(ownBuildC_0.Owner, buildC_0.BuildType, idxForBuild);

                            curStepUnitC.TakeSteps();
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
