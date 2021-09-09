using Assets.Scripts.Workers;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ECS.Components.Data.Else.Game.General.AvailCells
{
    internal struct AvailCellsForShiftComp
    {
        private Dictionary<bool, Dictionary<byte, List<byte>>> _availCellsForShift;

        internal AvailCellsForShiftComp(Dictionary<bool, Dictionary<byte, List<byte>>> availCellsForShift)
        {
            _availCellsForShift = availCellsForShift;

            //_availCellsForShift[true][79].Add(90);
        }

        internal List<byte> GetListCopy(bool isMaster, byte startIdxCell) => _availCellsForShift[isMaster][startIdxCell].Copy();
        internal void AddIdxCell(bool isMasterKey, byte startIdxCell, byte idxCell) => _availCellsForShift[isMasterKey][startIdxCell].Add(idxCell);
        internal void Clear(bool isMasterKey, byte startIdxCell) => _availCellsForShift[isMasterKey][startIdxCell].Clear();
        internal bool HaveIdxCell(bool isMasterKey, byte startIdxCell, byte idxCell) => _availCellsForShift[isMasterKey][startIdxCell].Contains(idxCell);
    }
}
