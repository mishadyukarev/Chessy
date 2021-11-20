using System.Collections.Generic;

namespace Game.Game
{
    public struct WaterAvailPickUpgC
    {
        private static Dictionary<PlayerTypes, bool> _available;

        public static Dictionary<PlayerTypes, bool> Available
        {
            get
            {
                var dict = new Dictionary<PlayerTypes, bool>();
                foreach (var item in _available) dict.Add(item.Key, item.Value);
                return dict;
            }
        }

        public WaterAvailPickUpgC(Dictionary<PlayerTypes, bool> avail)
        {
            _available = avail;

            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                _available.Add(player, true);
            }
        }


        public static void Set(PlayerTypes player, bool available = true)
        {
            _available[player] = available;
        }

        public static void Sync(PlayerTypes player, bool available)
        {
            _available[player] = available;
        }
    }
}