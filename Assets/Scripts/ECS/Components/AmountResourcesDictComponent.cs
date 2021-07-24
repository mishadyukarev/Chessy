using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components
{
    internal struct AmountResourcesDictComponent
    {
        private Dictionary<bool, int> _amountResourcesDict;

        internal void StartFill()
        {
            _amountResourcesDict = new Dictionary<bool, int>();

            _amountResourcesDict.Add(true, default);
            _amountResourcesDict.Add(false, default);
        }

        internal int AmountResources(bool key) => _amountResourcesDict[key];
        internal void SetAmountResources(bool key, int value) => _amountResourcesDict[key] = value;
        internal void AddAmountResources(bool key, int adding = 1) => _amountResourcesDict[key] += adding;
        internal void TakeAmountResources(bool key, int taking = 1) => _amountResourcesDict[key] -= taking;
    }
}
