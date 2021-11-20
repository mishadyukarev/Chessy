using System.Collections.Generic;

namespace Game.Game
{
    public struct UnitAvailPickUpgC
    {
        private static Dictionary<string, bool> _available;

        private static string Key(UnitTypes unit, PlayerTypes player) => unit.ToString() + player;
        public static Dictionary<string, bool> Available_0
        {
            get
            {
                var dict = new Dictionary<string, bool>();
                foreach (var item in _available) dict.Add(item.Key, item.Value);
                return dict;
            }
        }
        public static Dictionary<UnitTypes, Dictionary<PlayerTypes, bool>> Available_1
        {
            get
            {
                var dict = new Dictionary<UnitTypes, Dictionary<PlayerTypes, bool>>();

                for (var unit = UnitTypes.First; unit < UnitTypes.Elfemale; unit++)
                {
                    dict.Add(unit, new Dictionary<PlayerTypes, bool>());

                    for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                    {
                        dict[unit].Add(player, _available[Key(unit, player)]);
                    }
                }

                return dict;
            }
        }
        public static bool Availabled(UnitTypes unit, PlayerTypes player)
        {
            return _available[Key(unit, player)];
        }

        static UnitAvailPickUpgC()
        {
            _available = new Dictionary<string, bool>();

            for (var unit = UnitTypes.First; unit < UnitTypes.Elfemale; unit++)
            {
                for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                {
                    _available.Add(Key(unit, player), false);
                }
            }
        }

        public static void StartGame()
        {
            foreach (var item in Available_0)
            {
                _available[item.Key] = true;
            }
        }


        public static void Set(UnitTypes unit, PlayerTypes player, bool available)
        {
            _available[Key(unit, player)] = available;
        }

        public static void Sync(string key, bool available)
        {
            _available[key] = available;
        }
    }
}