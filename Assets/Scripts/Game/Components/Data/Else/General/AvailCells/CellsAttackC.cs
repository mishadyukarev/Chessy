using Scripts.Common;
using System.Collections.Generic;

namespace Scripts.Game
{
    public struct CellsAttackC
    {
        private static Dictionary<PlayerTypes, Dictionary<AttackTypes, Dictionary<byte, List<byte>>>> _cellsForAttack;

        public CellsAttackC(bool needNew) : this()
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

        public static List<byte> GetListCopy(PlayerTypes playerType, AttackTypes attackType, byte idxCell) => _cellsForAttack[playerType][attackType][idxCell].Copy();
        public static bool FindByIdx(PlayerTypes playerType, AttackTypes attackType, byte idxCell, byte idxForFind)
        {
            foreach (var idx in _cellsForAttack[playerType][attackType][idxCell])
            {
                if (idxForFind == idx) return true;
            }
            return false;
        }
        public static void Add(PlayerTypes playerType, AttackTypes attackType, byte idxCell, byte value) => _cellsForAttack[playerType][attackType][idxCell].Add(value);
        public static void Clear(PlayerTypes playerType, AttackTypes attackType, byte idxCell) => _cellsForAttack[playerType][attackType][idxCell].Clear();
    }
}
