using System;
using System.Collections.Generic;

namespace Chessy.Game
{
    public struct BuildsUpgC
    {
        private static Dictionary<PlayerTypes, Dictionary<BuildTypes, bool>> _haveUpgrades;

        public BuildsUpgC(bool needNew) : this()
        {
            if (needNew)
            {
                _haveUpgrades = new Dictionary<PlayerTypes, Dictionary<BuildTypes, bool>>();

                _haveUpgrades.Add(PlayerTypes.First, new Dictionary<BuildTypes, bool>());
                _haveUpgrades.Add(PlayerTypes.Second, new Dictionary<BuildTypes, bool>());


                for (BuildTypes build = 0; build < (BuildTypes)Enum.GetNames(typeof(BuildTypes)).Length; build++)
                {
                    _haveUpgrades[PlayerTypes.First].Add(build, false);
                    _haveUpgrades[PlayerTypes.Second].Add(build, false);
                }
            }
        }

        public static void AddUpgrade(PlayerTypes player, BuildTypes build) => _haveUpgrades[player][build] = true;
        public static bool HaveUpgrade(PlayerTypes player, BuildTypes build) => _haveUpgrades[player][build];
    }
}
