using Assets.Scripts.Workers;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.ECS.Component
{
    internal struct IdxBuildingsComponent
    {
        private Dictionary<BuildingTypes, Dictionary<bool, List<byte>>> _buildingsInGameDict;

        internal IdxBuildingsComponent(Dictionary<BuildingTypes, Dictionary<bool, List<byte>>> dict)
        {
            _buildingsInGameDict = dict;

            for (BuildingTypes buildingType = 0; buildingType < (BuildingTypes)Enum.GetNames(typeof(BuildingTypes)).Length; buildingType++)
            {
                var dict1 = new Dictionary<bool, List<byte>>();

                dict1.Add(true, new List<byte>());
                dict1.Add(false, new List<byte>());

                dict.Add(buildingType, dict1);
            }
        }

        internal List<byte> GetListIdxBuild(BuildingTypes buildingType, bool key) => _buildingsInGameDict[buildingType][key];

        internal bool IsSettedCity(bool key) => GetAmountBuild(BuildingTypes.City, key) > 0;

        internal void SetXyBuildings(BuildingTypes buildingType, bool key, List<byte> list) => _buildingsInGameDict[buildingType][key] = list.Copy();

        internal int GetAmountBuild(BuildingTypes buildingType, bool key) => GetListIdxBuild(buildingType, key).Count;
        internal int GetAmountAllBuild(bool key)
        {
            var amountAllBuildInGame = 0;

            for (int curNumberBuilType = 1; curNumberBuilType < Enum.GetNames(typeof(BuildingTypes)).Length; curNumberBuilType++)
            {
                amountAllBuildInGame += GetAmountBuild((BuildingTypes)curNumberBuilType, key);
            }

            return amountAllBuildInGame;
        }
        internal int GetAmountAllBuild() => GetAmountAllBuild(true) + GetAmountAllBuild(false);

        internal void AddIdxBuild(BuildingTypes buildingType, bool key, byte idxAdding) => GetListIdxBuild(buildingType, key).Add(idxAdding);
        internal void RemoveIdxBuild(BuildingTypes buildingType, bool key, byte idxTaking)
        {
            if (!GetListIdxBuild(buildingType, key).TryFindCellInListAndRemove(idxTaking)) throw new Exception();
        }
    }
}
