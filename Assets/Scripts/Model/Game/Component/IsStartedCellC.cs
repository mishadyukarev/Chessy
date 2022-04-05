using System.Collections.Generic;

namespace Chessy.Game.Model.Component
{
    public struct IsStartedCellC
    {
        readonly Dictionary<PlayerTypes, bool> _isStartedCell;
        public bool IsStartedCell(in PlayerTypes playerT) => _isStartedCell[playerT];

        internal IsStartedCellC(in Dictionary<PlayerTypes, bool> isStarted) => _isStartedCell = isStarted;
    }
}