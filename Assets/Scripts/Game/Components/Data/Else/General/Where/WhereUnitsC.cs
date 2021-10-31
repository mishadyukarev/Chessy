using System;
using System.Collections.Generic;

namespace Scripts.Game
{
    public struct WhereUnitsC
    {
        private static Dictionary<PlayerTypes, Dictionary<UnitTypes,Dictionary<LevelUnitTypes, List<byte>>>> _buildsInGame;

        public WhereUnitsC(bool needNew) : this()
        {
            if (needNew)
            {
                _buildsInGame = new Dictionary<PlayerTypes, Dictionary<UnitTypes, Dictionary<LevelUnitTypes, List<byte>>>>();

                _buildsInGame.Add(PlayerTypes.First, new Dictionary<UnitTypes, Dictionary<LevelUnitTypes, List<byte>>>());
                _buildsInGame.Add(PlayerTypes.Second, new Dictionary<UnitTypes, Dictionary<LevelUnitTypes, List<byte>>>());


                for (var unitType = (UnitTypes)1; unitType < (UnitTypes)Enum.GetNames(typeof(UnitTypes)).Length; unitType++)
                {
                    _buildsInGame[PlayerTypes.First].Add(unitType, new Dictionary<LevelUnitTypes, List<byte>>());
                    _buildsInGame[PlayerTypes.Second].Add(unitType, new Dictionary<LevelUnitTypes, List<byte>>());

                    for (var levUnit = (LevelUnitTypes)1; levUnit < (LevelUnitTypes)typeof(LevelUnitTypes).GetEnumNames().Length; levUnit++)
                    {
                        _buildsInGame[PlayerTypes.First][unitType].Add(levUnit, new List<byte>());
                        _buildsInGame[PlayerTypes.Second][unitType].Add(levUnit, new List<byte>());
                    }
                    
                }
            }
        }

        private static bool Contains(PlayerTypes playerType, UnitTypes unitType, LevelUnitTypes levelUnit, byte idx) => _buildsInGame[playerType][unitType][levelUnit].Contains(idx);
        public static void Add(PlayerTypes playerType, UnitTypes unitType, LevelUnitTypes levelUnit, byte idxCell)
        {
            if (!Contains(playerType, unitType, levelUnit, idxCell)) _buildsInGame[playerType][unitType][levelUnit].Add(idxCell);
            else throw new Exception();
        }
        public static void Remove(PlayerTypes playerType, UnitTypes unitType, LevelUnitTypes levelUnit, byte idxCell)
        {
            if (Contains(playerType, unitType, levelUnit, idxCell)) _buildsInGame[playerType][unitType][levelUnit].Remove(idxCell);
            else throw new Exception();
        }
        public static byte AmountUnits(PlayerTypes playerType, UnitTypes unitType) => (byte)_buildsInGame[playerType][unitType].Count;
        public static List<byte> IdxsUnits(PlayerTypes player, UnitTypes unit, LevelUnitTypes levelUnit) => _buildsInGame[player][unit][levelUnit].Copy();
    }
}