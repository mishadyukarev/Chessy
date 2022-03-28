using Chessy.Game.Entity.Model;
using Chessy.Game.Entity.Model.Cell.Unit;

namespace Chessy.Game.Model.System
{
    sealed class ClearUnitS
    {
        readonly UnitEs _unitEs;

        internal ClearUnitS(in UnitEs unitEs)
        {
            _unitEs = unitEs;
        }

        internal void Clear()
        {
            _unitEs.MainE.UnitTC.Unit = UnitTypes.None;
        }
    }
}