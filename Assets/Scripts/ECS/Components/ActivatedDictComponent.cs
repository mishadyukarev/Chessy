using System.Collections.Generic;

internal struct ActivatedDictComponent
{
    private Dictionary<bool, bool> _isActivatedDict;

    internal bool IsActivatedAll => _isActivatedDict[true] && _isActivatedDict[false];

    internal void StartFill()
    {
        _isActivatedDict = new Dictionary<bool, bool>();
        ResetAll();
    }

    internal bool IsActivated(bool key) => _isActivatedDict[key];
    internal void SetActivated(bool key, bool value) => _isActivatedDict[key] = value;

    internal void ResetAll()
    {
        _isActivatedDict[true] = default;
        _isActivatedDict[false] = default;
    }
}