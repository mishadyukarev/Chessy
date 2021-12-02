using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct WhereUnitsC
    {
        private static Dictionary<string, bool> _units;


        private static bool ContainsKey(string key) => _units.ContainsKey(key);
        private static string Key(UnitTypes unit, LevelTypes level, PlayerTypes player, byte idx) => unit.ToString() + level + player + idx;

        public static Dictionary<string, bool> Units
        {
            get
            {
                var dict = new Dictionary<string, bool>();
                foreach (var item in _units) dict.Add(item.Key, item.Value);
                return dict;
            }
        }
        public static List<byte> Idxs(UnitTypes unit, LevelTypes lev, PlayerTypes player)
        {
            var list = new List<byte>();

            for (byte idx = 0; idx < CellValues.AMOUNT_ALL_CELLS; idx++)
            {
                if (_units[Key(unit, lev, player, idx)])
                    list.Add(idx);
            }
            return list;
        }
        public static bool HaveMyHeroInGame
        {
            get
            {
                for (byte idx = 0; idx < CellValues.AMOUNT_ALL_CELLS; idx++)
                {
                    if (_units[Key(UnitTypes.Elfemale, LevelTypes.First, WhoseMoveC.CurPlayerI, idx)])
                    {
                        return true;
                    }
                }
                return false;
            }
        }



        static WhereUnitsC()
        {
            _units = new Dictionary<string, bool>();

            for (var unit = UnitTypes.First; unit < UnitTypes.End; unit++)
            {
                for (var lev = LevelTypes.First; lev < LevelTypes.End; lev++)
                {
                    for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                    {
                        for (byte idx = 0; idx < CellValues.AMOUNT_ALL_CELLS; idx++)
                        {
                            _units.Add(Key(unit, lev, player, idx), false);
                        }
                    }
                }
            }
        }
        public WhereUnitsC(bool needReset)
        {
            if (needReset) foreach (var item in Units) _units[item.Key] = false;
            else throw new Exception();
        }


        
        internal static void Set(UnitTypes unit, LevelTypes lev, PlayerTypes player, byte idx, bool have)
        {
            var key = Key(unit, lev, player, idx);

            if (!ContainsKey(key)) throw new Exception();
            if (_units[key] == have) throw new Exception();

            _units[key] = have;
        }
        public static void Sync(string key, bool have)
        {
            if (!ContainsKey(key)) throw new Exception();

            _units[key] = have;
        }
    }
}