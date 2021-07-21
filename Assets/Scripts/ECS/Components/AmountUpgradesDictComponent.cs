using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components
{
    internal struct AmountUpgradesDictComponent
    {
        internal Dictionary<bool, int> _amountUpgradesDict;

        internal void StartFill()
        {
            _amountUpgradesDict = new Dictionary<bool, int>();

            _amountUpgradesDict.Add(true, default);
            _amountUpgradesDict.Add(false, default);
        }

        internal int AmountUpgrades(bool key) => _amountUpgradesDict[key];
        internal int SetAmountUpgrades(bool key, int value) => _amountUpgradesDict[key] = value;
        internal void AddAmountUpgrades(bool key, int adding = 1) => _amountUpgradesDict[key] += adding;
        internal void TakeAmountUpgrades(bool key, int taking = 1) => _amountUpgradesDict[key] -= taking;
        internal void ResetAmountUpgrades(bool key) => _amountUpgradesDict[key] =default;
    }
}
