using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct InvTWC
    {
        private static Dictionary<string, int> _tWs;

        private static string Key(TWTypes tw, LevelTypes level, PlayerTypes player) => tw.ToString() + level + player;
        private static bool ContainsKey(string key) => _tWs.ContainsKey(key);

        public static Dictionary<string, int> ToolWeapons
        {
            get
            {
                var dict = new Dictionary<string, int>();
                foreach (var item in _tWs) dict.Add(item.Key, item.Value);
                return dict;
            }
        }
        public static int Amount(TWTypes tw, LevelTypes level, PlayerTypes player)
        {
            var key = Key(tw, level, player);
            if (!ContainsKey(key)) throw new Exception();

            return _tWs[key];
        }
        public static bool Have(TWTypes tW, LevelTypes level, PlayerTypes player) => Amount(tW, level, player) > 0;


        static InvTWC()
        {
            _tWs = new Dictionary<string, int>();

            for (var tw = TWTypes.First; tw < TWTypes.End; tw++)
            {
                for (var level = LevelTypes.First; level < LevelTypes.End; level++)
                {
                    for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                    {
                        _tWs.Add(Key(tw, level, player), default);
                    }
                }
            }
        }
        public InvTWC(bool needReset)
        {
            if (needReset) foreach (var item in ToolWeapons) _tWs[item.Key] = 0;
            else throw new Exception();
        }


        public static void Add(TWTypes tw, LevelTypes level, PlayerTypes player, byte adding = 1)
        {
            var key = Key(tw, level, player);
            if (!ContainsKey(key)) throw new Exception();

            _tWs[key] += adding;
        }
        public static void Take(TWTypes tw, LevelTypes level, PlayerTypes player, byte taking = 1)
        {
            var key = Key(tw, level, player);
            if (!ContainsKey(key)) throw new Exception();

            _tWs[key] -= taking;
        }
        public static void Sync(string key, int value)
        {
            if (!ContainsKey(key)) throw new Exception();
            _tWs[key] = (byte)value;
        }
    }
}
