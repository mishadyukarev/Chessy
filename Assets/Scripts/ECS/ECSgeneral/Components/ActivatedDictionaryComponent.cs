using System;
using System.Collections.Generic;

internal struct ActivatedDictionaryComponent : IDisposable
{
    internal Dictionary<bool, bool> IsActivatedDictionary;

    public void Dispose()
    {
        IsActivatedDictionary[true] = default;
        IsActivatedDictionary[false] = default;
    }
}