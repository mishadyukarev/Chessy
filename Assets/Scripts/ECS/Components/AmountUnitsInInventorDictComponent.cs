using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components
{
    public struct AmountUnitsInInventorDictComponent
    {
        private Dictionary<bool, int> _amountUnitsInInventorDict;

        internal void StartFill()
        {
            _amountUnitsInInventorDict = new Dictionary<bool, int>();

            _amountUnitsInInventorDict.Add(true, default);
            _amountUnitsInInventorDict.Add(false, default);
        }

        internal int AmountUnitsInInvetor(bool key) => _amountUnitsInInventorDict[key];
        internal bool HaveUnitInInventor(bool key) => _amountUnitsInInventorDict[key] > 0;
        internal void SetAmountUnitsInInventor(bool key, int value) => _amountUnitsInInventorDict[key] = value;
        internal void AddAmountUnitsInInventor(bool key, int adding = 1) => _amountUnitsInInventorDict[key] += adding;
        internal void TakeAmountUnitsInInventor(bool key, int taking = 1) => _amountUnitsInInventorDict[key] -= taking;
    }
}
