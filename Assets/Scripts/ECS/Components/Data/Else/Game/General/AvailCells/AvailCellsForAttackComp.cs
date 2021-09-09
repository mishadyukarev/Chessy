using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.Workers;
using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components.Data.Else.Game.General.AvailCells
{
    internal struct AvailCellsForAttackComp
    {
        private Dictionary<bool, Dictionary<byte, List<byte>>> _availCellsForSimpleAttack;
        private Dictionary<bool, Dictionary<byte, List<byte>>> _availCellsForUniqueAttack;

        internal AvailCellsForAttackComp(bool needNew) : this()
        {
            if (needNew)
            {
                _availCellsForSimpleAttack = new Dictionary<bool, Dictionary<byte, List<byte>>>();
                _availCellsForUniqueAttack = new Dictionary<bool, Dictionary<byte, List<byte>>>();


                _availCellsForSimpleAttack.Add(true, new Dictionary<byte, List<byte>>());
                _availCellsForSimpleAttack.Add(false, new Dictionary<byte, List<byte>>());

                _availCellsForUniqueAttack.Add(true, new Dictionary<byte, List<byte>>());
                _availCellsForUniqueAttack.Add(false, new Dictionary<byte, List<byte>>());


                for (byte idx = 0; idx < CellValues.AMOUNT_ALL_CELLS; idx++)
                {
                    _availCellsForSimpleAttack[true].Add(idx, new List<byte>());
                    _availCellsForSimpleAttack[false].Add(idx, new List<byte>());

                    _availCellsForUniqueAttack[true].Add(idx, new List<byte>());
                    _availCellsForUniqueAttack[false].Add(idx, new List<byte>());
                }    
            }
        }

        internal List<byte> GetSimpleListCopy(bool isMasterKey, byte idxCell) => _availCellsForSimpleAttack[isMasterKey][idxCell].Copy();
        internal List<byte> GetUniqueListCopy(bool isMasterKey, byte idxCell) => _availCellsForUniqueAttack[isMasterKey][idxCell].Copy();

        internal void AddSimple(bool isMasterKey, byte idxCell, byte value) => _availCellsForSimpleAttack[isMasterKey][idxCell].Add(value);
        internal void AddUnique(bool isMasterKey, byte idxCell, byte value) => _availCellsForUniqueAttack[isMasterKey][idxCell].Add(value);

        internal void ClearSimple(bool isMasterKey, byte idxCell) => _availCellsForSimpleAttack[isMasterKey][idxCell].Clear();
        internal void ClearUnique(bool isMasterKey, byte idxCell) => _availCellsForUniqueAttack[isMasterKey][idxCell].Clear();
    }
}
