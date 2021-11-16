using System.Collections.Generic;

namespace Chessy.Game
{
    public struct AttackCellsC
    {
        private static Dictionary<PlayerTypes, Dictionary<AttackTypes, Dictionary<byte, List<byte>>>> _cellsForAttack;

        public AttackCellsC(bool needNew) : this()
        {
            if (needNew)
            {
                _cellsForAttack = new Dictionary<PlayerTypes, Dictionary<AttackTypes, Dictionary<byte, List<byte>>>>();


                for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                {
                    _cellsForAttack[player] = new Dictionary<AttackTypes, Dictionary<byte, List<byte>>>();
                    _cellsForAttack[player].Add(AttackTypes.Simple, new Dictionary<byte, List<byte>>());
                    _cellsForAttack[player].Add(AttackTypes.Unique, new Dictionary<byte, List<byte>>());

                    for (byte idx = 0; idx < CellValues.AMOUNT_ALL_CELLS; idx++)
                    {
                        _cellsForAttack[player][AttackTypes.Simple].Add(idx, new List<byte>());
                        _cellsForAttack[player][AttackTypes.Unique].Add(idx, new List<byte>());
                    }
                }
            }
        }

        public static List<byte> List(PlayerTypes playerType, AttackTypes attackType, byte idxCell) => _cellsForAttack[playerType][attackType][idxCell].Copy();
        public static AttackTypes FindByIdx(PlayerTypes playerType, byte idxCell, byte idxForFind)
        {
            for (var attackType = AttackTypes.First; attackType < AttackTypes.End; attackType++)
            {
                if (_cellsForAttack[playerType][attackType][idxCell].Contains(idxForFind)) return attackType;
            }

            return AttackTypes.None;
        }
        public static void Add(PlayerTypes playerType, AttackTypes attackType, byte idxCell, byte value) => _cellsForAttack[playerType][attackType][idxCell].Add(value);
        public static void Clear(PlayerTypes playerType, AttackTypes attackType, byte idxCell) => _cellsForAttack[playerType][attackType][idxCell].Clear();
    }
}
