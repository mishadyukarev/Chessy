using System;
using System.Collections.Generic;

namespace Chessy.Game
{
    public struct BuildsUpgC
    {
        private static Dictionary<PlayerTypes, Dictionary<BuildTypes, bool>> _haveUpgrades;

        public static Dictionary<PlayerTypes, Dictionary<BuildTypes, bool>> HaveUpgrades
        {
            get
            {
                var dict = new Dictionary<PlayerTypes, Dictionary<BuildTypes, bool>>();
                foreach (var item_0 in _haveUpgrades)
                {
                    dict.Add(item_0.Key, new Dictionary<BuildTypes, bool>());
                    foreach (var item_1 in item_0.Value)
                    {
                        dict[item_0.Key].Add(item_1.Key, item_1.Value);
                    }
                }
                return dict;
            }
        }

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

        public static void Sync(PlayerTypes player, BuildTypes build, bool have)
        {
            _haveUpgrades[player][build] = have;
        }
    }
}
