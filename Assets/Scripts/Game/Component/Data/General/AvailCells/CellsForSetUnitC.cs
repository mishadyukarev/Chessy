using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct CellsForSetUnitC
    {
        private static Dictionary<PlayerTypes, List<byte>> _cellsForSetUnit;

        public static List<byte> List(PlayerTypes playerType)
        {
            if (playerType == default) throw new Exception();
            return _cellsForSetUnit[playerType].Copy();
        }
        public static bool HaveIdxCell(PlayerTypes playerType, byte idxCell)
        {
            if (playerType == default) throw new Exception();
            return _cellsForSetUnit[playerType].Contains(idxCell);
        }


        public CellsForSetUnitC(bool needNew) : this()
        {
            if (needNew)
            {
                _cellsForSetUnit = new Dictionary<PlayerTypes, List<byte>>();

                _cellsForSetUnit.Add(PlayerTypes.First, new List<byte>());
                _cellsForSetUnit.Add(PlayerTypes.Second, new List<byte>());
            }
        }


        public static bool RemoveIdxCell(PlayerTypes playerType, byte idxCell)
        {
            if (playerType == default) throw new Exception();
            return _cellsForSetUnit[playerType].Remove(idxCell);
        }
        public static void AddIdxCell(PlayerTypes playerType, byte idxCellValue)
        {
            if (playerType == default) throw new Exception();
            _cellsForSetUnit[playerType].Add(idxCellValue);
        }
        public static void ClearIdxCells(PlayerTypes playerType)
        {
            if (playerType == default) throw new Exception();
            _cellsForSetUnit[playerType].Clear();
        }
    }
}
