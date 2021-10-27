using Scripts.Common;
using System.Collections.Generic;

namespace Scripts.Game
{
    internal struct CellsArsonArcherComp
    {
        private Dictionary<PlayerTypes, Dictionary<byte, List<byte>>> _cellsArcherArson;

        internal CellsArsonArcherComp(bool needNew) : this()
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

        internal void Add(PlayerTypes playerType, byte idxCell, byte addIdxCell) => _cellsArcherArson[playerType][idxCell].Add(addIdxCell);
        internal List<byte> GetListCopy(PlayerTypes playerType, byte idxCell) => _cellsArcherArson[playerType][idxCell].Copy();
        internal bool HaveIdxCell(PlayerTypes playerType, byte onIdxCell, byte inIdxCell) => _cellsArcherArson[playerType][onIdxCell].Contains(inIdxCell);
        internal void Clear(PlayerTypes playerType, byte onIdxCell) => _cellsArcherArson[playerType][onIdxCell].Clear();
    }
}
