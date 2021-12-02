using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct ArsonCellsC
    {
        private static Dictionary<string, List<byte>> _cells;

        private static string Key(PlayerTypes player, byte idx) => player.ToString() + idx;

        public static Dictionary<string, List<byte>> Cells
        {
            get
            {
                var dict = new Dictionary<string, List<byte>>();
                foreach (var item in _cells) dict.Add(item.Key, item.Value.Copy());
                return dict;
            }
        }
        public static List<byte> List(PlayerTypes player, byte idx) => _cells[Key(player, idx)].Copy();
        public static bool ContainIdx(PlayerTypes player, byte onIdx, byte inIdx) => _cells[Key(player, onIdx)].Contains(inIdx);


        static ArsonCellsC()
        {
            _cells = new Dictionary<string, List<byte>>();


            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                for (byte idx = 0; idx < CellValues.AMOUNT_ALL_CELLS; idx++)
                {
                    _cells.Add(Key(player, idx), new List<byte>());
                }
            }
        }
        public ArsonCellsC(bool needReset)
        {
            if (needReset) foreach (var item in Cells) Clear(item.Key);
            else throw new Exception();
        }

        public static void Add(PlayerTypes player, byte idx, byte addIdx) => _cells[Key(player, idx)].Add(addIdx);
        public static void Clear(PlayerTypes player, byte onIdx) => _cells[Key(player, onIdx)].Clear();
        public static void Clear(string key) => _cells[key].Clear();
    }
}
