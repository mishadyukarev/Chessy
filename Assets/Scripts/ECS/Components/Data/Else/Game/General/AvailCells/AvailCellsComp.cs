//using Assets.Scripts.Abstractions.Enums;
//using Assets.Scripts.Workers;
//using System;
//using System.Collections.Generic;

//namespace Assets.Scripts.ECS.Components
//{
//    internal struct AvailCellsComp
//    {
//        private Dictionary<AvailableCellTypes, List<byte>> _availableCells;


//        internal AvailCellsComp(Dictionary<AvailableCellTypes, List<byte>> availableCells)
//        {
//            _availableCells = availableCells;

//            for (AvailableCellTypes availableCellType = 0; availableCellType < (AvailableCellTypes)Enum.GetNames(typeof(AvailableCellTypes)).Length; availableCellType++)
//            {
//                var list = new List<byte>();

//                _availableCells.Add(availableCellType, list);
//            }
//        }
//        internal void SetAllCellsCopy(AvailableCellTypes availableCellType, List<byte> list) => _availableCells[availableCellType] = list.Copy();

//        internal List<byte> GetAllCellsCopy(AvailableCellTypes availableCellType) => _availableCells[availableCellType].Copy();

//        internal void AddAvailableCell(AvailableCellTypes availableCellType, byte xy) => _availableCells[availableCellType].Add(xy);
//        internal void ClearAvailableCells(AvailableCellTypes availableCellType) => _availableCells[availableCellType].Clear();
//        internal byte GetCellByIndex(AvailableCellTypes availableCellType, byte index) => _availableCells[availableCellType][index];
//        internal void RemoveAt(AvailableCellTypes availableCellType, byte index) => _availableCells[availableCellType].RemoveAt(index);
//        internal bool TryFindCell(AvailableCellTypes availableCellType, byte idx) => _availableCells[availableCellType].Contains(idx);
//        internal int GetAmountCells(AvailableCellTypes availableCellType) => _availableCells[availableCellType].Count;
//    }
//}
