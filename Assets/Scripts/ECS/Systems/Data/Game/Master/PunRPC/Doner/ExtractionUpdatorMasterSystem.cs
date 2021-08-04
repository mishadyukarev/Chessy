using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.Else.Economy;
using Assets.Scripts.Workers.Info;
using Leopotam.Ecs;
using Photon.Pun;
using System;

internal sealed class ExtractionUpdatorMasterSystem : IEcsRunSystem
{
    private EcsFilter<XyUnitsComponent> _xyUnitsFilter;


    public void Run()
    {
        ref var xyUnitsCom = ref _xyUnitsFilter.Get1(0);

        int minus;
        bool isMasterKey;

        for (byte isMasterByte = 0; isMasterByte <= 1; isMasterByte++)
        {
            isMasterKey = true;
            if (isMasterByte == 1) isMasterKey = false;




            for (int xyIndex = 0; xyIndex < InitSystem.XyBuildingsCom.GetAmountBuild(BuildingTypes.Farm, isMasterKey); xyIndex++)
            {
                var xy = InitSystem.XyBuildingsCom.GetXyBuildByIndex(BuildingTypes.Farm, isMasterKey, xyIndex);

                minus = InfoExtractionWorker.GetExtractionOneBuilding(BuildingTypes.Farm, InitSystem.UpgradesBuildingsCom.AmountUpgrades(BuildingTypes.Farm, isMasterKey));

                CellEnvrDataSystem.TakeAmountResources(EnvironmentTypes.Fertilizer, xy, minus);
                ResourcesUIDataContainer.AddAmountResources(ResourceTypes.Food, isMasterKey, minus);

                if (!CellEnvrDataSystem.HaveResources(EnvironmentTypes.Fertilizer, xy))
                {
                    CellEnvrDataSystem.ResetEnvironment(EnvironmentTypes.Fertilizer, xy);

                    InitSystem.XyBuildingsCom.RemoveXyBuild(BuildingTypes.Farm, isMasterKey, xyIndex);
                    CellBuildDataSystem.ResetBuild(xy);
                }
            }





            for (int xyIndex = 0; xyIndex < InitSystem.XyBuildingsCom.GetAmountBuild(BuildingTypes.Woodcutter, isMasterKey); xyIndex++)
            {
                var xy = InitSystem.XyBuildingsCom.GetXyBuildByIndex(BuildingTypes.Woodcutter, isMasterKey, xyIndex);

                minus = InfoExtractionWorker.GetExtractionOneBuilding(BuildingTypes.Woodcutter, InitSystem.UpgradesBuildingsCom.AmountUpgrades(BuildingTypes.Woodcutter, isMasterKey));

                CellEnvrDataSystem.TakeAmountResources(EnvironmentTypes.AdultForest, xy, minus);
                ResourcesUIDataContainer.AddAmountResources(ResourceTypes.Wood, isMasterKey, minus);

                if (!CellEnvrDataSystem.HaveResources(EnvironmentTypes.AdultForest, xy))
                {
                    CellEnvrDataSystem.ResetEnvironment(EnvironmentTypes.AdultForest, xy);

                    InitSystem.XyBuildingsCom.RemoveXyBuild(BuildingTypes.Woodcutter, isMasterKey, xyIndex);
                    CellBuildDataSystem.ResetBuild(xy);

                    if (CellFireDataSystem.HaveFireCom(xy).HaveFire)
                    {
                        CellFireDataSystem.HaveFireCom(xy).Disable();
                    }
                }
            }


            for (int xyIndex = 0; xyIndex < InitSystem.XyBuildingsCom.GetAmountBuild(BuildingTypes.Mine, isMasterKey); xyIndex++)
            {
                var xy = InitSystem.XyBuildingsCom.GetXyBuildByIndex(BuildingTypes.Mine, isMasterKey, xyIndex);

                minus = InfoExtractionWorker.GetExtractionOneBuilding(BuildingTypes.Mine, InitSystem.UpgradesBuildingsCom.AmountUpgrades(BuildingTypes.Mine, isMasterKey));

                CellEnvrDataSystem.TakeAmountResources(EnvironmentTypes.Hill, xy, minus);
                ResourcesUIDataContainer.AddAmountResources(ResourceTypes.Ore, isMasterKey, minus);


                CellBuildDataSystem.TimeStepsCom(xy).Add(minus);

                if (CellBuildDataSystem.TimeStepsCom(xy).TimeSteps > 9
                    || !CellEnvrDataSystem.HaveResources(EnvironmentTypes.Hill, xy))
                {
                    if (CellBuildDataSystem.OwnerCom(xy).HaveOwner)
                    {
                        InitSystem.XyBuildingsCom.RemoveXyBuild(BuildingTypes.Mine, isMasterKey, xyIndex);
                    }
                    CellBuildDataSystem.ResetBuild(xy);

                    CellBuildDataSystem.TimeStepsCom(xy).Reset();
                }
            }




            for (UnitTypes unitType = UnitTypes.King; (byte)unitType < Enum.GetNames(typeof(UnitTypes)).Length; unitType++)
            {
                var curConditionType = ConditionUnitTypes.Relaxed;

                for (int xyIndex = 0; xyIndex < InitSystem.XyUnitsContitionCom.GetAmountUnitsInCondition(curConditionType, unitType, isMasterKey); xyIndex++)
                {
                    var xy = InitSystem.XyUnitsContitionCom.GetXyInConditionByIndex(curConditionType, unitType, isMasterKey, xyIndex);

                    if (CellFireDataSystem.HaveFireCom(xy).HaveFire)
                    {
                        InitSystem.XyUnitsContitionCom.ReplaceCondition(curConditionType, ConditionUnitTypes.None, unitType, isMasterKey, xy);
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
                                    ResourcesUIDataContainer.AddAmountResources(ResourceTypes.Wood, isMasterKey);
                                    CellEnvrDataSystem.TakeAmountResources(EnvironmentTypes.AdultForest, xy);

                                    if (CellBuildDataSystem.BuildTypeCom(xy).HaveBuild)
                                    {
                                        if (CellBuildDataSystem.BuildTypeCom(xy).Is(BuildingTypes.Woodcutter))
                                        {

                                        }
                                        else
                                        {
                                            InitSystem.XyUnitsContitionCom.ReplaceCondition(curConditionType, ConditionUnitTypes.Protected, unitType, isMasterKey, xy);
                                            CellUnitsDataSystem.SetConditionType(ConditionUnitTypes.Protected, xy);
                                        }
                                    }
                                    else
                                    {
                                        CellBuildDataSystem.SetPlayerBuilding(BuildingTypes.Woodcutter, PhotonNetwork.MasterClient, xy);
                                        InitSystem.XyBuildingsCom.AddXyBuild(BuildingTypes.Woodcutter, isMasterKey, xy);
                                    }
                                }
                                else
                                {
                                    InitSystem.XyUnitsContitionCom.ReplaceCondition(curConditionType, ConditionUnitTypes.Protected, unitType, isMasterKey, xy);
                                    CellUnitsDataSystem.SetConditionType(ConditionUnitTypes.Protected, xy);
                                }
                            }

                            else
                            {
                                InitSystem.XyUnitsContitionCom.ReplaceCondition(curConditionType, ConditionUnitTypes.Protected, unitType, isMasterKey, xy);
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

                for (int xyIndex = 0; xyIndex < InitSystem.XyUnitsContitionCom.GetAmountUnitsInCondition(curConditionType, unitType, isMasterKey); xyIndex++)
                {
                    var xy = InitSystem.XyUnitsContitionCom.GetXyInConditionByIndex(curConditionType, unitType, isMasterKey, xyIndex);

                    if (CellUnitsDataSystem.HaveMaxAmountSteps(xy))
                    {
                        InitSystem.XyUnitsContitionCom.RemoveUnitInCondition(curConditionType, unitType, isMasterKey, xy);
                        InitSystem.XyUnitsContitionCom.AddUnitInCondition(ConditionUnitTypes.Protected, unitType, isMasterKey, xy);
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

            ResourcesUIDataContainer.TakeAmountResources(ResourceTypes.Food, isMasterKey, amountUnits);
            ResourcesUIDataContainer.AddAmountResources(ResourceTypes.Food, isMasterKey);
        }





        TryRemoveUnit(true, ref xyUnitsCom);
        TryRemoveUnit(false, ref xyUnitsCom);
    }


    private void TryRemoveUnit(bool isMaster, ref XyUnitsComponent xyUnitsCom)
    {
        if (ResourcesUIDataContainer.GetAmountResources(ResourceTypes.Food, isMaster) < 0)
        {
            for (UnitTypes unitType = (UnitTypes)Enum.GetNames(typeof(UnitTypes)).Length - 1; unitType != UnitTypes.None; unitType--)
            {
                var amountUnits = xyUnitsCom.GetAmountUnitsInGame(unitType, isMaster);

                if (amountUnits > 0)
                {
                    var xyUnit = xyUnitsCom.GetXyUnitInGame(unitType, isMaster, amountUnits - 1);


                    xyUnitsCom.RemoveAmountUnitsInGame(unitType, isMaster, xyUnit);
                    InitSystem.XyUnitsContitionCom.RemoveUnitInCondition(CellUnitsDataSystem.ConditionType(xyUnit), unitType, isMaster, xyUnit);

                    CellUnitsDataSystem.ResetUnit(xyUnit);
                    break;
                }
            }

            ResourcesUIDataContainer.SetAmountResources(ResourceTypes.Food, isMaster, 0);
        }
    }
}
