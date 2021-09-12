using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.Workers;
using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components.Data.Else.Game.General.AvailCells
{
    internal struct AvailCellsForShiftComp
    {
        private Dictionary<bool, Dictionary<byte, List<byte>>> _availCellsForShift;

        internal AvailCellsForShiftComp(Dictionary<bool, Dictionary<byte, List<byte>>> availCellsForShift)
        {
            _availCellsForShift = availCellsForShift;

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

            availCellsForShift.Add(true, dict1);
            availCellsForShift.Add(false, dict2);
        }

        internal List<byte> GetListCopy(bool isMaster, byte startIdxCell) => _availCellsForShift[isMaster][startIdxCell].Copy();
        internal void AddIdxCell(bool isMasterKey, byte startIdxCell, byte idxCell) => _availCellsForShift[isMasterKey][startIdxCell].Add(idxCell);
        internal void Clear(bool isMasterKey, byte startIdxCell) => _availCellsForShift[isMasterKey][startIdxCell].Clear();
        internal bool HaveIdxCell(bool isMasterKey, byte startIdxCell, byte idxCell) => _availCellsForShift[isMasterKey][startIdxCell].Contains(idxCell);
    }
}
