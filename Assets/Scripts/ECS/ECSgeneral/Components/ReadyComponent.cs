using System;
using System.Collections.Generic;

internal struct ReadyComponent : IDisposable
{
    private Dictionary<bool, bool> _isActivatedDict;

    internal bool IsSkipped;

    internal Dictionary<bool, bool> IsActivatedDictionary => _isActivatedDict;

    internal ReadyComponent(List<IDisposable> disposables)
    {
        IsSkipped = default;

        _isActivatedDict = new Dictionary<bool, bool>();
        _isActivatedDict.Add(true, default);
        _isActivatedDict.Add(false, default);

        disposables.Add(this);
    }

    public void Dispose()
    {
        _isActivatedDict[true] = default;
        _isActivatedDict[false] = default;
        IsSkipped = default;
    }
}
