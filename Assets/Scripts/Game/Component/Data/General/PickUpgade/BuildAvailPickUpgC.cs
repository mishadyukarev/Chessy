using System.Collections.Generic;

namespace Game.Game
{
    public struct BuildAvailPickUpgC
    {
        private static Dictionary<string, bool> _available;

        private static string Key(BuildTypes build, PlayerTypes player) => build.ToString() + player;
        public static Dictionary<string, bool> Available
        {
            get
            {
                var dict = new Dictionary<string, bool>();
                foreach (var item in _available) dict.Add(item.Key, item.Value);
                return dict;
            }
        }
        public static Dictionary<BuildTypes, Dictionary<PlayerTypes, bool>> Available_1
        {
            get
            {
                var dict = new Dictionary<BuildTypes, Dictionary<PlayerTypes, bool>>();

                for (var build = BuildTypes.Farm; build <= BuildTypes.Mine; build++)
                {
                    dict.Add(build, new Dictionary<PlayerTypes, bool>());

                    for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                    {
                        dict[build].Add(player, _available[Key(build, player)]);
                    }
                }

                return dict;
            }
        }


        public BuildAvailPickUpgC(Dictionary<string, bool> avail)
        {
            _available = avail;

            for (var build = BuildTypes.Farm; build <= BuildTypes.Mine; build++)
            {
                for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                {
                    _available.Add(Key(build, player), true);
                }
            }
        }


        public static void Set(BuildTypes build, PlayerTypes player, bool available)
        {
            _available[Key(build, player)] = available;
        }

        public static void Sync(string key, bool available)
        {
            _available[key] = available;
        }
    }
}