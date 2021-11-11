using System.Collections.Generic;

namespace Chessy.Game
{
    public struct CellsArsonArcherComp
    {
        private static Dictionary<PlayerTypes, Dictionary<byte, List<byte>>> _cellsArcherArson;

        public CellsArsonArcherComp(bool needNew) : this()
        {
            if (needNew)
            {
                _cellsArcherArson = new Dictionary<PlayerTypes, Dictionary<byte, List<byte>>>();

                _cellsArcherArson.Add(PlayerTypes.First, new Dictionary<byte, List<byte>>());
                _cellsArcherArson.Add(PlayerTypes.Second, new Dictionary<byte, List<byte>>());

                for (byte idx = 0; idx < CellValues.AMOUNT_ALL_CELLS; idx++)
                {
                    _cellsArcherArson[PlayerTypes.First].Add(idx, new List<byte>());
                    _cellsArcherArson[PlayerTypes.Second].Add(idx, new List<byte>());
                }
            }
        }

        public static void Add(PlayerTypes playerType, byte idxCell, byte addIdxCell) => _cellsArcherArson[playerType][idxCell].Add(addIdxCell);
        public static List<byte> GetListCopy(PlayerTypes playerType, byte idxCell) => _cellsArcherArson[playerType][idxCell].Copy();
        public static bool HaveIdxCell(PlayerTypes playerType, byte onIdxCell, byte inIdxCell) => _cellsArcherArson[playerType][onIdxCell].Contains(inIdxCell);
        public static void Clear(PlayerTypes playerType, byte onIdxCell) => _cellsArcherArson[playerType][onIdxCell].Clear();
    }
}
