using System;
using System.Collections.Generic;

namespace Scripts.Game
{
    public struct WhereBuildsC
    {
        private static Dictionary<PlayerTypes, Dictionary<BuildTypes, List<byte>>> _buildsInGame;

        public WhereBuildsC(bool needNew) : this()
        {
            if (needNew)
            {
                _buildsInGame = new Dictionary<PlayerTypes, Dictionary<BuildTypes, List<byte>>>();

                _buildsInGame.Add(PlayerTypes.First, new Dictionary<BuildTypes, List<byte>>());
                _buildsInGame.Add(PlayerTypes.Second, new Dictionary<BuildTypes, List<byte>>());


                for (BuildTypes buildingType = 0; buildingType < (BuildTypes)Enum.GetNames(typeof(BuildTypes)).Length; buildingType++)
                {
                    _buildsInGame[PlayerTypes.First].Add(buildingType, new List<byte>());
                    _buildsInGame[PlayerTypes.Second].Add(buildingType, new List<byte>());
                }
            }
        }

        private static bool Contains(PlayerTypes playerType, BuildTypes buildType, byte idx) => _buildsInGame[playerType][buildType].Contains(idx);
        public static void Add(PlayerTypes playerType, BuildTypes buildType, byte idxCell)
        {
            if (!Contains(playerType, buildType, idxCell)) _buildsInGame[playerType][buildType].Add(idxCell);
            else throw new Exception();
        }
        public static void Remove(PlayerTypes playerType, BuildTypes buildType, byte idxCell)
        {
            if (Contains(playerType, buildType, idxCell)) _buildsInGame[playerType][buildType].Remove(idxCell);
            else throw new Exception();
        }
        public static byte AmountBuilds(PlayerTypes playerType, BuildTypes buildType) => (byte)_buildsInGame[playerType][buildType].Count;


        public static bool IsSettedCity(PlayerTypes playerType) => _buildsInGame[playerType][BuildTypes.City].Count >= 1;
        public static byte IdxCity(PlayerTypes playerType) => _buildsInGame[playerType][BuildTypes.City][0];
    }
}
