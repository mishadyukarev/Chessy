using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Game;
using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.Workers.Game.Else.Economy;
using Assets.Scripts.Workers.Game.UI.Vis.Up;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class EconomyUISystem : IEcsRunSystem
{
    private EcsFilter<XyUnitsComponent> _xyUnitsFilter;
    private EcsFilter<InventorResourcesComponent> _amountResFilter;

    public void Run()
    {
        ref var xyUnitsCom = ref _xyUnitsFilter.Get1(0);
        ref var amountRes = ref _amountResFilter.Get1(0);


        var isMasterClient = PhotonNetwork.IsMasterClient;

        var amountFarm = MainGameSystem.XyBuildingsCom.GetAmountBuild(BuildingTypes.Farm, isMasterClient);
        var amountUpgradesFarm = MainGameSystem.UpgradesBuildingsCom.AmountUpgrades(BuildingTypes.Farm, isMasterClient);
        var extractionOneFarm = ExtractionInfoSupport.GetExtractionOneBuilding(BuildingTypes.Farm, amountUpgradesFarm);

        var amountAddingFood = 1 + amountFarm * extractionOneFarm
            - xyUnitsCom.GetAmountUnitsInGame(isMasterClient, new UnitTypes[]
            {
                UnitTypes.Pawn, UnitTypes.PawnSword,
                UnitTypes.Rook, UnitTypes.RookCrossbow,
                UnitTypes.Bishop, UnitTypes.BishopCrossbow
            });

        var v = amountAddingFood.ToString();

        if (amountAddingFood < 0)
            ResourcesViewUIWorker.SetAddingText(ResourceTypes.Food, amountAddingFood.ToString());
        else ResourcesViewUIWorker.SetAddingText(ResourceTypes.Food, "+ " + amountAddingFood.ToString());



        var amountUpgradesWoodcutter = MainGameSystem.UpgradesBuildingsCom.AmountUpgrades(BuildingTypes.Woodcutter, isMasterClient);
        var amountAddingWood = MainGameSystem.XyBuildingsCom.GetAmountBuild(BuildingTypes.Woodcutter, isMasterClient)
            * ExtractionInfoSupport.GetExtractionOneBuilding(BuildingTypes.Woodcutter, amountUpgradesWoodcutter);

        for (int xyIndex = 0; xyIndex < MainGameSystem.XyUnitsContitionCom.GetAmountUnitsInCondition(ConditionUnitTypes.Relaxed, UnitTypes.Pawn, isMasterClient); xyIndex++)
        {
            var xy = MainGameSystem.XyUnitsContitionCom.GetXyInConditionByIndex(ConditionUnitTypes.Relaxed, UnitTypes.Pawn, isMasterClient, xyIndex);

            if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
            {
                amountAddingWood += 1;
            }
        }
        for (int xyIndex = 0; xyIndex < MainGameSystem.XyUnitsContitionCom.GetAmountUnitsInCondition(ConditionUnitTypes.Relaxed, UnitTypes.PawnSword, isMasterClient); xyIndex++)
        {
            var xy = MainGameSystem.XyUnitsContitionCom.GetXyInConditionByIndex(ConditionUnitTypes.Relaxed, UnitTypes.PawnSword, isMasterClient, xyIndex);

            if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
            {
                amountAddingWood += 1;
            }
        }

        ResourcesViewUIWorker.SetAddingText(ResourceTypes.Wood, "+ " + amountAddingWood);



        var amountUpgradesMine = MainGameSystem.UpgradesBuildingsCom.AmountUpgrades(BuildingTypes.Mine, isMasterClient);
        var amountAddingOre = MainGameSystem.XyBuildingsCom.GetAmountBuild(BuildingTypes.Mine, isMasterClient)
            * ExtractionInfoSupport.GetExtractionOneBuilding(BuildingTypes.Mine, amountUpgradesMine);

        ResourcesViewUIWorker.SetAddingText(ResourceTypes.Ore, "+ " + amountAddingOre);

        ResourcesViewUIWorker.SetMainText(ResourceTypes.Food, amountRes.GetAmountResources(ResourceTypes.Food, PhotonNetwork.IsMasterClient).ToString());
        ResourcesViewUIWorker.SetMainText(ResourceTypes.Wood, amountRes.GetAmountResources(ResourceTypes.Wood, PhotonNetwork.IsMasterClient).ToString());
        ResourcesViewUIWorker.SetMainText(ResourceTypes.Ore, amountRes.GetAmountResources(ResourceTypes.Ore, PhotonNetwork.IsMasterClient).ToString());
        ResourcesViewUIWorker.SetMainText(ResourceTypes.Iron, amountRes.GetAmountResources(ResourceTypes.Iron, PhotonNetwork.IsMasterClient).ToString());
        ResourcesViewUIWorker.SetMainText(ResourceTypes.Gold, amountRes.GetAmountResources(ResourceTypes.Gold, PhotonNetwork.IsMasterClient).ToString());
    }
}
