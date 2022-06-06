using Chessy.Game.Model.Entity.Cell.Unit;

namespace Chessy.Game.Model.System
{
    struct ClearUnitS
    {
        readonly UnitEs _unitEs;

        internal ClearUnitS(in UnitEs unitEs)
        {
            _unitEs = unitEs;
        }

        internal void Clear()
        {
            _unitEs.MainE.UnitTC.UnitT = UnitTypes.None;
        }
    }
}