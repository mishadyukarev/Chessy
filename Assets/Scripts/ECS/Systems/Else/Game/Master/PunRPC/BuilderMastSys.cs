using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Game;
using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.ECS.Component.View.Else.Game.General.Cell;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Leopotam.Ecs;
using Photon.Pun;
using System;

internal sealed class BuilderMastSys : IEcsRunSystem
{
    private EcsFilter<InfoMasCom> _infoFilter = default;
    private EcsFilter<ForBuildingMasCom> _forBuilderFilter = default;

    private EcsFilter<XyCellComponent> _xyCellFilter = default;
    private EcsFilter<CellViewComponent> _cellViewFilter = default;
    private EcsFilter<CellBuildDataComponent, OwnerOnlineComp, OwnerOfflineCom> _cellBuildFilter = default;
    private EcsFilter<CellUnitDataCom> _cellUnitFilter = default;
    private EcsFilter<CellEnvironDataCom> _cellEnvFilter = default;
    private EcsFilter<CellFireDataComponent> _cellFireFilter = default;

    private EcsFilter<InventorResourcesComponent> _amountResFilt = default;

    public void Run()
    {
        ref var infoMasCom = ref _infoFilter.Get1(0);
        ref var invResCom = ref _amountResFilt.Get1(0);
        ref var forBuildMasCom = ref _forBuilderFilter.Get1(0);

        var sender = infoMasCom.FromInfo.Sender;
        var idxForBuild = forBuildMasCom.IdxForBuild;
        var forBuildType = forBuildMasCom.BuildingTypeForBuidling;

        ref var curBuildDatCom = ref _cellBuildFilter.Get1(idxForBuild);
        ref var curOnBuildCom = ref _cellBuildFilter.Get2(idxForBuild);
        ref var curOffBuildCom = ref _cellBuildFilter.Get3(idxForBuild);

        ref var curUnitDatCom = ref _cellUnitFilter.Get1(idxForBuild);
        ref var curCellEnvCom = ref _cellEnvFilter.Get1(idxForBuild);
        ref var curFireCom = ref _cellFireFilter.Get1(idxForBuild);



        if (curBuildDatCom.HaveBuild)
        {
            RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
            RpcSys.SoundToGeneral(sender, SoundEffectTypes.Mistake);
        }

        else
        {
            switch (forBuildType)
            {
                case BuildingTypes.None:
                    throw new Exception();


                case BuildingTypes.City:
                    if (curUnitDatCom.HaveMinAmountSteps)
                    {
                        bool haveNearBorder = false;

                        foreach (var xy in CellSpaceSupport.TryGetXyAround(_xyCellFilter.GetXyCell(idxForBuild)))
                        {
                            var curIdx = _xyCellFilter.GetIdxCell(xy);

                            if (!_cellViewFilter.Get1(curIdx).IsActiveParent)
                            {
                                haveNearBorder = true;
                            }
                        }

                        if (!haveNearBorder)
                        {
                            RpcSys.SoundToGeneral(sender, SoundEffectTypes.Building);

                            curBuildDatCom.BuildType = forBuildType;

                            if (PhotonNetwork.OfflineMode)
                            {
                                curOffBuildCom.LocalPlayerType = WhoseMoveCom.PlayerType;
                            }
                            else
                            {
                                curOnBuildCom.Owner = sender;
                            }


                            curUnitDatCom.ResetAmountSteps();

                            curFireCom.DisableFire();

                            if (curCellEnvCom.HaveEnvironment(EnvironmentTypes.AdultForest)) curCellEnvCom.ResetEnvironment(EnvironmentTypes.AdultForest);
                            if (curCellEnvCom.HaveEnvironment(EnvironmentTypes.Fertilizer)) curCellEnvCom.ResetEnvironment(EnvironmentTypes.Fertilizer);
                        }

                        else
                        {
                            RpcSys.SimpleMistakeToGeneral(MistakeTypes.NearBorder, sender);
                            RpcSys.SoundToGeneral(sender, SoundEffectTypes.Mistake);
                        }
                    }
                    else
                    {
                        RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                        RpcSys.SoundToGeneral(sender, SoundEffectTypes.Mistake);
                    }
                    break;


                case BuildingTypes.Farm:
                    if (curUnitDatCom.HaveMinAmountSteps)
                    {
                        if (!curCellEnvCom.HaveEnvironment(EnvironmentTypes.AdultForest) && !curCellEnvCom.HaveEnvironment(EnvironmentTypes.YoungForest))
                        {
                            if (invResCom.CanCreateNewBuilding(forBuildType, sender.IsMasterClient, out bool[] haves))
                            {

                                RpcSys.SoundToGeneral(sender, SoundEffectTypes.Building);

                                if (curCellEnvCom.HaveEnvironment(EnvironmentTypes.Fertilizer))
                                {
                                    curCellEnvCom.AddAmountResources(EnvironmentTypes.Fertilizer, curCellEnvCom.MaxAmountResources(EnvironmentTypes.Fertilizer));
                                }
                                else
                                {
                                    curCellEnvCom.SetNewEnvironment(EnvironmentTypes.Fertilizer);
                                }

                                invResCom.BuyNewBuilding(forBuildType, sender.IsMasterClient);

                                curBuildDatCom.BuildType = forBuildType;
                                curOnBuildCom.Owner = sender;

                                curUnitDatCom.TakeAmountSteps();

                            }
                            else
                            {
                                RpcSys.SoundToGeneral(sender, SoundEffectTypes.Mistake);
                                RpcSys.MistakeEconomyToGeneral(sender, haves);
                            }
                        }
                        else
                        {
                            RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                            RpcSys.SoundToGeneral(sender, SoundEffectTypes.Mistake);
                        }
                    }
                    else
                    {
                        RpcSys.SoundToGeneral(sender, SoundEffectTypes.Mistake);
                        RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                    }
                    break;

                case BuildingTypes.Woodcutter:
                    throw new Exception();


                case BuildingTypes.Mine:
                    if (curCellEnvCom.HaveEnvironment(EnvironmentTypes.Hill) && curCellEnvCom.HaveResources(EnvironmentTypes.Hill))
                    {
                        if (invResCom.CanCreateNewBuilding(forBuildType, sender.IsMasterClient, out bool[] haves))
                        {
                            if (curUnitDatCom.HaveMaxAmountSteps)
                            {
                                RpcSys.SoundToGeneral(sender, SoundEffectTypes.Building);

                                invResCom.BuyNewBuilding(forBuildType, sender.IsMasterClient);

                                curBuildDatCom.BuildType = forBuildType;
                                curOnBuildCom.Owner = sender;

                                curUnitDatCom.ResetAmountSteps();
                            }
                            else
                            {
                                RpcSys.SoundToGeneral(sender, SoundEffectTypes.Mistake);
                                RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            }
                        }
                        else
                        {
                            RpcSys.SoundToGeneral(sender, SoundEffectTypes.Mistake);
                            RpcSys.MistakeEconomyToGeneral(sender, haves);
                        }
                    }
                    else
                    {
                        RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                        RpcSys.SoundToGeneral(sender, SoundEffectTypes.Mistake);
                    }
                    break;


                default:
                    throw new Exception();
            }
        }
    }
}
