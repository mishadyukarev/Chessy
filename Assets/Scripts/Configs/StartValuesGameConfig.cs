using UnityEngine;

[CreateAssetMenu(menuName = "StartValues", fileName = "StartValues")]
public class StartValuesGameConfig : ScriptableObject
{
    [SerializeField] internal bool IS_TEST;


    #region PERCENT ENVIRONMENT

    internal readonly byte PERCENT_FOOD = 25;
    internal readonly byte PERCENT_TREE = 10;
    internal readonly byte PERCENT_HILL = 10;
    internal readonly byte PERCENT_MOUNTAIN = 2;

    #endregion


    #region ECONOMY

    #region Unit

    internal readonly int AMOUNT_KING_MASTER = 1;
    internal readonly int AMOUNT_KING_OTHER = 1;

    internal readonly int AMOUNT_PAWN_MASTER = 1;
    internal readonly int AMOUNT_PAWN_OTHER = 1;

    #endregion


    #region Stats

    internal readonly int AMOUNT_GOLD_MASTER = 0;
    internal readonly int AMOUNT_GOLD_OTHER = 0;

    internal readonly int AMOUNT_FOOD_MASTER = 200;
    internal readonly int AMOUNT_FOOD_OTHER = 200;

    internal readonly int AMOUNT_WOOD_MASTER = 100;
    internal readonly int AMOUNT_WOOD_OTHER = 100;

    internal readonly int AMOUNT_ORE_MASTER = 0;
    internal readonly int AMOUNT_ORE_OTHER = 0;

    internal readonly int AMOUNT_IRON_MASTER = 0;
    internal readonly int AMOUNT_IRON_OTHER = 0;

    #endregion


    #region Costs

    internal readonly int FOOD_FOR_BUYING_PAWN = 50;

    #region Building

    internal readonly int GOLD_FOR_BUILDING_FARM = 0;
    internal readonly int FOOD_FOR_BUILDING_FARM = 0;
    internal readonly int WOOD_FOR_BUILDING_FARM = 50;
    internal readonly int ORE_FOR_BUILDING_FARM = 0;
    internal readonly int IRON_FOR_BUILDING_FARM = 0;

    internal readonly int GOLD_FOR_BUILDING_WOODCUTTER = 0;
    internal readonly int FOOD_FOR_BUILDING_WOODCUTTER = 0;
    internal readonly int WOOD_FOR_BUILDING_WOODCUTTER = 50;
    internal readonly int ORE_FOR_BUILDING_WOODCUTTER = 0;
    internal readonly int IRON_FOR_BUILDING_WOODCUTTER = 0;

    #endregion

    #endregion


    #region Benefit

    internal readonly int BENEFIT_FOOD_FARM = 5;
    internal readonly int BENEFIT_WOOD_WOODCUTTER = 2;

    internal readonly int BENEFIT_FOOD_CITY = 10;
    internal readonly int BENEFIT_WOOD_CITY = 5;

    #endregion

    #endregion


    #region UNIT

    #region Stats

    #region Health

    internal readonly int AMOUNT_HEALTH_KING = 300;
    internal readonly int AMOUNT_HEALTH_PAWN = 100;


    private readonly float PERCENT_FOR_HEALTH_KING = 0.15f;
    private readonly float PERCENT_FOR_HEALTH_PAWN = 0.15f;

    internal int HEALTH_FOR_ADDING_KING => (int)(AMOUNT_HEALTH_KING * PERCENT_FOR_HEALTH_KING);
    internal int HEALTH_FOR_ADDING_PAWN => (int)(AMOUNT_HEALTH_PAWN * PERCENT_FOR_HEALTH_PAWN);

    #endregion


    #region Damage

    internal readonly int POWER_DAMAGE_KING = 70;
    internal readonly int POWER_DAMAGE_PAWN = 50;

    #endregion


    #region Protection

    #region Building

    private readonly float PERCENT_PROTECTION_CITY_FOR_KING = 0.15f;
    private readonly float PERCENT_PROTECTION_CITY_FOR_PAWN = 0.15f;

    internal int PROTECTION_CITY_KING => (int)(POWER_DAMAGE_KING * PERCENT_PROTECTION_CITY_FOR_KING);
    internal int PROTECTION_CITY_PAWN => (int)(POWER_DAMAGE_PAWN * PERCENT_PROTECTION_CITY_FOR_PAWN);

    #endregion


    #region Environment

    private readonly float PERCENT_PROTECTION_FOOD_FOR_KING = -0.1f;
    private readonly float PERCENT_PROTECTION_FOOD_FOR_PAWN = -0.1f;

    private readonly float PERCENT_PROTECTION_TREE_FOR_KING = 0.1f;
    private readonly float PERCENT_PROTECTION_TREE_FOR_PAWN = 0.1f;

    private readonly float PERCENT_PROTECTION_HILL_FOR_KING = 0.15f;
    private readonly float PERCENT_PROTECTION_HILL_FOR_PAWN = 0.15f;


    internal int PROTECTION_FOOD_FOR_KING => (int)(POWER_DAMAGE_KING * PERCENT_PROTECTION_FOOD_FOR_KING);
    internal int PROTECTION_FOOD_FOR_PAWN => (int)(POWER_DAMAGE_KING * PERCENT_PROTECTION_FOOD_FOR_PAWN);

    internal int PROTECTION_HILL_FOR_KING => (int)(POWER_DAMAGE_KING * PERCENT_PROTECTION_HILL_FOR_KING);
    internal int PROTECTION_HILL_FOR_PAWN => (int)(POWER_DAMAGE_KING * PERCENT_PROTECTION_HILL_FOR_PAWN);

    internal int PROTECTION_TREE_FOR_KING => (int)(POWER_DAMAGE_KING * PERCENT_PROTECTION_TREE_FOR_KING);
    internal int PROTECTION_TREE_FOR_PAWN => (int)(POWER_DAMAGE_KING * PERCENT_PROTECTION_TREE_FOR_PAWN);

    #endregion


    #region Click

    private readonly float PERCENT_FOR_PROTECTION_KING = 0.15f;
    private readonly float PERCENT_FOR_PROTECTION_PAWN = 0.15f;

    internal int PROTECTION_KING => (int)(POWER_DAMAGE_KING * PERCENT_FOR_PROTECTION_KING);
    internal int PROTECTION_PAWN => (int)(POWER_DAMAGE_PAWN * PERCENT_FOR_PROTECTION_PAWN);

    #endregion

    #endregion


    #region Step

    internal readonly int NEED_AMOUNT_STEPS_FOOD = 0;
    internal readonly int NEED_AMOUNT_STEPS_TREE = 1;
    internal readonly int NEED_AMOUNT_STEPS_HILL = 2;

    internal readonly int MAX_AMOUNT_STEPS_KING = 1;
    internal readonly int MAX_AMOUNT_STEPS_PAWN = 2;

    #endregion

    #endregion


    #region Else

    internal readonly int AMOUNT_FOR_TAKE_UNIT = 1;
    internal readonly int MIN_AMOUNT_STEPS_FOR_UNIT = 1;
    internal readonly int AMOUNT_FOR_DEATH = 0;

    #endregion

    #endregion


    #region CELL

    internal readonly int CELL_COUNT_X = 15;
    internal readonly int CELL_COUNT_Y = 12;
    internal readonly int X = 0;
    internal readonly int Y = 1;
    internal readonly int XY_FOR_ARRAY = 2;

    #endregion


    #region PHOTON

    internal readonly int NUMBER_PHOTON_VIEW = 1001;

    #endregion

}
