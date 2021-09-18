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
    private EcsFilter<CellBuildDataComponent, OwnerOnlineComp, OwnerOfflineCom> _cellBuildFilt = default;

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
            ref var curOnUnitCom = ref _cellUnitFilter.Get2(curIdxCell);
            ref var curOffUnitCom = ref _cellUnitFilter.Get3(curIdxCell);
            ref var curBotUnitCom = ref _cellUnitFilter.Get4(curIdxCell);

            ref var curBuilDatCom = ref _cellBuildFilt.Get1(curIdxCell);
            ref var curOnBuilCom = ref _cellBuildFilt.Get2(curIdxCell);
            ref var curOffBuildCom = ref _cellBuildFilt.Get3(curIdxCell);

            ref var curFireDatCom = ref _cellFireDataFilter.Get1(curIdxCell);
            ref var curEnvrDatCom = ref _cellEnvDataFilter.Get1(curIdxCell);


            if (curUnitDatCom.HaveUnit)
            {
                if (curBotUnitCom.IsBot)
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
                else
                {
                    if (curOnUnitCom.HaveOwner  || curOffUnitCom.HaveLocalPlayer)
                    {
                        var isCurUnitMaster = false;

                        if (PhotonNetwork.OfflineMode) isCurUnitMaster = curOffUnitCom.IsMainMaster;

                        else isCurUnitMaster = PhotonNetwork.IsMasterClient;



                        if (!curUnitDatCom.IsUnit(UnitTypes.King)) invResCom.TakeAmountResources(ResourceTypes.Food, isCurUnitMaster);

                        if (curFireDatCom.HaveFire)
                        {
                            curUnitDatCom.CondUnitType = CondUnitTypes.None;
                        }

                        else
                        {
                            if (curUnitDatCom.IsCondType(CondUnitTypes.Relaxed))
                            {
                                if (curUnitDatCom.HaveMaxAmountHealth)
                                {
                                    if (curUnitDatCom.IsUnit(UnitTypes.Pawn))
                                    {
                                        if (curEnvrDatCom.HaveEnvir(EnvirTypes.AdultForest))
                                        {
                                            invResCom.AddAmountResources(ResourceTypes.Wood, isCurUnitMaster);
                                            curEnvrDatCom.TakeAmountResources(EnvirTypes.AdultForest);

                                            if (curEnvrDatCom.HaveResources(EnvirTypes.AdultForest))
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

                                                    if (PhotonNetwork.OfflineMode) curOffBuildCom.IsMainMaster = curOffUnitCom.IsMainMaster;
                                                    else curOnBuilCom.SetOwner(curOnUnitCom.Owner);

                                                }
                                            }
                                            else
                                            {
                                                curBuilDatCom.DefBuildType();
                                                curEnvrDatCom.ResetEnvironment(EnvirTypes.AdultForest);
                                            }
                                        }

                                        else if (curUnitDatCom.ExtraTWPawnType == ToolWeaponTypes.Pick)
                                        {
                                            if (curEnvrDatCom.HaveEnvir(EnvirTypes.Hill))
                                            {
                                                if (curBuilDatCom.HaveBuild)
                                                {
                                                    curUnitDatCom.CondUnitType = CondUnitTypes.Protected;
                                                }
                                                else
                                                {
                                                    if (curEnvrDatCom.GetAmountResources(EnvirTypes.Hill) < curEnvrDatCom.MaxAmountResources(EnvirTypes.Hill))
                                                    {
                                                        curEnvrDatCom.AddAmountRes(EnvirTypes.Hill);
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
                            else if (curUnitDatCom.IsCondType(CondUnitTypes.None))
                            {
                                if (curUnitDatCom.HaveMaxAmountSteps)
                                {
                                    curUnitDatCom.CondUnitType = CondUnitTypes.Protected;
                                }
                            }
                        }

                        curUnitDatCom.RefreshAmountSteps();
                    }
                }
            }

            if (curBuilDatCom.HaveBuild)
            {
                if (curOnBuilCom.HaveOwner || curOffBuildCom.HaveLocalPlayer)
                {
                    var isCurBuildMast = false;

                    if (PhotonNetwork.OfflineMode) isCurBuildMast = curOffBuildCom.IsMainMaster;

                    else isCurBuildMast = PhotonNetwork.IsMasterClient;


                    if (curBuilDatCom.IsBuildType(BuildingTypes.Farm))
                    {
                        minus = ExtractionInfoSupport.ExtractOneBuild(BuildingTypes.Farm, amountUpgradesCom.AmountUpgs(BuildingTypes.Farm, isCurBuildMast));

                        curEnvrDatCom.TakeAmountResources(EnvirTypes.Fertilizer, minus);
                        invResCom.AddAmountResources(ResourceTypes.Food, isCurBuildMast, minus);

                        if (!curEnvrDatCom.HaveResources(EnvirTypes.Fertilizer))
                        {
                            curEnvrDatCom.ResetEnvironment(EnvirTypes.Fertilizer);

                            curBuilDatCom.DefBuildType();
                            _cellBuildFilt.Get2(curIdxCell).DefOwner();
                        }
                    }

                    else if (curBuilDatCom.IsBuildType(BuildingTypes.Woodcutter))
                    {
                        minus = ExtractionInfoSupport.ExtractOneBuild(BuildingTypes.Woodcutter, amountUpgradesCom.AmountUpgs(BuildingTypes.Woodcutter, isCurBuildMast));

                        curEnvrDatCom.TakeAmountResources(EnvirTypes.AdultForest, minus);
                        invResCom.AddAmountResources(ResourceTypes.Wood, isCurBuildMast, minus);

                        if (!curEnvrDatCom.HaveResources(EnvirTypes.AdultForest))
                        {
                            curEnvrDatCom.ResetEnvironment(EnvirTypes.AdultForest);

                            curBuilDatCom.DefBuildType();

                            if (curFireDatCom.HaveFire)
                            {
                                curFireDatCom.HaveFire = false;
                            }
                        }
                    }

                    else if (curBuilDatCom.IsBuildType(BuildingTypes.Mine))
                    {
                        minus = ExtractionInfoSupport.ExtractOneBuild(BuildingTypes.Mine, amountUpgradesCom.AmountUpgs(BuildingTypes.Mine, isCurBuildMast));

                        curEnvrDatCom.TakeAmountResources(EnvirTypes.Hill, minus);
                        invResCom.AddAmountResources(ResourceTypes.Ore, isCurBuildMast, minus);

                        if (!curEnvrDatCom.HaveResources(EnvirTypes.Hill))
                        {
                            curBuilDatCom.DefBuildType();
                        }

                    }
                }
            }

            if (curEnvrDatCom.HaveEnvir(EnvirTypes.AdultForest))
            {
                ++amountAdultForest;
            }

            if (curFireDatCom.HaveFire)
            {
                curEnvrDatCom.TakeAmountResources(EnvirTypes.AdultForest, 2);

                if (curUnitDatCom.HaveUnit)
                {
                    curUnitDatCom.TakeAmountHealth(40);

                    if (!curUnitDatCom.HaveAmountHealth)
                    {
                        curUnitDatCom.ResetUnit();
                        curOnUnitCom.DefOwner();
                    }
                }



                if (!curEnvrDatCom.HaveResources(EnvirTypes.AdultForest))
                {
                    if (curBuilDatCom.HaveBuild)
                    {
                        curBuilDatCom.BuildType = default;
                        curOnBuilCom.DefOwner();
                    }

                    curEnvrDatCom.ResetEnvironment(EnvirTypes.AdultForest);

                    curFireDatCom.HaveFire = false;


                    var aroundXYList = CellSpaceSupport.TryGetXyAround(_xyCellFilter.GetXyCell(curIdxCell));
                    foreach (var xy1 in aroundXYList)
                    {
                        var curIdxCell1 = _xyCellFilter.GetIdxCell(xy1);

                        if (_cellViewFilter.Get1(curIdxCell1).IsActiveParent)
                        {
                            if (_cellEnvDataFilter.Get1(curIdxCell1).HaveEnvir(EnvirTypes.AdultForest))
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
                        ref var curOnUnitCom = ref _cellUnitFilter.Get2(curIdxCell);
                        ref var curOffUnitCom = ref _cellUnitFilter.Get3(curIdxCell);

                        if (curUnitDatCom.IsUnit(unitType))
                        {
                            if (curOnUnitCom.HaveOwner)
                            {
                                if (curOnUnitCom.IsMasterClient == isMaster)
                                {
                                    curUnitDatCom.ResetUnit();

                                    isFindedUnit = true;
                                    break;
                                }
                            }

                            else if (curOffUnitCom.HaveLocalPlayer)
                            {
                                if (curOffUnitCom.IsMainMaster == isMaster)
                                {
                                    curUnitDatCom.ResetUnit();

                                    isFindedUnit = true;
                                    break;
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

