using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct BuildsUpgC
    {
        static Dictionary<string, bool> _haveUpgrades;

        static string Key(BuildTypes build, PlayerTypes player, UpgTypes upg) => build.ToString() + player + upg;
        public static Dictionary<string, bool> HaveUpgrades
        {
            get
            {
                var dict = new Dictionary<string, bool>();
                foreach (var item in _haveUpgrades) dict.Add(item.Key, item.Value);
                return dict;
            }
        }
        public static float PercUpg(BuildTypes build, PlayerTypes player)
        {
            var percent = 0f;

            for (var upg = UpgTypes.First; upg < UpgTypes.End; upg++)
            {
                if (_haveUpgrades[Key(build, player, upg)])
                {
                    percent = 1;
                }
            }

            return percent;
        }


        static BuildsUpgC()
        {
            _haveUpgrades = new Dictionary<string, bool>();

            for (var build = BuildTypes.First; build < BuildTypes.End; build++)
            {
                for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                {
                    for (var upg = UpgTypes.First; upg < UpgTypes.End; upg++)
                    {
                        _haveUpgrades.Add(Key(build, player, upg), default);
                    }
                }
            }
        }

        public static void Start()
        {
            foreach (var item in HaveUpgrades) _haveUpgrades[item.Key] = false;
        }


        public static void AddUpgrade(BuildTypes build, PlayerTypes player, UpgTypes upg) => _haveUpgrades[Key(build, player, upg)] = true;

        public static void Sync(string key, bool have)
        {
            _haveUpgrades[key] = have;
        }
    }
}
