using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.Else.Economy;
using Assets.Scripts.Workers.Game.Else.Info.Units;
using Assets.Scripts.Workers.Game.UI.Vis.Up;
using Assets.Scripts.Workers.Info;
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
            ResourcesViewUIWorker.SetAddingText(ResourceTypes.Food, amountAddingFood.ToString());
        else ResourcesViewUIWorker.SetAddingText(ResourceTypes.Food, "+ " + amountAddingFood.ToString());



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

        ResourcesViewUIWorker.SetAddingText(ResourceTypes.Wood, "+ " + amountAddingWood);



        var amountUpgradesMine = InfoBuidlingsWorker.AmountUpgrades(BuildingTypes.Mine, isMasterClient);
        var amountAddingOre = InfoBuidlingsWorker.GetAmountBuild(BuildingTypes.Mine, isMasterClient)
            * InfoExtractionWorker.GetExtractionOneBuilding(BuildingTypes.Mine, amountUpgradesMine);

        ResourcesViewUIWorker.SetAddingText(ResourceTypes.Ore, "+ " + amountAddingOre);

        ResourcesViewUIWorker.SetMainText(ResourceTypes.Food, ResourcesDataUIWorker.GetAmountResources(ResourceTypes.Food, Instance.IsMasterClient).ToString());
        ResourcesViewUIWorker.SetMainText(ResourceTypes.Wood, ResourcesDataUIWorker.GetAmountResources(ResourceTypes.Wood, Instance.IsMasterClient).ToString());
        ResourcesViewUIWorker.SetMainText(ResourceTypes.Ore, ResourcesDataUIWorker.GetAmountResources(ResourceTypes.Ore, Instance.IsMasterClient).ToString());
        ResourcesViewUIWorker.SetMainText(ResourceTypes.Iron, ResourcesDataUIWorker.GetAmountResources(ResourceTypes.Iron, Instance.IsMasterClient).ToString());
        ResourcesViewUIWorker.SetMainText(ResourceTypes.Gold, ResourcesDataUIWorker.GetAmountResources(ResourceTypes.Gold, Instance.IsMasterClient).ToString());
    }
}
