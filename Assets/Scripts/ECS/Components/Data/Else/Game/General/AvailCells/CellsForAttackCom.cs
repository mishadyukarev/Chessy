using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.Workers;
using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components.Data.Else.Game.General.AvailCells
{
    internal struct CellsForAttackCom
    {
        private Dictionary<AttackTypes, Dictionary<bool, Dictionary<byte, List<byte>>>> _availCellsForSimpleAttack;

        internal CellsForAttackCom(bool needNew) : this()
        {
            if (needNew)
            {
                _availCellsForSimpleAttack = new Dictionary<AttackTypes, Dictionary<bool, Dictionary<byte, List<byte>>>>();


                _availCellsForSimpleAttack[AttackTypes.Simple] = new Dictionary<bool, Dictionary<byte, List<byte>>>();
                _availCellsForSimpleAttack[AttackTypes.Unique] = new Dictionary<bool, Dictionary<byte, List<byte>>>();


                _availCellsForSimpleAttack[AttackTypes.Simple].Add(true, new Dictionary<byte, List<byte>>());
                _availCellsForSimpleAttack[AttackTypes.Simple].Add(false, new Dictionary<byte, List<byte>>());

                _availCellsForSimpleAttack[AttackTypes.Unique].Add(true, new Dictionary<byte, List<byte>>());
                _availCellsForSimpleAttack[AttackTypes.Unique].Add(false, new Dictionary<byte, List<byte>>());


                for (byte idx = 0; idx < CellValues.AMOUNT_ALL_CELLS; idx++)
                {
                    _availCellsForSimpleAttack[AttackTypes.Simple][true].Add(idx, new List<byte>());
                    _availCellsForSimpleAttack[AttackTypes.Simple][false].Add(idx, new List<byte>());

                    _availCellsForSimpleAttack[AttackTypes.Unique][true].Add(idx, new List<byte>());
                    _availCellsForSimpleAttack[AttackTypes.Unique][false].Add(idx, new List<byte>());
                }
            }
        }

        internal List<byte> GetListCopy(AttackTypes attackType, bool isMasterKey, byte idxCell) => _availCellsForSimpleAttack[attackType][isMasterKey][idxCell].Copy();
        internal bool FindByIdx(AttackTypes attackType, bool isMasterKey, byte idxCell, byte idxForFind)
        {
            foreach (var idx in _availCellsForSimpleAttack[attackType][isMasterKey][idxCell])
            {
                if (idxForFind == idx) return true;
            }
            return false;
        }
        internal void Add(AttackTypes attackType, bool isMasterKey, byte idxCell, byte value) => _availCellsForSimpleAttack[attackType][isMasterKey][idxCell].Add(value);
        internal void Clear(AttackTypes attackType, bool isMasterKey, byte idxCell) => _availCellsForSimpleAttack[attackType][isMasterKey][idxCell].Clear();
    }
}
