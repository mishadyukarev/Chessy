using Assets.Scripts.Workers;
using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components.Data.Else.Game.General
{
    internal struct AvailCellsForSetUnitComp
    {
        private Dictionary<bool, List<byte>> _availCellsForSetUnit;

        internal AvailCellsForSetUnitComp(Dictionary<bool, List<byte>> availCellsForSetUnit)
        {
            _availCellsForSetUnit = availCellsForSetUnit;
        }

        internal List<byte> GetListAvailCellsCopy(bool isMasterKey) => _availCellsForSetUnit[isMasterKey].Copy();
        internal bool HaveIdxCell(bool isMasterKey, byte idxCell) => _availCellsForSetUnit[isMasterKey].Contains(idxCell);
        internal bool RemoveIdxCell(bool isMasterKey, byte idxCell) => _availCellsForSetUnit[isMasterKey].Remove(idxCell);
        internal void AddIdxCell(bool isMasterKey, byte idxCellValue) => _availCellsForSetUnit[isMasterKey].Add(idxCellValue);
        internal void ClearIdxCells(bool isMasterKey) => _availCellsForSetUnit[isMasterKey].Clear();
    }
}
