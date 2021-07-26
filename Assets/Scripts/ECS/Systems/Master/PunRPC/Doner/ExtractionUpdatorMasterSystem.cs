using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.Else.Fire;
using Assets.Scripts.Workers.Info;
using Photon.Pun;
using UnityEngine;

internal sealed class ExtractionUpdatorMasterSystem : SystemMasterReduction
{
    public override void Run()
    {
        base.Run();



        int minus;

        foreach (var xy in InfoBuidlingsWorker.GetListXyBuildingsInGame(BuildingTypes.Farm, true))
        {
            minus = InfoBuidlingsWorker.GetExtractionBuildingType(BuildingTypes.Farm, true);

            CellEnvirDataWorker.TakeAmountResources(EnvironmentTypes.Fertilizer, xy, minus);
            InfoResourcesDataWorker.AddAmountResources(ResourceTypes.Food, true, minus);

            if (!CellEnvirDataWorker.HaveResources(EnvironmentTypes.Fertilizer, xy))
            {
                CellEnvirDataWorker.ResetEnvironment(EnvironmentTypes.Fertilizer, xy);
                CellBuildingsDataWorker.ResetBuilding(xy);
                InfoBuidlingsWorker.TakeXyBuildingsInGame(BuildingTypes.Farm, true, xy);
            }
        }

        foreach (var xy in InfoBuidlingsWorker.GetListXyBuildingsInGame(BuildingTypes.Woodcutter, true))
        {
            minus = InfoBuidlingsWorker.GetExtractionBuildingType(BuildingTypes.Woodcutter, true);

            CellEnvirDataWorker.TakeAmountResources(EnvironmentTypes.AdultForest, xy, minus);
            InfoResourcesDataWorker.AddAmountResources(ResourceTypes.Wood, true, minus);

            if (!CellEnvirDataWorker.HaveResources(EnvironmentTypes.AdultForest, xy))
            {
                CellEnvirDataWorker.ResetEnvironment(EnvironmentTypes.AdultForest, xy);

                InfoBuidlingsWorker.TakeXyBuildingsInGame(BuildingTypes.Woodcutter, true, xy);
                CellBuildingsDataWorker.ResetBuilding(xy);

                if (CellFireDataWorker.HaveFire(xy))
                {
                    CellFireDataWorker.ResetFire(xy);
                    CellFireDataWorker.ResetTimeSteps(xy);
                }
            }
        }

        Debug.Log(InfoBuidlingsWorker.GetAmountAllBuildingsInGame(true));

        foreach (var xy in InfoBuidlingsWorker.GetListXyBuildingsInGame(BuildingTypes.Mine, true))
        {
            minus = InfoBuidlingsWorker.GetExtractionBuildingType(BuildingTypes.Mine, true);

            CellEnvirDataWorker.TakeAmountResources(EnvironmentTypes.Hill, xy, minus);
            InfoResourcesDataWorker.AddAmountResources(ResourceTypes.Ore, true, minus);

            if (CellBuildingsDataWorker.GetTimeStepsBuilding(BuildingTypes.Mine, xy) >= 9
                || !CellEnvirDataWorker.HaveResources(EnvironmentTypes.Hill, xy))
            {

                CellBuildingsDataWorker.ResetBuilding(xy);
                if (CellBuildingsDataWorker.HaveOwner(xy))
                {
                    InfoBuidlingsWorker.TakeXyBuildingsInGame
                        (CellBuildingsDataWorker.GetBuildingType(xy), CellBuildingsDataWorker.IsMasterBuilding(xy), xy);
                }





                InfoBuidlingsWorker.TakeXyBuildingsInGame(BuildingTypes.Mine, true, xy);

                CellBuildingsDataWorker.SetTimeStepsBuilding(BuildingTypes.Mine, 0, xy);
            }
            else
            {
                CellBuildingsDataWorker.AddTimeStepsBuilding(BuildingTypes.Mine, xy, 1);
            }
        }

        foreach (var xy in InfoUnitsWorker.GetUnitsInStandardCondition(ConditionTypes.Relaxed, UnitTypes.Pawn, true))
        {
            if (CellUnitsDataWorker.AmountHealth(xy) < CellUnitsDataWorker.MaxAmountHealth(UnitTypes.Pawn))
            {
                CellUnitsDataWorker.AddStandartHeal(xy);
                if (CellUnitsDataWorker.AmountHealth(xy) > CellUnitsDataWorker.MaxAmountHealth(xy))
                {
                    CellUnitsDataWorker.SetAmountHealth(CellUnitsDataWorker.MaxAmountHealth(UnitTypes.Pawn), xy);
                }
            }
            else
            {
                if (CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                {
                    InfoResourcesDataWorker.AddAmountResources(ResourceTypes.Wood, true);
                    CellEnvirDataWorker.TakeAmountResources(EnvironmentTypes.AdultForest, xy);

                    if (CellBuildingsDataWorker.HaveAnyBuilding(xy))
                    {

                    }
                    else
                    {

                        CellBuildingsDataWorker.CreatePlayerBuilding(BuildingTypes.Woodcutter, PhotonNetwork.MasterClient, xy);
                        InfoBuidlingsWorker.AddXyBuildingsInGame(BuildingTypes.Woodcutter, true, xy);
                    }
                }
            }
        }
    }
}
