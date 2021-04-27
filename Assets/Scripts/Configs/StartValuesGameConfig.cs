using UnityEngine;

[CreateAssetMenu(menuName = "StartValues", fileName = "StartValues")]
public class StartValuesGameConfig : ScriptableObject
{

    #region ENVIRONMENT

    internal readonly byte PERCENT_FOOD = 15;
    internal readonly byte PERCENT_TREE = 25;
    internal readonly byte PERCENT_HILL = 5;
    internal readonly byte PERCENT_MOUNTAIN = 2;

    internal readonly int PROTECTION_HILL = 10;
    internal readonly int PROTECTION_TREE = 20;

    internal readonly int NEED_AMOUNT_STEPS_TREE = 1;
    internal readonly int NEED_AMOUNT_STEPS_HILL = 2;

    #endregion


    #region Buildings

    internal readonly int PROTECTION_CITY = 25;

    #endregion


    #region ECONOMY

    internal readonly int AMOUNT_GOLD_MASTER = 100;
    internal readonly int AMOUNT_GOLD_OTHER = 100;

    internal readonly int AMOUNT_WOOD_MASTER = 50;
    internal readonly int AMOUNT_WOOD_OTHER = 50;



    internal readonly int GOLD_FOR_BUYING_PAWN = 50;

    internal readonly int WOOD_FOR_BUILDING_FARM = 50;

    #endregion


    #region UNITS

    internal readonly int AMOUNT_KING_MASTER = 1;
    internal readonly int AMOUNT_KING_OTHER = 1;

    internal readonly int AMOUNT_PAWN_MASTER = 1;
    internal readonly int AMOUNT_PAWN_OTHER = 1;


    internal readonly int MAX_AMOUNT_STEPS_KING = 1;
    internal readonly int MAX_AMOUNT_STEPS_PAWN = 2;

    internal readonly int AMOUNT_FOR_TAKE_UNIT = 1;
    internal readonly int MIN_AMOUNT_STEPS_FOR_UNIT = 1;
    internal readonly int AMOUNT_FOR_DEATH = 0;

    internal readonly int AMOUNT_HEALTH_KING = 200;
    internal readonly int AMOUNT_HEALTH_PAWN = 100;

    internal readonly int POWER_DAMAGE_KING = 70;
    internal readonly int POWER_DAMAGE_PAWN = 50;

    private readonly int PERCENT_FOR_HEALTH_KING = 20;
    private readonly int PERCENT_FOR_HEALTH_PAWN = 20;

    private readonly int PERCENT_FOR_PROTECTION_KING = 50;
    private readonly int PERCENT_FOR_PROTECTION_PAWN = 50;

    internal int HEALTH_FOR_ADDING_KING => AMOUNT_HEALTH_KING * PERCENT_FOR_HEALTH_KING / MAX_PERCENT;
    internal int HEALTH_FOR_ADDING_PAWN => AMOUNT_HEALTH_PAWN * PERCENT_FOR_HEALTH_PAWN / MAX_PERCENT;

    internal int PROTECTION_KING => POWER_DAMAGE_KING * PERCENT_FOR_PROTECTION_KING / MAX_PERCENT;
    internal int PROTECTION_PAWN => POWER_DAMAGE_PAWN * PERCENT_FOR_PROTECTION_PAWN / MAX_PERCENT;


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


    #region Else

    private readonly int MAX_PERCENT = 100;

    #endregion

}
