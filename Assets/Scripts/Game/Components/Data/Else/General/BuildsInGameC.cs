using System;
using System.Collections.Generic;

namespace Scripts.Game
{
    public struct BuildsInGameC
    {
        private static Dictionary<PlayerTypes, Dictionary<BuildingTypes, List<byte>>> _buildsInGame;

        public BuildsInGameC(bool needNew) : this()
        {
            if (needNew)
            {
                _buildsInGame = new Dictionary<PlayerTypes, Dictionary<BuildingTypes, List<byte>>>();

                _buildsInGame.Add(PlayerTypes.First, new Dictionary<BuildingTypes, List<byte>>());
                _buildsInGame.Add(PlayerTypes.Second, new Dictionary<BuildingTypes, List<byte>>());


                for (BuildingTypes buildingType = 0; buildingType < (BuildingTypes)Enum.GetNames(typeof(BuildingTypes)).Length; buildingType++)
                {
                    _buildsInGame[PlayerTypes.First].Add(buildingType, new List<byte>());
                    _buildsInGame[PlayerTypes.Second].Add(buildingType, new List<byte>());
                }
            }
        }

        public static void Add(PlayerTypes playerType, BuildingTypes buildType, byte idxCell) => _buildsInGame[playerType][buildType].Add(idxCell);
        public static void Remove(PlayerTypes playerType, BuildingTypes buildType, byte idxCell) => _buildsInGame[playerType][buildType].Remove(idxCell);

        public static bool IsSettedCity(PlayerTypes playerType) => _buildsInGame[playerType][BuildingTypes.City].Count >= 1;
        public static byte IdxCity(PlayerTypes playerType) => _buildsInGame[playerType][BuildingTypes.City][0];
    }
}
