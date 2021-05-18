using Leopotam.Ecs;
using UnityEngine;
using static MainGame;

public class EntitiesMasterManager : EntitiesManager
{
    private EcsEntity _masterRPCEntity;
    internal ref BuildingTypeComponent MasterRPCEntBuildingTypeCom => ref _masterRPCEntity.Get<BuildingTypeComponent>();
    internal ref XyCellComponent MasterRPCEntXyCellCom => ref _masterRPCEntity.Get<XyCellComponent>();
    internal ref XySelPreComponent MasterRPCEntXySelPreCom => ref _masterRPCEntity.Get<XySelPreComponent>();


    public EntitiesMasterManager(EcsWorld ecsWorld) : base(ecsWorld) { }

    internal void CreateEntities(ECSmanager eCSmanager)
    {
        var eGM = eCSmanager.EntitiesGeneralManager;

        #region Cells

        for (int x = 0; x < InstanceGame.StartValuesGameConfig.CELL_COUNT_X; x++)
        {
            for (int y = 0; y < InstanceGame.StartValuesGameConfig.CELL_COUNT_Y; y++)
            {
                int random;
                random = Random.Range(1, 100);
                if (random <= InstanceGame.StartValuesGameConfig.PERCENT_TREE)
                    eCSmanager.EntitiesGeneralManager.CellEnvironmentComponent(x, y).SetResetEnvironment(true, EnvironmentTypes.Tree);

                random = Random.Range(1, 100);
                if (random <= InstanceGame.StartValuesGameConfig.PERCENT_HILL)
                    eCSmanager.EntitiesGeneralManager.CellEnvironmentComponent(x, y).SetResetEnvironment(true, EnvironmentTypes.Hill);

                random = Random.Range(1, 100);
                if (random <= InstanceGame.StartValuesGameConfig.PERCENT_MOUNTAIN)
                    eCSmanager.EntitiesGeneralManager.CellEnvironmentComponent(x, y).SetResetEnvironment(true, EnvironmentTypes.Mountain);



                if (!eCSmanager.EntitiesGeneralManager.CellEnvironmentComponent(x, y).HaveMountain
                    && !eCSmanager.EntitiesGeneralManager.CellEnvironmentComponent(x, y).HaveTree)
                {
                    random = Random.Range(1, 100);
                    if (random <= InstanceGame.StartValuesGameConfig.PERCENT_FOOD)
                        eCSmanager.EntitiesGeneralManager.CellEnvironmentComponent(x, y).SetResetEnvironment(true, EnvironmentTypes.Food);
                }

            }
        }

        #endregion


        #region Economy

        eGM.FoodEAmountDictC.AmountDict[true] = InstanceGame.StartValuesGameConfig.AMOUNT_FOOD_MASTER;
        eGM.WoodEAmountDictC.AmountDict[true] = InstanceGame.StartValuesGameConfig.AMOUNT_WOOD_MASTER;
        eGM.OreEAmountDictC.AmountDict[true] = InstanceGame.StartValuesGameConfig.AMOUNT_ORE_MASTER;
        eGM.IronEAmountDictC.AmountDict[true] = InstanceGame.StartValuesGameConfig.AMOUNT_IRON_MASTER;
        eGM.GoldEAmountDictC.AmountDict[true] = InstanceGame.StartValuesGameConfig.AMOUNT_GOLD_MASTER;

        eGM.FoodEAmountDictC.AmountDict[false] = InstanceGame.StartValuesGameConfig.AMOUNT_FOOD_OTHER;
        eGM.WoodEAmountDictC.AmountDict[false] = InstanceGame.StartValuesGameConfig.AMOUNT_WOOD_OTHER;
        eGM.OreEAmountDictC.AmountDict[false] = InstanceGame.StartValuesGameConfig.AMOUNT_ORE_OTHER;
        eGM.IronEAmountDictC.AmountDict[false] = InstanceGame.StartValuesGameConfig.AMOUNT_IRON_OTHER;
        eGM.GoldEAmountDictC.AmountDict[false] = InstanceGame.StartValuesGameConfig.AMOUNT_GOLD_OTHER;

        #endregion


        #region Info

        eGM.InfoEntityUnitsInfoComponent.IsSettedKingDict[true] = false;
        eGM.InfoEntityUnitsInfoComponent.IsSettedKingDict[false] = false;

        eGM.InfoEntityUnitsInfoComponent.AmountKingDict[true] = InstanceGame.StartValuesGameConfig.AMOUNT_KING_MASTER;
        eGM .InfoEntityUnitsInfoComponent.AmountKingDict[false] = InstanceGame.StartValuesGameConfig.AMOUNT_KING_OTHER;

        eGM .InfoEntityUnitsInfoComponent.AmountPawnDict[true] = InstanceGame.StartValuesGameConfig.AMOUNT_PAWN_MASTER;
        eGM .InfoEntityUnitsInfoComponent.AmountPawnDict[false] = InstanceGame.StartValuesGameConfig.AMOUNT_PAWN_OTHER;

        eGM .InfoEntityUnitsInfoComponent.AmountRookDict[true] = InstanceGame.StartValuesGameConfig.AMOUNT_ROOK_MASTER;
        eGM .InfoEntityUnitsInfoComponent.AmountRookDict[false] = InstanceGame.StartValuesGameConfig.AMOUNT_ROOK_OTHER;

        eGM .InfoEntityUnitsInfoComponent.AmountBishopDict[true] = InstanceGame.StartValuesGameConfig.AMOUNT_BISHOP_MASTER;
        eGM .InfoEntityUnitsInfoComponent.AmountBishopDict[false] = InstanceGame.StartValuesGameConfig.AMOUNT_BISHOP_OTHER;


        eGM.InfoEntBuildingsInfoCom.IsBuildedCityDictionary[true] = false;
        eGM.InfoEntBuildingsInfoCom.IsBuildedCityDictionary[false] = false;

        eGM.InfoEntBuildingsInfoCom.AmountFarmDict[true] = default;
        eGM.InfoEntBuildingsInfoCom.AmountFarmDict[false] = default;

        eGM.InfoEntBuildingsInfoCom.AmountWoodcutterDict[true] = default;
        eGM.InfoEntBuildingsInfoCom.AmountWoodcutterDict[false] = default;

        eGM.InfoEntBuildingsInfoCom.AmountMineDict[true] = default;
        eGM.InfoEntBuildingsInfoCom.AmountMineDict[false] = default;

        #endregion


        #region BuilderEntity

        _masterRPCEntity = GameWorld.NewEntity();

        MasterRPCEntBuildingTypeCom.BuildingType = default;

        MasterRPCEntXyCellCom.XyCell = new int[StartValuesGameConfig.XY_FOR_ARRAY];

        MasterRPCEntXySelPreCom.XySelected = new int[StartValuesGameConfig.XY_FOR_ARRAY];
        MasterRPCEntXySelPreCom.XyPrevious = new int[StartValuesGameConfig.XY_FOR_ARRAY];

        #endregion

    }
}
