using System.Collections.Generic;

internal struct AmountDictionaryComponent
{
    private Dictionary<bool, int> _amountDict;

    internal void StartFill()
    {
        _amountDict = new Dictionary<bool, int>();
        _amountDict.Add(true, default);
        _amountDict.Add(false, default);
    }

    internal int Amount(bool key) => _amountDict[key];
    internal void SetAmount(bool key, int value) => _amountDict[key] = value;
    internal void AddAmount(bool key, int adding = 1) => _amountDict[key] += adding;
    internal void TakeAmount(bool key, int taking = 1) => _amountDict[key] -= taking;
}
