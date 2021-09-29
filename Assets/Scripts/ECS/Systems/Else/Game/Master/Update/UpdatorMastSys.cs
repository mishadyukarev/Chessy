using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.Game;
using Assets.Scripts.ECS.Component.View.Else.Game.General.Cell;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Assets.Scripts.Workers.Game.Else.Economy;
using Leopotam.Ecs;
using System;

internal sealed class UpdatorMastSys : IEcsRunSystem
{
    private EcsFilter<XyCellComponent> _xyCellFilter = default;
    private EcsFilter<CellViewComponent> _cellViewFilter = default;
    private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;
    private EcsFilter<CellFireDataComponent> _cellFireDataFilter = default;
    private EcsFilter<CellEnvironDataCom> _cellEnvDataFilter = default;
    private EcsFilter<CellBuildDataComponent, OwnerCom> _cellBuildFilt = default;

    private EcsFilter<UpgradesBuildsCom> _upgradeBuildsFilter = default;
    private EcsFilter<InventResourCom> _invResFilt = default;
    private EcsFilter<MotionsDataUIComponent> _motionsUIFilter = default;
    private EcsFilter<EndGameDataUIComponent> _endGameDataUIFilt = default;

    public void Run()
    {
        ref var invResCom = ref _invResFilt.Get1(0);
        ref var amountUpgradesCom = ref _upgradeBuildsFilter.Get1(0);
        int minus;
        int amountAdultForest = 0;


        invResCom.AddAmountResources(PlayerTypes.First, ResourceTypes.Food);
        invResCom.AddAmountResources(PlayerTypes.Second, ResourceTypes.Food);

        RpcSys.ActiveAmountMotionUIToGeneral(PhotonTargets.MasterClient);

        foreach (byte curIdxCell in _xyCellFilter)
        {
            ref var curCellViewCom = ref _cellViewFilter.Get1(curIdxCell);

            ref var curUnitDatCom = ref _cellUnitFilter.Get1(curIdxCell);
            ref var curOwnUnitCom = ref _cellUnitFilter.Get2(curIdxCell);

            ref var curBuilDatCom = ref _cellBuildFilt.Get1(curIdxCell);
            ref var curOnBuilCom = ref _cellBuildFilt.Get2(curIdxCell);

            ref var curFireDatCom = ref _cellFireDataFilter.Get1(curIdxCell);
            ref var curEnvrDatCom = ref _cellEnvDataFilter.Get1(curIdxCell);


            if (curUnitDatCom.HaveUnit)
            {
                if (GameModesCom.IsGameMode(GameModes.TrainingOff))
                {
                    if (curOwnUnitCom.IsPlayerType(PlayerTypes.Second))
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


                if (!curUnitDatCom.Is(UnitTypes.King)) invResCom.TakeAmountResources(curOwnUnitCom.PlayerType, ResourceTypes.Food);

                if (curFireDatCom.HaveFire)
                {
                    curUnitDatCom.CondUnitType = CondUnitTypes.None;
                }

                else
                {
                    if (curUnitDatCom.Is(CondUnitTypes.Relaxed))
                    {
                        if (curUnitDatCom.HaveMaxAmountHealth)
                        {
                            if (curUnitDatCom.Is(UnitTypes.Pawn))
                            {
                                if (curEnvrDatCom.HaveEnvir(EnvirTypes.AdultForest))
                                {
                                    invResCom.AddAmountResources(curOwnUnitCom.PlayerType, ResourceTypes.Wood);
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
                                            curOnBuilCom.PlayerType = curOwnUnitCom.PlayerType;
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
                    else if (curUnitDatCom.Is(CondUnitTypes.None))
                    {
                        if (curUnitDatCom.HaveMaxAmountSteps)
                        {
                            curUnitDatCom.CondUnitType = CondUnitTypes.Protected;
                        }
                    }
                }

                curUnitDatCom.RefreshAmountSteps();
            }

            if (curBuilDatCom.HaveBuild)
            {

                if (curBuilDatCom.IsBuildType(BuildingTypes.Farm))
                {
                    minus = ExtractionInfoSupport.ExtractOneBuild(BuildingTypes.Farm, amountUpgradesCom.AmountUpgs(curOwnUnitCom.PlayerType, BuildingTypes.Farm));

                    curEnvrDatCom.TakeAmountResources(EnvirTypes.Fertilizer, minus);
                    invResCom.AddAmountResources(curOwnUnitCom.PlayerType, ResourceTypes.Food, minus);

                    if (!curEnvrDatCom.HaveResources(EnvirTypes.Fertilizer))
                    {
                        curEnvrDatCom.ResetEnvironment(EnvirTypes.Fertilizer);

                        curBuilDatCom.DefBuildType();
                    }
                }

                else if (curBuilDatCom.IsBuildType(BuildingTypes.Woodcutter))
                {
                    minus = ExtractionInfoSupport.ExtractOneBuild(BuildingTypes.Woodcutter, amountUpgradesCom.AmountUpgs(curOwnUnitCom.PlayerType, BuildingTypes.Woodcutter));

                    curEnvrDatCom.TakeAmountResources(EnvirTypes.AdultForest, minus);
                    invResCom.AddAmountResources(curOwnUnitCom.PlayerType, ResourceTypes.Wood, minus);

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
                    minus = ExtractionInfoSupport.ExtractOneBuild(BuildingTypes.Mine, amountUpgradesCom.AmountUpgs(curOwnUnitCom.PlayerType, BuildingTypes.Mine));

                    curEnvrDatCom.TakeAmountResources(EnvirTypes.Hill, minus);
                    invResCom.AddAmountResources(curOwnUnitCom.PlayerType, ResourceTypes.Ore, minus);

                    if (!curEnvrDatCom.HaveResources(EnvirTypes.Hill))
                    {
                        curBuilDatCom.DefBuildType();
                    }
                }
            }

            if (curCellViewCom.IsActiveParent)
            {
                if (curEnvrDatCom.HaveEnvir(EnvirTypes.AdultForest))
                {
                    ++amountAdultForest;
                }
            }

            if (curFireDatCom.HaveFire)
            {
                curEnvrDatCom.TakeAmountResources(EnvirTypes.AdultForest, 2);

                if (curUnitDatCom.HaveUnit)
                {
                    curUnitDatCom.TakeAmountHealth(40);

                    if (!curUnitDatCom.HaveAmountHealth)
                    {
                        if (curUnitDatCom.Is(UnitTypes.King))
                        {
                            if (curOwnUnitCom.IsPlayerType(PlayerTypes.First))
                            {
                                _endGameDataUIFilt.Get1(0).PlayerWinner = PlayerTypes.Second;
                            }
                            else
                            {
                                _endGameDataUIFilt.Get1(0).PlayerWinner = PlayerTypes.First;
                            }

                        }

                        curUnitDatCom.DefUnitType();
                    }
                }



                if (!curEnvrDatCom.HaveResources(EnvirTypes.AdultForest))
                {
                    if (curBuilDatCom.HaveBuild)
                    {
                        curBuilDatCom.BuildType = default;
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
            RpcSys.SoundToGeneral(PhotonTargets.All, SoundEffectTypes.Truce);
            GameMasterSystemManager.TruceSystems.Run();
        }

        for (PlayerTypes playerType = (PlayerTypes)1; playerType < (PlayerTypes)Enum.GetNames(typeof(PlayerTypes)).Length; playerType++)
        {
            if (invResCom.AmountResources(playerType, ResourceTypes.Food) < 0)
            {
                invResCom.SetAmountResources(playerType, ResourceTypes.Food, 0);

                for (UnitTypes unitType = UnitTypes.Bishop; unitType >= UnitTypes.Pawn; unitType--)
                {
                    bool isFindedUnit = false;

                    foreach (byte curIdxCell in _xyCellFilter)
                    {
                        ref var curUnitDatCom = ref _cellUnitFilter.Get1(curIdxCell);
                        ref var curOnUnitCom = ref _cellUnitFilter.Get2(curIdxCell);

                        if (curUnitDatCom.Is(unitType))
                        {
                            if (curOnUnitCom.PlayerType == playerType)
                            {
                                curUnitDatCom.ResetUnit();

                                isFindedUnit = true;
                                break;
                            }
                        }


                    }
                    if (isFindedUnit) break;
                }
            }
        }



        //_donerUIFilter.Get1(0).SetDoned(true, false);
        //_donerUIFilter.Get1(0).SetDoned(false, false);

        _motionsUIFilter.Get1(0).AmountMotions += 1;
    }
}

