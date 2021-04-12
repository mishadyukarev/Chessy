public class NameValueManager
{
    #region Economy

    internal readonly int GOLD_MASTER = 100;
    internal readonly int GOLD_OTHER = 100;

    internal readonly int GOLD_FOR_BUYING_PAWN = 50;

    #endregion


    #region Inventor

    internal readonly int TAKE_UNIT = 1;

    internal readonly int AMOUNT_MASTER_PAWN = 1;
    internal readonly int AMOUNT_OTHER_PAWN = 3;

    #endregion


    #region Units

    internal readonly int AMOUNT_FOR_TAKE_UNIT = 1;
    public readonly int AMOUNT_FOR_DEATH = 0;

    public readonly int AMOUNT_STEPS_PAWN = 1;
    public readonly int TAKE_AMOUNT_STEPS = 1;
    public readonly int AMOUNT_HEALTH_PAWN = 100;
    public readonly int POWER_DAMAGE_PAWN = 50;
    public readonly int PROTECTION_PAWN = 10;

    #endregion


    #region Environment

    public readonly int PROTECTION_HILL = 10;
    public readonly int PROTECTION_TREE = 20;
    internal readonly int PROTECTION_CAMP = 25;


    #region For Spawn

    public readonly int PERCENT_TREE;
    public readonly int PERCENT_HILL;
    public readonly int PERCENT_MOUNTAIN;

    #endregion

    #endregion


    #region Cells

    public readonly int CELL_COUNT_X;
    public readonly int CELL_COUNT_Y;
    public readonly int CELL_COUNT_XY;

    public readonly int X = 0;
    public readonly int Y = 1;
    public readonly int XY_FOR_ARRAY = 2;

    #endregion


    #region Photon

    public readonly int NUMBER_PHOTON_VIEW = 1001;

    #endregion


    #region Tags

    public readonly string TAG_CELL = "Cell";
    public readonly string TAG_BACKGROUND = "Background";

    #endregion



    public NameValueManager(int percentTree, int percentHill, int percentMountain, int cellCountX, int cellCountY)
    {
        PERCENT_TREE = percentTree;
        PERCENT_HILL = percentHill;
        PERCENT_MOUNTAIN = percentMountain;

        CELL_COUNT_X = cellCountX;
        CELL_COUNT_Y = cellCountY;
        CELL_COUNT_XY = cellCountX * cellCountY;
    }
}
