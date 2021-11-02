using System.Collections.Generic;

namespace Scripts.Game
{
    public struct PickUpgZoneDataUIC
    {
        private static Dictionary<PlayerTypes, bool> _activatedZone;
        private static Dictionary<UpgButTypes, bool> _activated_Buts;

        public static Dictionary<UpgButTypes, bool> Activated_Buts
        {
            get
            {
                var dict = new Dictionary<UpgButTypes, bool>();
                foreach (var item in _activated_Buts) dict.Add(item.Key, item.Value);
                return dict;
            }
        }

        public PickUpgZoneDataUIC(Dictionary<PlayerTypes, bool> activatedZone)
        {
            _activatedZone = activatedZone;
            _activated_Buts = new Dictionary<UpgButTypes, bool>();

            for (PlayerTypes player = 0; player < (PlayerTypes)typeof(PlayerTypes).GetEnumNames().Length; player++)
            {
                _activatedZone.Add(player, false);
            }

            foreach (var upgBut in typeof(UpgButTypes).GetEnumValues())
            {
                _activated_Buts.Add((UpgButTypes)upgBut, false);
            }
        }

        public static void SetActive(PlayerTypes player, bool isActivated) => _activatedZone[player] = isActivated;
        public static bool IsActivated(PlayerTypes player) => _activatedZone[player];

        public static void SetActive(UpgButTypes but, bool isActivated) => _activated_Buts[but] = isActivated;
        public static bool IsActivated(UpgButTypes but) => _activated_Buts[but];
    }
}