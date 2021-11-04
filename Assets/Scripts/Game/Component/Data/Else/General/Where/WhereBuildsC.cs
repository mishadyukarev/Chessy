using System;
using System.Collections.Generic;

namespace Scripts.Game
{
    public struct WhereBuildsC
    {
        private static Dictionary<PlayerTypes, Dictionary<BuildTypes, List<byte>>> _buildsInGame;

        public static Dictionary<PlayerTypes, Dictionary<BuildTypes, List<byte>>> BuildsInGame
        {
            get
            {
                var newDict_0 = new Dictionary<PlayerTypes, Dictionary<BuildTypes, List<byte>>>();

                foreach (var item_0 in _buildsInGame)
                {
                    newDict_0.Add(item_0.Key, new Dictionary<BuildTypes, List<byte>>());

                    foreach (var item_1 in item_0.Value)
                    {
                        newDict_0[item_0.Key].Add(item_1.Key, new List<byte>());

                        foreach (var item_2 in item_1.Value)
                        {
                            newDict_0[item_0.Key][item_1.Key].Add(item_2);
                        }
                    }
                }

                return newDict_0;
            }
        }

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
        public static void Sync(PlayerTypes playerType, BuildTypes buildType, byte idxCell)
        {
            _buildsInGame[playerType][buildType].Add(idxCell);
        }
        public static void Clear(PlayerTypes playerType, BuildTypes buildType)
        {
            _buildsInGame[playerType][buildType].Clear();
        }
        public static void Remove(PlayerTypes playerType, BuildTypes buildType, byte idxCell)
        {
            if (Contains(playerType, buildType, idxCell)) _buildsInGame[playerType][buildType].Remove(idxCell);
            else throw new Exception();
        }
        public static byte AmountBuilds(PlayerTypes playerType, BuildTypes buildType) => (byte)_buildsInGame[playerType][buildType].Count;
        public static List<byte> IdxBuilds(PlayerTypes playerType, BuildTypes buildType) => _buildsInGame[playerType][buildType].Copy();

        public static bool IsSettedCity(PlayerTypes playerType) => _buildsInGame[playerType][BuildTypes.City].Count >= 1;
        public static byte IdxCity(PlayerTypes playerType) => _buildsInGame[playerType][BuildTypes.City][0];
    }
}
