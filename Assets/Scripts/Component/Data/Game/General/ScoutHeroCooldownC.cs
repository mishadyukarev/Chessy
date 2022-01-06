using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct ScoutHeroCooldownC
    {
        private static Dictionary<string, int> _cooldowns;


        private static string Key(UnitTypes unit, PlayerTypes player) => unit.ToString() + player;
        private static bool ContainsKey(string key) => _cooldowns.ContainsKey(key);

        public static Dictionary<string, int> Cooldowns
        {
            get
            {
                var dict = new Dictionary<string, int>();
                foreach (var item in _cooldowns) dict.Add(item.Key, item.Value);
                return dict;
            }
        }

        public static int Cooldown(UnitTypes unit, PlayerTypes player)
        {
            var key = Key(unit, player);
            if (!ContainsKey(key)) throw new Exception();

            return _cooldowns[key];
        }
        public static bool HaveCooldown(UnitTypes unit, PlayerTypes player) => Cooldown(unit, player) > 0;


        static ScoutHeroCooldownC()
        {
            _cooldowns = new Dictionary<string, int>();

            for (var unit = UnitTypes.Scout; unit < UnitTypes.End; unit++)
            {
                for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                {
                    _cooldowns.Add(Key(unit, player), default);
                }
            }
        }
        public ScoutHeroCooldownC(bool needReset)
        {
            if (needReset) foreach (var item in Cooldowns) _cooldowns[item.Key] = 0;
            else throw new Exception();
        }

        public static void SetStandCooldown(UnitTypes unit, PlayerTypes player)
        {
            var key = Key(unit, player);
            if (!ContainsKey(key)) throw new Exception();

            switch (unit)
            {
                case UnitTypes.Scout:
                    _cooldowns[key] = 3;
                    break;

                case UnitTypes.Elfemale:
                    _cooldowns[key] = 5;
                    break;

                default: throw new Exception();
            }
        }

        public static void TakeCooldown(UnitTypes unit, PlayerTypes player)
        {
            var key = Key(unit, player);
            if (!ContainsKey(key)) throw new Exception();

            _cooldowns[key] -= 1;
        }

        public static void Sync(string key, int cooldown)
        {
            if (!ContainsKey(key)) throw new Exception();

            _cooldowns[key] = cooldown;
        }
    }
}