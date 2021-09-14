using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Game;
using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.ECS.Component.View.Else.Game.General.Cell;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Leopotam.Ecs;
using System;

internal sealed class BuilderMastSys : IEcsRunSystem
{
    private EcsFilter<InfoMasCom> _infoFilter = default;
    private EcsFilter<ForBuildingMasCom> _forBuilderFilter = default;

    private EcsFilter<XyCellComponent> _xyCellFilter = default;
    private EcsFilter<CellViewComponent> _cellViewFilter = default;
    private EcsFilter<CellBuildDataComponent, OwnerComponent> _cellBuildFilter = default;
    private EcsFilter<CellUnitDataComponent> _cellUnitFilter = default;
    private EcsFilter<CellEnvironDataCom> _cellEnvFilter = default;
    private EcsFilter<CellFireDataComponent> _cellFireFilter = default;

    private EcsFilter<InventorResourcesComponent> _amountResFilt = default;

    public void Run()
    {
        ref var infoMasCom = ref _infoFilter.Get1(0);
        ref var inventorResCom = ref _amountResFilt.Get1(0);
        ref var forBuildMasCom = ref _forBuilderFilter.Get1(0);

        var sender = infoMasCom.FromInfo.Sender;
        var idxCellForBuild = forBuildMasCom.IdxForBuild;
        var buildTypeForBuild = forBuildMasCom.BuildingTypeForBuidling;

        ref var curCellBuildDataCom = ref _cellBuildFilter.Get1(idxCellForBuild);
        ref var curOwnerCellBuildCom = ref _cellBuildFilter.Get2(idxCellForBuild);
        ref var curCellUnitDataCom = ref _cellUnitFilter.Get1(idxCellForBuild);
        ref var curCellEnvCom = ref _cellEnvFilter.Get1(idxCellForBuild);
        ref var curFireCom = ref _cellFireFilter.Get1(idxCellForBuild);


        if (curCellBuildDataCom.HaveBuild)
        {
            RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
            RpcSys.SoundToGeneral(sender, SoundEffectTypes.Mistake);
        }

        else
        {
            switch (buildTypeForBuild)
            {
                case BuildingTypes.None:
                    throw new Exception();


                case BuildingTypes.City:
                    if (curCellUnitDataCom.HaveMinAmountSteps)
                    {
                        bool haveNearBorder = false;

                        foreach (var xy in CellSpaceSupport.TryGetXyAround(_xyCellFilter.GetXyCell(idxCellForBuild)))
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

                            curCellBuildDataCom.BuildingType = buildTypeForBuild;
                            curOwnerCellBuildCom.Owner = sender;

                            curCellUnitDataCom.ResetAmountSteps();

                            curFireCom.DisableFire();

                            if (curCellEnvCom.HaveEnvironment(EnvironmentTypes.AdultForest)) curCellEnvCom.ResetEnvironment(EnvironmentTypes.AdultForest);
                            if (curCellEnvCom.HaveEnvironment(EnvironmentTypes.Fertilizer)) curCellEnvCom.ResetEnvironment(EnvironmentTypes.Fertilizer);
                        }

                        else
                        {
                            RpcSys.SimpleMistakeToGeneral(MistakeTypes.NearTheDesert, sender);
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
                    if (curCellUnitDataCom.HaveMinAmountSteps)
                    {
                        if (!curCellEnvCom.HaveEnvironment(EnvironmentTypes.AdultForest) && !curCellEnvCom.HaveEnvironment(EnvironmentTypes.YoungForest))
                        {
                            if (inventorResCom.CanCreateNewBuilding(buildTypeForBuild, sender, out bool[] haves))
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

                                inventorResCom.BuyNewBuilding(buildTypeForBuild, sender);

                                curCellBuildDataCom.BuildingType = buildTypeForBuild;
                                curOwnerCellBuildCom.Owner = sender;

                                curCellUnitDataCom.TakeAmountSteps();

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
                        if (inventorResCom.CanCreateNewBuilding(buildTypeForBuild, sender, out bool[] haves))
                        {
                            if (curCellUnitDataCom.HaveMaxAmountSteps)
                            {
                                RpcSys.SoundToGeneral(sender, SoundEffectTypes.Building);

                                inventorResCom.BuyNewBuilding(buildTypeForBuild, sender);

                                curCellBuildDataCom.BuildingType = buildTypeForBuild;
                                curOwnerCellBuildCom.Owner = sender;

                                curCellUnitDataCom.ResetAmountSteps();
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
