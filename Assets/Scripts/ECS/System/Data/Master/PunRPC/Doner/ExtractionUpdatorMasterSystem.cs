using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.Else.Economy;
using Assets.Scripts.Workers.Game.Else.Fire;
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

                CellEnvirDataContainer.TakeAmountResources(EnvironmentTypes.Fertilizer, xy, minus);
                ResourcesUIDataContainer.AddAmountResources(ResourceTypes.Food, isMasterKey, minus);

                if (!CellEnvirDataContainer.HaveResources(EnvironmentTypes.Fertilizer, xy))
                {
                    CellEnvirDataContainer.ResetEnvironment(EnvironmentTypes.Fertilizer, xy);

                    InfoBuidlingsDataContainer.RemoveXyBuild(BuildingTypes.Farm, isMasterKey, xyIndex);
                    CellBuildDataContainer.ResetBuild(xy);
                }
            }





            for (int xyIndex = 0; xyIndex < InfoBuidlingsDataContainer.GetAmountBuild(BuildingTypes.Woodcutter, isMasterKey); xyIndex++)
            {
                var xy = InfoBuidlingsDataContainer.GetXyBuildByIndex(BuildingTypes.Woodcutter, isMasterKey, xyIndex);

                minus = InfoExtractionWorker.GetExtractionOneBuilding(BuildingTypes.Woodcutter, InfoBuidlingsDataContainer.AmountUpgrades(BuildingTypes.Woodcutter, isMasterKey));

                CellEnvirDataContainer.TakeAmountResources(EnvironmentTypes.AdultForest, xy, minus);
                ResourcesUIDataContainer.AddAmountResources(ResourceTypes.Wood, isMasterKey, minus);

                if (!CellEnvirDataContainer.HaveResources(EnvironmentTypes.AdultForest, xy))
                {
                    CellEnvirDataContainer.ResetEnvironment(EnvironmentTypes.AdultForest, xy);

                    InfoBuidlingsDataContainer.RemoveXyBuild(BuildingTypes.Woodcutter, isMasterKey, xyIndex);
                    CellBuildDataContainer.ResetBuild(xy);

                    if (CellFireDataContainer.HaveFire(xy))
                    {
                        CellFireDataContainer.ResetFire(xy);
                    }
                }
            }


            for (int xyIndex = 0; xyIndex < InfoBuidlingsDataContainer.GetAmountBuild(BuildingTypes.Mine, isMasterKey); xyIndex++)
            {
                var xy = InfoBuidlingsDataContainer.GetXyBuildByIndex(BuildingTypes.Mine, isMasterKey, xyIndex);

                minus = InfoExtractionWorker.GetExtractionOneBuilding(BuildingTypes.Mine, InfoBuidlingsDataContainer.AmountUpgrades(BuildingTypes.Mine, isMasterKey));

                CellEnvirDataContainer.TakeAmountResources(EnvironmentTypes.Hill, xy, minus);
                ResourcesUIDataContainer.AddAmountResources(ResourceTypes.Ore, isMasterKey, minus);


                CellBuildDataContainer.AddTimeStepsBuilding(BuildingTypes.Mine, xy, minus);

                if (CellBuildDataContainer.GetTimeStepsBuilding(BuildingTypes.Mine, xy) > 9
                    || !CellEnvirDataContainer.HaveResources(EnvironmentTypes.Hill, xy))
                {
                    if (CellBuildDataContainer.HaveOwner(xy))
                    {
                        InfoBuidlingsDataContainer.RemoveXyBuild(BuildingTypes.Mine, isMasterKey, xyIndex);
                    }
                    CellBuildDataContainer.ResetBuild(xy);

                    CellBuildDataContainer.SetTimeStepsBuilding(BuildingTypes.Mine, 0, xy);
                }
            }




            for (UnitTypes unitType = UnitTypes.King; (byte)unitType < Enum.GetNames(typeof(UnitTypes)).Length; unitType++)
            {
                var curConditionType = ConditionUnitTypes.Relaxed;

                for (int xyIndex = 0; xyIndex < InfoUnitsDataContainer.GetAmountUnitsInCondition(curConditionType, unitType, isMasterKey); xyIndex++)
                {
                    var xy = InfoUnitsDataContainer.GetXyInConditionByIndex(curConditionType, unitType, isMasterKey, xyIndex);

                    if (CellFireDataContainer.HaveFire(xy))
                    {
                        InfoUnitsDataContainer.ReplaceCondition(curConditionType, ConditionUnitTypes.None, unitType, isMasterKey, xy);
                        CellUnitsDataContainer.SetConditionType(ConditionUnitTypes.None, xy);
                    }

                    else
                    {
                        if (CellUnitsDataContainer.AmountHealth(xy) == CellUnitsDataContainer.MaxAmountHealth(unitType))
                        {
                            if (unitType == UnitTypes.Pawn || unitType == UnitTypes.PawnSword)
                            {
                                if (CellEnvirDataContainer.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                                {
                                    ResourcesUIDataContainer.AddAmountResources(ResourceTypes.Wood, isMasterKey);
                                    CellEnvirDataContainer.TakeAmountResources(EnvironmentTypes.AdultForest, xy);

                                    if (CellBuildDataContainer.HaveAnyBuilding(xy))
                                    {
                                        if (CellBuildDataContainer.IsBuildingType(BuildingTypes.Woodcutter, xy))
                                        {

                                        }
                                        else
                                        {
                                            InfoUnitsDataContainer.ReplaceCondition(curConditionType, ConditionUnitTypes.Protected, unitType, isMasterKey, xy);
                                            CellUnitsDataContainer.SetConditionType(ConditionUnitTypes.Protected, xy);
                                        }
                                    }
                                    else
                                    {
                                        CellBuildDataContainer.SetPlayerBuilding(BuildingTypes.Woodcutter, PhotonNetwork.MasterClient, xy);
                                        InfoBuidlingsDataContainer.AddXyBuild(BuildingTypes.Woodcutter, isMasterKey, xy);
                                    }
                                }
                                else
                                {
                                    InfoUnitsDataContainer.ReplaceCondition(curConditionType, ConditionUnitTypes.Protected, unitType, isMasterKey, xy);
                                    CellUnitsDataContainer.SetConditionType(ConditionUnitTypes.Protected, xy);
                                }
                            }

                            else
                            {
                                InfoUnitsDataContainer.ReplaceCondition(curConditionType, ConditionUnitTypes.Protected, unitType, isMasterKey, xy);
                                CellUnitsDataContainer.SetConditionType(ConditionUnitTypes.Protected, xy);
                            }
                        }

                        else
                        {
                            CellUnitsDataContainer.AddStandartHeal(xy);
                            if (CellUnitsDataContainer.AmountHealth(xy) > CellUnitsDataContainer.MaxAmountHealth(xy))
                            {
                                CellUnitsDataContainer.SetAmountHealth(CellUnitsDataContainer.MaxAmountHealth(unitType), xy);
                            }
                        }
                    }
                }


                curConditionType = ConditionUnitTypes.None;

                for (int xyIndex = 0; xyIndex < InfoUnitsDataContainer.GetAmountUnitsInCondition(curConditionType, unitType, isMasterKey); xyIndex++)
                {
                    var xy = InfoUnitsDataContainer.GetXyInConditionByIndex(curConditionType, unitType, isMasterKey, xyIndex);

                    if (CellUnitsDataContainer.HaveMaxAmountSteps(xy))
                    {
                        InfoUnitsDataContainer.RemoveUnitInCondition(curConditionType, unitType, isMasterKey, xy);
                        InfoUnitsDataContainer.AddUnitInCondition(ConditionUnitTypes.Protected, unitType, isMasterKey, xy);
                        CellUnitsDataContainer.SetConditionType(ConditionUnitTypes.Protected, xy);
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
                    InfoUnitsDataContainer.RemoveUnitInCondition(CellUnitsDataContainer.ConditionType(xyUnit), unitType, isMaster, xyUnit);

                    CellUnitsDataContainer.ResetUnit(xyUnit);
                    break;
                }
            }

            ResourcesUIDataContainer.SetAmountResources(ResourceTypes.Food, isMaster, 0);
        }
    }
}
