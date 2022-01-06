using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct WhereUnitsC
    {
        static Dictionary<string, bool> _units;


        static bool ContainsKey(in string key) => _units.ContainsKey(key);
        static string Key(in (UnitTypes, LevelTypes, PlayerTypes) unit, in byte idx) => unit.Item1.ToString() + unit.Item2 + unit.Item3 + idx;

        public static Dictionary<string, bool> Units
        {
            get
            {
                var dict = new Dictionary<string, bool>();
                foreach (var item in _units) dict.Add(item.Key, item.Value);
                return dict;
            }
        }
        public static List<byte> Idxs(in UnitTypes unit, in LevelTypes lev, in PlayerTypes player)
        {
            var list = new List<byte>();

            for (byte idx = 0; idx < CellValues.ALL_CELLS_AMOUNT; idx++)
            {
                if (_units[Key((unit, lev, player), idx)])
                    list.Add(idx);
            }
            return list;
        }
        public static bool HaveMyHeroInGame
        {
            get
            {
                for (byte idx = 0; idx < CellValues.ALL_CELLS_AMOUNT; idx++)
                {
                    if (_units[Key((UnitTypes.Elfemale, LevelTypes.First, WhoseMoveC.CurPlayerI), idx)])
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
                        for (byte idx = 0; idx < CellValues.ALL_CELLS_AMOUNT; idx++)
                        {
                            _units.Add(Key((unit, lev, player), idx), false);
                        }
                    }
                }
            }
        }
        public WhereUnitsC(in bool needReset)
        {
            if (needReset) foreach (var item in Units) _units[item.Key] = false;
            else throw new Exception();
        }


        
        public static void Set(in (UnitTypes, LevelTypes, PlayerTypes) unit, in byte idx, in bool have)
        {
            var key = Key(unit, idx);

            if (!ContainsKey(key)) throw new Exception();
            if (_units[key] == have) throw new Exception();

            _units[key] = have;
        }

        public static void Sync(in string key, in bool have)
        {
            if (!ContainsKey(key)) throw new Exception();

            _units[key] = have;
        }
    }
}