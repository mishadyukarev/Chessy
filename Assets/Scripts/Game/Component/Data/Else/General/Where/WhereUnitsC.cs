using System;
using System.Collections.Generic;

namespace Chessy.Game
{
    public struct WhereUnitsC
    {
        private static Dictionary<PlayerTypes, Dictionary<UnitTypes, Dictionary<LevelUnitTypes, List<byte>>>> _unitsInGame;

        public static Dictionary<PlayerTypes, Dictionary<UnitTypes, Dictionary<LevelUnitTypes, List<byte>>>> UnitsInGame
        {
            get
            {
                var newDict_0 = new Dictionary<PlayerTypes, Dictionary<UnitTypes, Dictionary<LevelUnitTypes, List<byte>>>>();

                foreach (var item_0 in _unitsInGame)
                {
                    newDict_0.Add(item_0.Key, new Dictionary<UnitTypes, Dictionary<LevelUnitTypes, List<byte>>>());

                    foreach (var item_1 in item_0.Value)
                    {
                        newDict_0[item_0.Key].Add(item_1.Key, new Dictionary<LevelUnitTypes, List<byte>>());

                        foreach (var item_2 in item_1.Value)
                        {
                            newDict_0[item_0.Key][item_1.Key].Add(item_2.Key, new List<byte>());

                            foreach (var item_3 in item_2.Value)
                            {
                                newDict_0[item_0.Key][item_1.Key][item_2.Key].Add(item_3);
                            }
                        }
                    }
                }

                return newDict_0;
            }
        }

        public WhereUnitsC(bool needNew) : this()
        {
            if (needNew)
            {
                _unitsInGame = new Dictionary<PlayerTypes, Dictionary<UnitTypes, Dictionary<LevelUnitTypes, List<byte>>>>();

                _unitsInGame.Add(PlayerTypes.First, new Dictionary<UnitTypes, Dictionary<LevelUnitTypes, List<byte>>>());
                _unitsInGame.Add(PlayerTypes.Second, new Dictionary<UnitTypes, Dictionary<LevelUnitTypes, List<byte>>>());


                for (var unitType = UnitTypes.First; unitType < UnitTypes.End; unitType++)
                {
                    _unitsInGame[PlayerTypes.First].Add(unitType, new Dictionary<LevelUnitTypes, List<byte>>());
                    _unitsInGame[PlayerTypes.Second].Add(unitType, new Dictionary<LevelUnitTypes, List<byte>>());

                    for (var levUnit = (LevelUnitTypes)1; levUnit < (LevelUnitTypes)typeof(LevelUnitTypes).GetEnumNames().Length; levUnit++)
                    {
                        _unitsInGame[PlayerTypes.First][unitType].Add(levUnit, new List<byte>());
                        _unitsInGame[PlayerTypes.Second][unitType].Add(levUnit, new List<byte>());
                    }

                }
            }
        }

        private static bool Contains(PlayerTypes playerType, UnitTypes unitType, LevelUnitTypes levelUnit, byte idx) => _unitsInGame[playerType][unitType][levelUnit].Contains(idx);
        public static void Add(PlayerTypes playerType, UnitTypes unitType, LevelUnitTypes levelUnit, byte idxCell)
        {
            if (!Contains(playerType, unitType, levelUnit, idxCell)) _unitsInGame[playerType][unitType][levelUnit].Add(idxCell);
            else throw new Exception();
        }
        public static void Remove(PlayerTypes playerType, UnitTypes unitType, LevelUnitTypes levelUnit, byte idxCell)
        {
            if (Contains(playerType, unitType, levelUnit, idxCell)) _unitsInGame[playerType][unitType][levelUnit].Remove(idxCell);
            else throw new Exception();
        }
        public static void Sync(PlayerTypes playerType, UnitTypes unitType, LevelUnitTypes levelUnit, byte idx)
        {
            _unitsInGame[playerType][unitType][levelUnit].Add(idx);
        }
        public static void Clear(PlayerTypes playerType, UnitTypes unitType, LevelUnitTypes levelUnit)
        {
            _unitsInGame[playerType][unitType][levelUnit].Clear();
        }
        public static byte AmountUnits(PlayerTypes playerType, UnitTypes unitType, LevelUnitTypes levelUnit) => (byte)_unitsInGame[playerType][unitType][levelUnit].Count;
        public static List<byte> IdxsUnits(PlayerTypes player, UnitTypes unit, LevelUnitTypes levelUnit) => _unitsInGame[player][unit][levelUnit].Copy();

        public static int AmountUnitsExcept(PlayerTypes player, UnitTypes unit)
        {
            var amountUnits = 0;

            foreach (var item_0 in _unitsInGame)
            {
                if (item_0.Key == player)
                {
                    foreach (var item_1 in item_0.Value)
                    {
                        if (item_1.Key != unit)
                        {
                            foreach (var item_2 in item_1.Value)
                            {
                                amountUnits += item_2.Value.Count;
                            }
                        }
                    }
                }
            }

            return amountUnits;
        }

        public static bool HaveMyHeroInGame
        {
            get
            {
                foreach (var item_0 in UnitsInGame)
                {
                    if (item_0.Key == WhoseMoveC.CurPlayerI)
                    {
                        foreach (var item_1 in item_0.Value)
                        {
                            if (item_1.Key >= UnitTypes.Elfemale)
                            {
                                foreach (var item_2 in item_1.Value)
                                {
                                    if (item_2.Value.Count == 1) return true;
                                    else if (item_2.Value.Count > 1) throw new Exception();
                                }
                            }
                        }
                    }
                }

                return false;
            }
        }
    }
}