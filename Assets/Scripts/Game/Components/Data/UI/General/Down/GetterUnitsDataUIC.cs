using System.Collections.Generic;

namespace Scripts.Game
{
    internal struct GetterUnitsDataUIC
    {
        private static Dictionary<UnitTypes, bool> _needCreateButtons;
        private static Dictionary<UnitTypes, float> _curTimers;

        internal GetterUnitsDataUIC(Dictionary<UnitTypes, bool> needCreateButtons)
        {
            _needCreateButtons = needCreateButtons;

            _needCreateButtons.Add(UnitTypes.Pawn, default);
            _needCreateButtons.Add(UnitTypes.Rook, default);
            _needCreateButtons.Add(UnitTypes.Bishop, default);


            _curTimers = new Dictionary<UnitTypes, float>();

            _curTimers.Add(UnitTypes.Pawn, default);
            _curTimers.Add(UnitTypes.Rook, default);
            _curTimers.Add(UnitTypes.Bishop, default);
        }

        internal static void ActiveNeedCreateButton(UnitTypes unitType, bool isActive) => _needCreateButtons[unitType] = isActive;
        internal static bool IsActivatedButton(UnitTypes unitType) => _needCreateButtons[unitType];

        internal static float GetTimer(UnitTypes unitType) => _curTimers[unitType];
        internal static void AddTimer(UnitTypes unitType, float adding) => _curTimers[unitType] += adding;
        internal static void ResetCurTimer(UnitTypes unitType) => _curTimers[unitType] = default;
    }
}
