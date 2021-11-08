using System;
using System.Collections.Generic;

namespace Chessy.Game
{
    public struct UnitPercUpgC
    {
        private static Dictionary<PlayerTypes, Dictionary<UnitTypes, Dictionary<UnitStatTypes, float>>> _percUpgs;

        public static Dictionary<PlayerTypes, Dictionary<UnitTypes, Dictionary<UnitStatTypes, float>>> PercUpgs
        {
            get
            {
                var dict = new Dictionary<PlayerTypes, Dictionary<UnitTypes, Dictionary<UnitStatTypes, float>>>();

                foreach (var item_0 in _percUpgs)
                {
                    dict.Add(item_0.Key, new Dictionary<UnitTypes, Dictionary<UnitStatTypes, float>>());

                    foreach (var item_1 in item_0.Value)
                    {
                        dict[item_0.Key].Add(item_1.Key, new Dictionary<UnitStatTypes, float>());

                        foreach (var item_2 in item_1.Value)
                        {
                            dict[item_0.Key][item_1.Key].Add(item_2.Key, item_2.Value);
                        }
                    }
                }

                return dict;
            }
        }


        public UnitPercUpgC(bool needNew) : this()
        {
            if (needNew)
            {
                _percUpgs = new Dictionary<PlayerTypes, Dictionary<UnitTypes, Dictionary<UnitStatTypes, float>>>();

                for (var player = (PlayerTypes)1; player < (PlayerTypes)typeof(PlayerTypes).GetEnumNames().Length; player++)
                {
                    _percUpgs.Add(player, new Dictionary<UnitTypes, Dictionary<UnitStatTypes, float>>());

                    for (var unit = (UnitTypes)1; unit < (UnitTypes)typeof(UnitTypes).GetEnumNames().Length; unit++)
                    {
                        _percUpgs[player].Add(unit, new Dictionary<UnitStatTypes, float>());

                        _percUpgs[player][unit].Add(UnitStatTypes.Damage, 0);
                        _percUpgs[player][unit].Add(UnitStatTypes.Water, 0);
                    }
                }


            }
            else throw new Exception();
        }

        public static void SetUpg(PlayerTypes player, UnitTypes unit, UnitStatTypes stat, float percent)
        {
            if (!_percUpgs.ContainsKey(player)) throw new Exception();
            if (!_percUpgs[player].ContainsKey(unit)) throw new Exception();
            if (!_percUpgs[player][unit].ContainsKey(stat)) throw new Exception();

            _percUpgs[player][unit][stat] = percent;
        }

        public static float UpgPercent(PlayerTypes player, UnitTypes unit, UnitStatTypes stat)
        {
            if (!_percUpgs.ContainsKey(player)) throw new Exception();
            if (!_percUpgs[player].ContainsKey(unit)) throw new Exception();
            if (!_percUpgs[player][unit].ContainsKey(stat)) throw new Exception();

            return _percUpgs[player][unit][stat];
        }
    }
}