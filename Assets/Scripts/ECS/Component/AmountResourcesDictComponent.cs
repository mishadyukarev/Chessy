using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components
{
    internal struct AmountResourcesDictComponent
    {
        internal Dictionary<bool, int> AmountResourcesDict { get; private set; }


        internal AmountResourcesDictComponent(Dictionary<bool, int> amountResourcesDict)
        {
            AmountResourcesDict = amountResourcesDict;

            AmountResourcesDict.Add(true, default);
            AmountResourcesDict.Add(false, default);
        }
    }
}
