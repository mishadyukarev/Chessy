using System;
using System.Collections.Generic;

namespace Scripts.Game
{
    internal struct BuildsInGameCom
    {
        private Dictionary<PlayerTypes, Dictionary<BuildingTypes, List<byte>>> _buildsInGame;

        internal BuildsInGameCom(bool needNew) : this()
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

        internal void Add(PlayerTypes playerType, BuildingTypes buildType, byte idxCell) => _buildsInGame[playerType][buildType].Add(idxCell);
        internal void Remove(PlayerTypes playerType, BuildingTypes buildType, byte idxCell) => _buildsInGame[playerType][buildType].Remove(idxCell);
    }
}
