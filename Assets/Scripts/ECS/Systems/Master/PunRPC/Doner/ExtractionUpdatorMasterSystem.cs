using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.Else.Economy;
using Assets.Scripts.Workers.Game.Else.Fire;
using Assets.Scripts.Workers.Game.Else.Info.Units;
using Assets.Scripts.Workers.Info;
using Leopotam.Ecs;
using Photon.Pun;

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




            for (int xyIndex = 0; xyIndex < InfoBuidlingsWorker.GetAmountBuild(BuildingTypes.Farm, isMasterKey); xyIndex++)
            {
                var xy = InfoBuidlingsWorker.GetXyBuildByIndex(BuildingTypes.Farm, isMasterKey, xyIndex);

                minus = InfoExtractionWorker.GetExtractionOneBuilding(BuildingTypes.Farm, InfoBuidlingsWorker.AmountUpgrades(BuildingTypes.Farm, isMasterKey));

                CellEnvirDataWorker.TakeAmountResources(EnvironmentTypes.Fertilizer, xy, minus);
                InfoResourcesDataWorker.AddAmountResources(ResourceTypes.Food, isMasterKey, minus);

                if (!CellEnvirDataWorker.HaveResources(EnvironmentTypes.Fertilizer, xy))
                {
                    CellEnvirDataWorker.ResetEnvironment(EnvironmentTypes.Fertilizer, xy);

                    InfoBuidlingsWorker.RemoveXyBuild(BuildingTypes.Farm, isMasterKey, xyIndex);
                    CellBuildingsDataWorker.ResetBuild(xy);
                }
            }





            for (int xyIndex = 0; xyIndex < InfoBuidlingsWorker.GetAmountBuild(BuildingTypes.Woodcutter, isMasterKey); xyIndex++)
            {
                var xy = InfoBuidlingsWorker.GetXyBuildByIndex(BuildingTypes.Woodcutter, isMasterKey, xyIndex);

                minus = InfoExtractionWorker.GetExtractionOneBuilding(BuildingTypes.Woodcutter, InfoBuidlingsWorker.AmountUpgrades(BuildingTypes.Woodcutter, isMasterKey));

                CellEnvirDataWorker.TakeAmountResources(EnvironmentTypes.AdultForest, xy, minus);
                InfoResourcesDataWorker.AddAmountResources(ResourceTypes.Wood, isMasterKey, minus);

                if (!CellEnvirDataWorker.HaveResources(EnvironmentTypes.AdultForest, xy))
                {
                    CellEnvirDataWorker.ResetEnvironment(EnvironmentTypes.AdultForest, xy);

                    InfoBuidlingsWorker.RemoveXyBuild(BuildingTypes.Woodcutter, isMasterKey, xyIndex);
                    CellBuildingsDataWorker.ResetBuild(xy);

                    if (CellFireDataWorker.HaveFire(xy))
                    {
                        CellFireDataWorker.ResetFire(xy);
                        CellFireDataWorker.ResetTimeSteps(xy);
                    }
                }
            }






            for (int xyIndex = 0; xyIndex < InfoBuidlingsWorker.GetAmountBuild(BuildingTypes.Mine, isMasterKey); xyIndex++)
            {
                var xy = InfoBuidlingsWorker.GetXyBuildByIndex(BuildingTypes.Mine, isMasterKey, xyIndex);

                minus = InfoExtractionWorker.GetExtractionOneBuilding(BuildingTypes.Mine, InfoBuidlingsWorker.AmountUpgrades(BuildingTypes.Mine, isMasterKey));

                CellEnvirDataWorker.TakeAmountResources(EnvironmentTypes.Hill, xy, minus);
                InfoResourcesDataWorker.AddAmountResources(ResourceTypes.Ore, isMasterKey, minus);


                CellBuildingsDataWorker.AddTimeStepsBuilding(BuildingTypes.Mine, xy, minus);

                if (CellBuildingsDataWorker.GetTimeStepsBuilding(BuildingTypes.Mine, xy) > 9
                    || !CellEnvirDataWorker.HaveResources(EnvironmentTypes.Hill, xy))
                {
                    if (CellBuildingsDataWorker.HaveOwner(xy))
                    {
                        InfoBuidlingsWorker.RemoveXyBuild(BuildingTypes.Mine, isMasterKey, xyIndex);
                    }
                    CellBuildingsDataWorker.ResetBuild(xy);

                    CellBuildingsDataWorker.SetTimeStepsBuilding(BuildingTypes.Mine, 0, xy);
                }
            }




            for (byte unitTypeByte = 2; unitTypeByte <= 3; unitTypeByte++)
            {
                var curUnitType = (UnitTypes)unitTypeByte;

                for (int xyIndex = 0; xyIndex < InfoUnitsConditionWorker.GetAmountUnitsInCondition(ConditionUnitTypes.Relaxed, curUnitType, isMasterKey); xyIndex++)
                {
                    var xy = InfoUnitsConditionWorker.GetXyInConditionByIndex(ConditionUnitTypes.Relaxed, curUnitType, isMasterKey, xyIndex);

                    if (CellUnitsDataWorker.AmountHealth(xy) < CellUnitsDataWorker.MaxAmountHealth(curUnitType))
                    {
                        CellUnitsDataWorker.AddStandartHeal(xy);
                        if (CellUnitsDataWorker.AmountHealth(xy) > CellUnitsDataWorker.MaxAmountHealth(xy))
                        {
                            CellUnitsDataWorker.SetAmountHealth(CellUnitsDataWorker.MaxAmountHealth(curUnitType), xy);
                        }
                    }
                    else
                    {
                        if (CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                        {
                            InfoResourcesDataWorker.AddAmountResources(ResourceTypes.Wood, isMasterKey);
                            CellEnvirDataWorker.TakeAmountResources(EnvironmentTypes.AdultForest, xy);

                            if (CellBuildingsDataWorker.HaveAnyBuilding(xy))
                            {

                            }
                            else
                            {
                                CellBuildingsDataWorker.SetPlayerBuilding(BuildingTypes.Woodcutter, PhotonNetwork.MasterClient, xy);
                                InfoBuidlingsWorker.AddXyBuild(BuildingTypes.Woodcutter, isMasterKey, xy);
                            }
                        }
                    }
                }
            }

            var amountUnits = InfoAmountUnitsWorker.GetAmountUnitsInGame(isMasterKey, new[]
            {
                UnitTypes.Pawn, UnitTypes.PawnSword,
                UnitTypes.Rook, UnitTypes.RookCrossbow,
                UnitTypes.Bishop, UnitTypes.BishopCrossbow
            });

            InfoResourcesDataWorker.TakeAmountResources(ResourceTypes.Food, isMasterKey, amountUnits);
            InfoResourcesDataWorker.AddAmountResources(ResourceTypes.Food, isMasterKey);
        }
    }
}
