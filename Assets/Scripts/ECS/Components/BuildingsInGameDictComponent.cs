using Assets.Scripts.Workers;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components
{
    internal struct BuildingsInGameDictComponent
    {
        internal Dictionary<bool, List<int[]>> _buildingsInGameDict;

        internal void StartFill()
        {
            _buildingsInGameDict = new Dictionary<bool, List<int[]>>();

            _buildingsInGameDict.Add(true, new List<int[]>());
            _buildingsInGameDict.Add(false, new List<int[]>());
        }

        internal int AmountBuildingsInGame(bool key) => _buildingsInGameDict[key].Count;
        internal List<int[]> GetXyBuildingsInGame(bool key) => _buildingsInGameDict[key].Copy();

        internal void AddAmountBuildingsInGame(bool key, int[] xyAdding) => _buildingsInGameDict[key].Add(xyAdding);
        internal void TakeAmountBuildingsInGame(bool key, int[] xyTaking)
        {
            var curList = _buildingsInGameDict[key];

            for (int xyNumber = 0; xyNumber < curList.Count; xyNumber++)
            {
                if (curList[xyNumber].Compare(xyTaking))
                {
                    curList.RemoveAt(xyNumber);
                    return;
                }
            }

            throw new Exception();
        }
        internal void SyncBuildingsInGame(bool key, List<int[]> buildingsInGame) => _buildingsInGameDict[key] = buildingsInGame.Copy();

        internal int[] GetXyAnyFirstBuildingInGame(bool key)
        {
            var curList = _buildingsInGameDict[key];

            return curList[0];
        }
    }
}
