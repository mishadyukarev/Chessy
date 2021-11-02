using System;
using System.Collections.Generic;

namespace Scripts.Game
{
    public struct UnitsUpgC
    {
        private static Dictionary<PlayerTypes, Dictionary<UnitTypes, Dictionary<UnitStatTypes, float>>> _percUpgs;
        private static Dictionary<PlayerTypes, Dictionary<UnitTypes,  int>> _stepUpgs;

        public UnitsUpgC(bool needNew) : this()
        {
            if (needNew)
            {
                _percUpgs = new Dictionary<PlayerTypes, Dictionary<UnitTypes, Dictionary<UnitStatTypes, float>>>();
                _stepUpgs = new Dictionary<PlayerTypes, Dictionary<UnitTypes, int>>();

                for (var player = (PlayerTypes)1; player < (PlayerTypes)typeof(PlayerTypes).GetEnumNames().Length; player++)
                {
                    _percUpgs.Add(player, new Dictionary<UnitTypes, Dictionary<UnitStatTypes, float>>());
                    _stepUpgs.Add(player, new Dictionary<UnitTypes, int>());

                    for (var unit = (UnitTypes)1; unit < (UnitTypes)typeof(UnitTypes).GetEnumNames().Length; unit++)
                    {
                        _percUpgs[player].Add(unit, new Dictionary<UnitStatTypes, float>());
                        _stepUpgs[player].Add(unit, 0);

                        for (var unitUpg = (UnitStatTypes)1; unitUpg < (UnitStatTypes)typeof(UnitStatTypes).GetEnumNames().Length; unitUpg++)
                        {
                            _percUpgs[player][unit].Add(unitUpg, 0);
                        }
                    }
                }


            }
            else throw new Exception();
        }

        public static void SetUpg(PlayerTypes player, UnitTypes unit, UnitStatTypes stat, float percent)
        {
            if (player == default) throw new Exception();
            if (unit == default) throw new Exception();
            if (stat == default) throw new Exception();
            if (stat == UnitStatTypes.Steps) throw new Exception();

            _percUpgs[player][unit][stat] = percent; 
        }
        public static void SetStepUpg(PlayerTypes player, UnitTypes unit, int steps)
        {
            if (player == default) throw new Exception();
            if (unit == default) throw new Exception();

            _stepUpgs[player][unit] = steps;
        }
        public static float UpgPercent(PlayerTypes player, UnitTypes unit, UnitStatTypes stat)
        {
            if (player == default) throw new Exception();
            if (unit == default) throw new Exception();
            if (stat == default) throw new Exception();
            if (stat == UnitStatTypes.Steps) throw new Exception();

            return _percUpgs[player][unit][stat];
        }
        public static float UpgSteps(PlayerTypes player, UnitTypes unit) => _stepUpgs[player][unit];
    }
}