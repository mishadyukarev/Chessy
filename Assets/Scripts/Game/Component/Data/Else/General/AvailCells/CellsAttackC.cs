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


                for (var playerType = Support.MinPlayerType; playerType < Support.MaxPlayerType; playerType++)
                {
                    _cellsForAttack[playerType] = new Dictionary<AttackTypes, Dictionary<byte, List<byte>>>();
                    _cellsForAttack[playerType].Add(AttackTypes.Simple, new Dictionary<byte, List<byte>>());
                    _cellsForAttack[playerType].Add(AttackTypes.Unique, new Dictionary<byte, List<byte>>());

                    for (byte idx = 0; idx < CellValues.AMOUNT_ALL_CELLS; idx++)
                    {
                        _cellsForAttack[playerType][AttackTypes.Simple].Add(idx, new List<byte>());
                        _cellsForAttack[playerType][AttackTypes.Unique].Add(idx, new List<byte>());
                    }
                }
            }
        }

        public static List<byte> GetListCopy(PlayerTypes playerType, AttackTypes attackType, byte idxCell) => _cellsForAttack[playerType][attackType][idxCell].Copy();
        public static AttackTypes FindByIdx(PlayerTypes playerType, byte idxCell, byte idxForFind)
        {
            for (var attackType = Support.MinAttackType; attackType < Support.MaxAttackType; attackType++)
            {
                if (_cellsForAttack[playerType][attackType][idxCell].Contains(idxForFind)) return attackType;
            }

            return AttackTypes.None;
        }
        public static void Add(PlayerTypes playerType, AttackTypes attackType, byte idxCell, byte value) => _cellsForAttack[playerType][attackType][idxCell].Add(value);
        public static void Clear(PlayerTypes playerType, AttackTypes attackType, byte idxCell) => _cellsForAttack[playerType][attackType][idxCell].Clear();
    }
}
