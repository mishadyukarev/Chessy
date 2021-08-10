using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components
{
    internal struct IdxAvailableCellsComponent
    {
        private Dictionary<AvailableCellTypes, List<byte>> _availableCells;


        internal IdxAvailableCellsComponent(Dictionary<AvailableCellTypes, List<byte>> availableCells)
        {
            _availableCells = availableCells;

            for (AvailableCellTypes availableCellType = 0; availableCellType < (AvailableCellTypes)Enum.GetNames(typeof(AvailableCellTypes)).Length; availableCellType++)
            {
                var list = new List<byte>();

                _availableCells.Add(availableCellType, list);
            }
        }

        private List<byte> GetAllCells(AvailableCellTypes availableCellType) => _availableCells[availableCellType];

        internal void SetAllCellsCopy(AvailableCellTypes availableCellType, List<byte> list) => _availableCells[availableCellType] = list.Copy();

        internal List<byte> GetAllCellsCopy(AvailableCellTypes availableCellType) => GetAllCells(availableCellType).Copy();

        internal void AddAvailableCell(AvailableCellTypes availableCellType, byte xy) => GetAllCells(availableCellType).Add(xy);
        internal void ClearAvailableCells(AvailableCellTypes availableCellType) => GetAllCells(availableCellType).Clear();
        internal byte GetCellByIndex(AvailableCellTypes availableCellType, byte index) => GetAllCells(availableCellType)[index];
        internal void RemoveAt(AvailableCellTypes availableCellType, byte index) => GetAllCells(availableCellType).RemoveAt(index);
        internal bool TryFindCell(AvailableCellTypes availableCellType, byte xy) => GetAllCells(availableCellType).TryFindCell(xy);
        internal int GetAmountCells(AvailableCellTypes availableCellType) => GetAllCells(availableCellType).Count;
    }
}
