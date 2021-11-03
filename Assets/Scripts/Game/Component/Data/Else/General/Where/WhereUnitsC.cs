using System;
using System.Collections.Generic;

namespace Scripts.Game
{
    public struct WhereUnitsC
    {
        private static Dictionary<PlayerTypes, Dictionary<UnitTypes,Dictionary<LevelUnitTypes, List<byte>>>> _unitsInGame;

        public WhereUnitsC(bool needNew) : this()
        {
            if (needNew)
            {
                _unitsInGame = new Dictionary<PlayerTypes, Dictionary<UnitTypes, Dictionary<LevelUnitTypes, List<byte>>>>();

                _unitsInGame.Add(PlayerTypes.First, new Dictionary<UnitTypes, Dictionary<LevelUnitTypes, List<byte>>>());
                _unitsInGame.Add(PlayerTypes.Second, new Dictionary<UnitTypes, Dictionary<LevelUnitTypes, List<byte>>>());


                for (var unitType = (UnitTypes)1; unitType < (UnitTypes)Enum.GetNames(typeof(UnitTypes)).Length; unitType++)
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
        public static byte AmountUnits(PlayerTypes playerType, UnitTypes unitType, LevelUnitTypes levelUnit) => (byte)_unitsInGame[playerType][unitType][levelUnit].Count;
        public static List<byte> IdxsUnits(PlayerTypes player, UnitTypes unit, LevelUnitTypes levelUnit) => _unitsInGame[player][unit][levelUnit].Copy();

        public static int AmountUnitsExcept(PlayerTypes player, UnitTypes unit)
        {
            var amountUnits = 0;

            for (var unitType = (UnitTypes)1; unitType < (UnitTypes)Enum.GetNames(typeof(UnitTypes)).Length; unitType++)
            {
                if(unitType != unit)
                {
                    for (var levUnit = (LevelUnitTypes)1; levUnit < (LevelUnitTypes)typeof(LevelUnitTypes).GetEnumNames().Length; levUnit++)
                    {
                        amountUnits += _unitsInGame[player][unitType][levUnit].Count;
                    }
                }
            }

            return amountUnits;
        }
    }
}