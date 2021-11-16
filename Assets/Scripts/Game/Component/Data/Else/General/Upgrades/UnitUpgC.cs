using System;
using System.Collections.Generic;

namespace Chessy.Game
{
    public struct UnitUpgC
    {
        private static Dictionary<string, bool> _upgrades;
        private const string BETWEEN = "_";

        private static string Key(UpgTypes upg, UnitStatTypes stat, UnitTypes unit, LevelUnitTypes level, PlayerTypes player)
            => upg.ToString() + BETWEEN + stat + BETWEEN + unit + BETWEEN + level + BETWEEN + player;

        public static Dictionary<string, bool> Upgrades
        {
            get
            {
                var dict = new Dictionary<string, bool>();
                foreach (var item in _upgrades) dict.Add(item.Key, item.Value);
                return dict;
            }
        }

        public static bool Have(UpgTypes upg, UnitStatTypes stat, UnitTypes unit, LevelUnitTypes level, PlayerTypes player)
            => _upgrades[Key(upg, stat, unit, level, player)];
        

        public UnitUpgC(Dictionary<string, bool> upgrades) : this()
        {
            _upgrades = upgrades;

            for (var upg = UpgTypes.Start; upg < UpgTypes.End; upg++)
            {
                for (var stat = UnitStatTypes.First; stat < UnitStatTypes.End; stat++)
                {
                    for (var unit = UnitTypes.First; unit < UnitTypes.End; unit++)
                    {
                        for (var level = LevelUnitTypes.First; level < LevelUnitTypes.End; level++)
                        {
                            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                            {
                                _upgrades.Add(Key(upg, stat, unit, level, player), false);
                            }
                        }
                    }
                }
            }
        }

        public static void AddUpg(UpgTypes upg, UnitStatTypes stat, UnitTypes unit, LevelUnitTypes level, PlayerTypes player)
        {
            var key = Key(upg, stat, unit, level, player);
            if (!_upgrades.ContainsKey(key)) throw new Exception();

            _upgrades[key] = true;
        }

        public static float UpgPercent(UpgTypes upg, UnitStatTypes stat, UnitTypes unit, LevelUnitTypes level, PlayerTypes player)
        {
            var key = Key(upg, stat, unit, level, player);
            if (!_upgrades.ContainsKey(key)) throw new Exception();

            if (_upgrades[key]) return 0.2f;
            else return 0;
        }

        public static void Sync(string key, bool have)
        {
            if (!_upgrades.ContainsKey(key)) throw new Exception();

            _upgrades[key] = have;
        }
    }
}