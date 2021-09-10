using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.Game;
using Assets.Scripts.ECS.Component.View.Else.Game.General.Cell;
using Assets.Scripts.ECS.Game.General.Components;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Assets.Scripts.Workers.Game.Else.Economy;
using Leopotam.Ecs;
using Photon.Pun;
using System;

internal sealed class UpdatorMasterSystem : IEcsRunSystem
{
    private EcsFilter<XyCellComponent> _xyCellFilter = default;
    private EcsFilter<CellViewComponent> _cellViewFilter = default;
    private EcsFilter<CellUnitDataComponent, OwnerComponent, OwnerBotComponent> _cellUnitFilter = default;
    private EcsFilter<CellFireDataComponent> _cellFireDataFilter = default;
    private EcsFilter<CellEnvironDataCom> _cellEnvDataFilter = default;
    private EcsFilter<CellBuildDataComponent, OwnerComponent> _cellBuildDataFilter = default;

    private EcsFilter<UpgradesBuildingsComponent> _upgradeBuildsFilter = default;
    private EcsFilter<InventorResourcesComponent> _invResFilt = default;
    private EcsFilter<MotionsDataUIComponent> _motionsUIFilter = default;
    private EcsFilter<DonerDataUIComponent> _donerUIFilter = default;

    public void Run()
    {
        ref var inventorResCom = ref _invResFilt.Get1(0);
        ref var amountUpgradesCom = ref _upgradeBuildsFilter.Get1(0);
        int minus;
        int amountAdultForest = 0;


        inventorResCom.AddAmountResources(ResourceTypes.Food, true);
        inventorResCom.AddAmountResources(ResourceTypes.Food, false);


        foreach (byte curIdxCell in _xyCellFilter)
        {
            ref var curCellViewCom = ref _cellViewFilter.Get1(curIdxCell);

            ref var curCellUnitDataCom = ref _cellUnitFilter.Get1(curIdxCell);
            ref var curOwnerCellUnitCom = ref _cellUnitFilter.Get2(curIdxCell);
            ref var curOwnerBotCellUnitsComp = ref _cellUnitFilter.Get3(curIdxCell);

            ref var cellBuildDataCom = ref _cellBuildDataFilter.Get1(curIdxCell);
            ref var curOwnerCellBuildCom = ref _cellBuildDataFilter.Get2(curIdxCell);

            ref var curCellFireDataCom = ref _cellFireDataFilter.Get1(curIdxCell);
            ref var curCellEnvrDataCom = ref _cellEnvDataFilter.Get1(curIdxCell);


            if (curCellUnitDataCom.HaveUnit)
            {
                if (curOwnerCellUnitCom.HaveOwner)
                {
                    if (!curCellUnitDataCom.IsUnitType(UnitTypes.King)) inventorResCom.TakeAmountResources(ResourceTypes.Food, curOwnerCellUnitCom.IsMasterClient);

                    if (curCellFireDataCom.HaveFire)
                    {
                        curCellUnitDataCom.ConditionUnitType = ConditionUnitTypes.None;
                    }

                    else
                    {
                        if (curCellUnitDataCom.IsConditionType(ConditionUnitTypes.Relaxed))
                        {
                            if (curCellUnitDataCom.HaveMaxAmountHealth)
                            {
                                if (curCellUnitDataCom.IsUnitType(UnitTypes.Pawn))
                                {
                                    if (curCellEnvrDataCom.HaveEnvironment(EnvironmentTypes.AdultForest))
                                    {
                                        inventorResCom.AddAmountResources(ResourceTypes.Wood, curOwnerCellUnitCom.IsMasterClient);
                                        curCellEnvrDataCom.TakeAmountResources(EnvironmentTypes.AdultForest);

                                        if (cellBuildDataCom.HaveBuild)
                                        {
                                            if (cellBuildDataCom.IsBuildType(BuildingTypes.Woodcutter))
                                            {
                                                if (!curCellEnvrDataCom.HaveResources(EnvironmentTypes.AdultForest))
                                                {

                                                    cellBuildDataCom.BuildingType = default;
                                                    curOwnerCellBuildCom.ResetOwner();
                                                }
                                            }
                                            else
                                            {
                                                curCellUnitDataCom.ConditionUnitType = ConditionUnitTypes.Protected;
                                            }
                                        }
                                        else
                                        {
                                            if (curCellEnvrDataCom.HaveResources(EnvironmentTypes.AdultForest))
                                            {
                                                cellBuildDataCom.BuildingType = BuildingTypes.Woodcutter;
                                                curOwnerCellBuildCom.SetOwner(curOwnerCellUnitCom.Owner);
                                            }
                                            else
                                            {
                                                curCellEnvrDataCom.ResetEnvironment(EnvironmentTypes.AdultForest);
                                            }
                                        }
                                    }

                                    else if (curCellUnitDataCom.ExtraToolWeaponType == ToolWeaponTypes.Pick)
                                    {
                                        if (curCellEnvrDataCom.HaveEnvironment(EnvironmentTypes.Hill))
                                        {
                                            if (cellBuildDataCom.HaveBuild)
                                            {
                                                curCellUnitDataCom.ConditionUnitType = ConditionUnitTypes.Protected;
                                            }
                                            else
                                            {
                                                if (curCellEnvrDataCom.GetAmountResources(EnvironmentTypes.Hill) < curCellEnvrDataCom.MaxAmountResources(EnvironmentTypes.Hill))
                                                {
                                                    curCellEnvrDataCom.AddAmountResources(EnvironmentTypes.Hill);
                                                }
                                                else
                                                {
                                                    curCellUnitDataCom.ConditionUnitType = ConditionUnitTypes.Protected;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            curCellUnitDataCom.ConditionUnitType = ConditionUnitTypes.Protected;
                                        }
                                    }

                                    else
                                    {
                                        curCellUnitDataCom.ConditionUnitType = ConditionUnitTypes.Protected;
                                    }
                                }

                                else
                                {
                                    curCellUnitDataCom.ConditionUnitType = ConditionUnitTypes.Protected;
                                }
                            }

                            else
                            {
                                curCellUnitDataCom.AddStandartHeal();
                                if (curCellUnitDataCom.AmountHealth > curCellUnitDataCom.MaxAmountHealth)
                                {
                                    curCellUnitDataCom.AmountHealth = curCellUnitDataCom.MaxAmountHealth;
                                }
                            }
                        }
                        else if (curCellUnitDataCom.IsConditionType(ConditionUnitTypes.None))
                        {
                            if (curCellUnitDataCom.HaveMaxAmountSteps)
                            {
                                curCellUnitDataCom.ConditionUnitType = ConditionUnitTypes.Protected;
                            }
                        }
                    }

                    _cellUnitFilter.Get1(curIdxCell).RefreshAmountSteps();
                }

                else if (curOwnerBotCellUnitsComp.IsBot)
                { 
                    if (!curCellUnitDataCom.HaveMaxAmountHealth)
                    {
                        curCellUnitDataCom.AddAmountHealth(50);

                        if (curCellUnitDataCom.MaxAmountHealth < curCellUnitDataCom.AmountHealth)
                        {
                            curCellUnitDataCom.AmountHealth = curCellUnitDataCom.MaxAmountHealth;
                        }
                    }      
                }
            }

            if (cellBuildDataCom.HaveBuild)
            {
                if (curOwnerCellBuildCom.HaveOwner)
                {
                    if (cellBuildDataCom.IsBuildType(BuildingTypes.Farm))
                    {
                        minus = ExtractionInfoSupport.GetExtractionOneBuilding(BuildingTypes.Farm, amountUpgradesCom.GetAmountUpgrades(BuildingTypes.Farm, curOwnerCellBuildCom.IsMasterClient));

                        curCellEnvrDataCom.TakeAmountResources(EnvironmentTypes.Fertilizer, minus);
                        inventorResCom.AddAmountResources(ResourceTypes.Food, curOwnerCellBuildCom.IsMasterClient, minus);

                        if (!curCellEnvrDataCom.HaveResources(EnvironmentTypes.Fertilizer))
                        {
                            curCellEnvrDataCom.ResetEnvironment(EnvironmentTypes.Fertilizer);

                            cellBuildDataCom.BuildingType = default;
                            _cellBuildDataFilter.Get2(curIdxCell).ResetOwner();
                        }
                    }

                    else if (cellBuildDataCom.IsBuildType(BuildingTypes.Woodcutter))
                    {
                        minus = ExtractionInfoSupport.GetExtractionOneBuilding(BuildingTypes.Woodcutter, amountUpgradesCom.GetAmountUpgrades(BuildingTypes.Woodcutter, curOwnerCellBuildCom.IsMasterClient));

                        curCellEnvrDataCom.TakeAmountResources(EnvironmentTypes.AdultForest, minus);
                        inventorResCom.AddAmountResources(ResourceTypes.Wood, curOwnerCellBuildCom.IsMasterClient, minus);

                        if (!curCellEnvrDataCom.HaveResources(EnvironmentTypes.AdultForest))
                        {
                            curCellEnvrDataCom.ResetEnvironment(EnvironmentTypes.AdultForest);

                            cellBuildDataCom.ResetBuildType();

                            if (curCellFireDataCom.HaveFire)
                            {
                                curCellFireDataCom.HaveFire = false;
                            }
                        }
                    }

                    else if (cellBuildDataCom.IsBuildType(BuildingTypes.Mine))
                    {
                        minus = ExtractionInfoSupport.GetExtractionOneBuilding(BuildingTypes.Mine, amountUpgradesCom.GetAmountUpgrades(BuildingTypes.Mine, curOwnerCellBuildCom.IsMasterClient));

                        curCellEnvrDataCom.TakeAmountResources(EnvironmentTypes.Hill, minus);
                        inventorResCom.AddAmountResources(ResourceTypes.Ore, curOwnerCellBuildCom.IsMasterClient, minus);

                        if (!curCellEnvrDataCom.HaveResources(EnvironmentTypes.Hill))
                        {
                            cellBuildDataCom.ResetBuildType();
                        }

                    }
                }
            }

            if (curCellEnvrDataCom.HaveEnvironment(EnvironmentTypes.AdultForest))
            {
                ++amountAdultForest;
            }

            if (curCellFireDataCom.HaveFire)
            {
                curCellEnvrDataCom.TakeAmountResources(EnvironmentTypes.AdultForest, 2);

                if (curCellUnitDataCom.HaveUnit)
                {
                    curCellUnitDataCom.TakeAmountHealth(40);

                    if (!curCellUnitDataCom.HaveAmountHealth)
                    {
                        curCellUnitDataCom.ResetUnit();
                        curOwnerCellUnitCom.ResetOwner();
                    }
                }



                if (!curCellEnvrDataCom.HaveResources(EnvironmentTypes.AdultForest))
                {
                    if (cellBuildDataCom.HaveBuild)
                    {
                        cellBuildDataCom.BuildingType = default;
                        curOwnerCellBuildCom.ResetOwner();
                    }

                    curCellEnvrDataCom.ResetEnvironment(EnvironmentTypes.AdultForest);

                    curCellFireDataCom.HaveFire = false;


                    var aroundXYList = CellSpaceSupport.TryGetXyAround(_xyCellFilter.GetXyCell(curIdxCell));
                    foreach (var xy1 in aroundXYList)
                    {
                        var curIdxCell1 = _xyCellFilter.GetIndexCell(xy1);

                        if (_cellViewFilter.Get1(curIdxCell1).IsActiveParent)
                        {
                            if (_cellEnvDataFilter.Get1(curIdxCell1).HaveEnvironment(EnvironmentTypes.AdultForest))
                            {
                                _cellFireDataFilter.Get1(curIdxCell1).HaveFire = true;
                            }
                        }
                    }
                }
            }
        }

        if (amountAdultForest <= 3)
        {
            RpcSys.SoundToGeneral(RpcTarget.All, SoundEffectTypes.Truce);
            GameMasterSystemManager.TruceSystems.Run();
        }

        for (byte i = 0; i < 2; i++)
        {
            var isMaster = true;
            if (i == 1) isMaster = false;

            if (inventorResCom.GetAmountResources(ResourceTypes.Food, isMaster) < 0)
            {
                inventorResCom.SetAmountResources(ResourceTypes.Food, isMaster, 0);

                foreach (byte curIdxCell in _xyCellFilter)
                {
                    ref var curCellUnitDataComp = ref _cellUnitFilter.Get1(curIdxCell);
                    ref var curOwnerCellUnitComp = ref _cellUnitFilter.Get2(curIdxCell);

                    if (curCellUnitDataComp.HaveUnit)
                    {
                        if (!curCellUnitDataComp.IsUnitType(UnitTypes.King))
                        {
                            if (curOwnerCellUnitComp.IsMasterClient == isMaster)
                            {
                                curCellUnitDataComp.ResetUnit();
                                break;
                            }
                        }
                    }
                }
            }
        }



        _donerUIFilter.Get1(0).SetDoned(true, false);
        _donerUIFilter.Get1(0).SetDoned(false, false);

        _motionsUIFilter.Get1(0).AmountMotions += 1;
    }
}

