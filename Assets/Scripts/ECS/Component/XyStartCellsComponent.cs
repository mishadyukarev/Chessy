using Assets.Scripts.Workers;
using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components
{
    internal struct XyStartCellsComponent
    {
        private Dictionary<bool, List<int[]>> _xyStartCellsDict;


        internal XyStartCellsComponent(Dictionary<bool, List<int[]>> dict) => _xyStartCellsDict = dict;


        internal bool IsStartedCell(bool key, int[] xy) => _xyStartCellsDict[key].TryFindCell(xy);
    }
}
