using System;
using System.Collections.Generic;

namespace Chessy.Game
{
    public struct ScoutHeroCooldownC
    {
        private static Dictionary<PlayerTypes, Dictionary<UnitTypes, int>> _cooldowns;

        public static Dictionary<PlayerTypes, Dictionary<UnitTypes, int>> Cooldowns
        {
            get
            {
                var dict = new Dictionary<PlayerTypes, Dictionary<UnitTypes, int>>();
                foreach (var item_0 in _cooldowns)
                {
                    dict.Add(item_0.Key, new Dictionary<UnitTypes, int>());
                    foreach (var item_1 in item_0.Value)
                    {
                        dict[item_0.Key].Add(item_1.Key, item_1.Value);
                    }
                }

                return dict;
            }
        }

        public static bool HaveCooldown(PlayerTypes player, UnitTypes unit)
        {
            if (!_cooldowns.ContainsKey(player)) throw new Exception();
            if (!_cooldowns[player].ContainsKey(unit)) throw new Exception();

            return _cooldowns[player][unit] > 0;
        }

        public static int Cooldown(PlayerTypes player, UnitTypes unit)
        {
            if (!_cooldowns.ContainsKey(player)) throw new Exception();
            if (!_cooldowns[player].ContainsKey(unit)) throw new Exception();

            return _cooldowns[player][unit];
        }


        public ScoutHeroCooldownC(bool isStart)
        {
            if (isStart)
            {
                if (_cooldowns == default)
                {
                    _cooldowns = new Dictionary<PlayerTypes, Dictionary<UnitTypes, int>>();

                    for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                    {
                        _cooldowns.Add(player, new Dictionary<UnitTypes, int>());

                        _cooldowns[player].Add(UnitTypes.Scout, 0);
                        _cooldowns[player].Add(UnitTypes.Elfemale, 0);
                    }
                }

                else
                {
                    foreach (var item_0 in Cooldowns)
                    {
                        foreach (var item_1 in item_0.Value)
                        {
                            _cooldowns[item_0.Key][item_1.Key] = 0;
                        }
                    }
                }
            }

            else throw new Exception();
        }


        public static void StartGame()
        {

        }

        public static void SetStandCooldown(PlayerTypes player, UnitTypes unit)
        {
            if (!_cooldowns.ContainsKey(player)) throw new Exception();
            if (!_cooldowns[player].ContainsKey(unit)) throw new Exception();

            switch (unit)
            {
                case UnitTypes.Scout:
                    _cooldowns[player][unit] = 3;
                    break;

                case UnitTypes.Elfemale:
                    _cooldowns[player][unit] = 5;
                    break;

                default: throw new Exception();
            }
        }

        public static void TakeCooldown(PlayerTypes player, UnitTypes unit)
        {
            if (!_cooldowns.ContainsKey(player)) throw new Exception();
            if (!_cooldowns[player].ContainsKey(unit)) throw new Exception();

            _cooldowns[player][unit] -= 1;
        }

        public static void Sync(PlayerTypes player, UnitTypes unit, int cooldown)
        {
            _cooldowns[player][unit] = cooldown;
        }
    }
}