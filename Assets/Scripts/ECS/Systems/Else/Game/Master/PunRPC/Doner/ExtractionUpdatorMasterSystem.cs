using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.Game;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.ECS.System.View.Game.General.Cell;
using Assets.Scripts.Workers.Cell;
using Assets.Scripts.Workers.Game.Else.Economy;
using Assets.Scripts.Workers.Game.UI;
using Leopotam.Ecs;
using Photon.Pun;
using System;

internal sealed class ExtractionUpdatorMasterSystem : IEcsRunSystem
{
    private EcsFilter<UpgradesBuildingsComponent> _upgradeBuildsFilter = default;
    private EcsFilter<XyUnitsComponent> _xyUnitsFilter = default;
    private EcsFilter<InventorResourcesComponent> _invResFilt = default;
    private EcsFilter<MotionsDataUIComponent> _motionsUIFilter = default;
    private EcsFilter<DonerDataUIComponent, DonerViewUIComponent> _donerUIFilter = default;

    private int GetAmountUpgrades(BuildingTypes buildingType, bool key) => _upgradeBuildsFilter.Get1(0).GetAmountUpgrades(buildingType, key);

    public void Run()
    {
        ref var xyUnitsCom = ref _xyUnitsFilter.Get1(0);
        ref var inventorResCom = ref _invResFilt.Get1(0);

        int minus;
        bool isMasterKey;

        for (byte isMasterByte = 0; isMasterByte <= 1; isMasterByte++)
        {
            isMasterKey = true;
            if (isMasterByte == 1) isMasterKey = false;




            for (int xyIndex = 0; xyIndex < MainGameSystem.XyBuildingsCom.GetAmountBuild(BuildingTypes.Farm, isMasterKey); xyIndex++)
            {
                var xy = MainGameSystem.XyBuildingsCom.GetXyBuildByIndex(BuildingTypes.Farm, isMasterKey, xyIndex);

                minus = ExtractionInfoSupport.GetExtractionOneBuilding(BuildingTypes.Farm, GetAmountUpgrades(BuildingTypes.Farm, isMasterKey));

                CellEnvrDataSystem.TakeAmountResources(EnvironmentTypes.Fertilizer, xy, minus);
                inventorResCom.AddAmountResources(ResourceTypes.Food, isMasterKey, minus);

                if (!CellEnvrDataSystem.HaveResources(EnvironmentTypes.Fertilizer, xy))
                {
                    CellEnvrDataSystem.ResetEnvironment(EnvironmentTypes.Fertilizer, xy);

                    MainGameSystem.XyBuildingsCom.RemoveXyBuild(BuildingTypes.Farm, isMasterKey, xyIndex);
                    CellBuildDataSystem.ResetBuild(xy);
                }
            }





            for (int xyIndex = 0; xyIndex < MainGameSystem.XyBuildingsCom.GetAmountBuild(BuildingTypes.Woodcutter, isMasterKey); xyIndex++)
            {
                var xy = MainGameSystem.XyBuildingsCom.GetXyBuildByIndex(BuildingTypes.Woodcutter, isMasterKey, xyIndex);

                minus = ExtractionInfoSupport.GetExtractionOneBuilding(BuildingTypes.Woodcutter, GetAmountUpgrades(BuildingTypes.Woodcutter, isMasterKey));

                CellEnvrDataSystem.TakeAmountResources(EnvironmentTypes.AdultForest, xy, minus);
                inventorResCom.AddAmountResources(ResourceTypes.Wood, isMasterKey, minus);

                if (!CellEnvrDataSystem.HaveResources(EnvironmentTypes.AdultForest, xy))
                {
                    CellEnvrDataSystem.ResetEnvironment(EnvironmentTypes.AdultForest, xy);

                    MainGameSystem.XyBuildingsCom.RemoveXyBuild(BuildingTypes.Woodcutter, isMasterKey, xyIndex);
                    CellBuildDataSystem.ResetBuild(xy);

                    if (CellFireDataSystem.HaveFireCom(xy).HaveFire)
                    {
                        CellFireDataSystem.HaveFireCom(xy).Disable();
                    }
                }
            }


            for (int xyIndex = 0; xyIndex < MainGameSystem.XyBuildingsCom.GetAmountBuild(BuildingTypes.Mine, isMasterKey); xyIndex++)
            {
                var xy = MainGameSystem.XyBuildingsCom.GetXyBuildByIndex(BuildingTypes.Mine, isMasterKey, xyIndex);

                minus = ExtractionInfoSupport.GetExtractionOneBuilding(BuildingTypes.Mine, GetAmountUpgrades(BuildingTypes.Mine, isMasterKey));

                CellEnvrDataSystem.TakeAmountResources(EnvironmentTypes.Hill, xy, minus);
                inventorResCom.AddAmountResources(ResourceTypes.Ore, isMasterKey, minus);


                CellBuildDataSystem.TimeStepsCom(xy).Add(minus);

                if (CellBuildDataSystem.TimeStepsCom(xy).TimeSteps > 9
                    || !CellEnvrDataSystem.HaveResources(EnvironmentTypes.Hill, xy))
                {
                    if (CellBuildDataSystem.OwnerCom(xy).HaveOwner)
                    {
                        MainGameSystem.XyBuildingsCom.RemoveXyBuild(BuildingTypes.Mine, isMasterKey, xyIndex);
                    }
                    CellBuildDataSystem.ResetBuild(xy);

                    CellBuildDataSystem.TimeStepsCom(xy).Reset();
                }
            }




            for (UnitTypes unitType = UnitTypes.King; (byte)unitType < Enum.GetNames(typeof(UnitTypes)).Length; unitType++)
            {
                var curConditionType = ConditionUnitTypes.Relaxed;

                for (int xyIndex = 0; xyIndex < MainGameSystem.XyUnitsContitionCom.GetAmountUnitsInCondition(curConditionType, unitType, isMasterKey); xyIndex++)
                {
                    var xy = MainGameSystem.XyUnitsContitionCom.GetXyInConditionByIndex(curConditionType, unitType, isMasterKey, xyIndex);

                    if (CellFireDataSystem.HaveFireCom(xy).HaveFire)
                    {
                        MainGameSystem.XyUnitsContitionCom.ReplaceCondition(curConditionType, ConditionUnitTypes.None, unitType, isMasterKey, xy);
                        CellUnitsDataSystem.SetConditionType(ConditionUnitTypes.None, xy);
                    }

                    else
                    {
                        if (CellUnitsDataSystem.AmountHealth(xy) == CellUnitsDataSystem.MaxAmountHealth(unitType))
                        {
                            if (unitType == UnitTypes.Pawn || unitType == UnitTypes.PawnSword)
                            {
                                if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                                {
                                    inventorResCom.AddAmountResources(ResourceTypes.Wood, isMasterKey);
                                    CellEnvrDataSystem.TakeAmountResources(EnvironmentTypes.AdultForest, xy);

                                    if (CellBuildDataSystem.BuildTypeCom(xy).HaveBuild)
                                    {
                                        if (CellBuildDataSystem.BuildTypeCom(xy).Is(BuildingTypes.Woodcutter))
                                        {

                                        }
                                        else
                                        {
                                            MainGameSystem.XyUnitsContitionCom.ReplaceCondition(curConditionType, ConditionUnitTypes.Protected, unitType, isMasterKey, xy);
                                            CellUnitsDataSystem.SetConditionType(ConditionUnitTypes.Protected, xy);
                                        }
                                    }
                                    else
                                    {
                                        CellBuildDataSystem.SetPlayerBuilding(BuildingTypes.Woodcutter, PhotonNetwork.MasterClient, xy);
                                        MainGameSystem.XyBuildingsCom.AddXyBuild(BuildingTypes.Woodcutter, isMasterKey, xy);
                                    }
                                }
                                else
                                {
                                    MainGameSystem.XyUnitsContitionCom.ReplaceCondition(curConditionType, ConditionUnitTypes.Protected, unitType, isMasterKey, xy);
                                    CellUnitsDataSystem.SetConditionType(ConditionUnitTypes.Protected, xy);
                                }
                            }

                            else
                            {
                                MainGameSystem.XyUnitsContitionCom.ReplaceCondition(curConditionType, ConditionUnitTypes.Protected, unitType, isMasterKey, xy);
                                CellUnitsDataSystem.SetConditionType(ConditionUnitTypes.Protected, xy);
                            }
                        }

                        else
                        {
                            CellUnitsDataSystem.AddStandartHeal(xy);
                            if (CellUnitsDataSystem.AmountHealth(xy) > CellUnitsDataSystem.MaxAmountHealth(xy))
                            {
                                CellUnitsDataSystem.SetAmountHealth(CellUnitsDataSystem.MaxAmountHealth(unitType), xy);
                            }
                        }
                    }
                }


                curConditionType = ConditionUnitTypes.None;

                for (int xyIndex = 0; xyIndex < MainGameSystem.XyUnitsContitionCom.GetAmountUnitsInCondition(curConditionType, unitType, isMasterKey); xyIndex++)
                {
                    var xy = MainGameSystem.XyUnitsContitionCom.GetXyInConditionByIndex(curConditionType, unitType, isMasterKey, xyIndex);

                    if (CellUnitsDataSystem.HaveMaxAmountSteps(xy))
                    {
                        MainGameSystem.XyUnitsContitionCom.RemoveUnitInCondition(curConditionType, unitType, isMasterKey, xy);
                        MainGameSystem.XyUnitsContitionCom.AddUnitInCondition(ConditionUnitTypes.Protected, unitType, isMasterKey, xy);
                        CellUnitsDataSystem.SetConditionType(ConditionUnitTypes.Protected, xy);
                    }
                }
            }

            var amountUnits = xyUnitsCom.GetAmountUnitsInGame(isMasterKey, new[]
            {
                UnitTypes.Pawn, UnitTypes.PawnSword,
                UnitTypes.Rook, UnitTypes.RookCrossbow,
                UnitTypes.Bishop, UnitTypes.BishopCrossbow
            });

            inventorResCom.TakeAmountResources(ResourceTypes.Food, isMasterKey, amountUnits);
            inventorResCom.AddAmountResources(ResourceTypes.Food, isMasterKey);
        }





        TryRemoveUnit(true, ref xyUnitsCom, ref inventorResCom);
        TryRemoveUnit(false, ref xyUnitsCom, ref inventorResCom);




        for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
        {
            for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
            {
                var xy = new int[] { x, y };

                if (CellFireDataSystem.HaveFireCom(xy).HaveFire)
                {
                    CellEnvrDataSystem.TakeAmountResources(EnvironmentTypes.AdultForest, xy, 2);

                    if (CellUnitsDataSystem.HaveAnyUnit(xy))
                    {
                        CellUnitsDataSystem.TakeAmountHealth(xy, 40);

                        if (!CellUnitsDataSystem.HaveAmountHealth(xy))
                        {
                            var conditionType = CellUnitsDataSystem.ConditionType(xy);
                            var unitType = CellUnitsDataSystem.UnitType(xy);
                            var key = CellUnitsDataSystem.IsMasterClient(xy);

                            xyUnitsCom.RemoveAmountUnitsInGame(unitType, key, xy);
                            MainGameSystem.XyUnitsContitionCom.RemoveUnitInCondition(conditionType, unitType, key, xy);
                            CellUnitsDataSystem.ResetUnit(xy);
                        }
                    }



                    if (!CellEnvrDataSystem.HaveResources(EnvironmentTypes.AdultForest, xy))
                    {
                        if (CellBuildDataSystem.BuildTypeCom(xy).HaveBuild)
                        {
                            var buildType = CellBuildDataSystem.BuildTypeCom(xy).BuildingType;
                            var key = CellBuildDataSystem.OwnerCom(xy).IsMasterClient;

                            MainGameSystem.XyBuildingsCom.RemoveXyBuild(buildType, key, xy);
                            CellBuildDataSystem.ResetBuild(xy);
                        }

                        CellEnvrDataSystem.ResetEnvironment(EnvironmentTypes.AdultForest, xy);

                        CellFireDataSystem.HaveFireCom(xy).Disable();


                        var aroundXYList = CellSpaceSupport.TryGetXyAround(xy);
                        foreach (var xy1 in aroundXYList)
                        {
                            if (CellViewSystem.IsActiveSelfParentCell(xy1))
                            {
                                if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, xy1))
                                {
                                    CellFireDataSystem.HaveFireCom(xy1).Disable();
                                }
                            }
                        }
                    }
                }
            }
        }






        for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
            for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
            {
                var xy = new int[] { x, y };

                if (CellUnitsDataSystem.HaveAnyUnit(xy))
                {
                    CellUnitsDataSystem.RefreshAmountSteps(xy);
                }
            }


        _donerUIFilter.Get1(0).SetDoned(true, false);
        _donerUIFilter.Get1(0).SetDoned(false, false);

        _motionsUIFilter.Get1(0).AmountMotions += 1;


        int amountAdultForest = 0;

        for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
            for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
            {
                var xy = new int[] { x, y };

                if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, xy)
                    && CellViewSystem.IsActiveSelfParentCell(xy))
                {
                    ++amountAdultForest;
                }
            }

        if (amountAdultForest <= 3)
        {
            RPCGameSystem.SoundToGeneral(RpcTarget.All, SoundEffectTypes.Truce);
            GameMasterSystemManager.TruceSystems.Run();
        }



    }


    private void TryRemoveUnit(bool isMaster, ref XyUnitsComponent xyUnitsCom, ref InventorResourcesComponent invResCom)
    {
        if (invResCom.GetAmountResources(ResourceTypes.Food, isMaster) < 0)
        {
            for (UnitTypes unitType = (UnitTypes)Enum.GetNames(typeof(UnitTypes)).Length - 1; unitType != UnitTypes.None; unitType--)
            {
                var amountUnits = xyUnitsCom.GetAmountUnitsInGame(unitType, isMaster);

                if (amountUnits > 0)
                {
                    var xyUnit = xyUnitsCom.GetXyUnitInGame(unitType, isMaster, amountUnits - 1);


                    xyUnitsCom.RemoveAmountUnitsInGame(unitType, isMaster, xyUnit);
                    MainGameSystem.XyUnitsContitionCom.RemoveUnitInCondition(CellUnitsDataSystem.ConditionType(xyUnit), unitType, isMaster, xyUnit);

                    CellUnitsDataSystem.ResetUnit(xyUnit);
                    break;
                }
            }

            invResCom.SetAmountResources(ResourceTypes.Food, isMaster, 0);
        }
    }


}
