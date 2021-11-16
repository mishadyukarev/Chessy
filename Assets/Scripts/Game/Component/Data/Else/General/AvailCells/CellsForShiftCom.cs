using System.Collections.Generic;

namespace Chessy.Game
{
    public struct CellsForShiftCom
    {
        private static Dictionary<PlayerTypes, Dictionary<byte, List<byte>>> _availCellsForShift;

        public CellsForShiftCom(bool needNew) : this()
        {
            if (needNew)
            {
                _availCellsForShift = new Dictionary<PlayerTypes, Dictionary<byte, List<byte>>>();

                var dict1 = new Dictionary<byte, List<byte>>();
                var dict2 = new Dictionary<byte, List<byte>>();

                byte curIdx = 0;
                for (byte x = 0; x < CellValues.CELL_COUNT_X; x++)
                    for (byte y = 0; y < CellValues.CELL_COUNT_Y; y++)
                    {
                        dict1.Add(curIdx, new List<byte>());
                        dict2.Add(curIdx, new List<byte>());

                        curIdx++;
                    }

                _availCellsForShift.Add(PlayerTypes.First, dict1);
                _availCellsForShift.Add(PlayerTypes.Second, dict2);
            }
        }

        public static List<byte> List(PlayerTypes playerType, byte startIdxCell) => _availCellsForShift[playerType][startIdxCell].Copy();
        public static void AddIdxCell(PlayerTypes playerType, byte startIdxCell, byte idxCell) => _availCellsForShift[playerType][startIdxCell].Add(idxCell);
        public static void Clear(PlayerTypes playerType, byte startIdxCell) => _availCellsForShift[playerType][startIdxCell].Clear();
        public static bool HaveIdxCell(PlayerTypes playerType, byte startIdxCell, byte idxCell) => _availCellsForShift[playerType][startIdxCell].Contains(idxCell);
    }
}
