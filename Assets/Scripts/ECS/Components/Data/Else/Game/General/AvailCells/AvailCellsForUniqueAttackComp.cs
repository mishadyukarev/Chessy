using Assets.Scripts.Workers;
using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components.Data.Else.Game.General.AvailCells
{
    internal struct AvailCellsForUniqueAttackComp
    {
        private Dictionary<bool, List<byte>> _availCellsForUniqueAttack;

        internal AvailCellsForUniqueAttackComp(Dictionary<bool, List<byte>> availCellsForUniqueAttack)
        {
            _availCellsForUniqueAttack = availCellsForUniqueAttack;
            _availCellsForUniqueAttack.Add(true, new List<byte>());
            _availCellsForUniqueAttack.Add(false, new List<byte>());
        }

        internal void Add(bool isMasterKey, byte idxCell) => _availCellsForUniqueAttack[isMasterKey].Add(idxCell);
        internal List<byte> GetListCopy(bool isMasterKey) => _availCellsForUniqueAttack[isMasterKey].Copy();
        internal void Clear(bool isMasterKey) => _availCellsForUniqueAttack[isMasterKey].Clear();
    }
}
