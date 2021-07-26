using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components
{
    public struct AmountUnitsInInventorDictComponent
    {
        internal Dictionary<bool, int> AmountUnitsInInventorDict { get; private set; }

        internal AmountUnitsInInventorDictComponent(Dictionary<bool, int> dict)
        {
            AmountUnitsInInventorDict = dict;

            AmountUnitsInInventorDict.Add(true, default);
            AmountUnitsInInventorDict.Add(false, default);
        }
    }
}
