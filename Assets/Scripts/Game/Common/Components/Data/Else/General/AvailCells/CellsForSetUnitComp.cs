using Scripts.Common;
using System;
using System.Collections.Generic;

namespace Scripts.Game
{
    public struct CellsForSetUnitComp
    {
        private Dictionary<PlayerTypes, List<byte>> _cellsForSetUnit;

        public CellsForSetUnitComp(bool needNew) : this()
        {
            if (needNew)
            {
                _cellsForSetUnit = new Dictionary<PlayerTypes, List<byte>>();

                _cellsForSetUnit.Add(PlayerTypes.First, new List<byte>());
                _cellsForSetUnit.Add(PlayerTypes.Second, new List<byte>());
            }
        }

        public List<byte> GetListCells(PlayerTypes playerType)
        {
            if(playerType == default) throw new Exception();
            return _cellsForSetUnit[playerType].Copy();
        }
        public bool HaveIdxCell(PlayerTypes playerType, byte idxCell)
        {
            if (playerType == default) throw new Exception();
            return _cellsForSetUnit[playerType].Contains(idxCell);
        }
        public bool RemoveIdxCell(PlayerTypes playerType, byte idxCell)
        {
            if(playerType == default) throw new Exception();
            return _cellsForSetUnit[playerType].Remove(idxCell);
        }
        public void AddIdxCell(PlayerTypes playerType, byte idxCellValue)
        {
            if (playerType == default) throw new Exception();
            _cellsForSetUnit[playerType].Add(idxCellValue);
        }
        public void ClearIdxCells(PlayerTypes playerType)
        {
            if (playerType == default) throw new Exception();
            _cellsForSetUnit[playerType].Clear();
        }
    }
}
