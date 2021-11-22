using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct InvUnitsC
    {
        private static Dictionary<string, int> _units;

        private static string Key(UnitTypes unit, LevelTypes level, PlayerTypes player) => unit.ToString() + level + player;
        private static bool ContainsKey(string key) => _units.ContainsKey(key);

        public static Dictionary<string, int> Units
        {
            get
            {
                var dict = new Dictionary<string, int>();
                foreach (var item in _units) dict.Add(item.Key, item.Value);
                return dict;
            }
        }
        public static bool Have(UnitTypes unit, LevelTypes level, PlayerTypes player) => Amount(unit, level, player) > 0;
        public static int Amount(UnitTypes unit, LevelTypes level, PlayerTypes player)
        {
            var key = Key(unit, level, player);

            if (!ContainsKey(key)) throw new Exception();

            return _units[key];
        }
        public static int AmountUnits(UnitTypes unit, PlayerTypes player)
        {
            return Amount(unit, LevelTypes.First, player) + Amount(unit, LevelTypes.Second, player);
        }



        static InvUnitsC()
        {
            _units = new Dictionary<string, int>();
            for (var unit = UnitTypes.First; unit < UnitTypes.End; unit++)
            {
                for (var level = LevelTypes.First; level < LevelTypes.End; level++)
                {
                    for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                    {
                        _units.Add(Key(unit, level, player), default);
                    }
                }
            }
        }
        public InvUnitsC(bool needReset) : this()
        {
            if (needReset) foreach (var item in Units) _units[item.Key] = 0;
            else throw new Exception();
        }


        public static void Sync(string key, int value)
        {
            if (!ContainsKey(key)) throw new Exception();

            _units[key] = value;
        }
        public static void AddUnit(UnitTypes unit, LevelTypes level, PlayerTypes player, int adding = 1)
        {
            var key = Key(unit, level, player);

            if (!ContainsKey(key)) throw new Exception();
            _units[Key(unit, level, player)] += adding;
        }
        public static void TakeUnit(PlayerTypes player, UnitTypes unit, LevelTypes level, int taking = 1)
        {
            var key = Key(unit, level, player);

            if (!ContainsKey(key)) throw new Exception();

            _units[Key(unit, level, player)] -= taking;
        }
    }
}
