namespace Assets.Scripts.Abstractions.ValuesConsts
{
    public static class EconomyValues
    {
        public const int NULL_RESOURCES = 0;

        public const int AMOUNT_RESOURCES_TYPES = 5;
        public const int FOOD_NUMBER = 0;
        public const int WOOD_NUMBER = 1;
        public const int ORE_NUMBER = 2;
        public const int IRON_NUMBER = 3;
        public const int GOLD_NUMBER = 4;


        #region Unit

        public const int AMOUNT_KING_MASTER = 1;
        public const int AMOUNT_KING_OTHER = 1;

        public const int AMOUNT_PAWN_MASTER = 1;
        public const int AMOUNT_PAWN_OTHER = 1;

        public const int AMOUNT_ROOK_MASTER = 0;
        public const int AMOUNT_ROOK_OTHER = 0;

        public const int AMOUNT_BISHOP_MASTER = 0;
        public const int AMOUNT_BISHOP_OTHER = 0;

        #endregion


        #region Stats

        public const int AMOUNT_FOOD_MASTER = 25;
        public const int AMOUNT_FOOD_OTHER = 25;

        public const int AMOUNT_WOOD_MASTER = 25;
        public const int AMOUNT_WOOD_OTHER = 25;

        public const int AMOUNT_ORE_MASTER = 0;
        public const int AMOUNT_ORE_OTHER = 0;

        public const int AMOUNT_IRON_MASTER = 6;
        public const int AMOUNT_IRON_OTHER = 6;

        public const int AMOUNT_GOLD_MASTER = 0;
        public const int AMOUNT_GOLD_OTHER = 0;

        #endregion


        #region Costs

        #region Create Unit

        public const int FOOD_FOR_BUYING_PAWN = 5;
        public const int WOOD_FOR_BUYING_PAWN = 0;
        public const int ORE_FOR_BUYING_PAWN = 0;
        public const int IRON_FOR_BUYING_PAWN = 0;
        public const int GOLD_FOR_BUYING_PAWN = 0;

        public const int FOOD_FOR_BUYING_ROOK = 5;
        public const int WOOD_FOR_BUYING_ROOK = 5;
        public const int ORE_FOR_BUYING_ROOK = 0;
        public const int IRON_FOR_BUYING_ROOK = 0;
        public const int GOLD_FOR_BUYING_ROOK = 0;

        public const int FOOD_FOR_BUYING_BISHOP = 5;
        public const int WOOD_FOR_BUYING_BISHOP = 5;
        public const int ORE_FOR_BUYING_BISHOP = 0;
        public const int IRON_FOR_BUYING_BISHOP = 0;
        public const int GOLD_FOR_BUYING_BISHOP = 0;

        #endregion


        #region Upgrade


        #region Units

        public const int FOOD_FOR_UPGRADE_PAWN = 0;
        public const int WOOD_FOR_UPGRADE_PAWN = 0;
        public const int ORE_FOR_UPGRADE_PAWN = 0;
        public const int IRON_FOR_UPGRADE_PAWN = 1;
        public const int GOLD_FOR_UPGRADE_PAWN = 0;

        public const int FOOD_FOR_UPGRADE_ROOK = 0;
        public const int WOOD_FOR_UPGRADE_ROOK = 0;
        public const int ORE_FOR_UPGRADE_ROOK = 0;
        public const int IRON_FOR_UPGRADE_ROOK = 1;
        public const int GOLD_FOR_UPGRADE_ROOK = 0;

        public const int FOOD_FOR_UPGRADE_BISHOP = 0;
        public const int WOOD_FOR_UPGRADE_BISHOP = 0;
        public const int ORE_FOR_UPGRADE_BISHOP = 0;
        public const int IRON_FOR_UPGRADE_BISHOP = 1;
        public const int GOLD_FOR_UPGRADE_BISHOP = 0;

        #endregion


        #region Buildings

        public const int FOOD_FOR_UPGRADE_FARM = 0;
        public const int WoodForUpgradeFarm = 0;
        public const int OreForUpgradeFarm = 0;
        public const int IronForUpgradeFarm = 0;
        public const int GoldForUpgradeFarm = 3;

        public static int FoodForUpgradeWoodcutter = 0;
        public static int WoodForUpgradeWoodcutter = 0;
        public static int OreForUpgradeWoodcutter = 0;
        public static int IronForUpgradeWoodcutter = 0;
        public static int GoldForUpgradeWoodcutter = 3;

        public static int FoodForUpgradeMine = 0;
        public static int WoodForUpgradeMine = 0;
        public static int OreForUpgradeMine = 0;
        public static int IronForUpgradeMine = 0;
        public static int GoldForUpgradeMine = 3;

        #endregion

        #endregion


        #region Melting

        public const int FOOD_FOR_MELTING_ORE = 0;
        public const int WOOD_FOR_MELTING_ORE = 10;
        public const int ORE_FOR_MELTING_ORE = 4;
        public const int IRON_FOR_MELTING_ORE = 0;
        public const int GOLD_FOR_MELTING_ORE = 0;

        #endregion


        #region Fire

        public static int FoodForPawnFire = 0;
        public static int WoodForPawnFire = 0;
        public static int OreForPawnFire = 0;
        public static int IronForPawnFire = 0;
        public static int GoldForPawnFire = 0;

        public static int FoodForPawnSwordFire = 0;
        public static int WoodForPawnSwordFire = 0;
        public static int OreForPawnSwordFire = 0;
        public static int IronForPawnSwordFire = 0;
        public static int GoldForPawnSwordFire = 0;

        public static int FoodForRookFire = 0;
        public static int WoodForRookFire = 0;
        public static int OreForRookFire = 0;
        public static int IronForRookFire = 0;
        public static int GoldForRookFire = 0;

        public static int FoodForRookCrossbowFire = 0;
        public static int WoodForRookCrossbowFire = 0;
        public static int OreForRookCrossbowFire = 0;
        public static int IronForRookCrossbowFire = 0;
        public static int GoldForRookCrossbowFire = 0;

        public static int FoodForBishopFire = 0;
        public static int WoodForBishopFire = 0;
        public static int OreForBishopFire = 0;
        public static int IronForBishopFire = 0;
        public static int GoldForBishopFire = 0;

        public static int FoodForBishopCrossbowFire = 0;
        public static int WoodForBishopCrossbowFire = 0;
        public static int OreForBishopCrossbowFire = 0;
        public static int IronForBishopCrossbowFire = 0;
        public static int GoldForBishopCrossbowFire = 0;


        #endregion


        #region Building

        public const int FOOD_FOR_BUILDING_FARM = 0;
        public const int WOOD_FOR_BUILDING_FARM = 5;
        public const int ORE_FOR_BUILDING_FARM = 0;
        public const int IRON_FOR_BUILDING_FARM = 0;
        public const int GOLD_FOR_BUILDING_FARM = 0;

        public const int FOOD_FOR_BUILDING_MINE = 0;
        public const int WOOD_FOR_BUILDING_MINE = 10;
        public const int ORE_FOR_BUILDING_MINE = 0;
        public const int IRON_FOR_BUILDING_MINE = 0;
        public const int GOLD_FOR_BUILDING_MINE = 0;

        #endregion

        #endregion


        #region Benefit

        public const int BENEFIT_FOOD_FARM = 1;
        public const int BENEFIT_WOOD_WOODCUTTER = 1;
        public const int BENEFIT_ORE_MINE = 1;

        public const int BENEFIT_FOOD_CITY = 1;
        public const int BENEFIT_WOOD_CITY = 1;

        #endregion
    }
}
