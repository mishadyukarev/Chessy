using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Game;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Assets.Scripts.Workers.Game.Else.Economy;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class SyncEconomyUpUISystem : IEcsRunSystem
{
    private EcsFilter<EconomyViewUICom> _economyUIFilter = default;

    private EcsFilter<BuildsInGameComponent> _idxBuildsFilter = default;
    private EcsFilter<UnitsInGameInfoComponent> _idxUnitsFilter = default;
    private EcsFilter<UnitsInConditionInGameCom> _idxUnitsInCondFilter = default;

    private EcsFilter<CellEnvironDataCom> _cellEnvDataFilter = default;

    private EcsFilter<InventorResourcesComponent> _amountResFilter = default;
    private EcsFilter<UpgradesBuildingsComponent> _upgradeBuildsFilter = default;


    public void Run()
    {
        ref var economyViewUICom = ref _economyUIFilter.Get1(0);

        ref var idxUnitsCom = ref _idxUnitsFilter.Get1(0);
        ref var idxUnitsInCondCom = ref _idxUnitsInCondFilter.Get1(0);
        ref var idxBuildsCom = ref _idxBuildsFilter.Get1(0);

        ref var amountResCom = ref _amountResFilter.Get1(0);
        ref var amountBuildUpgradesCom = ref _upgradeBuildsFilter.Get1(0);



        var amountFarm = idxBuildsCom.GetAmountBuild(BuildingTypes.Farm, PhotonNetwork.IsMasterClient);
        var amountUpgradesFarm = amountBuildUpgradesCom.GetAmountUpgrades(BuildingTypes.Farm, PhotonNetwork.IsMasterClient);
        var extractionOneFarm = ExtractionInfoSupport.GetExtractionOneBuilding(BuildingTypes.Farm, amountUpgradesFarm);

        var amountAddingFood = 1 + amountFarm * extractionOneFarm
            - idxUnitsCom.GetAmountUnitsInGame(PhotonNetwork.IsMasterClient, new UnitTypes[]
            {
                UnitTypes.Pawn_Axe, UnitTypes.Rook_Bow, UnitTypes.Bishop_Bow
            });


        if (amountAddingFood < 0)economyViewUICom.SetAddingText(ResourceTypes.Food, amountAddingFood.ToString());

        else economyViewUICom.SetAddingText(ResourceTypes.Food, "+ " + amountAddingFood.ToString());



        var amountUpgradesWoodcutter = amountBuildUpgradesCom.GetAmountUpgrades(BuildingTypes.Woodcutter, PhotonNetwork.IsMasterClient);
        var amountAddingWood = idxBuildsCom.GetAmountBuild(BuildingTypes.Woodcutter, PhotonNetwork.IsMasterClient)
            * ExtractionInfoSupport.GetExtractionOneBuilding(BuildingTypes.Woodcutter, amountUpgradesWoodcutter);

        for (int xyIndex = 0; xyIndex < idxUnitsInCondCom.GetAmountUnitsInCondition(ConditionUnitTypes.Relaxed, UnitTypes.Pawn_Axe, PhotonNetwork.IsMasterClient); xyIndex++)
        {
            var idx = idxUnitsInCondCom.GetIdxInConditionByIndex(ConditionUnitTypes.Relaxed, UnitTypes.Pawn_Axe, PhotonNetwork.IsMasterClient, xyIndex);

            if (_cellEnvDataFilter.Get1(idx).HaveEnvironment(EnvironmentTypes.AdultForest))
            {
                amountAddingWood += 1;
            }
        }

        economyViewUICom.SetAddingText(ResourceTypes.Wood, "+ " + amountAddingWood);



        var amountUpgradesMine = amountBuildUpgradesCom.GetAmountUpgrades(BuildingTypes.Mine, PhotonNetwork.IsMasterClient);
        var amountAddingOre = idxBuildsCom.GetAmountBuild(BuildingTypes.Mine, PhotonNetwork.IsMasterClient)
            * ExtractionInfoSupport.GetExtractionOneBuilding(BuildingTypes.Mine, amountUpgradesMine);

        economyViewUICom.SetAddingText(ResourceTypes.Ore, "+ " + amountAddingOre);

        economyViewUICom.SetMainText(ResourceTypes.Food, amountResCom.GetAmountResources(ResourceTypes.Food, PhotonNetwork.IsMasterClient).ToString());
        economyViewUICom.SetMainText(ResourceTypes.Wood, amountResCom.GetAmountResources(ResourceTypes.Wood, PhotonNetwork.IsMasterClient).ToString());
        economyViewUICom.SetMainText(ResourceTypes.Ore, amountResCom.GetAmountResources(ResourceTypes.Ore, PhotonNetwork.IsMasterClient).ToString());
        economyViewUICom.SetMainText(ResourceTypes.Iron, amountResCom.GetAmountResources(ResourceTypes.Iron, PhotonNetwork.IsMasterClient).ToString());
        economyViewUICom.SetMainText(ResourceTypes.Gold, amountResCom.GetAmountResources(ResourceTypes.Gold, PhotonNetwork.IsMasterClient).ToString());
    }
}
