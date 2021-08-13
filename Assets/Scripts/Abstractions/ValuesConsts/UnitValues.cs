namespace Assets.Scripts.Abstractions.ValuesConsts
{
    public static class UnitValues
    {
        #region Health

        public const int STANDART_AMOUNT_HEALTH_KING = 300;
        public const int STANDART_AMOUNT_HEALTH_PAWN_AXE = 100;
        public const int STANDART_AMOUNT_HEALTH_PAWN_SWORD = 150;
        public const int STANDART_AMOUNT_HEALTH_ROOK_BOW = 100;
        public const int STANDART_AMOUNT_HEALTH_ROOK_CROSSBOW = 100;
        public const int STANDART_AMOUNT_HEALTH_BISHOP_BOW = 100;
        public const int STANDART_AMOUNT_HEALTH_BISHOP_CROSSBOW = 100;

        public const float PERCENT_FOR_HEALTH_KING = 0.2f;
        public const float PERCENT_FOR_HEALTH_PAWN_AXE = 0.3f;
        public const float PERCENT_FOR_HEALTH_PAWN_SWORD = 0.2f;
        public const float PERCENT_FOR_HEALTH_ROOK_BOW = 0.3f;
        public const float PERCENT_FOR_HEALTH_ROOK_CROSSBOW = 0.3f;
        public const float PERCENT_FOR_HEALTH_BISHOP_BOW = 0.3f;
        public const float PERCENT_FOR_HEALTH_BISHOP_CROSSBOW = 0.3f;

        #endregion


        #region Damage

        public const int SIMPLE_POWER_DAMAGE_KING = 110;
        public const int SIMPLE_POWER_DAMAGE_PAWN = 50;
        public const int SIMPLE_POWER_DAMAGE_PAWN_SWORD = 75;
        public const int SIMPLE_POWER_DAMAGE_ROOK = 25;
        public const int SIMPLE_POWER_DAMAGE_ROOK_CROSSBOW = 50;
        public const int SIMPLE_POWER_DAMAGE_BISHOP = 25;
        public const int SIMPLE_POWER_DAMAGE_BISHOP_CROSSBOW = 50;

        public const float RATION_UNIQUE_POWER_DAMAGE_KING = 0.4f;
        public const float RATION_UNIQUE_POWER_DAMAGE_PAWN = 0.4f;
        public const float RATION_UNIQUE_POWER_DAMAGE_PAWN_SWORD = 0.4f;
        public const float RATION_UNIQUE_POWER_DAMAGE_ROOK = 0.4f;
        public const float RATION_UNIQUE_POWER_DAMAGE_ROOK_CROSSBOW = 0.4f;
        public const float RATION_UNIQUE_POWER_DAMAGE_BISHOP_BOW = 0.4f;
        public const float RATION_UNIQUE_POWER_DAMAGE_BISHOP_CROSSBOW = 0.4f;

        #endregion


        #region Protection

        #region Building

        private const float PERCENT_PROTECTION_CITY_FOR_KING = 0.15f;
        private const float PERCENT_PROTECTION_CITY_FOR_PAWN = 0.15f;
        private const float PERCENT_PROTECTION_CITY_FOR_PAWN_SWORD = 0.15f;
        private const float PERCENT_PROTECTION_CITY_FOR_ROOK = 0.15f;
        private const float PERCENT_PROTECTION_CITY_FOR_ROOK_CROSSBOW = 0.15f;
        private const float PERCENT_PROTECTION_CITY_FOR_BISHOP = 0.15f;
        private const float PERCENT_PROTECTION_CITY_FOR_BISHOP_CROSSBOW = 0.15f;

        public static int PROTECTION_CITY_KING => (int)(SIMPLE_POWER_DAMAGE_KING * PERCENT_PROTECTION_CITY_FOR_KING);
        public static int PROTECTION_CITY_PAWN => (int)(SIMPLE_POWER_DAMAGE_PAWN * PERCENT_PROTECTION_CITY_FOR_PAWN);
        public static int PROTECTION_CITY_PAWN_SWORD => (int)(SIMPLE_POWER_DAMAGE_PAWN_SWORD * PERCENT_PROTECTION_CITY_FOR_PAWN_SWORD);
        public static int PROTECTION_CITY_ROOK => (int)(SIMPLE_POWER_DAMAGE_ROOK * PERCENT_PROTECTION_CITY_FOR_ROOK);
        public static int PROTECTION_CITY_ROOK_CROSSBOW => (int)(SIMPLE_POWER_DAMAGE_ROOK_CROSSBOW * PERCENT_PROTECTION_CITY_FOR_ROOK_CROSSBOW);
        public static int PROTECTION_CITY_BISHOP => (int)(SIMPLE_POWER_DAMAGE_BISHOP * PERCENT_PROTECTION_CITY_FOR_BISHOP);
        public static int PROTECTION_CITY_BISHOP_CROSSBOW => (int)(SIMPLE_POWER_DAMAGE_BISHOP_CROSSBOW * PERCENT_PROTECTION_CITY_FOR_BISHOP_CROSSBOW);

        #endregion


        #region Environment

        private const float PERCENT_PROTECTION_FOOD_FOR_KING = 0.1f;
        private const float PERCENT_PROTECTION_FOOD_FOR_PAWN = 0.1f;
        private const float PERCENT_PROTECTION_FOOD_FOR_PAWN_SWORD = 0.1f;
        private const float PERCENT_PROTECTION_FOOD_FOR_ROOK = 0.1f;
        private const float PERCENT_PROTECTION_FOOD_FOR_ROOK_CROSSBOW = 0.1f;
        private const float PERCENT_PROTECTION_FOOD_FOR_BISHOP = 0.1f;
        private const float PERCENT_PROTECTION_FOOD_FOR_BISHOP_CROSSBOW = 0.1f;

        private const float PERCENT_PROTECTION_TREE_FOR_KING = 0.15f;
        private const float PERCENT_PROTECTION_TREE_FOR_PAWN = 0.15f;
        private const float PERCENT_PROTECTION_TREE_FOR_PAWN_SWORD = 0.15f;
        private const float PERCENT_PROTECTION_TREE_FOR_ROOK = 0.15f;
        private const float PERCENT_PROTECTION_TREE_FOR_ROOK_CROSSBOW = 0.15f;
        private const float PERCENT_PROTECTION_TREE_FOR_BISHOP = 0.15f;
        private const float PERCENT_PROTECTION_TREE_FOR_BISHOP_CROSSBOW = 0.15f;

        private const float PERCENT_PROTECTION_HILL_FOR_KING = 0.15f;
        private const float PERCENT_PROTECTION_HILL_FOR_PAWN = 0.15f;
        private const float PERCENT_PROTECTION_HILL_FOR_PAWN_SWORD = 0.15f;
        private const float PERCENT_PROTECTION_HILL_FOR_ROOK = 0.15f;
        private const float PERCENT_PROTECTION_HILL_FOR_ROOK_CROSSBOW = 0.15f;
        private const float PERCENT_PROTECTION_HILL_FOR_BISHOP = 0.15f;
        private const float PERCENT_PROTECTION_HILL_FOR_BISHOP_CROSSBOW = 0.15f;


        public static int PROTECTION_FOOD_FOR_KING => (int)(SIMPLE_POWER_DAMAGE_KING * PERCENT_PROTECTION_FOOD_FOR_KING);
        public static int PROTECTION_FOOD_FOR_PAWN => (int)(SIMPLE_POWER_DAMAGE_PAWN * PERCENT_PROTECTION_FOOD_FOR_PAWN);
        public static int PROTECTION_FOOD_FOR_PAWN_SWORD => (int)(SIMPLE_POWER_DAMAGE_PAWN_SWORD * PERCENT_PROTECTION_FOOD_FOR_PAWN_SWORD);
        public static int PROTECTION_FOOD_FOR_ROOK => (int)(SIMPLE_POWER_DAMAGE_ROOK * PERCENT_PROTECTION_FOOD_FOR_ROOK);
        public static int PROTECTION_FOOD_FOR_ROOK_CROSSBOW => (int)(SIMPLE_POWER_DAMAGE_ROOK_CROSSBOW * PERCENT_PROTECTION_FOOD_FOR_ROOK_CROSSBOW);
        public static int PROTECTION_FOOD_FOR_BISHOP => (int)(SIMPLE_POWER_DAMAGE_BISHOP * PERCENT_PROTECTION_FOOD_FOR_BISHOP);
        public static int PROTECTION_FOOD_FOR_BISHOP_CROSSBOW => (int)(SIMPLE_POWER_DAMAGE_BISHOP_CROSSBOW * PERCENT_PROTECTION_FOOD_FOR_BISHOP_CROSSBOW);

        public static int PROTECTION_HILL_FOR_KING => (int)(SIMPLE_POWER_DAMAGE_KING * PERCENT_PROTECTION_HILL_FOR_KING);
        public static int PROTECTION_HILL_FOR_PAWN => (int)(SIMPLE_POWER_DAMAGE_PAWN * PERCENT_PROTECTION_HILL_FOR_PAWN);
        public static int PROTECTION_HILL_FOR_PAWN_SWORD => (int)(SIMPLE_POWER_DAMAGE_PAWN_SWORD * PERCENT_PROTECTION_HILL_FOR_PAWN_SWORD);
        public static int PROTECTION_HILL_FOR_ROOK => (int)(SIMPLE_POWER_DAMAGE_ROOK * PERCENT_PROTECTION_HILL_FOR_ROOK);
        public static int PROTECTION_HILL_FOR_ROOK_CROSSBOW => (int)(SIMPLE_POWER_DAMAGE_ROOK_CROSSBOW * PERCENT_PROTECTION_HILL_FOR_ROOK_CROSSBOW);
        public static int PROTECTION_HILL_FOR_BISHOP => (int)(SIMPLE_POWER_DAMAGE_BISHOP * PERCENT_PROTECTION_HILL_FOR_BISHOP);
        public static int PROTECTION_HILL_FOR_BISHOP_CROSSBOW => (int)(SIMPLE_POWER_DAMAGE_BISHOP_CROSSBOW * PERCENT_PROTECTION_HILL_FOR_BISHOP_CROSSBOW);

        public static int PROTECTION_TREE_FOR_KING => (int)(SIMPLE_POWER_DAMAGE_KING * PERCENT_PROTECTION_TREE_FOR_KING);
        public static int PROTECTION_TREE_FOR_PAWN => (int)(SIMPLE_POWER_DAMAGE_PAWN * PERCENT_PROTECTION_TREE_FOR_PAWN);
        public static int PROTECTION_TREE_FOR_PAWN_SWORD => (int)(SIMPLE_POWER_DAMAGE_PAWN_SWORD * PERCENT_PROTECTION_TREE_FOR_PAWN_SWORD);
        public static int PROTECTION_TREE_FOR_ROOK => (int)(SIMPLE_POWER_DAMAGE_ROOK * PERCENT_PROTECTION_TREE_FOR_ROOK);
        public static int PROTECTION_TREE_FOR_ROOK_CROSSBOW => (int)(SIMPLE_POWER_DAMAGE_ROOK_CROSSBOW * PERCENT_PROTECTION_TREE_FOR_ROOK_CROSSBOW);
        public static int PROTECTION_TREE_FOR_BISHOP => (int)(SIMPLE_POWER_DAMAGE_BISHOP * PERCENT_PROTECTION_TREE_FOR_BISHOP);
        public static int PROTECTION_TREE_FOR_BISHOP_CROSSBOW => (int)(SIMPLE_POWER_DAMAGE_BISHOP_CROSSBOW * PERCENT_PROTECTION_TREE_FOR_BISHOP_CROSSBOW);

        #endregion


        #region Click

        public const float PERCENT_FOR_PROTECTION_KING = 0.3f;
        public const float PERCENT_FOR_PROTECTION_PAWN = 0.3f;
        public const float PERCENT_FOR_PROTECTION_PAWN_SWORD = 0.3f;
        public const float PERCENT_FOR_PROTECTION_ROOK = 0.3f;
        public const float PERCENT_FOR_PROTECTION_ROOK_CROSSBOW = 0.3f;
        public const float PERCENT_FOR_PROTECTION_BISHOP = 0.3f;
        public const float PERCENT_FOR_PROTECTION_BISHOP_CROSSBOW = 0.3f;

        #endregion

        #endregion


        #region Step

        public const int NEED_AMOUNT_STEPS_FOOD = 0;
        public const int NEED_AMOUNT_STEPS_TREE = 2;
        public const int NEED_AMOUNT_STEPS_HILL = 2;

        public const int STANDART_AMOUNT_STEPS_KING = 1;
        public const int STANDART_AMOUNT_STEPS_PAWN = 2;
        public const int STANDART_AMOUNT_STEPS_PAWN_SWORD = 2;
        public const int STANDART_AMOUNT_STEPS_ROOK_BOW = 2;
        public const int STANDART_AMOUNT_STEPS_ROOK_CROSSBOW = 2;
        public const int STANDART_AMOUNT_STEPS_BISHOP_BOW = 2;
        public const int STANDART_AMOUNT_STEPS_BISHOP_CROSSBOW = 2;

        #endregion
    }
}
