using Assets.Scripts.Workers;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.ECS.Component
{
    internal struct XyBuildingsComponent
    {
        private Dictionary<BuildingTypes, Dictionary<bool, List<int[]>>> _buildingsInGameDict;

        internal XyBuildingsComponent(Dictionary<BuildingTypes, Dictionary<bool, List<int[]>>> dict)
        {
            _buildingsInGameDict = dict;

            for (BuildingTypes buildingType = 0; buildingType < (BuildingTypes)Enum.GetNames(typeof(BuildingTypes)).Length; buildingType++)
            {
                var dict1 = new Dictionary<bool, List<int[]>>();

                dict1.Add(true, new List<int[]>());
                dict1.Add(false, new List<int[]>());

                dict.Add(buildingType, dict1);
            }
        }

        private List<int[]> GetListXyBuild(BuildingTypes buildingType, bool key) => _buildingsInGameDict[buildingType][key];

        internal bool IsSettedCity(bool key) => GetAmountBuild(BuildingTypes.City, key) > 0;

        internal void SetXyBuildings(BuildingTypes buildingType, bool key, List<int[]> list) => _buildingsInGameDict[buildingType][key] = list.Copy();


        internal int[] GetXyBuildByIndex(BuildingTypes buildingType, bool key, int index) => (int[])GetListXyBuild(buildingType, key)[index].Clone();
        internal int GetAmountBuild(BuildingTypes buildingType, bool key) => GetListXyBuild(buildingType, key).Count;
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

        internal void AddXyBuild(BuildingTypes buildingType, bool key, int[] xyAdding) => GetListXyBuild(buildingType, key).Add(xyAdding);
        internal void RemoveXyBuild(BuildingTypes buildingType, bool key, int[] xyTaking)
        {
            if (!GetListXyBuild(buildingType, key).TryFindCellInListAndRemove(xyTaking)) throw new Exception();
        }
        internal void RemoveXyBuild(BuildingTypes buildingType, bool key, int index) => GetListXyBuild(buildingType, key).RemoveAt(index);



    }
}
