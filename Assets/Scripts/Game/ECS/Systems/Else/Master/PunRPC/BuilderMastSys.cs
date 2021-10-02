using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Game;
using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.ECS.Component.View.Else.Game.General.Cell;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.Supports;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Leopotam.Ecs;
using System;

internal sealed class BuilderMastSys : IEcsRunSystem
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

    public void Run()
    {
        ref var infoMasCom = ref _infoFilter.Get1(0);
        ref var invResCom = ref _amountResFilt.Get1(0);
        ref var forBuildMasCom = ref _forBuilderFilter.Get1(0);

        var sender = infoMasCom.FromInfo.sender;
        var idxForBuild = forBuildMasCom.IdxForBuild;
        var forBuildType = forBuildMasCom.BuildingTypeForBuidling;

        ref var curBuildDatCom = ref _cellBuildFilter.Get1(idxForBuild);
        ref var curOwnBuildCom = ref _cellBuildFilter.Get2(idxForBuild);

        ref var curUnitDatCom = ref _cellUnitFilter.Get1(idxForBuild);
        ref var curCellEnvCom = ref _cellEnvFilter.Get1(idxForBuild);
        ref var curFireCom = ref _cellFireFilter.Get1(idxForBuild);


        PlayerTypes playerTypeSender = default;
        if (PhotonNetwork.offlineMode) playerTypeSender = WhoseMoveCom.WhoseMoveOffline;
        else playerTypeSender = sender.GetPlayerType();


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

                        foreach (var xy in CellSpaceSupport.TryGetXyAround(_xyCellFilt.GetXyCell(idxForBuild)))
                        {
                            var curIdx = _xyCellFilt.GetIdxCell(xy);

                            if (!_cellViewFilt.Get1(curIdx).IsActiveParent)
                            {
                                haveNearBorder = true;
                            }
                        }

                        if (!haveNearBorder)
                        {
                            RpcSys.SoundToGeneral(sender, SoundEffectTypes.Building);

                            curBuildDatCom.BuildType = forBuildType;
                            curOwnBuildCom.PlayerType = playerTypeSender;


                            curUnitDatCom.ResetAmountSteps();

                            curFireCom.DisableFire();

                            if (curCellEnvCom.HaveEnvir(EnvirTypes.AdultForest)) curCellEnvCom.ResetEnvironment(EnvirTypes.AdultForest);
                            if (curCellEnvCom.HaveEnvir(EnvirTypes.Fertilizer)) curCellEnvCom.ResetEnvironment(EnvirTypes.Fertilizer);
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
                        if (!curCellEnvCom.HaveEnvir(EnvirTypes.AdultForest) && !curCellEnvCom.HaveEnvir(EnvirTypes.YoungForest))
                        {
                            if (invResCom.CanCreateBuild(playerTypeSender, forBuildType, out bool[] haves))
                            {

                                RpcSys.SoundToGeneral(sender, SoundEffectTypes.Building);

                                if (curCellEnvCom.HaveEnvir(EnvirTypes.Fertilizer))
                                {
                                    curCellEnvCom.AddAmountRes(EnvirTypes.Fertilizer, curCellEnvCom.MaxAmountResources(EnvirTypes.Fertilizer));
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
                    if (curCellEnvCom.HaveEnvir(EnvirTypes.Hill) && curCellEnvCom.HaveResources(EnvirTypes.Hill))
                    {
                        if (invResCom.CanCreateBuild(playerTypeSender, forBuildType, out bool[] haves))
                        {
                            if (curUnitDatCom.HaveMaxAmountSteps)
                            {
                                RpcSys.SoundToGeneral(sender, SoundEffectTypes.Building);

                                invResCom.BuyBuild(playerTypeSender, forBuildType);

                                curBuildDatCom.BuildType = forBuildType;
                                curOwnBuildCom.PlayerType = playerTypeSender;

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
