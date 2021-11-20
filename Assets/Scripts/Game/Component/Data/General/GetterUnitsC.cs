using System.Collections.Generic;

namespace Game.Game
{
    public struct GetterUnitsC
    {
        private static Dictionary<UnitTypes, bool> _needCreateButtons;
        private static Dictionary<UnitTypes, float> _curTimers;

        public GetterUnitsC(Dictionary<UnitTypes, bool> needCreateButtons)
        {
            _needCreateButtons = needCreateButtons;

            _needCreateButtons.Add(UnitTypes.Pawn, default);
            _needCreateButtons.Add(UnitTypes.Archer, default);


            _curTimers = new Dictionary<UnitTypes, float>();

            _curTimers.Add(UnitTypes.Pawn, default);
            _curTimers.Add(UnitTypes.Archer, default);
        }

        public static void ActiveNeedCreateButton(UnitTypes unitType, bool isActive) => _needCreateButtons[unitType] = isActive;
        public static bool IsActivatedButton(UnitTypes unitType) => _needCreateButtons[unitType];

        public static float GetTimer(UnitTypes unitType) => _curTimers[unitType];
        public static void AddTimer(UnitTypes unitType, float adding) => _curTimers[unitType] += adding;
        public static void ResetCurTimer(UnitTypes unitType) => _curTimers[unitType] = default;
    }
}
