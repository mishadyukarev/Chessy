using System.Collections.Generic;

namespace Chessy.Game
{
    public struct GetterUnitsDataUIC
    {
        private static Dictionary<UnitTypes, bool> _needCreateButtons;
        private static Dictionary<UnitTypes, float> _curTimers;

        public GetterUnitsDataUIC(Dictionary<UnitTypes, bool> needCreateButtons)
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

        public static void ActiveNeedCreateButton(UnitTypes unitType, bool isActive) => _needCreateButtons[unitType] = isActive;
        public static bool IsActivatedButton(UnitTypes unitType) => _needCreateButtons[unitType];

        public static float GetTimer(UnitTypes unitType) => _curTimers[unitType];
        public static void AddTimer(UnitTypes unitType, float adding) => _curTimers[unitType] += adding;
        public static void ResetCurTimer(UnitTypes unitType) => _curTimers[unitType] = default;
    }
}
