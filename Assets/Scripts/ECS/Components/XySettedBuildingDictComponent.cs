using Assets.Scripts.Abstractions.ValuesConsts;
using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components
{
    internal struct XySettedBuildingDictComponent
    {
        private Dictionary<bool, int[]> _xySettedBuildingDict;

        internal void StartFill()
        {
            _xySettedBuildingDict = new Dictionary<bool, int[]>();

            _xySettedBuildingDict.Add(true, new int[CellValues.XY_FOR_ARRAY]);
            _xySettedBuildingDict.Add(false, new int[CellValues.XY_FOR_ARRAY]);
        }

        internal int[] GetXySettedBuilding(bool key) => (int[])_xySettedBuildingDict[key].Clone();
        internal void SetXySettedBuilding(bool key, int[] value) => _xySettedBuildingDict[key] = (int[])value.Clone();
    }
}
