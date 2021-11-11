using System;
using System.Collections.Generic;

namespace Chessy.Game
{
    public struct InvUnitsC
    {
        private static Dictionary<PlayerTypes, Dictionary<UnitTypes, Dictionary<LevelUnitTypes, int>>> _unitsInv;

        public static Dictionary<PlayerTypes, Dictionary<UnitTypes, Dictionary<LevelUnitTypes, int>>> Units
        {
            get
            {
                var dict_0 = new Dictionary<PlayerTypes, Dictionary<UnitTypes, Dictionary<LevelUnitTypes, int>>>();

                foreach (var item_0 in _unitsInv)
                {
                    dict_0.Add(item_0.Key, new Dictionary<UnitTypes, Dictionary<LevelUnitTypes, int>>());

                    foreach (var item_1 in item_0.Value)
                    {
                        dict_0[item_0.Key].Add(item_1.Key, new Dictionary<LevelUnitTypes, int>());
                        foreach (var item_2 in item_1.Value)
                        {
                            dict_0[item_0.Key][item_1.Key].Add(item_2.Key, item_2.Value);
                        }
                    }
                }

                return dict_0;
            }
        }

        public InvUnitsC(bool needNew) : this()
        {
            if (needNew)
            {
                _unitsInv = new Dictionary<PlayerTypes, Dictionary<UnitTypes, Dictionary<LevelUnitTypes, int>>>();

                _unitsInv.Add(PlayerTypes.First, new Dictionary<UnitTypes, Dictionary<LevelUnitTypes, int>>());
                _unitsInv.Add(PlayerTypes.Second, new Dictionary<UnitTypes, Dictionary<LevelUnitTypes, int>>());


                for (var unit = (UnitTypes)1; unit < (UnitTypes)Enum.GetNames(typeof(UnitTypes)).Length; unit++)
                {
                    _unitsInv[PlayerTypes.First].Add(unit, new Dictionary<LevelUnitTypes, int>());
                    _unitsInv[PlayerTypes.Second].Add(unit, new Dictionary<LevelUnitTypes, int>());


                    _unitsInv[PlayerTypes.First][unit].Add(LevelUnitTypes.First, default);
                    _unitsInv[PlayerTypes.First][unit].Add(LevelUnitTypes.Second, default);

                    _unitsInv[PlayerTypes.Second][unit].Add(LevelUnitTypes.First, default);
                    _unitsInv[PlayerTypes.Second][unit].Add(LevelUnitTypes.Second, default);
                }
            }
        }

        public static int AmountUnits(PlayerTypes player, UnitTypes unit, LevelUnitTypes level) => _unitsInv[player][unit][level];
        public static int AmountUnits(PlayerTypes playerType, UnitTypes unitType) => _unitsInv[playerType][unitType][LevelUnitTypes.First] + _unitsInv[playerType][unitType][LevelUnitTypes.Second];
        public static void Set(PlayerTypes player, UnitTypes unit, LevelUnitTypes level, int value) => _unitsInv[player][unit][level] = value;
        public static void SetStartAmountUnitAll()
        {
            var dict = Units;
            foreach (var item_0 in _unitsInv)
            {
                foreach (var item_1 in item_0.Value)
                {
                    foreach (var item_2 in item_1.Value)
                    {
                        dict[item_0.Key][item_1.Key][item_2.Key] = EconomyValues.StartAmountUnits(item_1.Key, item_2.Key);
                    }
                }
            }
            _unitsInv = dict;
        }

        public static void AddUnit(PlayerTypes player, UnitTypes unit, LevelUnitTypes level, int adding = 1) => Set(player, unit, level, AmountUnits(player, unit, level) + adding);
        public static void TakeUnit(PlayerTypes player, UnitTypes unit, LevelUnitTypes level, int taking = 1)
        {
            _unitsInv[player][unit][level] -= taking;
        }

        public static bool Have(PlayerTypes player, UnitTypes unit, LevelUnitTypes level) => AmountUnits(player, unit, level) > 0;

        public static UnitTypes MyHero
        {
            get
            {
                foreach (var item_0 in Units)
                {
                    if (item_0.Key == WhoseMoveC.CurPlayerI)
                    {
                        foreach (var item_1 in item_0.Value)
                        {
                            if (item_1.Key >= UnitTypes.Elfemale)
                            {
                                foreach (var item_2 in item_1.Value)
                                {
                                    if (item_2.Value > 1) throw new Exception();
                                    else if (item_2.Value == 1) return item_1.Key;
                                }
                            }
                        }
                    }
                }

                throw new Exception();
            }
        }
    }
}
