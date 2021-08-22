using Assets.Scripts.Workers;
using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components.Data.Else.Game.General.AvailCells
{
    internal struct AvailCellsForSimpleAttackComp
    {
        private Dictionary<bool, List<byte>> _availCellsForSimpleAttack;

        internal AvailCellsForSimpleAttackComp(Dictionary<bool, List<byte>> availCellsForSimpleAttack)
        {
            _availCellsForSimpleAttack = availCellsForSimpleAttack;
            _availCellsForSimpleAttack.Add(true, new List<byte>());
            _availCellsForSimpleAttack.Add(false, new List<byte>());
        }

        internal List<byte> GetListCopy(bool isMasterKey) => _availCellsForSimpleAttack[isMasterKey].Copy();
        internal void Add(bool isMasterKey, byte idxCell) => _availCellsForSimpleAttack[isMasterKey].Add(idxCell);
        internal void Clear(bool isMasterKey) => _availCellsForSimpleAttack[isMasterKey].Clear();
    }
}
