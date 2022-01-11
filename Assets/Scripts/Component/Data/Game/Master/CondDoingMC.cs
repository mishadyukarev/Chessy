namespace Game.Game
{
    public struct CondDoingMC
    {
        private static ConditionUnitTypes _condUnit;

        public static void Set(ConditionUnitTypes cond)
        {
            _condUnit = cond;
        }

        public static void Get(out ConditionUnitTypes cond)
        {
            cond = _condUnit;
        }
    }
}
