using UnityEngine;

[CreateAssetMenu(menuName = "StartValues", fileName = "StartValues")]
public class StartValuesGameConfig : ScriptableObject
{
    [SerializeField] internal bool IS_TEST = true;


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

    internal readonly int AMOUNT_ROOK_MASTER = 1;
    internal readonly int AMOUNT_ROOK_OTHER = 1;

    internal readonly int AMOUNT_BISHOP_MASTER = 1;
    internal readonly int AMOUNT_BISHOP_OTHER = 1;


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

    internal readonly int ORE_FOR_MELTING_ORE = 5;
    internal readonly int WOOD_FOR_MELTING_ORE = 50;

    internal readonly int IRON_FOR_BUYING_ROOK = 1;
    internal readonly int FOOD_FOR_BUYING_ROOK = 50;

    internal readonly int IRON_FOR_BUYING_BISHOP = 1;
    internal readonly int FOOD_FOR_BUYING_BISHOP = 50;


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

    internal readonly int GOLD_FOR_BUILDING_MINE = 0;
    internal readonly int FOOD_FOR_BUILDING_MINE = 0;
    internal readonly int WOOD_FOR_BUILDING_MINE = 150;
    internal readonly int ORE_FOR_BUILDING_MINE = 0;
    internal readonly int IRON_FOR_BUILDING_MINE = 0;

    #endregion

    #endregion


    #region Benefit

    internal readonly int BENEFIT_FOOD_FARM = 5;
    internal readonly int BENEFIT_WOOD_WOODCUTTER = 2;
    internal readonly int BENEFIT_ORE_MINE = 1;

    internal readonly int BENEFIT_FOOD_CITY = 10;
    internal readonly int BENEFIT_WOOD_CITY = 5;

    #endregion

    #endregion


    #region UNIT

    #region Stats

    #region Health

    internal readonly int AMOUNT_HEALTH_KING = 300;
    internal readonly int AMOUNT_HEALTH_PAWN = 100;
    internal readonly int AMOUNT_HEALTH_ROOK = 100;
    internal readonly int AMOUNT_HEALTH_BISHOP = 100;


    private readonly float PERCENT_FOR_HEALTH_KING = 0.15f;
    private readonly float PERCENT_FOR_HEALTH_PAWN = 0.15f;

    internal int HEALTH_FOR_ADDING_KING => (int)(AMOUNT_HEALTH_KING * PERCENT_FOR_HEALTH_KING);
    internal int HEALTH_FOR_ADDING_PAWN => (int)(AMOUNT_HEALTH_PAWN * PERCENT_FOR_HEALTH_PAWN);

    #endregion


    #region Damage

    internal readonly int SIMPLE_POWER_DAMAGE_KING = 70;
    internal readonly int SIMPLE_POWER_DAMAGE_PAWN = 50;
    internal readonly int SIMPLE_POWER_DAMAGE_ROOK = 50;
    internal readonly int SIMPLE_POWER_DAMAGE_BISHOP = 50;

    internal int UNIQIE_POWER_DAMAGE_KING => SIMPLE_POWER_DAMAGE_KING / 3;
    internal int UNIQIE_POWER_DAMAGE_PAWN => SIMPLE_POWER_DAMAGE_PAWN / 3;
    internal int UNIQIE_POWER_DAMAGE_ROOK => SIMPLE_POWER_DAMAGE_ROOK / 3;
    internal int UNIQIE_POWER_DAMAGE_BISHOP => SIMPLE_POWER_DAMAGE_BISHOP / 3;

    #endregion


    #region Protection

    #region Building

    private readonly float PERCENT_PROTECTION_CITY_FOR_KING = 0.15f;
    private readonly float PERCENT_PROTECTION_CITY_FOR_PAWN = 0.15f;
    private readonly float PERCENT_PROTECTION_CITY_FOR_ROOK = 0.15f;
    private readonly float PERCENT_PROTECTION_CITY_FOR_BISHOP = 0.15f;

    internal int PROTECTION_CITY_KING => (int)(SIMPLE_POWER_DAMAGE_KING * PERCENT_PROTECTION_CITY_FOR_KING);
    internal int PROTECTION_CITY_PAWN => (int)(SIMPLE_POWER_DAMAGE_PAWN * PERCENT_PROTECTION_CITY_FOR_PAWN);
    internal int PROTECTION_CITY_ROOK => (int)(SIMPLE_POWER_DAMAGE_ROOK * PERCENT_PROTECTION_CITY_FOR_ROOK);
    internal int PROTECTION_CITY_BISHOP => (int)(SIMPLE_POWER_DAMAGE_BISHOP * PERCENT_PROTECTION_CITY_FOR_BISHOP);

    #endregion


    #region Environment

    private readonly float PERCENT_PROTECTION_FOOD_FOR_KING = -0.1f;
    private readonly float PERCENT_PROTECTION_FOOD_FOR_PAWN = -0.1f;
    private readonly float PERCENT_PROTECTION_FOOD_FOR_ROOK = -0.1f;
    private readonly float PERCENT_PROTECTION_FOOD_FOR_BISHOP = -0.1f;

    private readonly float PERCENT_PROTECTION_TREE_FOR_KING = 0.1f;
    private readonly float PERCENT_PROTECTION_TREE_FOR_PAWN = 0.1f;
    private readonly float PERCENT_PROTECTION_TREE_FOR_ROOK = 0.1f;
    private readonly float PERCENT_PROTECTION_TREE_FOR_BISHOP = 0.1f;

    private readonly float PERCENT_PROTECTION_HILL_FOR_KING = 0.15f;
    private readonly float PERCENT_PROTECTION_HILL_FOR_PAWN = 0.15f;
    private readonly float PERCENT_PROTECTION_HILL_FOR_ROOK = 0.15f;
    private readonly float PERCENT_PROTECTION_HILL_FOR_BISHOP = 0.15f;


    internal int PROTECTION_FOOD_FOR_KING => (int)(SIMPLE_POWER_DAMAGE_KING * PERCENT_PROTECTION_FOOD_FOR_KING);
    internal int PROTECTION_FOOD_FOR_PAWN => (int)(SIMPLE_POWER_DAMAGE_PAWN * PERCENT_PROTECTION_FOOD_FOR_PAWN);
    internal int PROTECTION_FOOD_FOR_ROOK => (int)(SIMPLE_POWER_DAMAGE_ROOK * PERCENT_PROTECTION_FOOD_FOR_ROOK);
    internal int PROTECTION_FOOD_FOR_BISHOP => (int)(SIMPLE_POWER_DAMAGE_BISHOP * PERCENT_PROTECTION_FOOD_FOR_BISHOP);

    internal int PROTECTION_HILL_FOR_KING => (int)(SIMPLE_POWER_DAMAGE_KING * PERCENT_PROTECTION_HILL_FOR_KING);
    internal int PROTECTION_HILL_FOR_PAWN => (int)(SIMPLE_POWER_DAMAGE_PAWN * PERCENT_PROTECTION_HILL_FOR_PAWN);
    internal int PROTECTION_HILL_FOR_ROOK => (int)(SIMPLE_POWER_DAMAGE_ROOK * PERCENT_PROTECTION_HILL_FOR_ROOK);
    internal int PROTECTION_HILL_FOR_BISHOP => (int)(SIMPLE_POWER_DAMAGE_BISHOP * PERCENT_PROTECTION_HILL_FOR_BISHOP);

    internal int PROTECTION_TREE_FOR_KING => (int)(SIMPLE_POWER_DAMAGE_KING * PERCENT_PROTECTION_TREE_FOR_KING);
    internal int PROTECTION_TREE_FOR_PAWN => (int)(SIMPLE_POWER_DAMAGE_PAWN * PERCENT_PROTECTION_TREE_FOR_PAWN);
    internal int PROTECTION_TREE_FOR_ROOK => (int)(SIMPLE_POWER_DAMAGE_ROOK * PERCENT_PROTECTION_TREE_FOR_ROOK);
    internal int PROTECTION_TREE_FOR_BISHOP => (int)(SIMPLE_POWER_DAMAGE_BISHOP * PERCENT_PROTECTION_TREE_FOR_BISHOP);

    #endregion


    #region Click

    private readonly float PERCENT_FOR_PROTECTION_KING = 0.15f;
    private readonly float PERCENT_FOR_PROTECTION_PAWN = 0.15f;
    private readonly float PERCENT_FOR_PROTECTION_ROOK = 0.15f;
    private readonly float PERCENT_FOR_PROTECTION_BISHOP = 0.15f;

    internal int PROTECTION_KING => (int)(SIMPLE_POWER_DAMAGE_KING * PERCENT_FOR_PROTECTION_KING);
    internal int PROTECTION_PAWN => (int)(SIMPLE_POWER_DAMAGE_PAWN * PERCENT_FOR_PROTECTION_PAWN);
    internal int PROTECTION_ROOK => (int)(SIMPLE_POWER_DAMAGE_ROOK * PERCENT_FOR_PROTECTION_ROOK);
    internal int PROTECTION_BISHOP => (int)(SIMPLE_POWER_DAMAGE_BISHOP * PERCENT_FOR_PROTECTION_BISHOP);

    #endregion

    #endregion


    #region Step

    internal readonly int NEED_AMOUNT_STEPS_FOOD = 0;
    internal readonly int NEED_AMOUNT_STEPS_TREE = 2;
    internal readonly int NEED_AMOUNT_STEPS_HILL = 2;

    internal readonly int STANDART_AMOUNT_STEPS_KING = 1;
    internal readonly int STANDART_AMOUNT_STEPS_PAWN = 1;
    internal readonly int STANDART_AMOUNT_STEPS_ROOK = 1;
    internal readonly int STANDART_AMOUNT_STEPS_BISHOP = 1;

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
