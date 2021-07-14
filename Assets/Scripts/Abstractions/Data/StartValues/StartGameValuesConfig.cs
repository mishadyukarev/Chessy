using UnityEngine;

[CreateAssetMenu(menuName = "StartValues", fileName = "StartValues")]
public class StartGameValuesConfig : ScriptableObject
{

    #region PERCENT ENVIRONMENT



    #endregion


    #region ECONOMY

    #region Unit

    public readonly int AMOUNT_KING_MASTER = 1;
    public readonly int AMOUNT_KING_OTHER = 1;

    public readonly int AMOUNT_PAWN_MASTER = 1;
    public readonly int AMOUNT_PAWN_OTHER = 1;

    public readonly int AMOUNT_ROOK_MASTER = 0;
    public readonly int AMOUNT_ROOK_OTHER = 0;

    public readonly int AMOUNT_BISHOP_MASTER = 0;
    public readonly int AMOUNT_BISHOP_OTHER = 0;

    #endregion


    #region Stats

    public readonly int AMOUNT_FOOD_MASTER = 25;
    public readonly int AMOUNT_FOOD_OTHER = 25;

    public readonly int AMOUNT_WOOD_MASTER = 25;
    public readonly int AMOUNT_WOOD_OTHER = 25;

    public readonly int AMOUNT_ORE_MASTER = 0;
    public readonly int AMOUNT_ORE_OTHER = 0;

    public readonly int AMOUNT_IRON_MASTER = 0;
    public readonly int AMOUNT_IRON_OTHER = 0;

    public readonly int AMOUNT_GOLD_MASTER = 0;
    public readonly int AMOUNT_GOLD_OTHER = 0;

    #endregion


    #region Costs

    #region Create Unit

    public readonly int FOOD_FOR_BUYING_PAWN = 5;
    public readonly int WOOD_FOR_BUYING_PAWN = 0;
    public readonly int ORE_FOR_BUYING_PAWN = 0;
    public readonly int IRON_FOR_BUYING_PAWN = 0;
    public readonly int GOLD_FOR_BUYING_PAWN = 0;

    public readonly int FOOD_FOR_BUYING_ROOK = 5;
    public readonly int WOOD_FOR_BUYING_ROOK = 5;
    public readonly int ORE_FOR_BUYING_ROOK = 0;
    public readonly int IRON_FOR_BUYING_ROOK = 0;
    public readonly int GOLD_FOR_BUYING_ROOK = 0;

    public readonly int FOOD_FOR_BUYING_BISHOP = 5;
    public readonly int WOOD_FOR_BUYING_BISHOP = 5;
    public readonly int ORE_FOR_BUYING_BISHOP = 0;
    public readonly int IRON_FOR_BUYING_BISHOP = 0;
    public readonly int GOLD_FOR_BUYING_BISHOP = 0;

    #endregion


    #region Upgrade

    public readonly int FOOD_FOR_UPGRADE_PAWN = 0;
    public readonly int WOOD_FOR_UPGRADE_PAWN = 0;
    public readonly int ORE_FOR_UPGRADE_PAWN = 0;
    public readonly int IRON_FOR_UPGRADE_PAWN = 1;
    public readonly int GOLD_FOR_UPGRADE_PAWN = 0;

    public readonly int FOOD_FOR_UPGRADE_ROOK = 0;
    public readonly int WOOD_FOR_UPGRADE_ROOK = 0;
    public readonly int ORE_FOR_UPGRADE_ROOK = 0;
    public readonly int IRON_FOR_UPGRADE_ROOK = 1;
    public readonly int GOLD_FOR_UPGRADE_ROOK = 0;

    public readonly int FOOD_FOR_UPGRADE_BISHOP = 0;
    public readonly int WOOD_FOR_UPGRADE_BISHOP = 0;
    public readonly int ORE_FOR_UPGRADE_BISHOP = 0;
    public readonly int IRON_FOR_UPGRADE_BISHOP = 1;
    public readonly int GOLD_FOR_UPGRADE_BISHOP = 0;


    private int _foodForUpgradeFarm = 0;
    private int _woodForUpgradeFarm = 0;
    private int _oreForUpgradeFarm = 0;
    private int _ironForUpgradeFarm = 0;
    private int _goldForUpgradeFarm = 3;

    public int FoodForUpgradeFarm => _foodForUpgradeFarm;
    public int WoodForUpgradeFarm => _woodForUpgradeFarm;
    public int OreForUpgradeFarm => _oreForUpgradeFarm;
    public int IronForUpgradeFarm => _ironForUpgradeFarm;
    public int GoldForUpgradeFarm => _goldForUpgradeFarm;


    private int _foodForUpgradeWoodcutter = 0;
    private int _woodForUpgradeWoodcutter = 0;
    private int _oreForUpgradeWoodcutter = 0;
    private int _ironForUpgradeWoodcutter = 0;
    private int _goldForUpgradeWoodcutter = 3;

    public int FoodForUpgradeWoodcutter => _foodForUpgradeWoodcutter;
    public int WoodForUpgradeWoodcutter => _woodForUpgradeWoodcutter;
    public int OreForUpgradeWoodcutter => _oreForUpgradeWoodcutter;
    public int IronForUpgradeWoodcutter => _ironForUpgradeWoodcutter;
    public int GoldForUpgradeWoodcutter => _goldForUpgradeWoodcutter;


    private int _foodForUpgradeMine = 0;
    private int _woodForUpgradeMine = 0;
    private int _oreForUpgradeMine = 0;
    private int _ironForUpgradeMine = 0;
    private int _goldForUpgradeMine = 3;

    public int FoodForUpgradeMine => _foodForUpgradeMine;
    public int WoodForUpgradeMine => _woodForUpgradeMine;
    public int OreForUpgradeMine => _oreForUpgradeMine;
    public int IronForUpgradeMine => _ironForUpgradeMine;
    public int GoldForUpgradeMine => _goldForUpgradeMine;


    #endregion


    #region Melting

    public readonly int FOOD_FOR_MELTING_ORE = 0;
    public readonly int WOOD_FOR_MELTING_ORE = 10;
    public readonly int ORE_FOR_MELTING_ORE = 4;
    public readonly int IRON_FOR_MELTING_ORE = 0;
    public readonly int GOLD_FOR_MELTING_ORE = 0;

    #endregion


    #region Fire

    private int _foodForPawnFire = 0;
    private int _woodForPawnFire = 0;
    private int _oreForPawnFire = 0;
    private int _ironForPawnFire = 0;
    private int _goldForPawnFire = 0;

    public int FoodForPawnFire => _foodForPawnFire;
    public int WoodForPawnFire => _woodForPawnFire;
    public int OreForPawnFire => _oreForPawnFire;
    public int IronForPawnFire => _ironForPawnFire;
    public int GoldForPawnFire => _goldForPawnFire;



    private int _foodForPawnSwordFire = 0;
    private int _woodForPawnSwordFire = 0;
    private int _oreForPawnSwordFire = 0;
    private int _ironForPawnSwordFire = 0;
    private int _goldForPawnSwordFire = 0;

    public int FoodForPawnSwordFire => _foodForPawnSwordFire;
    public int WoodForPawnSwordFire => _woodForPawnSwordFire;
    public int OreForPawnSwordFire => _oreForPawnSwordFire;
    public int IronForPawnSwordFire => _ironForPawnSwordFire;
    public int GoldForPawnSwordFire => _goldForPawnSwordFire;



    private int _foodForRookFire = 0;
    private int _woodForRookFire = 0;
    private int _oreForRookFire = 0;
    private int _ironForRookFire = 0;
    private int _goldForRookFire = 0;

    public int FoodForRookFire => _foodForRookFire;
    public int WoodForRookFire => _woodForRookFire;
    public int OreForRookFire => _oreForRookFire;
    public int IronForRookFire => _ironForRookFire;
    public int GoldForRookFire => _goldForRookFire;



    private int _foodForRookCrossbowFire = 0;
    private int _woodForRookCrossbowFire = 0;
    private int _oreForRookCrossbowFire = 0;
    private int _ironForRookCrossbowFire = 0;
    private int _goldForRookCrossbowFire = 0;

    public int FoodForRookCrossbowFire => _foodForRookCrossbowFire;
    public int WoodForRookCrossbowFire => _woodForRookCrossbowFire;
    public int OreForRookCrossbowFire => _oreForRookCrossbowFire;
    public int IronForRookCrossbowFire => _ironForRookCrossbowFire;
    public int GoldForRookCrossbowFire => _goldForRookCrossbowFire;



    private int _foodForBishopFire = 0;
    private int _woodForBishopFire = 0;
    private int _oreForBishopFire = 0;
    private int _ironForBishopFire = 0;
    private int _goldForBishopFire = 0;

    public int FoodForBishopFire => _foodForBishopFire;
    public int WoodForBishopFire => _woodForBishopFire;
    public int OreForBishopFire => _oreForBishopFire;
    public int IronForBishopFire => _ironForBishopFire;
    public int GoldForBishopFire => _goldForBishopFire;



    private int _foodForBishopCrossbowFire = 0;
    private int _woodForBishopCrossbowFire = 0;
    private int _oreForBishopCrossbowFire = 0;
    private int _ironForBishopCrossbowFire = 0;
    private int _goldForBishopCrossbowFire = 0;

    public int FoodForBishopCrossbowFire => _foodForBishopCrossbowFire;
    public int WoodForBishopCrossbowFire => _woodForBishopCrossbowFire;
    public int OreForBishopCrossbowFire => _oreForBishopCrossbowFire;
    public int IronForBishopCrossbowFire => _ironForBishopCrossbowFire;
    public int GoldForBishopCrossbowFire => _goldForBishopCrossbowFire;


    #endregion


    #region Building

    public readonly int FOOD_FOR_BUILDING_FARM = 0;
    public readonly int WOOD_FOR_BUILDING_FARM = 5;
    public readonly int ORE_FOR_BUILDING_FARM = 0;
    public readonly int IRON_FOR_BUILDING_FARM = 0;
    public readonly int GOLD_FOR_BUILDING_FARM = 0;

    //public readonly int FOOD_FOR_BUILDING_WOODCUTTER = 0;
    //public readonly int WOOD_FOR_BUILDING_WOODCUTTER = 10;
    //public readonly int ORE_FOR_BUILDING_WOODCUTTER = 0;
    //public readonly int IRON_FOR_BUILDING_WOODCUTTER = 0;
    //public readonly int GOLD_FOR_BUILDING_WOODCUTTER = 0;

    public readonly int FOOD_FOR_BUILDING_MINE = 0;
    public readonly int WOOD_FOR_BUILDING_MINE = 10;
    public readonly int ORE_FOR_BUILDING_MINE = 0;
    public readonly int IRON_FOR_BUILDING_MINE = 0;
    public readonly int GOLD_FOR_BUILDING_MINE = 0;

    #endregion

    #endregion


    #region Benefit

    public readonly int BENEFIT_FOOD_FARM = 1;
    public readonly int BENEFIT_WOOD_WOODCUTTER = 1;
    public readonly int BENEFIT_ORE_MINE = 1;

    public readonly int BENEFIT_FOOD_CITY = 1;
    public readonly int BENEFIT_WOOD_CITY = 1;

    #endregion

    #endregion


    #region UNIT

    #region Stats

    #region Health

    public readonly int AMOUNT_HEALTH_KING = 300;
    public readonly int AMOUNT_HEALTH_PAWN = 100;
    public readonly int AMOUNT_HEALTH_PAWN_SWORD = 150;
    public readonly int AMOUNT_HEALTH_ROOK = 100;
    public readonly int AMOUNT_HEALTH_ROOK_CROSSBOW = 100;
    public readonly int AMOUNT_HEALTH_BISHOP = 100;
    public readonly int AMOUNT_HEALTH_BISHOP_CROSSBOW = 100;


    private readonly float PERCENT_FOR_HEALTH_KING = 0.2f;
    private readonly float PERCENT_FOR_HEALTH_PAWN = 0.3f;
    private readonly float PERCENT_FOR_HEALTH_PAWN_SWORD = 0.2f;
    private readonly float PERCENT_FOR_HEALTH_ROOK = 0.3f;
    private readonly float PERCENT_FOR_HEALTH_ROOK_CROSSBOW = 0.3f;
    private readonly float PERCENT_FOR_HEALTH_BISHOP = 0.3f;
    private readonly float PERCENT_FOR_HEALTH_BISHOP_CROSSBOW = 0.3f;

    public int HEALTH_FOR_ADDING_KING => (int)(AMOUNT_HEALTH_KING * PERCENT_FOR_HEALTH_KING);
    public int HEALTH_FOR_ADDING_PAWN => (int)(AMOUNT_HEALTH_PAWN * PERCENT_FOR_HEALTH_PAWN);
    public int HEALTH_FOR_ADDING_PAWN_SWORD => (int)(AMOUNT_HEALTH_PAWN_SWORD * PERCENT_FOR_HEALTH_PAWN_SWORD);
    public int HEALTH_FOR_ADDING_ROOK => (int)(AMOUNT_HEALTH_ROOK * PERCENT_FOR_HEALTH_ROOK);
    public int HEALTH_FOR_ADDING_ROOK_CROSSBOW => (int)(AMOUNT_HEALTH_ROOK_CROSSBOW * PERCENT_FOR_HEALTH_ROOK_CROSSBOW);
    public int HEALTH_FOR_ADDING_BISHOP => (int)(AMOUNT_HEALTH_BISHOP * PERCENT_FOR_HEALTH_BISHOP);
    public int HEALTH_FOR_ADDING_BISHOP_CROSSBOW => (int)(AMOUNT_HEALTH_BISHOP_CROSSBOW * PERCENT_FOR_HEALTH_BISHOP_CROSSBOW);

    #endregion


    #region Damage

    public readonly int SIMPLE_POWER_DAMAGE_KING = 110;
    public readonly int SIMPLE_POWER_DAMAGE_PAWN = 50;
    public readonly int SIMPLE_POWER_DAMAGE_PAWN_SWORD = 75;
    public readonly int SIMPLE_POWER_DAMAGE_ROOK = 25;
    public readonly int SIMPLE_POWER_DAMAGE_ROOK_CROSSBOW = 50;
    public readonly int SIMPLE_POWER_DAMAGE_BISHOP = 25;
    public readonly int SIMPLE_POWER_DAMAGE_BISHOP_CROSSBOW = 50;

    public readonly float RATION_UNIQUE_POWER_DAMAGE_KING = 0.4f;
    public readonly float RATION_UNIQUE_POWER_DAMAGE_PAWN = 0.4f;
    public readonly float RATION_UNIQUE_POWER_DAMAGE_PAWN_SWORD = 0.4f;
    public readonly float RATION_UNIQUE_POWER_DAMAGE_ROOK = 0.4f;
    public readonly float RATION_UNIQUE_POWER_DAMAGE_ROOK_CROSSBOW = 0.4f;
    public readonly float RATION_UNIQUE_POWER_DAMAGE_BISHOP = 0.4f;
    public readonly float RATION_UNIQUE_POWER_DAMAGE_BISHOP_CROSSBOW = 0.4f;

    #endregion


    #region Protection

    #region Building

    private readonly float PERCENT_PROTECTION_CITY_FOR_KING = 0.15f;
    private readonly float PERCENT_PROTECTION_CITY_FOR_PAWN = 0.15f;
    private readonly float PERCENT_PROTECTION_CITY_FOR_PAWN_SWORD = 0.15f;
    private readonly float PERCENT_PROTECTION_CITY_FOR_ROOK = 0.15f;
    private readonly float PERCENT_PROTECTION_CITY_FOR_ROOK_CROSSBOW = 0.15f;
    private readonly float PERCENT_PROTECTION_CITY_FOR_BISHOP = 0.15f;
    private readonly float PERCENT_PROTECTION_CITY_FOR_BISHOP_CROSSBOW = 0.15f;

    public int PROTECTION_CITY_KING => (int)(SIMPLE_POWER_DAMAGE_KING * PERCENT_PROTECTION_CITY_FOR_KING);
    public int PROTECTION_CITY_PAWN => (int)(SIMPLE_POWER_DAMAGE_PAWN * PERCENT_PROTECTION_CITY_FOR_PAWN);
    public int PROTECTION_CITY_PAWN_SWORD => (int)(SIMPLE_POWER_DAMAGE_PAWN_SWORD * PERCENT_PROTECTION_CITY_FOR_PAWN_SWORD);
    public int PROTECTION_CITY_ROOK => (int)(SIMPLE_POWER_DAMAGE_ROOK * PERCENT_PROTECTION_CITY_FOR_ROOK);
    public int PROTECTION_CITY_ROOK_CROSSBOW => (int)(SIMPLE_POWER_DAMAGE_ROOK_CROSSBOW * PERCENT_PROTECTION_CITY_FOR_ROOK_CROSSBOW);
    public int PROTECTION_CITY_BISHOP => (int)(SIMPLE_POWER_DAMAGE_BISHOP * PERCENT_PROTECTION_CITY_FOR_BISHOP);
    public int PROTECTION_CITY_BISHOP_CROSSBOW => (int)(SIMPLE_POWER_DAMAGE_BISHOP_CROSSBOW * PERCENT_PROTECTION_CITY_FOR_BISHOP_CROSSBOW);

    #endregion


    #region Environment

    private readonly float PERCENT_PROTECTION_FOOD_FOR_KING = -0.1f;
    private readonly float PERCENT_PROTECTION_FOOD_FOR_PAWN = -0.1f;
    private readonly float PERCENT_PROTECTION_FOOD_FOR_PAWN_SWORD = -0.1f;
    private readonly float PERCENT_PROTECTION_FOOD_FOR_ROOK = -0.1f;
    private readonly float PERCENT_PROTECTION_FOOD_FOR_ROOK_CROSSBOW = -0.1f;
    private readonly float PERCENT_PROTECTION_FOOD_FOR_BISHOP = -0.1f;
    private readonly float PERCENT_PROTECTION_FOOD_FOR_BISHOP_CROSSBOW = -0.1f;

    private readonly float PERCENT_PROTECTION_TREE_FOR_KING = 0.15f;
    private readonly float PERCENT_PROTECTION_TREE_FOR_PAWN = 0.15f;
    private readonly float PERCENT_PROTECTION_TREE_FOR_PAWN_SWORD = 0.15f;
    private readonly float PERCENT_PROTECTION_TREE_FOR_ROOK = 0.15f;
    private readonly float PERCENT_PROTECTION_TREE_FOR_ROOK_CROSSBOW = 0.15f;
    private readonly float PERCENT_PROTECTION_TREE_FOR_BISHOP = 0.15f;
    private readonly float PERCENT_PROTECTION_TREE_FOR_BISHOP_CROSSBOW = 0.15f;

    private readonly float PERCENT_PROTECTION_HILL_FOR_KING = 0.15f;
    private readonly float PERCENT_PROTECTION_HILL_FOR_PAWN = 0.15f;
    private readonly float PERCENT_PROTECTION_HILL_FOR_PAWN_SWORD = 0.15f;
    private readonly float PERCENT_PROTECTION_HILL_FOR_ROOK = 0.15f;
    private readonly float PERCENT_PROTECTION_HILL_FOR_ROOK_CROSSBOW = 0.15f;
    private readonly float PERCENT_PROTECTION_HILL_FOR_BISHOP = 0.15f;
    private readonly float PERCENT_PROTECTION_HILL_FOR_BISHOP_CROSSBOW = 0.15f;


    public int PROTECTION_FOOD_FOR_KING => (int)(SIMPLE_POWER_DAMAGE_KING * PERCENT_PROTECTION_FOOD_FOR_KING);
    public int PROTECTION_FOOD_FOR_PAWN => (int)(SIMPLE_POWER_DAMAGE_PAWN * PERCENT_PROTECTION_FOOD_FOR_PAWN);
    public int PROTECTION_FOOD_FOR_PAWN_SWORD => (int)(SIMPLE_POWER_DAMAGE_PAWN_SWORD * PERCENT_PROTECTION_FOOD_FOR_PAWN_SWORD);
    public int PROTECTION_FOOD_FOR_ROOK => (int)(SIMPLE_POWER_DAMAGE_ROOK * PERCENT_PROTECTION_FOOD_FOR_ROOK);
    public int PROTECTION_FOOD_FOR_ROOK_CROSSBOW => (int)(SIMPLE_POWER_DAMAGE_ROOK_CROSSBOW * PERCENT_PROTECTION_FOOD_FOR_ROOK_CROSSBOW);
    public int PROTECTION_FOOD_FOR_BISHOP => (int)(SIMPLE_POWER_DAMAGE_BISHOP * PERCENT_PROTECTION_FOOD_FOR_BISHOP);
    public int PROTECTION_FOOD_FOR_BISHOP_CROSSBOW => (int)(SIMPLE_POWER_DAMAGE_BISHOP_CROSSBOW * PERCENT_PROTECTION_FOOD_FOR_BISHOP_CROSSBOW);

    public int PROTECTION_HILL_FOR_KING => (int)(SIMPLE_POWER_DAMAGE_KING * PERCENT_PROTECTION_HILL_FOR_KING);
    public int PROTECTION_HILL_FOR_PAWN => (int)(SIMPLE_POWER_DAMAGE_PAWN * PERCENT_PROTECTION_HILL_FOR_PAWN);
    public int PROTECTION_HILL_FOR_PAWN_SWORD => (int)(SIMPLE_POWER_DAMAGE_PAWN_SWORD * PERCENT_PROTECTION_HILL_FOR_PAWN_SWORD);
    public int PROTECTION_HILL_FOR_ROOK => (int)(SIMPLE_POWER_DAMAGE_ROOK * PERCENT_PROTECTION_HILL_FOR_ROOK);
    public int PROTECTION_HILL_FOR_ROOK_CROSSBOW => (int)(SIMPLE_POWER_DAMAGE_ROOK_CROSSBOW * PERCENT_PROTECTION_HILL_FOR_ROOK_CROSSBOW);
    public int PROTECTION_HILL_FOR_BISHOP => (int)(SIMPLE_POWER_DAMAGE_BISHOP * PERCENT_PROTECTION_HILL_FOR_BISHOP);
    public int PROTECTION_HILL_FOR_BISHOP_CROSSBOW => (int)(SIMPLE_POWER_DAMAGE_BISHOP_CROSSBOW * PERCENT_PROTECTION_HILL_FOR_BISHOP_CROSSBOW);

    public int PROTECTION_TREE_FOR_KING => (int)(SIMPLE_POWER_DAMAGE_KING * PERCENT_PROTECTION_TREE_FOR_KING);
    public int PROTECTION_TREE_FOR_PAWN => (int)(SIMPLE_POWER_DAMAGE_PAWN * PERCENT_PROTECTION_TREE_FOR_PAWN);
    public int PROTECTION_TREE_FOR_PAWN_SWORD => (int)(SIMPLE_POWER_DAMAGE_PAWN_SWORD * PERCENT_PROTECTION_TREE_FOR_PAWN_SWORD);
    public int PROTECTION_TREE_FOR_ROOK => (int)(SIMPLE_POWER_DAMAGE_ROOK * PERCENT_PROTECTION_TREE_FOR_ROOK);
    public int PROTECTION_TREE_FOR_ROOK_CROSSBOW => (int)(SIMPLE_POWER_DAMAGE_ROOK_CROSSBOW * PERCENT_PROTECTION_TREE_FOR_ROOK_CROSSBOW);
    public int PROTECTION_TREE_FOR_BISHOP => (int)(SIMPLE_POWER_DAMAGE_BISHOP * PERCENT_PROTECTION_TREE_FOR_BISHOP);
    public int PROTECTION_TREE_FOR_BISHOP_CROSSBOW => (int)(SIMPLE_POWER_DAMAGE_BISHOP_CROSSBOW * PERCENT_PROTECTION_TREE_FOR_BISHOP_CROSSBOW);

    #endregion


    #region Click

    public readonly float PERCENT_FOR_PROTECTION_KING = 0.3f;
    public readonly float PERCENT_FOR_PROTECTION_PAWN = 0.3f;
    public readonly float PERCENT_FOR_PROTECTION_PAWN_SWORD = 0.3f;
    public readonly float PERCENT_FOR_PROTECTION_ROOK = 0.3f;
    public readonly float PERCENT_FOR_PROTECTION_ROOK_CROSSBOW = 0.3f;
    public readonly float PERCENT_FOR_PROTECTION_BISHOP = 0.3f;
    public readonly float PERCENT_FOR_PROTECTION_BISHOP_CROSSBOW = 0.3f;

    #endregion

    #endregion


    #region Step

    public readonly int NEED_AMOUNT_STEPS_FOOD = 0;
    public readonly int NEED_AMOUNT_STEPS_TREE = 2;
    public readonly int NEED_AMOUNT_STEPS_HILL = 2;

    public readonly int STANDART_AMOUNT_STEPS_KING = 1;
    public readonly int STANDART_AMOUNT_STEPS_PAWN = 2;
    public readonly int STANDART_AMOUNT_STEPS_PAWN_SWORD = 2;
    public readonly int STANDART_AMOUNT_STEPS_ROOK = 2;
    public readonly int STANDART_AMOUNT_STEPS_ROOK_CROSSBOW = 2;
    public readonly int STANDART_AMOUNT_STEPS_BISHOP = 2;
    public readonly int STANDART_AMOUNT_STEPS_BISHOP_CROSSBOW = 2;

    #endregion

    #endregion

    #endregion

}
