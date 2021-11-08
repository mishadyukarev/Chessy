using System;
using System.Collections.Generic;

namespace Chessy.Game
{
    public struct UnitStepUpgC
    {
        private static Dictionary<PlayerTypes, Dictionary<UnitTypes, int>> _stepUpgs;

        public static Dictionary<PlayerTypes, Dictionary<UnitTypes, int>> StepUpgs
        {
            get
            {
                var dict = new Dictionary<PlayerTypes, Dictionary<UnitTypes, int>>();

                foreach (var item_0 in _stepUpgs)
                {
                    dict.Add(item_0.Key, new Dictionary<UnitTypes, int>());

                    foreach (var item_1 in item_0.Value)
                    {
                        dict[item_0.Key].Add(item_1.Key, item_1.Value);
                    }
                }

                return dict;
            }
        }

        public UnitStepUpgC(Dictionary<PlayerTypes, Dictionary<UnitTypes, int>> stepUpg)
        {
            _stepUpgs = stepUpg;

            for (var player = (PlayerTypes)1; player < (PlayerTypes)typeof(PlayerTypes).GetEnumNames().Length; player++)
            {
                _stepUpgs.Add(player, new Dictionary<UnitTypes, int>());

                for (var unit = (UnitTypes)1; unit < (UnitTypes)typeof(UnitTypes).GetEnumNames().Length; unit++)
                {
                    _stepUpgs[player].Add(unit, 0);
                }
            }
        }

        public static void SetStepUpg(PlayerTypes player, UnitTypes unit, int steps)
        {
            if (!_stepUpgs.ContainsKey(player)) throw new Exception();
            if (!_stepUpgs[player].ContainsKey(unit)) throw new Exception();

            _stepUpgs[player][unit] = steps;
        }

        public static int UpgSteps(PlayerTypes player, UnitTypes unit) => _stepUpgs[player][unit];
    }
}