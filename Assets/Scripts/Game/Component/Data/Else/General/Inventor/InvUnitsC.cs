using System;
using System.Collections.Generic;

namespace Scripts.Game
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


                for (var unitType = (UnitTypes)1; unitType < (UnitTypes)Enum.GetNames(typeof(UnitTypes)).Length; unitType++)
                {
                    _unitsInv[PlayerTypes.First].Add(unitType, new Dictionary<LevelUnitTypes, int>());
                    _unitsInv[PlayerTypes.Second].Add(unitType, new Dictionary<LevelUnitTypes, int>());


                    _unitsInv[PlayerTypes.First][unitType].Add(LevelUnitTypes.Wood, default);
                    _unitsInv[PlayerTypes.First][unitType].Add(LevelUnitTypes.Iron, default);

                    _unitsInv[PlayerTypes.Second][unitType].Add(LevelUnitTypes.Wood, default);
                    _unitsInv[PlayerTypes.Second][unitType].Add(LevelUnitTypes.Iron, default);
                }
            }
        }

        public static int AmountUnitsInInv(PlayerTypes playerType, UnitTypes unitType, LevelUnitTypes levelUnitType) => _unitsInv[playerType][unitType][levelUnitType];
        public static int AmountUnitsInInv(PlayerTypes playerType, UnitTypes unitType) => _unitsInv[playerType][unitType][LevelUnitTypes.Wood] + _unitsInv[playerType][unitType][LevelUnitTypes.Iron];
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

        public static void AddUnit(PlayerTypes playerType, UnitTypes unitType, LevelUnitTypes levelUnitType, int adding = 1) => Set(playerType, unitType, levelUnitType, AmountUnitsInInv(playerType, unitType, levelUnitType) + adding);
        public static void TakeUnitsInInv(PlayerTypes playerType, UnitTypes unitType, LevelUnitTypes levelUnitType, int taking = 1) => Set(playerType, unitType, levelUnitType, AmountUnitsInInv(playerType, unitType, levelUnitType) - taking);

        public static bool HaveUnitInInv(PlayerTypes playerType, UnitTypes unitType, LevelUnitTypes levelUnitType) => AmountUnitsInInv(playerType, unitType, levelUnitType) > 0;
    }
}
