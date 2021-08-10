using System.Collections.Generic;

namespace Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell
{
    internal struct CellDataComponent
    {
        private Dictionary<bool, bool> _isStarted;

        internal CellDataComponent(Dictionary<bool, bool> isStarted)
        {
            _isStarted = isStarted;
        }

        internal bool IsStartedCell(bool key) => _isStarted[key];
    }
}
