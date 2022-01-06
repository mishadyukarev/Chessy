using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct AttackCellsC
    {
        private static Dictionary<string, List<byte>> _cells;

        private static string Key(AttackTypes attack, PlayerTypes player, byte idx) => attack.ToString() + player + idx;
        
        public static Dictionary<string, List<byte>> Cells
        {
            get
            {
                var dict = new Dictionary<string, List<byte>>();
                foreach (var item in _cells) dict.Add(item.Key, item.Value.Copy());
                return dict;
            }
        }
        public static List<byte> List(PlayerTypes player, AttackTypes attack, byte idx) => _cells[Key(attack, player, idx)].Copy();
        public static AttackTypes CanAttack(PlayerTypes player, byte idx, byte idxForFind)
        {
            for (var attack = AttackTypes.First; attack < AttackTypes.End; attack++)
            {
                if (_cells[Key(attack, player, idx)].Contains(idxForFind)) return attack;
            }

            return AttackTypes.None;
        }
        public static bool ContainsKey(AttackTypes attack, PlayerTypes player, byte idx) => _cells.ContainsKey(Key(attack, player, idx));


        static AttackCellsC()
        {
            _cells = new Dictionary<string, List<byte>>();

            for (var attack = AttackTypes.First; attack < AttackTypes.End; attack++)
            {
                for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                {
                    for (byte idx = 0; idx < CellValues.ALL_CELLS_AMOUNT; idx++)
                    {
                        _cells.Add(Key(attack, player, idx), new List<byte>());
                    }
                }
            }
        }
        public AttackCellsC(bool needReset)
        {
            if (needReset) foreach (var item in Cells) _cells[item.Key].Clear();
            else throw new Exception();
        }


        public static void Add(AttackTypes attack, PlayerTypes player, byte idx, byte value)
        {
            if (!ContainsKey(attack, player, idx)) throw new Exception();

            _cells[Key(attack, player, idx)].Add(value);
        }
        public static void Clear(AttackTypes attack, PlayerTypes player, byte idx)
        {
            if (!ContainsKey(attack, player, idx)) throw new Exception();

            _cells[Key(attack, player, idx)].Clear();
        }
    }
}
