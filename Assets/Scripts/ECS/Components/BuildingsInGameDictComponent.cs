using Assets.Scripts.Workers;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components
{
    internal struct BuildingsInGameDictComponent
    {
        internal Dictionary<bool, List<int[]>> BuildingsInGameDict { get; set; }

        internal BuildingsInGameDictComponent(Dictionary<bool, List<int[]>> dict)
        {
            BuildingsInGameDict = dict;

            BuildingsInGameDict.Add(true, new List<int[]>());
            BuildingsInGameDict.Add(false, new List<int[]>());
        }
    }
}
