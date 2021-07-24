using Assets.Scripts.Workers;
using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components
{
    internal struct AvailableCellsComponent
    {
        private List<int[]> _availableCells;

        internal List<int[]> AvailableCells => _availableCells.Copy();
        internal int CountCells => _availableCells.Count;


        internal void StartFill() => _availableCells = new List<int[]>();

        internal void SetList(List<int[]> list) => _availableCells = list;

        internal int[] GetCellByIndex(int index) => _availableCells[index];
        internal void AddAvailableCell(int[] xy) => _availableCells.Add(xy);
        internal void Clear() => _availableCells.Clear();
        internal void RemoveAt(int index) => _availableCells.RemoveAt(index);
        internal bool TryFindCell(int[] xy) => _availableCells.TryFindCell(xy);
    }
}
