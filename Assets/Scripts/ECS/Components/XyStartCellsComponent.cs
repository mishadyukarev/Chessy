using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components
{
    internal struct XyStartCellsComponent
    {
        private Dictionary<bool, List<int[]>> _xyStartCellsDict;

        internal Dictionary<bool, List<int[]>> XyStartCellsDict => _xyStartCellsDict;


        internal XyStartCellsComponent(Dictionary<bool, List<int[]>> dict) => _xyStartCellsDict = dict;
    }
}
