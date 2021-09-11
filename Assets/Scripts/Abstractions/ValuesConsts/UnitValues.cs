namespace Assets.Scripts.Abstractions.ValuesConsts
{
    public static class UnitValues
    {
        #region Health

        public const int STANDART_AMOUNT_HEALTH_KING = 300;
        public const int STANDART_AMOUNT_HEALTH_PAWN = 100;
        public const int STANDART_AMOUNT_HEALTH_ROOK = 50;
        public const int STANDART_AMOUNT_HEALTH_BISHOP = 50;

        public const int FOR_ADD_HEALTH_KING = (int)(STANDART_AMOUNT_HEALTH_KING * 0.2f);
        public const int FOR_ADD_HEALTH_PAWN = (int)(STANDART_AMOUNT_HEALTH_PAWN * 0.4f);
        public const int FOR_ADD_HEALTH_ROOK = (int)(STANDART_AMOUNT_HEALTH_ROOK * 0.4f);
        public const int FOR_ADD_HEALTH_BISHOP = (int)(STANDART_AMOUNT_HEALTH_BISHOP * 0.4f);

        #endregion


        #region Damage

        public const int SIMPLE_POWER_DAMAGE_KING = 100;
        public const int SIMPLE_POWER_DAMAGE_PAWN = 50;
        public const int SIMPLE_POWER_DAMAGE_ROOK_AND_BISHOP = 40;

        #endregion


        #region Protection

        #region Building


        #endregion


        #region Environment

        public static int PROTECTION_FOOD_FOR_KING => (int)(SIMPLE_POWER_DAMAGE_KING * 0.1f);
        public static int PROTECTION_FOOD_FOR_PAWN => (int)(SIMPLE_POWER_DAMAGE_PAWN * 0.1f);
        public static int PROTECTION_FOOD_FOR_ROOK_AND_BISHOP => (int)(SIMPLE_POWER_DAMAGE_ROOK_AND_BISHOP * 0.1f);

        public static int PROTECTION_HILL_FOR_KING => (int)(SIMPLE_POWER_DAMAGE_KING * 0.15f);
        public static int PROTECTION_HILL_FOR_PAWN => (int)(SIMPLE_POWER_DAMAGE_PAWN * 0.15f);
        public static int PROTECTION_HILL_FOR_ROOK_AND_BISHOP => (int)(SIMPLE_POWER_DAMAGE_ROOK_AND_BISHOP * 0.15f);

        public static int PROTECTION_TREE_FOR_KING => (int)(SIMPLE_POWER_DAMAGE_KING * 0.15f);
        public static int PROTECTION_TREE_FOR_PAWN => (int)(SIMPLE_POWER_DAMAGE_PAWN * 0.15f);
        public static int PROTECTION_TREE_FOR_ROOK_AND_BISHOP => (int)(SIMPLE_POWER_DAMAGE_ROOK_AND_BISHOP * 0.15f);

        #endregion


        #region Click

        public const float PERCENT_FOR_PROTECTION_KING = 0.3f;
        public const float PERCENT_FOR_PROTECTION_PAWN = 0.3f;
        public const float PERCENT_FOR_PROTECTION_ROOK = 0.3f;
        public const float PERCENT_FOR_PROTECTION_BISHOP = 0.3f;

        #endregion

        #endregion


        #region Step

        public const int NEED_AMOUNT_STEPS_FERTILIZE = 0;
        public const int NEED_AMOUNT_STEPS_ADULTTREE = 2;
        public const int NEED_AMOUNT_STEPS_HILL = 2;

        public const int STANDART_AMOUNT_STEPS_KING = 1;
        public const int STANDART_AMOUNT_STEPS_PAWN = 2;
        public const int STANDART_AMOUNT_STEPS_ROOK = 2;
        public const int STANDART_AMOUNT_STEPS_BISHOP = 2;

        #endregion
    }
}
