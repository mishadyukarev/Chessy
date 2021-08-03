using Assets.Scripts.Abstractions.Enums;
using System.Collections.Generic;

namespace Assets.Scripts.ECS.Component
{
    internal struct XyUnitsContitionComponent
    {
        private Dictionary<ConditionUnitTypes, Dictionary<bool, List<byte[]>>> _xyUnitsContitionDict;

        internal XyUnitsContitionComponent(Dictionary<ConditionUnitTypes, Dictionary<bool, List<byte[]>>> dict)
        {
            _xyUnitsContitionDict = dict;
        }
    }
}
