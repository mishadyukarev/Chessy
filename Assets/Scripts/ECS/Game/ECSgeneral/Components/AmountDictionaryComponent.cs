using System.Collections.Generic;

internal struct AmountDictionaryComponent
{
    private Dictionary<bool, int> _amountDict;

    internal void CrateAmountDict()
    {
        _amountDict = new Dictionary<bool, int>();
        _amountDict.Add(true, default);
        _amountDict.Add(false, default);
    }

    internal int Amount(bool key) => _amountDict[key];
    internal void SetAmount(bool key, int value) => _amountDict[key] = value;
}
