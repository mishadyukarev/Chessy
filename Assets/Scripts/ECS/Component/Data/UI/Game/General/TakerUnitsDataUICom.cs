using System.Collections.Generic;

namespace Assets.Scripts.ECS.Component.Data.UI.Game.General
{
    internal struct TakerUnitsDataUICom
    {
        private Dictionary<UnitTypes, bool> _needCreateButtons;
        private Dictionary<UnitTypes, float> _curTimers;

        internal TakerUnitsDataUICom(Dictionary<UnitTypes, bool> needCreateButtons)
        {
            _needCreateButtons = needCreateButtons;

            _needCreateButtons.Add(UnitTypes.Pawn_Axe, default);
            _needCreateButtons.Add(UnitTypes.Rook_Bow, default);
            _needCreateButtons.Add(UnitTypes.Bishop_Bow, default);


            _curTimers = new Dictionary<UnitTypes, float>();

            _curTimers.Add(UnitTypes.Pawn_Axe, default);
            _curTimers.Add(UnitTypes.Rook_Bow, default);
            _curTimers.Add(UnitTypes.Bishop_Bow, default);
        }

        internal void ActiveNeedCreateButton(UnitTypes unitType, bool isActive) => _needCreateButtons[unitType] = isActive;
        internal bool IsActivatedButton(UnitTypes unitType) => _needCreateButtons[unitType];

        internal float GetTimer(UnitTypes unitType) => _curTimers[unitType];
        internal void AddTimer(UnitTypes unitType, float adding) => _curTimers[unitType] += adding;
        internal void ResetCurTimer(UnitTypes unitType) => _curTimers[unitType] = default;
    }
}
