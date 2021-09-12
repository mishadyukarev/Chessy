using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.Workers;
using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components.Data.Else.Game.General.AvailCells
{
    internal struct CellsArsonArcherComp
    {
        private Dictionary<bool, Dictionary<byte, List<byte>>> _cellsArcherArson;

        internal CellsArsonArcherComp(bool needNew) : this()
        {
            if (needNew)
            {
                _cellsArcherArson = new Dictionary<bool, Dictionary<byte, List<byte>>>();

                _cellsArcherArson.Add(true, new Dictionary<byte, List<byte>>());
                _cellsArcherArson.Add(false, new Dictionary<byte, List<byte>>());

                for (byte idx = 0; idx < CellValues.AMOUNT_ALL_CELLS; idx++)
                {
                    _cellsArcherArson[true].Add(idx, new List<byte>());
                    _cellsArcherArson[false].Add(idx, new List<byte>());
                }
            }
        }

        internal void Add(bool isMasterKey, byte idxCell, byte addIdxCell) => _cellsArcherArson[isMasterKey][idxCell].Add(addIdxCell);
        internal List<byte> GetListCopy(bool isMasterKey, byte idxCell) => _cellsArcherArson[isMasterKey][idxCell].Copy();
        internal bool HaveIdxCell(bool isMasterKey, byte onIdxCell, byte inIdxCell) => _cellsArcherArson[isMasterKey][onIdxCell].Contains(inIdxCell);
        internal void Clear(bool isMasterKey, byte onIdxCell) => _cellsArcherArson[isMasterKey][onIdxCell].Clear();
    }
}
