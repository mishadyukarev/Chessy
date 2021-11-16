using System.Collections.Generic;

namespace Chessy.Game
{
    public struct PickUpgC
    {
        private static Dictionary<PlayerTypes, bool> _haveUpgrade;


        public PickUpgC(Dictionary<PlayerTypes, bool> activatedZone)
        {
            _haveUpgrade = activatedZone;

            for (var player = PlayerTypes.Start; player < PlayerTypes.End; player++)
            {
                _haveUpgrade.Add(player, false);
            }
        }

        public static void SetHaveUpgrade(PlayerTypes player, bool isActivated) => _haveUpgrade[player] = isActivated;
        public static bool HaveUpgrade(PlayerTypes player) => _haveUpgrade[player];
    }
}