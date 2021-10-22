using System.Collections.Generic;

namespace Scripts.Game
{
    internal struct CellsForSetUnitComp
    {
        private Dictionary<PlayerTypes, List<byte>> _cellsForSetUnit;

        internal CellsForSetUnitComp(bool needNew) : this()
        {
            if (needNew)
            {
                _cellsForSetUnit = new Dictionary<PlayerTypes, List<byte>>();

                _cellsForSetUnit.Add(PlayerTypes.First, new List<byte>());
                _cellsForSetUnit.Add(PlayerTypes.Second, new List<byte>());
            }
        }

        internal List<byte> GetListCells(PlayerTypes playerType) => _cellsForSetUnit[playerType].Copy();
        internal bool HaveIdxCell(PlayerTypes playerType, byte idxCell) => _cellsForSetUnit[playerType].Contains(idxCell);
        internal bool RemoveIdxCell(PlayerTypes playerType, byte idxCell) => _cellsForSetUnit[playerType].Remove(idxCell);
        internal void AddIdxCell(PlayerTypes playerType, byte idxCellValue) => _cellsForSetUnit[playerType].Add(idxCellValue);
        internal void ClearIdxCells(PlayerTypes playerType) => _cellsForSetUnit[playerType].Clear();
    }
}
