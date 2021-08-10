using Assets.Scripts.Workers;
using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components
{
    internal struct XyStartCellsComponent
    {
        private Dictionary<bool, List<byte[]>> _xyStartCellsDict;


        internal XyStartCellsComponent(Dictionary<bool, List<byte[]>> dict) => _xyStartCellsDict = dict;


        internal bool IsStartedCell(bool key, byte[] xy) => _xyStartCellsDict[key].TryFindCell(xy);
    }
}
