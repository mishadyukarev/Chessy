using Assets.Scripts.Workers;
using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components.Data.Else.Game.General.AvailCells
{
    internal struct AvailCellsForArcherArsonComp
    {
        private Dictionary<bool, List<byte>> _availCellsForArcherArson;

        internal AvailCellsForArcherArsonComp(Dictionary<bool, List<byte>> availCellsForArcherArson)
        {
            _availCellsForArcherArson = availCellsForArcherArson;
            _availCellsForArcherArson.Add(true, new List<byte>());
            _availCellsForArcherArson.Add(false, new List<byte>());
        }

        internal void Add(bool isMasterKey, byte idxCell) => _availCellsForArcherArson[isMasterKey].Add(idxCell);
        internal List<byte> GetListCopy(bool isMasterKey) => _availCellsForArcherArson[isMasterKey].Copy();
        internal bool HaveIdxCell(bool isMasterKey, byte idxCell) => _availCellsForArcherArson[isMasterKey].Contains(idxCell);
        internal void Clear(bool isMasterKey) => _availCellsForArcherArson[isMasterKey].Clear();
    }
}
