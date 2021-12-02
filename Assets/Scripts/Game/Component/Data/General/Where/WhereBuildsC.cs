using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct WhereBuildsC
    {
        private static Dictionary<string, bool> _cells;

        private static string Key(BuildTypes build, PlayerTypes owner, byte idx) => build.ToString() + owner + idx;
        private static bool ContainsKey(string key) => _cells.ContainsKey(key);

        public static Dictionary<string, bool> Cells
        {
            get
            {
                var dict = new Dictionary<string, bool>();
                foreach (var item in _cells) dict.Add(item.Key, item.Value);
                return dict;
            }
        }
        public static byte Amount(BuildTypes build, PlayerTypes player)
        {
            byte amount = 0;
            for (byte idx = 0; idx < CellValues.AMOUNT_ALL_CELLS; idx++)
            {
                if (_cells[Key(build, player, idx)]) ++amount;
            }
            return amount;
        }
        public static List<byte> IdxBuilds(BuildTypes build, PlayerTypes player)
        {
            var list = new List<byte>();
            for (byte idx = 0; idx < CellValues.AMOUNT_ALL_CELLS; idx++)
            {
                if(_cells[Key(build, player, idx)]) list.Add(idx);
            }
            return list;
        }
        public static bool IsSetted(BuildTypes build, PlayerTypes player)
        {
            for (byte idx = 0; idx < CellValues.AMOUNT_ALL_CELLS; idx++)
            {
                if (_cells[Key(build, player, idx)]) return true;
            }
            return false;
        }
        public static byte Idx(BuildTypes build, PlayerTypes player)
        {
            for (byte idx = 0; idx < CellValues.AMOUNT_ALL_CELLS; idx++)
            {
                if (_cells[Key(build, player, idx)]) return idx;
            }
            throw new Exception();
        }



        static WhereBuildsC()
        {
            _cells = new Dictionary<string, bool>();

            for (var build = BuildTypes.First; build < BuildTypes.End; build++)
            {
                for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                {

                    for (byte idx = 0; idx < CellValues.AMOUNT_ALL_CELLS; idx++)
                    {
                        _cells.Add(Key(build, player, idx), default);
                    }
                }
            }
        }
        public WhereBuildsC(bool needReset)
        {
            if (needReset) foreach (var item in Cells) _cells[item.Key] = false;
            else throw new Exception();
        }



        internal static void Set(BuildTypes build, PlayerTypes player, byte idx, bool have)
        {
            var key = Key(build, player, idx);

            if (!ContainsKey(key)) throw new Exception();

            _cells[key] = have;
        }

        public static void Sync(string key, bool have)
        {
            if (!ContainsKey(key)) throw new Exception();
            _cells[key] = have;
        }
    }
}
