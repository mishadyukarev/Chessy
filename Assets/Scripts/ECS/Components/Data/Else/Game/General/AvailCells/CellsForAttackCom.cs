using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.Workers;
using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components.Data.Else.Game.General.AvailCells
{
    internal struct CellsForAttackCom
    {
        private Dictionary<PlayerTypes, Dictionary<AttackTypes, Dictionary<byte, List<byte>>>> _cellsForAttack;

        internal CellsForAttackCom(bool needNew) : this()
        {
            if (needNew)
            {
                _cellsForAttack = new Dictionary<PlayerTypes, Dictionary<AttackTypes, Dictionary<byte, List<byte>>>>();


                _cellsForAttack[PlayerTypes.First] = new Dictionary<AttackTypes, Dictionary<byte, List<byte>>>();
                _cellsForAttack[PlayerTypes.Second] = new Dictionary<AttackTypes, Dictionary<byte, List<byte>>>();


                _cellsForAttack[PlayerTypes.First].Add(AttackTypes.Simple, new Dictionary<byte, List<byte>>());
                _cellsForAttack[PlayerTypes.Second].Add(AttackTypes.Simple, new Dictionary<byte, List<byte>>());

                _cellsForAttack[PlayerTypes.First].Add(AttackTypes.Unique, new Dictionary<byte, List<byte>>());
                _cellsForAttack[PlayerTypes.Second].Add(AttackTypes.Unique, new Dictionary<byte, List<byte>>());


                for (byte idx = 0; idx < CellValues.AMOUNT_ALL_CELLS; idx++)
                {
                    _cellsForAttack[PlayerTypes.First][AttackTypes.Simple].Add(idx, new List<byte>());
                    _cellsForAttack[PlayerTypes.Second][AttackTypes.Simple].Add(idx, new List<byte>());

                    _cellsForAttack[PlayerTypes.First][AttackTypes.Unique].Add(idx, new List<byte>());
                    _cellsForAttack[PlayerTypes.Second][AttackTypes.Unique].Add(idx, new List<byte>());
                }
            }
        }

        internal List<byte> GetListCopy(PlayerTypes playerType, AttackTypes attackType, byte idxCell) => _cellsForAttack[playerType][attackType][idxCell].Copy();
        internal bool FindByIdx(PlayerTypes playerType, AttackTypes attackType, byte idxCell, byte idxForFind)
        {
            foreach (var idx in _cellsForAttack[playerType][attackType][idxCell])
            {
                if (idxForFind == idx) return true;
            }
            return false;
        }
        internal void Add(PlayerTypes playerType, AttackTypes attackType, byte idxCell, byte value) => _cellsForAttack[playerType][attackType][idxCell].Add(value);
        internal void Clear(PlayerTypes playerType, AttackTypes attackType, byte idxCell) => _cellsForAttack[playerType][attackType][idxCell].Clear();
    }
}
