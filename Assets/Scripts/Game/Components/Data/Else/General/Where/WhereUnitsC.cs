using System;
using System.Collections.Generic;

namespace Scripts.Game
{
    public struct WhereUnitsC
    {
        private static Dictionary<PlayerTypes, Dictionary<UnitTypes, List<byte>>> _buildsInGame;

        public WhereUnitsC(bool needNew) : this()
        {
            if (needNew)
            {
                _buildsInGame = new Dictionary<PlayerTypes, Dictionary<UnitTypes, List<byte>>>();

                _buildsInGame.Add(PlayerTypes.First, new Dictionary<UnitTypes, List<byte>>());
                _buildsInGame.Add(PlayerTypes.Second, new Dictionary<UnitTypes, List<byte>>());


                for (var unitType = (UnitTypes)1; unitType < (UnitTypes)Enum.GetNames(typeof(UnitTypes)).Length; unitType++)
                {
                    _buildsInGame[PlayerTypes.First].Add(unitType, new List<byte>());
                    _buildsInGame[PlayerTypes.Second].Add(unitType, new List<byte>());
                }
            }
        }

        private static bool Contains(PlayerTypes playerType, UnitTypes unitType, byte idx) => _buildsInGame[playerType][unitType].Contains(idx);
        public static void Add(PlayerTypes playerType, UnitTypes unitType, byte idxCell)
        {
            if (!Contains(playerType, unitType, idxCell)) _buildsInGame[playerType][unitType].Add(idxCell);
            else throw new Exception();
        }
        public static void Remove(PlayerTypes playerType, UnitTypes unitType, byte idxCell)
        {
            if (Contains(playerType, unitType, idxCell)) _buildsInGame[playerType][unitType].Remove(idxCell);
            else throw new Exception();
        }
        public static byte AmountUnits(PlayerTypes playerType, UnitTypes unitType) => (byte)_buildsInGame[playerType][unitType].Count;

    }
}