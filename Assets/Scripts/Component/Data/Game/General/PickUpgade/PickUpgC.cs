using System.Collections.Generic;

namespace Game.Game
{
    public struct PickUpgC
    {
        private static Dictionary<PlayerTypes, bool> _haveUpgrade;

        public static Dictionary<PlayerTypes, bool> HaveUpgrades
        {
            get
            {
                var dict = new Dictionary<PlayerTypes, bool>();
                foreach (var item in _haveUpgrade) dict.Add(item.Key, item.Value);
                return dict;
            }
        }
        public static bool HaveUpgrade(PlayerTypes player) => _haveUpgrade[player];


        public PickUpgC(Dictionary<PlayerTypes, bool> activatedZone)
        {
            _haveUpgrade = activatedZone;

            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                _haveUpgrade.Add(player, false);
            }
        }


        public static void SetHaveUpgrade(PlayerTypes player, bool isActivated) => _haveUpgrade[player] = isActivated;
        public static void Sync(PlayerTypes player, bool isActivated)
        {
            _haveUpgrade[player] = isActivated;
        }
    }
}