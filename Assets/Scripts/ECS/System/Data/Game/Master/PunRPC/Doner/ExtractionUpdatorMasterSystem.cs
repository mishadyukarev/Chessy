using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.Else.Economy;
using Assets.Scripts.Workers.Game.Else.Info.Units;
using Assets.Scripts.Workers.Info;
using Leopotam.Ecs;
using Photon.Pun;
using System;

internal sealed class ExtractionUpdatorMasterSystem : IEcsRunSystem
{
    public void Run()
    {

        int minus;
        bool isMasterKey;

        for (byte isMasterByte = 0; isMasterByte <= 1; isMasterByte++)
        {
            isMasterKey = true;
            if (isMasterByte == 1) isMasterKey = false;




            for (int xyIndex = 0; xyIndex < InfoBuidlingsDataContainer.GetAmountBuild(BuildingTypes.Farm, isMasterKey); xyIndex++)
            {
                var xy = InfoBuidlingsDataContainer.GetXyBuildByIndex(BuildingTypes.Farm, isMasterKey, xyIndex);

                minus = InfoExtractionWorker.GetExtractionOneBuilding(BuildingTypes.Farm, InfoBuidlingsDataContainer.AmountUpgrades(BuildingTypes.Farm, isMasterKey));

                CellEnvrDataSystem.TakeAmountResources(EnvironmentTypes.Fertilizer, xy, minus);
                ResourcesUIDataContainer.AddAmountResources(ResourceTypes.Food, isMasterKey, minus);

                if (!CellEnvrDataSystem.HaveResources(EnvironmentTypes.Fertilizer, xy))
                {
                    CellEnvrDataSystem.ResetEnvironment(EnvironmentTypes.Fertilizer, xy);

                    InfoBuidlingsDataContainer.RemoveXyBuild(BuildingTypes.Farm, isMasterKey, xyIndex);
                    CellBuildDataSystem.ResetBuild(xy);
                }
            }





            for (int xyIndex = 0; xyIndex < InfoBuidlingsDataContainer.GetAmountBuild(BuildingTypes.Woodcutter, isMasterKey); xyIndex++)
            {
                var xy = InfoBuidlingsDataContainer.GetXyBuildByIndex(BuildingTypes.Woodcutter, isMasterKey, xyIndex);

                minus = InfoExtractionWorker.GetExtractionOneBuilding(BuildingTypes.Woodcutter, InfoBuidlingsDataContainer.AmountUpgrades(BuildingTypes.Woodcutter, isMasterKey));

                CellEnvrDataSystem.TakeAmountResources(EnvironmentTypes.AdultForest, xy, minus);
                ResourcesUIDataContainer.AddAmountResources(ResourceTypes.Wood, isMasterKey, minus);

                if (!CellEnvrDataSystem.HaveResources(EnvironmentTypes.AdultForest, xy))
                {
                    CellEnvrDataSystem.ResetEnvironment(EnvironmentTypes.AdultForest, xy);

                    InfoBuidlingsDataContainer.RemoveXyBuild(BuildingTypes.Woodcutter, isMasterKey, xyIndex);
                    CellBuildDataSystem.ResetBuild(xy);

                    if (CellFireDataSystem.HaveFireCom(xy).HaveFire)
                    {
                        CellFireDataSystem.HaveFireCom(xy).Disable();
                    }
                }
            }


            for (int xyIndex = 0; xyIndex < InfoBuidlingsDataContainer.GetAmountBuild(BuildingTypes.Mine, isMasterKey); xyIndex++)
            {
                var xy = InfoBuidlingsDataContainer.GetXyBuildByIndex(BuildingTypes.Mine, isMasterKey, xyIndex);

                minus = InfoExtractionWorker.GetExtractionOneBuilding(BuildingTypes.Mine, InfoBuidlingsDataContainer.AmountUpgrades(BuildingTypes.Mine, isMasterKey));

                CellEnvrDataSystem.TakeAmountResources(EnvironmentTypes.Hill, xy, minus);
                ResourcesUIDataContainer.AddAmountResources(ResourceTypes.Ore, isMasterKey, minus);


                CellBuildDataSystem.TimeStepsCom(xy).Add(minus);

                if (CellBuildDataSystem.TimeStepsCom(xy).TimeSteps > 9
                    || !CellEnvrDataSystem.HaveResources(EnvironmentTypes.Hill, xy))
                {
                    if (CellBuildDataSystem.OwnerCom(xy).HaveOwner)
                    {
                        InfoBuidlingsDataContainer.RemoveXyBuild(BuildingTypes.Mine, isMasterKey, xyIndex);
                    }
                    CellBuildDataSystem.ResetBuild(xy);

                    CellBuildDataSystem.TimeStepsCom(xy).Reset();
                }
            }




            for (UnitTypes unitType = UnitTypes.King; (byte)unitType < Enum.GetNames(typeof(UnitTypes)).Length; unitType++)
            {
                var curConditionType = ConditionUnitTypes.Relaxed;

                for (int xyIndex = 0; xyIndex < InfoUnitsDataContainer.GetAmountUnitsInCondition(curConditionType, unitType, isMasterKey); xyIndex++)
                {
                    var xy = InfoUnitsDataContainer.GetXyInConditionByIndex(curConditionType, unitType, isMasterKey, xyIndex);

                    if (CellFireDataSystem.HaveFireCom(xy).HaveFire)
                    {
                        InfoUnitsDataContainer.ReplaceCondition(curConditionType, ConditionUnitTypes.None, unitType, isMasterKey, xy);
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
                                            InfoUnitsDataContainer.ReplaceCondition(curConditionType, ConditionUnitTypes.Protected, unitType, isMasterKey, xy);
                                            CellUnitsDataSystem.SetConditionType(ConditionUnitTypes.Protected, xy);
                                        }
                                    }
                                    else
                                    {
                                        CellBuildDataSystem.SetPlayerBuilding(BuildingTypes.Woodcutter, PhotonNetwork.MasterClient, xy);
                                        InfoBuidlingsDataContainer.AddXyBuild(BuildingTypes.Woodcutter, isMasterKey, xy);
                                    }
                                }
                                else
                                {
                                    InfoUnitsDataContainer.ReplaceCondition(curConditionType, ConditionUnitTypes.Protected, unitType, isMasterKey, xy);
                                    CellUnitsDataSystem.SetConditionType(ConditionUnitTypes.Protected, xy);
                                }
                            }

                            else
                            {
                                InfoUnitsDataContainer.ReplaceCondition(curConditionType, ConditionUnitTypes.Protected, unitType, isMasterKey, xy);
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

                for (int xyIndex = 0; xyIndex < InfoUnitsDataContainer.GetAmountUnitsInCondition(curConditionType, unitType, isMasterKey); xyIndex++)
                {
                    var xy = InfoUnitsDataContainer.GetXyInConditionByIndex(curConditionType, unitType, isMasterKey, xyIndex);

                    if (CellUnitsDataSystem.HaveMaxAmountSteps(xy))
                    {
                        InfoUnitsDataContainer.RemoveUnitInCondition(curConditionType, unitType, isMasterKey, xy);
                        InfoUnitsDataContainer.AddUnitInCondition(ConditionUnitTypes.Protected, unitType, isMasterKey, xy);
                        CellUnitsDataSystem.SetConditionType(ConditionUnitTypes.Protected, xy);
                    }
                }
            }

            var amountUnits = InfoUnitsDataContainer.GetAmountUnitsInGame(isMasterKey, new[]
            {
                UnitTypes.Pawn, UnitTypes.PawnSword,
                UnitTypes.Rook, UnitTypes.RookCrossbow,
                UnitTypes.Bishop, UnitTypes.BishopCrossbow
            });

            ResourcesUIDataContainer.TakeAmountResources(ResourceTypes.Food, isMasterKey, amountUnits);
            ResourcesUIDataContainer.AddAmountResources(ResourceTypes.Food, isMasterKey);
        }



        TryRemoveUnit(true);
        TryRemoveUnit(false);
    }


    private void TryRemoveUnit(bool isMaster)
    {
        if (ResourcesUIDataContainer.GetAmountResources(ResourceTypes.Food, isMaster) < 0)
        {
            for (UnitTypes unitType = (UnitTypes)Enum.GetNames(typeof(UnitTypes)).Length - 1; unitType != UnitTypes.None; unitType--)
            {
                var amountUnits = InfoUnitsDataContainer.GetAmountUnitsInGame(unitType, isMaster);

                if (amountUnits > 0)
                {
                    var xyUnit = InfoUnitsDataContainer.GetXyUnitInGame(unitType, isMaster, amountUnits - 1);


                    InfoUnitsDataContainer.RemoveAmountUnitsInGame(unitType, isMaster, xyUnit);
                    InfoUnitsDataContainer.RemoveUnitInCondition(CellUnitsDataSystem.ConditionType(xyUnit), unitType, isMaster, xyUnit);

                    CellUnitsDataSystem.ResetUnit(xyUnit);
                    break;
                }
            }

            ResourcesUIDataContainer.SetAmountResources(ResourceTypes.Food, isMaster, 0);
        }
    }
}
