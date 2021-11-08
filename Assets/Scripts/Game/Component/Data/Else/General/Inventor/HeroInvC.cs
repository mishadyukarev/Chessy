using System;
using System.Collections.Generic;

namespace Chessy.Game
{
    public readonly struct HeroInvC
    {
        private static Dictionary<PlayerTypes, UnitTypes> _heroes;

        public static Dictionary<PlayerTypes, UnitTypes> Heroes
        {
            get
            {
                var dict = new Dictionary<PlayerTypes, UnitTypes>();
                foreach (var item_0 in _heroes) dict.Add(item_0.Key, item_0.Value);
                return dict;
            }
        }

        static HeroInvC()
        {
            _heroes = new Dictionary<PlayerTypes, UnitTypes>();

            for (var player = (PlayerTypes)1; player < (PlayerTypes)typeof(PlayerTypes).GetEnumNames().Length; player++)
            {
                _heroes.Add(player, default);
            }
        }

        public static void Start()
        {
            foreach (var item_0 in Heroes)
            {
                _heroes[item_0.Key] = default;
            }
        }
        public static bool HaveHero(PlayerTypes player)
        {
            if (!_heroes.ContainsKey(player)) throw new Exception();

            return _heroes[player] != default;
        }
        public static void SetHero(PlayerTypes player, UnitTypes unit)
        {
            if (!_heroes.ContainsKey(player)) throw new Exception();
            if (_heroes.ContainsValue(unit)) throw new Exception();

            _heroes[player] = unit;
        }
        public static UnitTypes Hero(PlayerTypes player)
        {
            if (!_heroes.ContainsKey(player)) throw new Exception();

            return _heroes[player];
        }
    }
}