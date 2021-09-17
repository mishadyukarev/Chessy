using Assets.Scripts.Workers;
using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components.Data.Else.Game.General
{
    internal struct CellsForSetUnitComp
    {
        private Dictionary<bool, List<byte>> _availCellsForSetUnit;

        internal CellsForSetUnitComp(Dictionary<bool, List<byte>> availCellsForSetUnit)
        {
            _availCellsForSetUnit = availCellsForSetUnit;

            availCellsForSetUnit.Add(true, new List<byte>());
            availCellsForSetUnit.Add(false, new List<byte>());
        }

        internal List<byte> GetListCells(bool isMasterKey) => _availCellsForSetUnit[isMasterKey].Copy();
        internal bool HaveIdxCell(bool isMasterKey, byte idxCell) => _availCellsForSetUnit[isMasterKey].Contains(idxCell);
        internal bool RemoveIdxCell(bool isMasterKey, byte idxCell) => _availCellsForSetUnit[isMasterKey].Remove(idxCell);
        internal void AddIdxCell(bool isMasterKey, byte idxCellValue) => _availCellsForSetUnit[isMasterKey].Add(idxCellValue);
        internal void ClearIdxCells(bool isMasterKey) => _availCellsForSetUnit[isMasterKey].Clear();
    }
}
