using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
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
            minus = InfoResourcesWorker.GetExtractionBuildingType(BuildingTypes.Farm, true);

            CellEnvironmentWorker.TakeAmountResources(ResourceTypes.Food, xy, minus);
            InfoResourcesWorker.AddAmountResources(ResourceTypes.Food, true, minus);

            if (!CellEnvironmentWorker.HaveResources(ResourceTypes.Food, xy))
            {
                CellEnvironmentWorker.ResetEnvironment(EnvironmentTypes.Fertilizer, xy);
                CellBuildingWorker.ResetPlayerBuilding(xy);
                InfoBuidlingsWorker.TakeAmountBuildingsInGame(BuildingTypes.Farm, true, xy);
            }
        }

        foreach (var xy in InfoBuidlingsWorker.GetListXyBuildingsInGame(BuildingTypes.Woodcutter, true))
        {
            minus = InfoResourcesWorker.GetExtractionBuildingType(BuildingTypes.Woodcutter, true);

            CellEnvironmentWorker.TakeAmountResources(ResourceTypes.Wood, xy, minus);
            InfoResourcesWorker.AddAmountResources(ResourceTypes.Wood, true, minus);

            if (!CellEnvironmentWorker.HaveResources(ResourceTypes.Wood, xy))
            {
                CellEnvironmentWorker.ResetEnvironment(EnvironmentTypes.AdultForest, xy);

                InfoBuidlingsWorker.TakeAmountBuildingsInGame(BuildingTypes.Woodcutter, true, xy);
                CellBuildingWorker.ResetPlayerBuilding(xy);

                if (CellEffectsWorker.HaveEffect(EffectTypes.Fire, xy))
                    CellEffectsWorker.ResetEffect(EffectTypes.Fire, xy);

            }
        }

        foreach (var xy in InfoBuidlingsWorker.GetListXyBuildingsInGame(BuildingTypes.Mine, true))
        {
            minus = InfoResourcesWorker.GetExtractionBuildingType(BuildingTypes.Mine, true);

            CellEnvironmentWorker.TakeAmountResources(ResourceTypes.Ore, xy, minus);
            InfoResourcesWorker.AddAmountResources(ResourceTypes.Ore, true, minus);

            if (_eGM.CellBuildEnt_CellBuilCom(xy).TimeStepsBuilding(BuildingTypes.Mine) >= 10
                || !CellEnvironmentWorker.HaveResources(ResourceTypes.Ore, xy))
            {

                CellBuildingWorker.ResetPlayerBuilding(xy);
                InfoBuidlingsWorker.TakeAmountBuildingsInGame(BuildingTypes.Mine, true, xy);

                _eGM.CellBuildEnt_CellBuilCom(xy).SetTimeStepsBuilding(BuildingTypes.Mine, 0);
            }
            else
            {
                _eGM.CellBuildEnt_CellBuilCom(xy).AddTimeStepsBuilding(BuildingTypes.Mine, 1);
            }
        }


        Debug.Log(InfoUnitsWorker.GetAmountUnitsInStandartCondition(ProtectRelaxTypes.Relaxed, UnitTypes.Pawn, true));

        foreach (var xy in InfoUnitsWorker.GetUnitsInStandardCondition(ProtectRelaxTypes.Relaxed, UnitTypes.Pawn, true))
        {
            if (CellUnitWorker.AmountHealth(xy) < CellUnitWorker.MaxAmountHealth(UnitTypes.Pawn))
            {
                CellUnitWorker.AddStandartHeal(xy);
                if (CellUnitWorker.AmountHealth(xy) > CellUnitWorker.MaxAmountHealth(xy))
                {
                    CellUnitWorker.SetAmountHealth(CellUnitWorker.MaxAmountHealth(UnitTypes.Pawn), xy);
                }
            }
            else
            {
                if (CellEnvironmentWorker.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                {
                    InfoResourcesWorker.AddAmountResources(ResourceTypes.Wood, true);
                    CellEnvironmentWorker.TakeAmountResources(ResourceTypes.Wood, xy);

                    if (CellBuildingWorker.HaveBuilding(xy))
                    {

                    }
                    else
                    {

                        CellBuildingWorker.CreatePlayerBuilding(BuildingTypes.Woodcutter, PhotonNetwork.MasterClient, xy);
                        InfoBuidlingsWorker.AddAmountBuildingsInGame(BuildingTypes.Woodcutter, true, xy);
                    }
                }
            }
        }
    }
}
