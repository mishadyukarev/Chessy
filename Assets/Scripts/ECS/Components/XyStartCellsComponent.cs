using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components
{
    internal struct XyStartCellsComponent
    {
        internal Dictionary<bool, List<int[]>> XyStartCellsDict { get; private set; }


        internal XyStartCellsComponent(Dictionary<bool, List<int[]>> dict) => XyStartCellsDict = dict;
    }
}
