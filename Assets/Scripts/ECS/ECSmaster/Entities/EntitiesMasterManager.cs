using Leopotam.Ecs;
using UnityEngine;
using static MainGame;

public class EntitiesMasterManager : EntitiesManager
{
    private EcsEntity _masterRPCEntity;
    internal ref BuildingTypeComponent MasterRPCEntBuildingTypeCom => ref _masterRPCEntity.Get<BuildingTypeComponent>();
    internal ref XyCellComponent MasterRPCEntXyCellCom => ref _masterRPCEntity.Get<XyCellComponent>();
    internal ref XySelPreComponent MasterRPCEntXySelPreCom => ref _masterRPCEntity.Get<XySelPreComponent>();
    internal ref UnitTypeComponent MasterRPCEntUnitTypeCom => ref _masterRPCEntity.Get<UnitTypeComponent>();


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
                    eCSmanager.EntitiesGeneralManager.CellEnvEnt_CellEnvironmentCom(x, y).SetResetEnvironment(true, EnvironmentTypes.Tree);

                random = Random.Range(1, 100);
                if (random <= InstanceGame.StartValuesGameConfig.PERCENT_HILL)
                    eCSmanager.EntitiesGeneralManager.CellEnvEnt_CellEnvironmentCom(x, y).SetResetEnvironment(true, EnvironmentTypes.Hill);

                random = Random.Range(1, 100);
                if (random <= InstanceGame.StartValuesGameConfig.PERCENT_MOUNTAIN)
                    eCSmanager.EntitiesGeneralManager.CellEnvEnt_CellEnvironmentCom(x, y).SetResetEnvironment(true, EnvironmentTypes.Mountain);



                if (!eCSmanager.EntitiesGeneralManager.CellEnvEnt_CellEnvironmentCom(x, y).HaveMountain
                    && !eCSmanager.EntitiesGeneralManager.CellEnvEnt_CellEnvironmentCom(x, y).HaveTree)
                {
                    random = Random.Range(1, 100);
                    if (random <= InstanceGame.StartValuesGameConfig.PERCENT_FOOD)
                        eCSmanager.EntitiesGeneralManager.CellEnvEnt_CellEnvironmentCom(x, y).SetResetEnvironment(true, EnvironmentTypes.Food);
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

        eGM.InfoEnt_UnitsInfoCom.IsSettedKingDict[true] = false;
        eGM.InfoEnt_UnitsInfoCom.IsSettedKingDict[false] = false;

        eGM.InfoEnt_UnitsInfoCom.AmountKingDict[true] = InstanceGame.StartValuesGameConfig.AMOUNT_KING_MASTER;
        eGM.InfoEnt_UnitsInfoCom.AmountKingDict[false] = InstanceGame.StartValuesGameConfig.AMOUNT_KING_OTHER;

        eGM.InfoEnt_UnitsInfoCom.AmountPawnDict[true] = InstanceGame.StartValuesGameConfig.AMOUNT_PAWN_MASTER;
        eGM.InfoEnt_UnitsInfoCom.AmountPawnDict[false] = InstanceGame.StartValuesGameConfig.AMOUNT_PAWN_OTHER;

        eGM.InfoEnt_UnitsInfoCom.AmountRookDict[true] = InstanceGame.StartValuesGameConfig.AMOUNT_ROOK_MASTER;
        eGM.InfoEnt_UnitsInfoCom.AmountRookDict[false] = InstanceGame.StartValuesGameConfig.AMOUNT_ROOK_OTHER;

        eGM.InfoEnt_UnitsInfoCom.AmountBishopDict[true] = InstanceGame.StartValuesGameConfig.AMOUNT_BISHOP_MASTER;
        eGM.InfoEnt_UnitsInfoCom.AmountBishopDict[false] = InstanceGame.StartValuesGameConfig.AMOUNT_BISHOP_OTHER;

        eGM.InfoEnt_BuildingsInfoCom.AmountFarmDict[true] = default;
        eGM.InfoEnt_BuildingsInfoCom.AmountFarmDict[false] = default;

        eGM.InfoEnt_BuildingsInfoCom.AmountWoodcutterDict[true] = default;
        eGM.InfoEnt_BuildingsInfoCom.AmountWoodcutterDict[false] = default;

        eGM.InfoEnt_BuildingsInfoCom.AmountMineDict[true] = default;
        eGM.InfoEnt_BuildingsInfoCom.AmountMineDict[false] = default;

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
