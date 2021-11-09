namespace Chessy.Game
{
    public struct ForFromToNewUnitC
    {
        private static UnitTypes _unitType;
        private static byte _fromIdx;
        private static byte _toIdx;

        public static void Set(UnitTypes unitType, byte fromIdx, byte toIdx)
        {
            _unitType = unitType;
            _fromIdx = fromIdx;
            _toIdx = toIdx;
        }
        public static void Get(out UnitTypes unitType, out byte fromIdx, out byte toIdx)
        {
            unitType = _unitType;
            fromIdx = _fromIdx;
            toIdx = _toIdx;
        }
    }
}