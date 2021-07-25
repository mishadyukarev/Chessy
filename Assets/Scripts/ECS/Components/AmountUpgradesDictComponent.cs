using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components
{
    internal struct AmountUpgradesDictComponent
    {
        internal Dictionary<bool, int> AmountUpgradesDict { get; private set; }

        internal AmountUpgradesDictComponent(Dictionary<bool, int> dict)
        {
            AmountUpgradesDict = dict;

            dict.Add(true, default);
            dict.Add(false, default);
        }
    }
}
