namespace Chessy.Game
{
    public struct CondDoingMC
    {
        private static CondUnitTypes _condUnit;

        public static void Set(CondUnitTypes cond)
        {
            _condUnit = cond;
        }

        public static void Get(out CondUnitTypes cond)
        {
            cond = _condUnit;
        }
    }
}
