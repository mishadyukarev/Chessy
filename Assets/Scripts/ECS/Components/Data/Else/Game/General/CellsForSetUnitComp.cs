//using System.Collections.Generic;

//namespace Assets.Scripts.ECS.Components.Data.Else.Game.General
//{
//    internal struct CellsForSetUnitComp
//    {
//        private Dictionary<bool, List<byte>> _cellsForSetUnit;

//        internal CellsForSetUnitComp(Dictionary<bool, List<byte>> cellsForSetUnit)
//        {
//            _cellsForSetUnit = cellsForSetUnit;
//            _cellsForSetUnit.Add(true, new List<byte>());
//            _cellsForSetUnit.Add(false, new List<byte>());
//        }

//        internal void AddIdxCell(bool isMasterKey, byte idxCell) => _cellsForSetUnit[isMasterKey].Add(idxCell);
//        internal void RemoveIdxCell(bool isMasterKey, byte idxCell) => _cellsForSetUnit[isMasterKey].Remove(idxCell);
//        internal bool HaveIdxCell(bool isMasterKey, byte idxCell) => _cellsForSetUnit[isMasterKey].Contains(idxCell);
//    }
//}
