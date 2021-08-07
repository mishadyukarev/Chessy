using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components
{
    internal struct AvailableCellsComponent
    {
        private Dictionary<AvailableCellTypes, List<int[]>> _availableCells;


        internal AvailableCellsComponent(Dictionary<AvailableCellTypes, List<int[]>> availableCells)
        {
            _availableCells = availableCells;

            for (AvailableCellTypes availableCellType = 0; availableCellType < (AvailableCellTypes)Enum.GetNames(typeof(AvailableCellTypes)).Length; availableCellType++)
            {
                var list = new List<int[]>();

                _availableCells.Add(availableCellType, list);
            }
        }

        private List<int[]> GetAllCells(AvailableCellTypes availableCellType) => _availableCells[availableCellType];

        internal void SetAllCellsCopy(AvailableCellTypes availableCellType, List<int[]> list) => _availableCells[availableCellType] = list.Copy();

        internal List<int[]> GetAllCellsCopy(AvailableCellTypes availableCellType) => GetAllCells(availableCellType).Copy();

        internal void AddAvailableCell(AvailableCellTypes availableCellType, int[] xy) => GetAllCells(availableCellType).Add(xy);
        internal void ClearAvailableCells(AvailableCellTypes availableCellType) => GetAllCells(availableCellType).Clear();
        internal int[] GetCellByIndex(AvailableCellTypes availableCellType, int index) => GetAllCells(availableCellType)[index];
        internal void RemoveAt(AvailableCellTypes availableCellType, int index) => GetAllCells(availableCellType).RemoveAt(index);
        internal bool TryFindCell(AvailableCellTypes availableCellType, int[] xy) => GetAllCells(availableCellType).TryFindCell(xy);
        internal int GetAmountCells(AvailableCellTypes availableCellType) => GetAllCells(availableCellType).Count;
    }
}
