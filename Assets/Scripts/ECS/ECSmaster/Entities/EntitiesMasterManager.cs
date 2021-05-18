using Leopotam.Ecs;
using UnityEngine;
using static MainGame;

public class EntitiesMasterManager : EntitiesManager
{

    #region Properties

    internal EcsComponentRef<MotionComponent> RefresherMasterComponentRef => _elseEntity.Ref<MotionComponent>();
    internal EcsComponentRef<ReadyMasterComponent> ReadyMasterComponentRef => _elseEntity.Ref<ReadyMasterComponent>();
    internal EcsComponentRef<FromInfoComponent> FromInfoComponentRef => _elseEntity.Ref<FromInfoComponent>();

    #endregion



    public EntitiesMasterManager(EcsWorld ecsWorld) : base(ecsWorld) { }

    internal void CreateEntities(ECSmanager eCSmanager)
    {
        var eGM = eCSmanager.EntitiesGeneralManager;

        _elseEntity = GameWorld.NewEntity()
            .Replace(new MotionComponent())
            .Replace(new ReadyMasterComponent())
            .Replace(new FromInfoComponent());

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

        eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[true] = InstanceGame.StartValuesGameConfig.AMOUNT_FOOD_MASTER;
        eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[true] = InstanceGame.StartValuesGameConfig.AMOUNT_WOOD_MASTER;
        eGM.OreEntityAmountDictionaryComponent.AmountDictionary[true] = InstanceGame.StartValuesGameConfig.AMOUNT_ORE_MASTER;
        eGM.IronEntityAmountDictionaryComponent.AmountDictionary[true] = InstanceGame.StartValuesGameConfig.AMOUNT_IRON_MASTER;
        eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[true] = InstanceGame.StartValuesGameConfig.AMOUNT_GOLD_MASTER;

        eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[false] = InstanceGame.StartValuesGameConfig.AMOUNT_FOOD_OTHER;
        eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[false] = InstanceGame.StartValuesGameConfig.AMOUNT_WOOD_OTHER;
        eGM.OreEntityAmountDictionaryComponent.AmountDictionary[false] = InstanceGame.StartValuesGameConfig.AMOUNT_ORE_OTHER;
        eGM.IronEntityAmountDictionaryComponent.AmountDictionary[false] = InstanceGame.StartValuesGameConfig.AMOUNT_IRON_OTHER;
        eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[false] = InstanceGame.StartValuesGameConfig.AMOUNT_GOLD_OTHER;

        #endregion


        #region Info

        eGM.InfoEntityUnitsInfoComponent.IsSettedKingDictionary[true] = false;
        eGM.InfoEntityUnitsInfoComponent.IsSettedKingDictionary[false] = false;

        eGM.InfoEntityUnitsInfoComponent.AmountKingDictionary[true] = InstanceGame.StartValuesGameConfig.AMOUNT_KING_MASTER;
        eGM .InfoEntityUnitsInfoComponent.AmountKingDictionary[false] = InstanceGame.StartValuesGameConfig.AMOUNT_KING_OTHER;

        eGM .InfoEntityUnitsInfoComponent.AmountPawnDictionary[true] = InstanceGame.StartValuesGameConfig.AMOUNT_PAWN_MASTER;
        eGM .InfoEntityUnitsInfoComponent.AmountPawnDictionary[false] = InstanceGame.StartValuesGameConfig.AMOUNT_PAWN_OTHER;

        eGM .InfoEntityUnitsInfoComponent.AmountRookDictionary[true] = InstanceGame.StartValuesGameConfig.AMOUNT_ROOK_MASTER;
        eGM .InfoEntityUnitsInfoComponent.AmountRookDictionary[false] = InstanceGame.StartValuesGameConfig.AMOUNT_ROOK_OTHER;

        eGM .InfoEntityUnitsInfoComponent.AmountBishopDictionary[true] = InstanceGame.StartValuesGameConfig.AMOUNT_BISHOP_MASTER;
        eGM .InfoEntityUnitsInfoComponent.AmountBishopDictionary[false] = InstanceGame.StartValuesGameConfig.AMOUNT_BISHOP_OTHER;


        eGM.InfoEntityBuildingsInfoComponent.IsBuildedCityDictionary[true] = false;
        eGM.InfoEntityBuildingsInfoComponent.IsBuildedCityDictionary[false] = false;

        eGM.InfoEntityBuildingsInfoComponent.AmountFarmDictionary[true] = default;
        eGM.InfoEntityBuildingsInfoComponent.AmountFarmDictionary[false] = default;

        eGM.InfoEntityBuildingsInfoComponent.AmountWoodcutterDictionary[true] = default;
        eGM.InfoEntityBuildingsInfoComponent.AmountWoodcutterDictionary[false] = default;

        eGM.InfoEntityBuildingsInfoComponent.AmountMineDictionary[true] = default;
        eGM.InfoEntityBuildingsInfoComponent.AmountMineDictionary[false] = default;

        #endregion
    }
}
