using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct UnitUpgC
    {
        private static Dictionary<string, bool> _upgrades;
        private const string BETWEEN = "_";

        private static string Key(UpgTypes upg, UnitStatTypes stat, UnitTypes unit, LevelTypes level, PlayerTypes player)
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

        public static bool Have(UpgTypes upg, UnitStatTypes stat, UnitTypes unit, LevelTypes level, PlayerTypes player)
            => _upgrades[Key(upg, stat, unit, level, player)];

        public static float UpgPercent(UnitStatTypes stat, UnitTypes unit, LevelTypes level, PlayerTypes player)
        {
            if (stat == UnitStatTypes.Steps) throw new Exception();

            var percent = 0f;

            for (var upg = UpgTypes.First; upg < UpgTypes.End; upg++)
            {
                var key = Key(upg, stat, unit, level, player);
                if (!_upgrades.ContainsKey(key)) throw new Exception();

                if (_upgrades[key])
                {
                    if (stat == UnitStatTypes.Damage)
                    {
                        percent += 0.2f;
                    }
                    else if (stat == UnitStatTypes.Water)
                    {
                        percent += 0.2f;
                    }
                }
            }

            return percent;
        }
        public static int Steps(UnitTypes unit, LevelTypes level, PlayerTypes player)
        {
            var steps = 0;

            for (var upg = UpgTypes.First; upg < UpgTypes.End; upg++)
            {
                var key = Key(upg, UnitStatTypes.Steps, unit, level, player);
                if (!_upgrades.ContainsKey(key)) throw new Exception();

                switch (upg)
                {
                    case UpgTypes.None: throw new Exception();

                    case UpgTypes.PickCenter:
                        if (_upgrades[key]) steps += 2;
                        break;

                    case UpgTypes.End: throw new Exception();

                    default: throw new Exception();
                }

                
            }

            return steps;
        }

        static UnitUpgC()
        {
            _upgrades = new Dictionary<string, bool>();

            for (var upg = UpgTypes.First; upg < UpgTypes.End; upg++)
            {
                for (var stat = UnitStatTypes.First; stat < UnitStatTypes.End; stat++)
                {
                    for (var unit = UnitTypes.First; unit < UnitTypes.End; unit++)
                    {
                        for (var level = LevelTypes.First; level < LevelTypes.End; level++)
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

        public static void StartGame()
        {
            foreach (var item in Upgrades) _upgrades[item.Key] = false;
        }


        public static void AddUpg(UpgTypes upg, UnitStatTypes stat, UnitTypes unit, LevelTypes level, PlayerTypes player)
        {
            var key = Key(upg, stat, unit, level, player);
            if (!_upgrades.ContainsKey(key)) throw new Exception();

            _upgrades[key] = true;
        }



        public static void Sync(string key, bool have)
        {
            if (!_upgrades.ContainsKey(key)) throw new Exception();

            _upgrades[key] = have;
        }
    }
}