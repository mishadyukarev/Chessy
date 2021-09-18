using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.Game;
using Assets.Scripts.ECS.Component.View.Else.Game.General.Cell;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.ECS.Game.General.Components;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Assets.Scripts.Workers.Game.Else.Economy;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class UpdatorMastSys : IEcsRunSystem
{
    private EcsFilter<XyCellComponent> _xyCellFilter = default;
    private EcsFilter<CellViewComponent> _cellViewFilter = default;
    private EcsFilter<CellUnitDataCom, OwnerOnlineComp, OwnerOfflineCom, OwnerBotComponent> _cellUnitFilter = default;
    private EcsFilter<CellFireDataComponent> _cellFireDataFilter = default;
    private EcsFilter<CellEnvironDataCom> _cellEnvDataFilter = default;
    private EcsFilter<CellBuildDataComponent, OwnerOnlineComp> _cellBuildDataFilter = default;

    private EcsFilter<UpgradesBuildingsComponent> _upgradeBuildsFilter = default;
    private EcsFilter<InventorResourcesComponent> _invResFilt = default;
    private EcsFilter<MotionsDataUIComponent> _motionsUIFilter = default;
    private EcsFilter<DonerDataUIComponent> _donerUIFilter = default;
    private EcsFilter<WhoseMoveCom> _whoseMoveFilt = default;

    public void Run()
    {
        ref var invResCom = ref _invResFilt.Get1(0);
        ref var amountUpgradesCom = ref _upgradeBuildsFilter.Get1(0);
        int minus;
        int amountAdultForest = 0;


        invResCom.AddAmountResources(ResourceTypes.Food, true);
        invResCom.AddAmountResources(ResourceTypes.Food, false);


        foreach (byte curIdxCell in _xyCellFilter)
        {
            ref var curCellViewCom = ref _cellViewFilter.Get1(curIdxCell);

            ref var curUnitDatCom = ref _cellUnitFilter.Get1(curIdxCell);
            ref var curOnlineUnitCom = ref _cellUnitFilter.Get2(curIdxCell);
            ref var curOfflineUnitCom = ref _cellUnitFilter.Get3(curIdxCell);
            ref var curBotUnitCom = ref _cellUnitFilter.Get4(curIdxCell);

            ref var curBuilDatCom = ref _cellBuildDataFilter.Get1(curIdxCell);
            ref var curOwnBuilCom = ref _cellBuildDataFilter.Get2(curIdxCell);

            ref var curFireDatCom = ref _cellFireDataFilter.Get1(curIdxCell);
            ref var curEnvrDatCom = ref _cellEnvDataFilter.Get1(curIdxCell);


            if (curUnitDatCom.HaveUnit)
            {
                if (curOnlineUnitCom.HaveOwner || curOfflineUnitCom.HaveLocPlayer)
                {
                    var isCurUnitMaster = false;

                    if (PhotonNetwork.OfflineMode) isCurUnitMaster = WhoseMoveCom.IsMainMove;

                    else isCurUnitMaster = PhotonNetwork.IsMasterClient;



                    if (!curUnitDatCom.Is(UnitTypes.King)) invResCom.TakeAmountResources(ResourceTypes.Food, isCurUnitMaster);

                    if (curFireDatCom.HaveFire)
                    {
                        curUnitDatCom.CondUnitType = CondUnitTypes.None;
                    }

                    else
                    {
                        if (curUnitDatCom.IsConditionType(CondUnitTypes.Relaxed))
                        {
                            if (curUnitDatCom.HaveMaxAmountHealth)
                            {
                                if (curUnitDatCom.Is(UnitTypes.Pawn))
                                {
                                    if (curEnvrDatCom.HaveEnvironment(EnvironmentTypes.AdultForest))
                                    {
                                        invResCom.AddAmountResources(ResourceTypes.Wood, isCurUnitMaster);
                                        curEnvrDatCom.TakeAmountResources(EnvironmentTypes.AdultForest);

                                        if (curEnvrDatCom.HaveResources(EnvironmentTypes.AdultForest))
                                        {
                                            if (curBuilDatCom.HaveBuild)
                                            {
                                                if (!curBuilDatCom.HaveBuild)
                                                {
                                                    curUnitDatCom.CondUnitType = CondUnitTypes.Protected;
                                                }
                                            }
                                            else
                                            {
                                                curBuilDatCom.BuildType = BuildingTypes.Woodcutter;
                                                curOwnBuilCom.SetOwner(curOnlineUnitCom.Owner);
                                            }
                                        }
                                        else
                                        {
                                            curBuilDatCom.DefBuildType();
                                            curEnvrDatCom.ResetEnvironment(EnvironmentTypes.AdultForest);
                                        }
                                    }

                                    else if (curUnitDatCom.ExtraTWPawnType == ToolWeaponTypes.Pick)
                                    {
                                        if (curEnvrDatCom.HaveEnvironment(EnvironmentTypes.Hill))
                                        {
                                            if (curBuilDatCom.HaveBuild)
                                            {
                                                curUnitDatCom.CondUnitType = CondUnitTypes.Protected;
                                            }
                                            else
                                            {
                                                if (curEnvrDatCom.GetAmountResources(EnvironmentTypes.Hill) < curEnvrDatCom.MaxAmountResources(EnvironmentTypes.Hill))
                                                {
                                                    curEnvrDatCom.AddAmountResources(EnvironmentTypes.Hill);
                                                }
                                                else
                                                {
                                                    curUnitDatCom.CondUnitType = CondUnitTypes.Protected;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            curUnitDatCom.CondUnitType = CondUnitTypes.Protected;
                                        }
                                    }

                                    else
                                    {
                                        curUnitDatCom.CondUnitType = CondUnitTypes.Protected;
                                    }
                                }

                                else
                                {
                                    curUnitDatCom.CondUnitType = CondUnitTypes.Protected;
                                }
                            }

                            else
                            {
                                curUnitDatCom.AddStandartHeal();
                                if (curUnitDatCom.AmountHealth > curUnitDatCom.MaxAmountHealth)
                                {
                                    curUnitDatCom.AmountHealth = curUnitDatCom.MaxAmountHealth;
                                }
                            }
                        }
                        else if (curUnitDatCom.IsConditionType(CondUnitTypes.None))
                        {
                            if (curUnitDatCom.HaveMaxAmountSteps)
                            {
                                curUnitDatCom.CondUnitType = CondUnitTypes.Protected;
                            }
                        }
                    }

                    curUnitDatCom.RefreshAmountSteps();
                }

                else if (curBotUnitCom.IsBot)
                {
                    if (!curUnitDatCom.HaveMaxAmountHealth)
                    {
                        curUnitDatCom.AddAmountHealth(100);

                        if (curUnitDatCom.MaxAmountHealth < curUnitDatCom.AmountHealth)
                        {
                            curUnitDatCom.AmountHealth = curUnitDatCom.MaxAmountHealth;
                        }
                    }
                }
            }

            if (curBuilDatCom.HaveBuild)
            {
                if (curOwnBuilCom.HaveOwner)
                {
                    if (curBuilDatCom.IsBuildType(BuildingTypes.Farm))
                    {
                        minus = ExtractionInfoSupport.GetExtractionOneBuilding(BuildingTypes.Farm, amountUpgradesCom.GetAmountUpgrades(BuildingTypes.Farm, curOwnBuilCom.IsMasterClient));

                        curEnvrDatCom.TakeAmountResources(EnvironmentTypes.Fertilizer, minus);
                        invResCom.AddAmountResources(ResourceTypes.Food, curOwnBuilCom.IsMasterClient, minus);

                        if (!curEnvrDatCom.HaveResources(EnvironmentTypes.Fertilizer))
                        {
                            curEnvrDatCom.ResetEnvironment(EnvironmentTypes.Fertilizer);

                            curBuilDatCom.DefBuildType();
                            _cellBuildDataFilter.Get2(curIdxCell).ResetOwner();
                        }
                    }

                    else if (curBuilDatCom.IsBuildType(BuildingTypes.Woodcutter))
                    {
                        minus = ExtractionInfoSupport.GetExtractionOneBuilding(BuildingTypes.Woodcutter, amountUpgradesCom.GetAmountUpgrades(BuildingTypes.Woodcutter, curOwnBuilCom.IsMasterClient));

                        curEnvrDatCom.TakeAmountResources(EnvironmentTypes.AdultForest, minus);
                        invResCom.AddAmountResources(ResourceTypes.Wood, curOwnBuilCom.IsMasterClient, minus);

                        if (!curEnvrDatCom.HaveResources(EnvironmentTypes.AdultForest))
                        {
                            curEnvrDatCom.ResetEnvironment(EnvironmentTypes.AdultForest);

                            curBuilDatCom.DefBuildType();

                            if (curFireDatCom.HaveFire)
                            {
                                curFireDatCom.HaveFire = false;
                            }
                        }
                    }

                    else if (curBuilDatCom.IsBuildType(BuildingTypes.Mine))
                    {
                        minus = ExtractionInfoSupport.GetExtractionOneBuilding(BuildingTypes.Mine, amountUpgradesCom.GetAmountUpgrades(BuildingTypes.Mine, curOwnBuilCom.IsMasterClient));

                        curEnvrDatCom.TakeAmountResources(EnvironmentTypes.Hill, minus);
                        invResCom.AddAmountResources(ResourceTypes.Ore, curOwnBuilCom.IsMasterClient, minus);

                        if (!curEnvrDatCom.HaveResources(EnvironmentTypes.Hill))
                        {
                            curBuilDatCom.DefBuildType();
                        }

                    }
                }
            }

            if (curEnvrDatCom.HaveEnvironment(EnvironmentTypes.AdultForest))
            {
                ++amountAdultForest;
            }

            if (curFireDatCom.HaveFire)
            {
                curEnvrDatCom.TakeAmountResources(EnvironmentTypes.AdultForest, 2);

                if (curUnitDatCom.HaveUnit)
                {
                    curUnitDatCom.TakeAmountHealth(40);

                    if (!curUnitDatCom.HaveAmountHealth)
                    {
                        curUnitDatCom.ResetUnit();
                        curOnlineUnitCom.ResetOwner();
                    }
                }



                if (!curEnvrDatCom.HaveResources(EnvironmentTypes.AdultForest))
                {
                    if (curBuilDatCom.HaveBuild)
                    {
                        curBuilDatCom.BuildType = default;
                        curOwnBuilCom.ResetOwner();
                    }

                    curEnvrDatCom.ResetEnvironment(EnvironmentTypes.AdultForest);

                    curFireDatCom.HaveFire = false;


                    var aroundXYList = CellSpaceSupport.TryGetXyAround(_xyCellFilter.GetXyCell(curIdxCell));
                    foreach (var xy1 in aroundXYList)
                    {
                        var curIdxCell1 = _xyCellFilter.GetIdxCell(xy1);

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

        if (amountAdultForest <= 9)
        {
            RpcSys.SoundToGeneral(RpcTarget.All, SoundEffectTypes.Truce);
            GameMasterSystemManager.TruceSystems.Run();
        }

        for (byte i = 0; i < 2; i++)
        {
            var isMaster = true;
            if (i == 1) isMaster = false;

            if (invResCom.AmountResources(ResourceTypes.Food, isMaster) < 0)
            {
                invResCom.SetAmountResources(ResourceTypes.Food, isMaster, 0);

                for (UnitTypes unitType = UnitTypes.Bishop; unitType >= UnitTypes.Pawn; unitType--)
                {
                    bool isFindedUnit = false;

                    foreach (byte curIdxCell in _xyCellFilter)
                    {
                        ref var curUnitDatCom = ref _cellUnitFilter.Get1(curIdxCell);
                        ref var curOwnUnitCom = ref _cellUnitFilter.Get2(curIdxCell);

                        if (curUnitDatCom.HaveUnit)
                        {
                            if (curOwnUnitCom.HaveOwner)
                            {
                                if (curUnitDatCom.Is(unitType))
                                {
                                    if (curOwnUnitCom.IsMasterClient == isMaster)
                                    {
                                        curUnitDatCom.ResetUnit();

                                        isFindedUnit = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    if (isFindedUnit) break;
                }
            }
        }



        _donerUIFilter.Get1(0).SetDoned(true, false);
        _donerUIFilter.Get1(0).SetDoned(false, false);

        _motionsUIFilter.Get1(0).AmountMotions += 1;
    }
}

