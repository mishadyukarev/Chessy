using System.Collections.Generic;

namespace Chessy.Model.Model.Component
{
    public readonly struct IsStartedCellC
    {
        readonly Dictionary<PlayerTypes, bool> _isStartedCell;
        public bool IsStartedCell(in PlayerTypes playerT) => _isStartedCell[playerT];

        internal IsStartedCellC(in Dictionary<PlayerTypes, bool> isStarted) => _isStartedCell = isStarted;
    }
}