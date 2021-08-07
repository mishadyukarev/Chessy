using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Game;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.Workers.Game.Else.Economy;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class EconomyUISystem : IEcsRunSystem
{
    private EcsFilter<UpgradesBuildingsComponent> _upgradeBuildsFilter = default;
    private EcsFilter<XyUnitsComponent> _xyUnitsFilter = default;
    private EcsFilter<InventorResourcesComponent> _amountResFilter = default;
    private EcsFilter<EconomyViewUICom, EconomyDataUICom> _economyUIFilter = default;

    private int GetAmountUpgrades(BuildingTypes buildingType, bool key) => _upgradeBuildsFilter.Get1(0).GetAmountUpgrades(buildingType, key);

    public void Run()
    {
        ref var xyUnitsCom = ref _xyUnitsFilter.Get1(0);
        ref var amountRes = ref _amountResFilter.Get1(0);
        ref var economyViewUICom = ref _economyUIFilter.Get1(0);


        var isMasterClient = PhotonNetwork.IsMasterClient;

        var amountFarm = MainGameSystem.XyBuildingsCom.GetAmountBuild(BuildingTypes.Farm, isMasterClient);
        var amountUpgradesFarm = GetAmountUpgrades(BuildingTypes.Farm, isMasterClient);
        var extractionOneFarm = ExtractionInfoSupport.GetExtractionOneBuilding(BuildingTypes.Farm, amountUpgradesFarm);

        var amountAddingFood = 1 + amountFarm * extractionOneFarm
            - xyUnitsCom.GetAmountUnitsInGame(isMasterClient, new UnitTypes[]
            {
                UnitTypes.Pawn, UnitTypes.PawnSword,
                UnitTypes.Rook, UnitTypes.RookCrossbow,
                UnitTypes.Bishop, UnitTypes.BishopCrossbow
            });


        if (amountAddingFood < 0)economyViewUICom.SetAddingText(ResourceTypes.Food, amountAddingFood.ToString());

        else economyViewUICom.SetAddingText(ResourceTypes.Food, "+ " + amountAddingFood.ToString());



        var amountUpgradesWoodcutter = GetAmountUpgrades(BuildingTypes.Woodcutter, isMasterClient);
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

        economyViewUICom.SetAddingText(ResourceTypes.Wood, "+ " + amountAddingWood);



        var amountUpgradesMine = GetAmountUpgrades(BuildingTypes.Mine, isMasterClient);
        var amountAddingOre = MainGameSystem.XyBuildingsCom.GetAmountBuild(BuildingTypes.Mine, isMasterClient)
            * ExtractionInfoSupport.GetExtractionOneBuilding(BuildingTypes.Mine, amountUpgradesMine);

        economyViewUICom.SetAddingText(ResourceTypes.Ore, "+ " + amountAddingOre);

        economyViewUICom.SetMainText(ResourceTypes.Food, amountRes.GetAmountResources(ResourceTypes.Food, PhotonNetwork.IsMasterClient).ToString());
        economyViewUICom.SetMainText(ResourceTypes.Wood, amountRes.GetAmountResources(ResourceTypes.Wood, PhotonNetwork.IsMasterClient).ToString());
        economyViewUICom.SetMainText(ResourceTypes.Ore, amountRes.GetAmountResources(ResourceTypes.Ore, PhotonNetwork.IsMasterClient).ToString());
        economyViewUICom.SetMainText(ResourceTypes.Iron, amountRes.GetAmountResources(ResourceTypes.Iron, PhotonNetwork.IsMasterClient).ToString());
        economyViewUICom.SetMainText(ResourceTypes.Gold, amountRes.GetAmountResources(ResourceTypes.Gold, PhotonNetwork.IsMasterClient).ToString());
    }
}
