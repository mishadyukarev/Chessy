using System;
using System.Collections.Generic;

namespace Chessy.Game
{
    public struct UnitWaterUpgC
    {
        private static Dictionary<PlayerTypes, Dictionary<UnitTypes, float>> _percUpgs;

        public static Dictionary<PlayerTypes, Dictionary<UnitTypes, float>> PercUpgs
        {
            get
            {
                var dict = new Dictionary<PlayerTypes, Dictionary<UnitTypes, float>>();

                foreach (var item_0 in _percUpgs)
                {
                    dict.Add(item_0.Key, new Dictionary<UnitTypes, float>());

                    foreach (var item_1 in item_0.Value)
                    {
                        dict[item_0.Key].Add(item_1.Key, item_1.Value);
                    }
                }

                return dict;
            }
        }

        public UnitWaterUpgC(bool needNew) : this()
        {
            if (needNew)
            {
                _percUpgs = new Dictionary<PlayerTypes, Dictionary<UnitTypes, float>>();

                for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                {
                    _percUpgs.Add(player, new Dictionary<UnitTypes, float>());

                    for (var unit = UnitTypes.First; unit < UnitTypes.End; unit++)
                    {
                        _percUpgs[player][unit] = 0;
                    }
                }


            }
            else throw new Exception();
        }

        public static void AddUpg(PlayerTypes player, UnitTypes unit, float percent)
        {
            if (!_percUpgs.ContainsKey(player)) throw new Exception();
            if (!_percUpgs[player].ContainsKey(unit)) throw new Exception();

            _percUpgs[player][unit] += percent;
        }

        public static float UpgPercent(PlayerTypes player, UnitTypes unit)
        {
            if (!_percUpgs.ContainsKey(player)) throw new Exception();
            if (!_percUpgs[player].ContainsKey(unit)) throw new Exception();

            return _percUpgs[player][unit];
        }

        public static void Sync(PlayerTypes player, UnitTypes unit, float percent)
        {
            _percUpgs[player][unit] = percent;
        }
    }
}