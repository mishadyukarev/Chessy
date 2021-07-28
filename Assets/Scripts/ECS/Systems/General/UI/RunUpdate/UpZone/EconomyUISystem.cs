using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.Else.Economy;
using Assets.Scripts.Workers.Game.Else.Info.Units;
using Assets.Scripts.Workers.Info;
using Assets.Scripts.Workers.UI.Info;
using Leopotam.Ecs;
using Photon.Pun;
using static Assets.Scripts.Main;

internal sealed class EconomyUISystem : IEcsRunSystem
{
    public void Run()
    {
        var isMasterClient = PhotonNetwork.IsMasterClient;

        var amountFarm = InfoBuidlingsWorker.GetAmountBuild(BuildingTypes.Farm, isMasterClient);
        var amountUpgradesFarm = InfoBuidlingsWorker.AmountUpgrades(BuildingTypes.Farm, isMasterClient);
        var extractionOneFarm = InfoExtractionWorker.GetExtractionOneBuilding(BuildingTypes.Farm, amountUpgradesFarm);

        var amountAddingFood = 1 + amountFarm * extractionOneFarm
            - InfoAmountUnitsWorker.GetAmountUnitsInGame(isMasterClient, new UnitTypes[]
            {
                UnitTypes.Pawn, UnitTypes.PawnSword,
                UnitTypes.Rook, UnitTypes.RookCrossbow,
                UnitTypes.Bishop, UnitTypes.BishopCrossbow
            });

        if (amountAddingFood < 0)
            InfoResourcesUIWorker.SetAddingText(ResourceTypes.Food, amountAddingFood.ToString());
        else InfoResourcesUIWorker.SetAddingText(ResourceTypes.Food, "+ " + amountAddingFood.ToString());



        var amountUpgradesWoodcutter = InfoBuidlingsWorker.AmountUpgrades(BuildingTypes.Woodcutter, isMasterClient);
        var amountAddingWood = InfoBuidlingsWorker.GetAmountBuild(BuildingTypes.Woodcutter, isMasterClient)
            * InfoExtractionWorker.GetExtractionOneBuilding(BuildingTypes.Woodcutter, amountUpgradesWoodcutter);

        for (int xyIndex = 0; xyIndex < InfoUnitsConditionWorker.GetAmountUnitsInCondition(ConditionUnitTypes.Relaxed, UnitTypes.Pawn, isMasterClient); xyIndex++)
        {
            var xy = InfoUnitsConditionWorker.GetXyInConditionByIndex(ConditionUnitTypes.Relaxed, UnitTypes.Pawn, isMasterClient, xyIndex);

            if (CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
            {
                amountAddingWood += 1;
            }
        }
        for (int xyIndex = 0; xyIndex < InfoUnitsConditionWorker.GetAmountUnitsInCondition(ConditionUnitTypes.Relaxed, UnitTypes.PawnSword, isMasterClient); xyIndex++)
        {
            var xy = InfoUnitsConditionWorker.GetXyInConditionByIndex(ConditionUnitTypes.Relaxed, UnitTypes.PawnSword, isMasterClient, xyIndex);

            if (CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
            {
                amountAddingWood += 1;
            }
        }

        InfoResourcesUIWorker.SetAddingText(ResourceTypes.Wood, "+ " + amountAddingWood);



        var amountUpgradesMine = InfoBuidlingsWorker.AmountUpgrades(BuildingTypes.Mine, isMasterClient);
        var amountAddingOre = InfoBuidlingsWorker.GetAmountBuild(BuildingTypes.Mine, isMasterClient)
            * InfoExtractionWorker.GetExtractionOneBuilding(BuildingTypes.Mine, amountUpgradesMine);

        InfoResourcesUIWorker.SetAddingText(ResourceTypes.Ore, "+ " + amountAddingOre);

        InfoResourcesUIWorker.SetMainText(ResourceTypes.Food, InfoResourcesDataWorker.GetAmountResources(ResourceTypes.Food, Instance.IsMasterClient).ToString());
        InfoResourcesUIWorker.SetMainText(ResourceTypes.Wood, InfoResourcesDataWorker.GetAmountResources(ResourceTypes.Wood, Instance.IsMasterClient).ToString());
        InfoResourcesUIWorker.SetMainText(ResourceTypes.Ore, InfoResourcesDataWorker.GetAmountResources(ResourceTypes.Ore, Instance.IsMasterClient).ToString());
        InfoResourcesUIWorker.SetMainText(ResourceTypes.Iron, InfoResourcesDataWorker.GetAmountResources(ResourceTypes.Iron, Instance.IsMasterClient).ToString());
        InfoResourcesUIWorker.SetMainText(ResourceTypes.Gold, InfoResourcesDataWorker.GetAmountResources(ResourceTypes.Gold, Instance.IsMasterClient).ToString());
    }
}
